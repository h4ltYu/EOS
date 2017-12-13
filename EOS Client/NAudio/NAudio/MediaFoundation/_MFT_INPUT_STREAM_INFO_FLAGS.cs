using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Describes an input stream on a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x0200005F RID: 95
	[Flags]
	public enum _MFT_INPUT_STREAM_INFO_FLAGS
	{
		/// <summary>
		/// No flags set
		/// </summary>
		// Token: 0x04000322 RID: 802
		None = 0,
		/// <summary>
		/// Each media sample (IMFSample interface) of input data must contain complete, unbroken units of data. 
		/// </summary>
		// Token: 0x04000323 RID: 803
		MFT_INPUT_STREAM_WHOLE_SAMPLES = 1,
		/// <summary>
		/// Each media sample that the client provides as input must contain exactly one unit of data, as defined for the MFT_INPUT_STREAM_WHOLE_SAMPLES flag.
		/// </summary>
		// Token: 0x04000324 RID: 804
		MFT_INPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 2,
		/// <summary>
		/// All input samples must be the same size.
		/// </summary>
		// Token: 0x04000325 RID: 805
		MFT_INPUT_STREAM_FIXED_SAMPLE_SIZE = 4,
		/// <summary>
		/// MTF Input Stream Holds buffers
		/// </summary>
		// Token: 0x04000326 RID: 806
		MFT_INPUT_STREAM_HOLDS_BUFFERS = 8,
		/// <summary>
		/// The MFT does not hold input samples after the IMFTransform::ProcessInput method returns.
		/// </summary>
		// Token: 0x04000327 RID: 807
		MFT_INPUT_STREAM_DOES_NOT_ADDREF = 256,
		/// <summary>
		/// This input stream can be removed by calling IMFTransform::DeleteInputStream.
		/// </summary>
		// Token: 0x04000328 RID: 808
		MFT_INPUT_STREAM_REMOVABLE = 512,
		/// <summary>
		/// This input stream is optional. 
		/// </summary>
		// Token: 0x04000329 RID: 809
		MFT_INPUT_STREAM_OPTIONAL = 1024,
		/// <summary>
		/// The MFT can perform in-place processing.
		/// </summary>
		// Token: 0x0400032A RID: 810
		MFT_INPUT_STREAM_PROCESSES_IN_PLACE = 2048
	}
}
