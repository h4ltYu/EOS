using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Defines flags for the IMFTransform::ProcessOutput method. 
	/// </summary>
	// Token: 0x02000060 RID: 96
	[Flags]
	public enum _MFT_OUTPUT_DATA_BUFFER_FLAGS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x0400032C RID: 812
		None = 0,
		/// <summary>
		/// The MFT can still generate output from this stream without receiving any more input. 
		/// </summary>
		// Token: 0x0400032D RID: 813
		MFT_OUTPUT_DATA_BUFFER_INCOMPLETE = 16777216,
		/// <summary>
		/// The format has changed on this output stream, or there is a new preferred format for this stream. 
		/// </summary>
		// Token: 0x0400032E RID: 814
		MFT_OUTPUT_DATA_BUFFER_FORMAT_CHANGE = 256,
		/// <summary>
		/// The MFT has removed this output stream. 
		/// </summary>
		// Token: 0x0400032F RID: 815
		MFT_OUTPUT_DATA_BUFFER_STREAM_END = 512,
		/// <summary>
		/// There is no sample ready for this stream.
		/// </summary>
		// Token: 0x04000330 RID: 816
		MFT_OUTPUT_DATA_BUFFER_NO_SAMPLE = 768
	}
}
