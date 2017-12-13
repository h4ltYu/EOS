using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// IMMNotificationClient
	/// </summary>
	// Token: 0x0200012C RID: 300
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
	public interface IMMNotificationClient
	{
		/// <summary>
		/// Device State Changed
		/// </summary>
		// Token: 0x0600069C RID: 1692
		void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, [MarshalAs(UnmanagedType.I4)] DeviceState newState);

		/// <summary>
		/// Device Added
		/// </summary>
		// Token: 0x0600069D RID: 1693
		void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);

		/// <summary>
		/// Device Removed
		/// </summary>
		// Token: 0x0600069E RID: 1694
		void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId);

		/// <summary>
		/// Default Device Changed
		/// </summary>
		// Token: 0x0600069F RID: 1695
		void OnDefaultDeviceChanged(DataFlow flow, Role role, [MarshalAs(UnmanagedType.LPWStr)] string defaultDeviceId);

		/// <summary>
		/// Property Value Changed
		/// </summary>
		/// <param name="pwstrDeviceId"></param>
		/// <param name="key"></param>
		// Token: 0x060006A0 RID: 1696
		void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key);
	}
}
