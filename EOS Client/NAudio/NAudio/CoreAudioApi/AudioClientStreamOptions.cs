using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Defines values that describe the characteristics of an audio stream.
	/// </summary>
	// Token: 0x02000010 RID: 16
	public enum AudioClientStreamOptions
	{
		/// <summary>
		/// No stream options.
		/// </summary>
		// Token: 0x0400004E RID: 78
		None,
		/// <summary>
		/// The audio stream is a 'raw' stream that bypasses all signal processing except for endpoint specific, always-on processing in the APO, driver, and hardware.
		/// </summary>
		// Token: 0x0400004F RID: 79
		Raw
	}
}
