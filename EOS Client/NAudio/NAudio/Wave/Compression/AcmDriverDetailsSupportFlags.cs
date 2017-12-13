using System;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Flags indicating what support a particular ACM driver has
	/// </summary>
	// Token: 0x02000164 RID: 356
	[Flags]
	public enum AcmDriverDetailsSupportFlags
	{
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_CODEC - Codec</summary>
		// Token: 0x040007D6 RID: 2006
		Codec = 1,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_CONVERTER - Converter</summary>
		// Token: 0x040007D7 RID: 2007
		Converter = 2,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_FILTER - Filter</summary>
		// Token: 0x040007D8 RID: 2008
		Filter = 4,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_HARDWARE - Hardware</summary>
		// Token: 0x040007D9 RID: 2009
		Hardware = 8,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_ASYNC - Async</summary>
		// Token: 0x040007DA RID: 2010
		Async = 16,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_LOCAL - Local</summary>
		// Token: 0x040007DB RID: 2011
		Local = 1073741824,
		/// <summary>ACMDRIVERDETAILS_SUPPORTF_DISABLED - Disabled</summary>
		// Token: 0x040007DC RID: 2012
		Disabled = -2147483648
	}
}
