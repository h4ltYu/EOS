using System;

namespace NAudio.Wave
{
	// Token: 0x02000175 RID: 373
	[Flags]
	internal enum AcmStreamConvertFlags
	{
		/// <summary>
		/// ACM_STREAMCONVERTF_BLOCKALIGN
		/// </summary>
		// Token: 0x04000833 RID: 2099
		BlockAlign = 4,
		/// <summary>
		/// ACM_STREAMCONVERTF_START
		/// </summary>
		// Token: 0x04000834 RID: 2100
		Start = 16,
		/// <summary>
		/// ACM_STREAMCONVERTF_END
		/// </summary>
		// Token: 0x04000835 RID: 2101
		End = 32
	}
}
