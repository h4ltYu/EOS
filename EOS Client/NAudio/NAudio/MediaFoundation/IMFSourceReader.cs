using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// IMFSourceReader interface
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd374655%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000054 RID: 84
	[Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMFSourceReader
	{
		/// <summary>
		/// Queries whether a stream is selected.
		/// </summary>
		// Token: 0x0600022F RID: 559
		void GetStreamSelection([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Bool)] out bool pSelected);

		/// <summary>
		/// Selects or deselects one or more streams.
		/// </summary>
		// Token: 0x06000230 RID: 560
		void SetStreamSelection([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Bool)] [In] bool pSelected);

		/// <summary>
		/// Gets a format that is supported natively by the media source.
		/// </summary>
		// Token: 0x06000231 RID: 561
		void GetNativeMediaType([In] int dwStreamIndex, [In] int dwMediaTypeIndex, out IMFMediaType ppMediaType);

		/// <summary>
		/// Gets the current media type for a stream.
		/// </summary>
		// Token: 0x06000232 RID: 562
		void GetCurrentMediaType([In] int dwStreamIndex, out IMFMediaType ppMediaType);

		/// <summary>
		/// Sets the media type for a stream.
		/// </summary>
		// Token: 0x06000233 RID: 563
		void SetCurrentMediaType([In] int dwStreamIndex, IntPtr pdwReserved, [In] IMFMediaType pMediaType);

		/// <summary>
		/// Seeks to a new position in the media source.
		/// </summary>
		// Token: 0x06000234 RID: 564
		void SetCurrentPosition([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidTimeFormat, [In] ref PropVariant varPosition);

		/// <summary>
		/// Reads the next sample from the media source.
		/// </summary>
		// Token: 0x06000235 RID: 565
		void ReadSample([In] int dwStreamIndex, [In] int dwControlFlags, out int pdwActualStreamIndex, out MF_SOURCE_READER_FLAG pdwStreamFlags, out ulong pllTimestamp, out IMFSample ppSample);

		/// <summary>
		/// Flushes one or more streams.
		/// </summary>
		// Token: 0x06000236 RID: 566
		void Flush([In] int dwStreamIndex);

		/// <summary>
		/// Queries the underlying media source or decoder for an interface.
		/// </summary>
		// Token: 0x06000237 RID: 567
		void GetServiceForStream([In] int dwStreamIndex, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidService, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr ppvObject);

		/// <summary>
		/// Gets an attribute from the underlying media source.
		/// </summary>
		// Token: 0x06000238 RID: 568
		[PreserveSig]
		int GetPresentationAttribute([In] int dwStreamIndex, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidAttribute, out PropVariant pvarAttribute);
	}
}
