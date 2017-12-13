using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200012A RID: 298
	[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IMMDeviceEnumerator
	{
		// Token: 0x06000696 RID: 1686
		int EnumAudioEndpoints(DataFlow dataFlow, DeviceState stateMask, out IMMDeviceCollection devices);

		// Token: 0x06000697 RID: 1687
		[PreserveSig]
		int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice endpoint);

		// Token: 0x06000698 RID: 1688
		int GetDevice(string id, out IMMDevice deviceName);

		// Token: 0x06000699 RID: 1689
		int RegisterEndpointNotificationCallback(IMMNotificationClient client);

		// Token: 0x0600069A RID: 1690
		int UnregisterEndpointNotificationCallback(IMMNotificationClient client);
	}
}
