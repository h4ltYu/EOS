using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Describes an output stream on a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x02000062 RID: 98
	[Flags]
	public enum _MFT_OUTPUT_STREAM_INFO_FLAGS
	{
		/// <summary>
		/// No flags set
		/// </summary>
		// Token: 0x04000335 RID: 821
		None = 0,
		/// <summary>
		/// Each media sample (IMFSample interface) of output data from the MFT contains complete, unbroken units of data.
		/// </summary>
		// Token: 0x04000336 RID: 822
		MFT_OUTPUT_STREAM_WHOLE_SAMPLES = 1,
		/// <summary>
		/// Each output sample contains exactly one unit of data, as defined for the MFT_OUTPUT_STREAM_WHOLE_SAMPLES flag.
		/// </summary>
		// Token: 0x04000337 RID: 823
		MFT_OUTPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 2,
		/// <summary>
		/// All output samples are the same size.
		/// </summary>
		// Token: 0x04000338 RID: 824
		MFT_OUTPUT_STREAM_FIXED_SAMPLE_SIZE = 4,
		/// <summary>
		/// The MFT can discard the output data from this output stream, if requested by the client.
		/// </summary>
		// Token: 0x04000339 RID: 825
		MFT_OUTPUT_STREAM_DISCARDABLE = 8,
		/// <summary>
		/// This output stream is optional.
		/// </summary>
		// Token: 0x0400033A RID: 826
		MFT_OUTPUT_STREAM_OPTIONAL = 16,
		/// <summary>
		/// The MFT provides the output samples for this stream, either by allocating them internally or by operating directly on the input samples.
		/// </summary>
		// Token: 0x0400033B RID: 827
		MFT_OUTPUT_STREAM_PROVIDES_SAMPLES = 256,
		/// <summary>
		/// The MFT can either provide output samples for this stream or it can use samples that the client allocates. 
		/// </summary>
		// Token: 0x0400033C RID: 828
		MFT_OUTPUT_STREAM_CAN_PROVIDE_SAMPLES = 512,
		/// <summary>
		/// The MFT does not require the client to process the output for this stream. 
		/// </summary>
		// Token: 0x0400033D RID: 829
		MFT_OUTPUT_STREAM_LAZY_READ = 1024,
		/// <summary>
		/// The MFT might remove this output stream during streaming.
		/// </summary>
		// Token: 0x0400033E RID: 830
		MFT_OUTPUT_STREAM_REMOVABLE = 2048
	}
}
