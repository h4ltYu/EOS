using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000128 RID: 296
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	internal interface IMMDevice
	{
		// Token: 0x06000690 RID: 1680
		int Activate(ref Guid id, ClsCtx clsCtx, IntPtr activationParams, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);

		// Token: 0x06000691 RID: 1681
		int OpenPropertyStore(StorageAccessMode stgmAccess, out IPropertyStore properties);

		// Token: 0x06000692 RID: 1682
		int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);

		// Token: 0x06000693 RID: 1683
		int GetState(out DeviceState state);
	}
}
