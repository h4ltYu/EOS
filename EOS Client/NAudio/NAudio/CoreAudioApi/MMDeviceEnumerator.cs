using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// MM Device Enumerator
	/// </summary>
	// Token: 0x02000134 RID: 308
	public class MMDeviceEnumerator
	{
		/// <summary>
		/// Creates a new MM Device Enumerator
		/// </summary>
		// Token: 0x060006C2 RID: 1730 RVA: 0x00014F75 File Offset: 0x00013175
		public MMDeviceEnumerator()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
			}
			this.realEnumerator = (new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator);
		}

		/// <summary>
		/// Enumerate Audio Endpoints
		/// </summary>
		/// <param name="dataFlow">Desired DataFlow</param>
		/// <param name="dwStateMask">State Mask</param>
		/// <returns>Device Collection</returns>
		// Token: 0x060006C3 RID: 1731 RVA: 0x00014FAC File Offset: 0x000131AC
		public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
		{
			IMMDeviceCollection parent;
			Marshal.ThrowExceptionForHR(this.realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out parent));
			return new MMDeviceCollection(parent);
		}

		/// <summary>
		/// Get Default Endpoint
		/// </summary>
		/// <param name="dataFlow">Data Flow</param>
		/// <param name="role">Role</param>
		/// <returns>Device</returns>
		// Token: 0x060006C4 RID: 1732 RVA: 0x00014FD4 File Offset: 0x000131D4
		public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
		{
			IMMDevice realDevice = null;
			Marshal.ThrowExceptionForHR(this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out realDevice));
			return new MMDevice(realDevice);
		}

		/// <summary>
		/// Check to see if a default audio end point exists without needing an exception.
		/// </summary>
		/// <param name="dataFlow">Data Flow</param>
		/// <param name="role">Role</param>
		/// <returns>True if one exists, and false if one does not exist.</returns>
		// Token: 0x060006C5 RID: 1733 RVA: 0x00015000 File Offset: 0x00013200
		public bool HasDefaultAudioEndpoint(DataFlow dataFlow, Role role)
		{
			IMMDevice o = null;
			int defaultAudioEndpoint = this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out o);
			if (defaultAudioEndpoint == 0)
			{
				Marshal.ReleaseComObject(o);
				return true;
			}
			if (defaultAudioEndpoint == -2147023728)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(defaultAudioEndpoint);
			return false;
		}

		/// <summary>
		/// Get device by ID
		/// </summary>
		/// <param name="id">Device ID</param>
		/// <returns>Device</returns>
		// Token: 0x060006C6 RID: 1734 RVA: 0x0001503C File Offset: 0x0001323C
		public MMDevice GetDevice(string id)
		{
			IMMDevice realDevice = null;
			Marshal.ThrowExceptionForHR(this.realEnumerator.GetDevice(id, out realDevice));
			return new MMDevice(realDevice);
		}

		/// <summary>
		/// Registers a call back for Device Events
		/// </summary>
		/// <param name="client">Object implementing IMMNotificationClient type casted as IMMNotificationClient interface</param>
		/// <returns></returns>
		// Token: 0x060006C7 RID: 1735 RVA: 0x00015064 File Offset: 0x00013264
		public int RegisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient client)
		{
			return this.realEnumerator.RegisterEndpointNotificationCallback(client);
		}

		/// <summary>
		/// Unregisters a call back for Device Events
		/// </summary>
		/// <param name="client">Object implementing IMMNotificationClient type casted as IMMNotificationClient interface </param>
		/// <returns></returns>
		// Token: 0x060006C8 RID: 1736 RVA: 0x00015072 File Offset: 0x00013272
		public int UnregisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient client)
		{
			return this.realEnumerator.UnregisterEndpointNotificationCallback(client);
		}

		// Token: 0x04000747 RID: 1863
		private readonly IMMDeviceEnumerator realEnumerator;
	}
}
