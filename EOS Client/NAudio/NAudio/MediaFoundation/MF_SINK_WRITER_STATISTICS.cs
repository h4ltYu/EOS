using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains statistics about the performance of the sink writer.
	/// </summary>
	// Token: 0x0200005C RID: 92
	[StructLayout(LayoutKind.Sequential)]
	public class MF_SINK_WRITER_STATISTICS
	{
		/// <summary>
		/// The size of the structure, in bytes.
		/// </summary>
		// Token: 0x04000304 RID: 772
		public int cb;

		/// <summary>
		/// The time stamp of the most recent sample given to the sink writer.
		/// </summary>
		// Token: 0x04000305 RID: 773
		public long llLastTimestampReceived;

		/// <summary>
		/// The time stamp of the most recent sample to be encoded.
		/// </summary>
		// Token: 0x04000306 RID: 774
		public long llLastTimestampEncoded;

		/// <summary>
		/// The time stamp of the most recent sample given to the media sink.
		/// </summary>
		// Token: 0x04000307 RID: 775
		public long llLastTimestampProcessed;

		/// <summary>
		/// The time stamp of the most recent stream tick. 
		/// </summary>
		// Token: 0x04000308 RID: 776
		public long llLastStreamTickReceived;

		/// <summary>
		/// The system time of the most recent sample request from the media sink. 
		/// </summary>
		// Token: 0x04000309 RID: 777
		public long llLastSinkSampleRequest;

		/// <summary>
		/// The number of samples received.
		/// </summary>
		// Token: 0x0400030A RID: 778
		public long qwNumSamplesReceived;

		/// <summary>
		/// The number of samples encoded.
		/// </summary>
		// Token: 0x0400030B RID: 779
		public long qwNumSamplesEncoded;

		/// <summary>
		/// The number of samples given to the media sink.
		/// </summary>
		// Token: 0x0400030C RID: 780
		public long qwNumSamplesProcessed;

		/// <summary>
		/// The number of stream ticks received.
		/// </summary>
		// Token: 0x0400030D RID: 781
		public long qwNumStreamTicksReceived;

		/// <summary>
		/// The amount of data, in bytes, currently waiting to be processed. 
		/// </summary>
		// Token: 0x0400030E RID: 782
		public int dwByteCountQueued;

		/// <summary>
		/// The total amount of data, in bytes, that has been sent to the media sink.
		/// </summary>
		// Token: 0x0400030F RID: 783
		public long qwByteCountProcessed;

		/// <summary>
		/// The number of pending sample requests.
		/// </summary>
		// Token: 0x04000310 RID: 784
		public int dwNumOutstandingSinkSampleRequests;

		/// <summary>
		/// The average rate, in media samples per 100-nanoseconds, at which the application sent samples to the sink writer.
		/// </summary>
		// Token: 0x04000311 RID: 785
		public int dwAverageSampleRateReceived;

		/// <summary>
		/// The average rate, in media samples per 100-nanoseconds, at which the sink writer sent samples to the encoder
		/// </summary>
		// Token: 0x04000312 RID: 786
		public int dwAverageSampleRateEncoded;

		/// <summary>
		/// The average rate, in media samples per 100-nanoseconds, at which the sink writer sent samples to the media sink.
		/// </summary>
		// Token: 0x04000313 RID: 787
		public int dwAverageSampleRateProcessed;
	}
}
