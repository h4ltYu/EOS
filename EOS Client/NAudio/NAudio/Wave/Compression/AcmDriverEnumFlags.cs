using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000165 RID: 357
	[Flags]
	internal enum AcmDriverEnumFlags
	{
		/// <summary>
		/// ACM_DRIVERENUMF_NOLOCAL, Only global drivers should be included in the enumeration
		/// </summary>
		// Token: 0x040007DE RID: 2014
		NoLocal = 1073741824,
		/// <summary>
		/// ACM_DRIVERENUMF_DISABLED, Disabled ACM drivers should be included in the enumeration
		/// </summary>
		// Token: 0x040007DF RID: 2015
		Disabled = -2147483648
	}
}
