using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Indicates whether a Media Foundation transform (MFT) can produce output data.
	/// </summary>
	// Token: 0x02000061 RID: 97
	[Flags]
	public enum _MFT_OUTPUT_STATUS_FLAGS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000332 RID: 818
		None = 0,
		/// <summary>
		/// There is a sample available for at least one output stream.
		/// </summary>
		// Token: 0x04000333 RID: 819
		MFT_OUTPUT_STATUS_SAMPLE_READY = 1
	}
}
