using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016B RID: 363
	[Flags]
	internal enum AcmFormatSuggestFlags
	{
		/// <summary>
		/// ACM_FORMATSUGGESTF_WFORMATTAG
		/// </summary>
		// Token: 0x0400080E RID: 2062
		FormatTag = 65536,
		/// <summary>
		/// ACM_FORMATSUGGESTF_NCHANNELS
		/// </summary>
		// Token: 0x0400080F RID: 2063
		Channels = 131072,
		/// <summary>
		/// ACM_FORMATSUGGESTF_NSAMPLESPERSEC
		/// </summary>
		// Token: 0x04000810 RID: 2064
		SamplesPerSecond = 262144,
		/// <summary>
		/// ACM_FORMATSUGGESTF_WBITSPERSAMPLE
		/// </summary>
		// Token: 0x04000811 RID: 2065
		BitsPerSample = 524288,
		/// <summary>
		/// ACM_FORMATSUGGESTF_TYPEMASK
		/// </summary>
		// Token: 0x04000812 RID: 2066
		TypeMask = 16711680
	}
}
