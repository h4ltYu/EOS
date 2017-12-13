using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Client Buffer Flags
	/// </summary>
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum AudioClientBufferFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x0400003C RID: 60
		None = 0,
		/// <summary>
		/// AUDCLNT_BUFFERFLAGS_DATA_DISCONTINUITY
		/// </summary>
		// Token: 0x0400003D RID: 61
		DataDiscontinuity = 1,
		/// <summary>
		/// AUDCLNT_BUFFERFLAGS_SILENT
		/// </summary>
		// Token: 0x0400003E RID: 62
		Silent = 2,
		/// <summary>
		/// AUDCLNT_BUFFERFLAGS_TIMESTAMP_ERROR
		/// </summary>
		// Token: 0x0400003F RID: 63
		TimestampError = 4
	}
}
