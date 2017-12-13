using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// is defined in propsys.h
	/// </summary>
	// Token: 0x0200012D RID: 301
	[Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPropertyStore
	{
		// Token: 0x060006A1 RID: 1697
		int GetCount(out int propCount);

		// Token: 0x060006A2 RID: 1698
		int GetAt(int property, out PropertyKey key);

		// Token: 0x060006A3 RID: 1699
		int GetValue(ref PropertyKey key, out PropVariant value);

		// Token: 0x060006A4 RID: 1700
		int SetValue(ref PropertyKey key, ref PropVariant value);

		// Token: 0x060006A5 RID: 1701
		int Commit();
	}
}
