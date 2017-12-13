using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Interop definitions for MediaFoundation
	/// thanks to Lucian Wischik for the initial work on many of these definitions (also various interfaces)
	/// n.b. the goal is to make as much of this internal as possible, and provide
	/// better .NET APIs using the MediaFoundationApi class instead
	/// </summary>
	// Token: 0x0200004F RID: 79
	public static class MediaFoundationInterop
	{
		/// <summary>
		/// Initializes Microsoft Media Foundation.
		/// </summary>
		// Token: 0x060001BE RID: 446
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFStartup(int version, int dwFlags = 0);

		/// <summary>
		/// Shuts down the Microsoft Media Foundation platform
		/// </summary>
		// Token: 0x060001BF RID: 447
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFShutdown();

		/// <summary>
		/// Creates an empty media type.
		/// </summary>
		// Token: 0x060001C0 RID: 448
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateMediaType(out IMFMediaType ppMFType);

		/// <summary>
		/// Initializes a media type from a WAVEFORMATEX structure. 
		/// </summary>
		// Token: 0x060001C1 RID: 449
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFInitMediaTypeFromWaveFormatEx([In] IMFMediaType pMFType, [In] WaveFormat pWaveFormat, [In] int cbBufSize);

		/// <summary>
		/// Converts a Media Foundation audio media type to a WAVEFORMATEX structure.
		/// </summary>
		/// TODO: try making second parameter out WaveFormatExtraData
		// Token: 0x060001C2 RID: 450
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateWaveFormatExFromMFMediaType(IMFMediaType pMFType, ref IntPtr ppWF, ref int pcbSize, int flags = 0);

		/// <summary>
		/// Creates the source reader from a URL.
		/// </summary>
		// Token: 0x060001C3 RID: 451
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSourceReaderFromURL([MarshalAs(UnmanagedType.LPWStr)] [In] string pwszURL, [In] IMFAttributes pAttributes, [MarshalAs(UnmanagedType.Interface)] out IMFSourceReader ppSourceReader);

		/// <summary>
		/// Creates the source reader from a byte stream.
		/// </summary>
		// Token: 0x060001C4 RID: 452
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSourceReaderFromByteStream([In] IMFByteStream pByteStream, [In] IMFAttributes pAttributes, [MarshalAs(UnmanagedType.Interface)] out IMFSourceReader ppSourceReader);

		/// <summary>
		/// Creates the sink writer from a URL or byte stream.
		/// </summary>
		// Token: 0x060001C5 RID: 453
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSinkWriterFromURL([MarshalAs(UnmanagedType.LPWStr)] [In] string pwszOutputURL, [In] IMFByteStream pByteStream, [In] IMFAttributes pAttributes, out IMFSinkWriter ppSinkWriter);

		/// <summary>
		/// Creates a Microsoft Media Foundation byte stream that wraps an IRandomAccessStream object.
		/// </summary>
		// Token: 0x060001C6 RID: 454
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateMFByteStreamOnStreamEx([MarshalAs(UnmanagedType.IUnknown)] object punkStream, out IMFByteStream ppByteStream);

		/// <summary>
		/// Gets a list of Microsoft Media Foundation transforms (MFTs) that match specified search criteria. 
		/// </summary>
		// Token: 0x060001C7 RID: 455
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFTEnumEx([In] Guid guidCategory, [In] _MFT_ENUM_FLAG flags, [In] MFT_REGISTER_TYPE_INFO pInputType, [In] MFT_REGISTER_TYPE_INFO pOutputType, out IntPtr pppMFTActivate, out int pcMFTActivate);

		/// <summary>
		/// Creates an empty media sample.
		/// </summary>
		// Token: 0x060001C8 RID: 456
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateSample(out IMFSample ppIMFSample);

		/// <summary>
		/// Allocates system memory and creates a media buffer to manage it.
		/// </summary>
		// Token: 0x060001C9 RID: 457
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateMemoryBuffer(int cbMaxLength, out IMFMediaBuffer ppBuffer);

		/// <summary>
		/// Creates an empty attribute store. 
		/// </summary>
		// Token: 0x060001CA RID: 458
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateAttributes([MarshalAs(UnmanagedType.Interface)] out IMFAttributes ppMFAttributes, [In] int cInitialSize);

		/// <summary>
		/// Gets a list of output formats from an audio encoder.
		/// </summary>
		// Token: 0x060001CB RID: 459
		[DllImport("mf.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFTranscodeGetAudioOutputAvailableTypes([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidSubType, [In] _MFT_ENUM_FLAG dwMFTFlags, [In] IMFAttributes pCodecConfig, [MarshalAs(UnmanagedType.Interface)] out IMFCollection ppAvailableTypes);

		/// <summary>
		/// All streams
		/// </summary>
		// Token: 0x040002D1 RID: 721
		public const int MF_SOURCE_READER_ALL_STREAMS = -2;

		/// <summary>
		/// First audio stream
		/// </summary>
		// Token: 0x040002D2 RID: 722
		public const int MF_SOURCE_READER_FIRST_AUDIO_STREAM = -3;

		/// <summary>
		/// First video stream
		/// </summary>
		// Token: 0x040002D3 RID: 723
		public const int MF_SOURCE_READER_FIRST_VIDEO_STREAM = -4;

		/// <summary>
		/// Media source
		/// </summary>
		// Token: 0x040002D4 RID: 724
		public const int MF_SOURCE_READER_MEDIASOURCE = -1;

		/// <summary>
		/// Media Foundation SDK Version
		/// </summary>
		// Token: 0x040002D5 RID: 725
		public const int MF_SDK_VERSION = 2;

		/// <summary>
		/// Media Foundation API Version
		/// </summary>
		// Token: 0x040002D6 RID: 726
		public const int MF_API_VERSION = 112;

		/// <summary>
		/// Media Foundation Version
		/// </summary>
		// Token: 0x040002D7 RID: 727
		public const int MF_VERSION = 131184;
	}
}
