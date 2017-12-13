using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains flags for registering and enumeration Media Foundation transforms (MFTs).
	/// </summary>
	// Token: 0x0200005D RID: 93
	[Flags]
	public enum _MFT_ENUM_FLAG
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000315 RID: 789
		None = 0,
		/// <summary>
		/// The MFT performs synchronous data processing in software. 
		/// </summary>
		// Token: 0x04000316 RID: 790
		MFT_ENUM_FLAG_SYNCMFT = 1,
		/// <summary>
		/// The MFT performs asynchronous data processing in software.
		/// </summary>
		// Token: 0x04000317 RID: 791
		MFT_ENUM_FLAG_ASYNCMFT = 2,
		/// <summary>
		/// The MFT performs hardware-based data processing, using either the AVStream driver or a GPU-based proxy MFT. 
		/// </summary>
		// Token: 0x04000318 RID: 792
		MFT_ENUM_FLAG_HARDWARE = 4,
		/// <summary>
		/// The MFT that must be unlocked by the application before use.
		/// </summary>
		// Token: 0x04000319 RID: 793
		MFT_ENUM_FLAG_FIELDOFUSE = 8,
		/// <summary>
		/// For enumeration, include MFTs that were registered in the caller's process.
		/// </summary>
		// Token: 0x0400031A RID: 794
		MFT_ENUM_FLAG_LOCALMFT = 16,
		/// <summary>
		/// The MFT is optimized for transcoding rather than playback.
		/// </summary>
		// Token: 0x0400031B RID: 795
		MFT_ENUM_FLAG_TRANSCODE_ONLY = 32,
		/// <summary>
		/// For enumeration, sort and filter the results.
		/// </summary>
		// Token: 0x0400031C RID: 796
		MFT_ENUM_FLAG_SORTANDFILTER = 64,
		/// <summary>
		/// Bitwise OR of all the flags, excluding MFT_ENUM_FLAG_SORTANDFILTER.
		/// </summary>
		// Token: 0x0400031D RID: 797
		MFT_ENUM_FLAG_ALL = 63
	}
}
