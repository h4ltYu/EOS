using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x0200020A RID: 522
	[Guid("2c3cd98a-2bfa-4a53-9c27-5249ba64ba0f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IEnumDmo
	{
		// Token: 0x06000BEC RID: 3052
		int Next(int itemsToFetch, out Guid clsid, out IntPtr name, out int itemsFetched);

		// Token: 0x06000BED RID: 3053
		int Skip(int itemsToSkip);

		// Token: 0x06000BEE RID: 3054
		int Reset();

		// Token: 0x06000BEF RID: 3055
		int Clone(out IEnumDmo enumPointer);
	}
}
