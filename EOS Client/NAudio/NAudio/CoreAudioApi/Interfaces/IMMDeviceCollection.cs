using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000129 RID: 297
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
	internal interface IMMDeviceCollection
	{
		// Token: 0x06000694 RID: 1684
		int GetCount(out int numDevices);

		// Token: 0x06000695 RID: 1685
		int Item(int deviceNumber, out IMMDevice device);
	}
}
