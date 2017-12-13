using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// MM Device
	/// </summary>
	// Token: 0x02000133 RID: 307
	public class MMDevice
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00014C54 File Offset: 0x00012E54
		private void GetPropertyInformation()
		{
			IPropertyStore store;
			Marshal.ThrowExceptionForHR(this.deviceInterface.OpenPropertyStore(StorageAccessMode.Read, out store));
			this.propertyStore = new PropertyStore(store);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00014C80 File Offset: 0x00012E80
		private AudioClient GetAudioClient()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioClient, ClsCtx.ALL, IntPtr.Zero, out obj));
			return new AudioClient(obj as IAudioClient);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00014CB8 File Offset: 0x00012EB8
		private void GetAudioMeterInformation()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioMeterInformation, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioMeterInformation = new AudioMeterInformation(obj as IAudioMeterInformation);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00014CF4 File Offset: 0x00012EF4
		private void GetAudioEndpointVolume()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioEndpointVolume, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioEndpointVolume = new AudioEndpointVolume(obj as IAudioEndpointVolume);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00014D30 File Offset: 0x00012F30
		private void GetAudioSessionManager()
		{
			object obj;
			Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IDD_IAudioSessionManager, ClsCtx.ALL, IntPtr.Zero, out obj));
			this.audioSessionManager = new AudioSessionManager(obj as IAudioSessionManager);
		}

		/// <summary>
		/// Audio Client
		/// </summary>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00014D6C File Offset: 0x00012F6C
		public AudioClient AudioClient
		{
			get
			{
				return this.GetAudioClient();
			}
		}

		/// <summary>
		/// Audio Meter Information
		/// </summary>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00014D74 File Offset: 0x00012F74
		public AudioMeterInformation AudioMeterInformation
		{
			get
			{
				if (this.audioMeterInformation == null)
				{
					this.GetAudioMeterInformation();
				}
				return this.audioMeterInformation;
			}
		}

		/// <summary>
		/// Audio Endpoint Volume
		/// </summary>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00014D8A File Offset: 0x00012F8A
		public AudioEndpointVolume AudioEndpointVolume
		{
			get
			{
				if (this.audioEndpointVolume == null)
				{
					this.GetAudioEndpointVolume();
				}
				return this.audioEndpointVolume;
			}
		}

		/// <summary>
		/// AudioSessionManager instance
		/// </summary>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00014DA0 File Offset: 0x00012FA0
		public AudioSessionManager AudioSessionManager
		{
			get
			{
				if (this.audioSessionManager == null)
				{
					this.GetAudioSessionManager();
				}
				return this.audioSessionManager;
			}
		}

		/// <summary>
		/// Properties
		/// </summary>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00014DB6 File Offset: 0x00012FB6
		public PropertyStore Properties
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				return this.propertyStore;
			}
		}

		/// <summary>
		/// Friendly name for the endpoint
		/// </summary>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00014DCC File Offset: 0x00012FCC
		public string FriendlyName
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_Device_FriendlyName))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_Device_FriendlyName].Value;
				}
				return "Unknown";
			}
		}

		/// <summary>
		/// Friendly name of device
		/// </summary>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00014E1C File Offset: 0x0001301C
		public string DeviceFriendlyName
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_DeviceInterface_FriendlyName].Value;
				}
				return "Unknown";
			}
		}

		/// <summary>
		/// Icon path of device
		/// </summary>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00014E6C File Offset: 0x0001306C
		public string IconPath
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.GetPropertyInformation();
				}
				if (this.propertyStore.Contains(PropertyKeys.PKEY_Device_IconPath))
				{
					return (string)this.propertyStore[PropertyKeys.PKEY_Device_IconPath].Value;
				}
				return "Unknown";
			}
		}

		/// <summary>
		/// Device ID
		/// </summary>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00014EBC File Offset: 0x000130BC
		public string ID
		{
			get
			{
				string result;
				Marshal.ThrowExceptionForHR(this.deviceInterface.GetId(out result));
				return result;
			}
		}

		/// <summary>
		/// Data Flow
		/// </summary>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00014EDC File Offset: 0x000130DC
		public DataFlow DataFlow
		{
			get
			{
				IMMEndpoint immendpoint = this.deviceInterface as IMMEndpoint;
				DataFlow result;
				immendpoint.GetDataFlow(out result);
				return result;
			}
		}

		/// <summary>
		/// Device State
		/// </summary>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00014F00 File Offset: 0x00013100
		public DeviceState State
		{
			get
			{
				DeviceState result;
				Marshal.ThrowExceptionForHR(this.deviceInterface.GetState(out result));
				return result;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00014F20 File Offset: 0x00013120
		internal MMDevice(IMMDevice realDevice)
		{
			this.deviceInterface = realDevice;
		}

		/// <summary>
		/// To string
		/// </summary>
		// Token: 0x060006C0 RID: 1728 RVA: 0x00014F2F File Offset: 0x0001312F
		public override string ToString()
		{
			return this.FriendlyName;
		}

		// Token: 0x0400073E RID: 1854
		private readonly IMMDevice deviceInterface;

		// Token: 0x0400073F RID: 1855
		private PropertyStore propertyStore;

		// Token: 0x04000740 RID: 1856
		private AudioMeterInformation audioMeterInformation;

		// Token: 0x04000741 RID: 1857
		private AudioEndpointVolume audioEndpointVolume;

		// Token: 0x04000742 RID: 1858
		private AudioSessionManager audioSessionManager;

		// Token: 0x04000743 RID: 1859
		private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");

		// Token: 0x04000744 RID: 1860
		private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");

		// Token: 0x04000745 RID: 1861
		private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");

		// Token: 0x04000746 RID: 1862
		private static Guid IDD_IAudioSessionManager = new Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4");
	}
}
