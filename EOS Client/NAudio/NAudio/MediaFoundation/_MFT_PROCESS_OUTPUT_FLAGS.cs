using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Defines flags for processing output samples in a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x02000063 RID: 99
	[Flags]
	public enum _MFT_PROCESS_OUTPUT_FLAGS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000340 RID: 832
		None = 0,
		/// <summary>
		/// Do not produce output for streams in which the pSample member of the MFT_OUTPUT_DATA_BUFFER structure is NULL. 
		/// </summary>
		// Token: 0x04000341 RID: 833
		MFT_PROCESS_OUTPUT_DISCARD_WHEN_NO_BUFFER = 1,
		/// <summary>
		/// Regenerates the last output sample.
		/// </summary>
		// Token: 0x04000342 RID: 834
		MFT_PROCESS_OUTPUT_REGENERATE_LAST_OUTPUT = 2
	}
}
