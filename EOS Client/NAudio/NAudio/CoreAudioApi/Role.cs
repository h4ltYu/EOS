using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// The ERole enumeration defines constants that indicate the role 
	/// that the system has assigned to an audio endpoint device
	/// </summary>
	// Token: 0x02000132 RID: 306
	public enum Role
	{
		/// <summary>
		/// Games, system notification sounds, and voice commands.
		/// </summary>
		// Token: 0x0400073B RID: 1851
		Console,
		/// <summary>
		/// Music, movies, narration, and live music recording
		/// </summary>
		// Token: 0x0400073C RID: 1852
		Multimedia,
		/// <summary>
		/// Voice communications (talking to another person).
		/// </summary>
		// Token: 0x0400073D RID: 1853
		Communications
	}
}
