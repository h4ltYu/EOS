using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Specifies the category of an audio stream.
	/// </summary>
	// Token: 0x02000022 RID: 34
	public enum AudioStreamCategory
	{
		/// <summary>
		/// Other audio stream.
		/// </summary>
		// Token: 0x0400006C RID: 108
		Other,
		/// <summary>
		/// Media that will only stream when the app is in the foreground.
		/// </summary>
		// Token: 0x0400006D RID: 109
		ForegroundOnlyMedia,
		/// <summary>
		/// Media that can be streamed when the app is in the background.
		/// </summary>
		// Token: 0x0400006E RID: 110
		BackgroundCapableMedia,
		/// <summary>
		/// Real-time communications, such as VOIP or chat.
		/// </summary>
		// Token: 0x0400006F RID: 111
		Communications,
		/// <summary>
		/// Alert sounds.
		/// </summary>
		// Token: 0x04000070 RID: 112
		Alerts,
		/// <summary>
		/// Sound effects.
		/// </summary>
		// Token: 0x04000071 RID: 113
		SoundEffects,
		/// <summary>
		/// Game sound effects.
		/// </summary>
		// Token: 0x04000072 RID: 114
		GameEffects,
		/// <summary>
		/// Background audio for games.
		/// </summary>
		// Token: 0x04000073 RID: 115
		GameMedia
	}
}
