using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Implemented by the Microsoft Media Foundation sink writer object.
	/// </summary>
	// Token: 0x0200004A RID: 74
	[Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMFSinkWriter
	{
		/// <summary>
		/// Adds a stream to the sink writer.
		/// </summary>
		// Token: 0x06000191 RID: 401
		void AddStream([MarshalAs(UnmanagedType.Interface)] [In] IMFMediaType pTargetMediaType, out int pdwStreamIndex);

		/// <summary>
		/// Sets the input format for a stream on the sink writer.
		/// </summary>
		// Token: 0x06000192 RID: 402
		void SetInputMediaType([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Interface)] [In] IMFMediaType pInputMediaType, [MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pEncodingParameters);

		/// <summary>
		/// Initializes the sink writer for writing.
		/// </summary>
		// Token: 0x06000193 RID: 403
		void BeginWriting();

		/// <summary>
		/// Delivers a sample to the sink writer.
		/// </summary>
		// Token: 0x06000194 RID: 404
		void WriteSample([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Interface)] [In] IMFSample pSample);

		/// <summary>
		/// Indicates a gap in an input stream.
		/// </summary>
		// Token: 0x06000195 RID: 405
		void SendStreamTick([In] int dwStreamIndex, [In] long llTimestamp);

		/// <summary>
		/// Places a marker in the specified stream.
		/// </summary>
		// Token: 0x06000196 RID: 406
		void PlaceMarker([In] int dwStreamIndex, [In] IntPtr pvContext);

		/// <summary>
		/// Notifies the media sink that a stream has reached the end of a segment.
		/// </summary>
		// Token: 0x06000197 RID: 407
		void NotifyEndOfSegment([In] int dwStreamIndex);

		/// <summary>
		/// Flushes one or more streams.
		/// </summary>
		// Token: 0x06000198 RID: 408
		void Flush([In] int dwStreamIndex);

		/// <summary>
		/// (Finalize) Completes all writing operations on the sink writer.
		/// </summary>
		// Token: 0x06000199 RID: 409
		void DoFinalize();

		/// <summary>
		/// Queries the underlying media sink or encoder for an interface.
		/// </summary>
		// Token: 0x0600019A RID: 410
		void GetServiceForStream([In] int dwStreamIndex, [In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);

		/// <summary>
		/// Gets statistics about the performance of the sink writer.
		/// </summary>
		// Token: 0x0600019B RID: 411
		void GetStatistics([In] int dwStreamIndex, [In] [Out] MF_SINK_WRITER_STATISTICS pStats);
	}
}
