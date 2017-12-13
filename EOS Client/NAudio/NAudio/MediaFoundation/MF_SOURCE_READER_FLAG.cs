using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains flags that indicate the status of the IMFSourceReader::ReadSample method
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd375773(v=vs.85).aspx
	/// </summary>
	// Token: 0x02000055 RID: 85
	[Flags]
	public enum MF_SOURCE_READER_FLAG
	{
		/// <summary>
		/// No Error
		/// </summary>
		// Token: 0x040002D9 RID: 729
		None = 0,
		/// <summary>
		/// An error occurred. If you receive this flag, do not make any further calls to IMFSourceReader methods.
		/// </summary>
		// Token: 0x040002DA RID: 730
		MF_SOURCE_READERF_ERROR = 1,
		/// <summary>
		/// The source reader reached the end of the stream.
		/// </summary>
		// Token: 0x040002DB RID: 731
		MF_SOURCE_READERF_ENDOFSTREAM = 2,
		/// <summary>
		/// One or more new streams were created
		/// </summary>
		// Token: 0x040002DC RID: 732
		MF_SOURCE_READERF_NEWSTREAM = 4,
		/// <summary>
		/// The native format has changed for one or more streams. The native format is the format delivered by the media source before any decoders are inserted.
		/// </summary>
		// Token: 0x040002DD RID: 733
		MF_SOURCE_READERF_NATIVEMEDIATYPECHANGED = 16,
		/// <summary>
		/// The current media has type changed for one or more streams. To get the current media type, call the IMFSourceReader::GetCurrentMediaType method.
		/// </summary>
		// Token: 0x040002DE RID: 734
		MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED = 32,
		/// <summary>
		/// There is a gap in the stream. This flag corresponds to an MEStreamTick event from the media source.
		/// </summary>
		// Token: 0x040002DF RID: 735
		MF_SOURCE_READERF_STREAMTICK = 256,
		/// <summary>
		/// All transforms inserted by the application have been removed for a particular stream.
		/// </summary>
		// Token: 0x040002E0 RID: 736
		MF_SOURCE_READERF_ALLEFFECTSREMOVED = 512
	}
}
