using System;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Format Enumeration Flags
	/// </summary>
	// Token: 0x0200016A RID: 362
	[Flags]
	public enum AcmFormatEnumFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000803 RID: 2051
		None = 0,
		/// <summary>
		/// ACM_FORMATENUMF_CONVERT
		/// The WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will only enumerate destination formats that can be converted from the given pwfx format. 
		/// </summary>
		// Token: 0x04000804 RID: 2052
		Convert = 1048576,
		/// <summary>
		/// ACM_FORMATENUMF_HARDWARE
		/// The enumerator should only enumerate formats that are supported as native input or output formats on one or more of the installed waveform-audio devices. This flag provides a way for an application to choose only formats native to an installed waveform-audio device. This flag must be used with one or both of the ACM_FORMATENUMF_INPUT and ACM_FORMATENUMF_OUTPUT flags. Specifying both ACM_FORMATENUMF_INPUT and ACM_FORMATENUMF_OUTPUT will enumerate only formats that can be opened for input or output. This is true regardless of whether this flag is specified. 
		/// </summary>
		// Token: 0x04000805 RID: 2053
		Hardware = 4194304,
		/// <summary>
		/// ACM_FORMATENUMF_INPUT
		/// Enumerator should enumerate only formats that are supported for input (recording). 
		/// </summary>
		// Token: 0x04000806 RID: 2054
		Input = 8388608,
		/// <summary>
		/// ACM_FORMATENUMF_NCHANNELS 
		/// The nChannels member of the WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will enumerate only a format that conforms to this attribute. 
		/// </summary>
		// Token: 0x04000807 RID: 2055
		Channels = 131072,
		/// <summary>
		/// ACM_FORMATENUMF_NSAMPLESPERSEC
		/// The nSamplesPerSec member of the WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will enumerate only a format that conforms to this attribute. 
		/// </summary>
		// Token: 0x04000808 RID: 2056
		SamplesPerSecond = 262144,
		/// <summary>
		/// ACM_FORMATENUMF_OUTPUT 
		/// Enumerator should enumerate only formats that are supported for output (playback). 
		/// </summary>
		// Token: 0x04000809 RID: 2057
		Output = 16777216,
		/// <summary>
		/// ACM_FORMATENUMF_SUGGEST
		/// The WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will enumerate all suggested destination formats for the given pwfx format. This mechanism can be used instead of the acmFormatSuggest function to allow an application to choose the best suggested format for conversion. The dwFormatIndex member will always be set to zero on return. 
		/// </summary>
		// Token: 0x0400080A RID: 2058
		Suggest = 2097152,
		/// <summary>
		/// ACM_FORMATENUMF_WBITSPERSAMPLE
		/// The wBitsPerSample member of the WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will enumerate only a format that conforms to this attribute. 
		/// </summary>
		// Token: 0x0400080B RID: 2059
		BitsPerSample = 524288,
		/// <summary>
		/// ACM_FORMATENUMF_WFORMATTAG
		/// The wFormatTag member of the WAVEFORMATEX structure pointed to by the pwfx member of the ACMFORMATDETAILS structure is valid. The enumerator will enumerate only a format that conforms to this attribute. The dwFormatTag member of the ACMFORMATDETAILS structure must be equal to the wFormatTag member. 
		/// </summary>
		// Token: 0x0400080C RID: 2060
		FormatTag = 65536
	}
}
