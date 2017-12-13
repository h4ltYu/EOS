using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// The AudioClientProperties structure is used to set the parameters that describe the properties of the client's audio stream.
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/hh968105(v=vs.85).aspx</remarks>
	// Token: 0x0200000D RID: 13
	public struct AudioClientProperties
	{
		/// <summary>
		/// The size of the buffer for the audio stream.
		/// </summary>
		// Token: 0x04000040 RID: 64
		public uint cbSize;

		/// <summary>
		/// Boolean value to indicate whether or not the audio stream is hardware-offloaded
		/// </summary>
		// Token: 0x04000041 RID: 65
		public int bIsOffload;

		/// <summary>
		/// An enumeration that is used to specify the category of the audio stream.
		/// </summary>
		// Token: 0x04000042 RID: 66
		public AudioStreamCategory eCategory;

		/// <summary>
		/// A bit-field describing the characteristics of the stream. Supported in Windows 8.1 and later.
		/// </summary>
		// Token: 0x04000043 RID: 67
		public AudioClientStreamOptions Options;
	}
}
