using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains information about an input stream on a Media Foundation transform (MFT)
	/// </summary>
	// Token: 0x02000057 RID: 87
	public struct MFT_INPUT_STREAM_INFO
	{
		/// <summary>
		/// Maximum amount of time between an input sample and the corresponding output sample, in 100-nanosecond units.
		/// </summary>
		// Token: 0x040002EB RID: 747
		public long hnsMaxLatency;

		/// <summary>
		/// Bitwise OR of zero or more flags from the _MFT_INPUT_STREAM_INFO_FLAGS enumeration.
		/// </summary>
		// Token: 0x040002EC RID: 748
		public _MFT_INPUT_STREAM_INFO_FLAGS dwFlags;

		/// <summary>
		/// The minimum size of each input buffer, in bytes.
		/// </summary>
		// Token: 0x040002ED RID: 749
		public int cbSize;

		/// <summary>
		/// Maximum amount of input data, in bytes, that the MFT holds to perform lookahead.
		/// </summary>
		// Token: 0x040002EE RID: 750
		public int cbMaxLookahead;

		/// <summary>
		/// The memory alignment required for input buffers. If the MFT does not require a specific alignment, the value is zero.
		/// </summary>
		// Token: 0x040002EF RID: 751
		public int cbAlignment;
	}
}
