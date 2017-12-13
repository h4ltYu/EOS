using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains information about an output stream on a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x02000059 RID: 89
	public struct MFT_OUTPUT_STREAM_INFO
	{
		/// <summary>
		/// Bitwise OR of zero or more flags from the _MFT_OUTPUT_STREAM_INFO_FLAGS enumeration.
		/// </summary>
		// Token: 0x040002F4 RID: 756
		public _MFT_OUTPUT_STREAM_INFO_FLAGS dwFlags;

		/// <summary>
		/// Minimum size of each output buffer, in bytes.
		/// </summary>
		// Token: 0x040002F5 RID: 757
		public int cbSize;

		/// <summary>
		/// The memory alignment required for output buffers.
		/// </summary>
		// Token: 0x040002F6 RID: 758
		public int cbAlignment;
	}
}
