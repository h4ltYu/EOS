using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// AUDCLNT_STREAMFLAGS
	/// </summary>
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum AudioClientStreamFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000048 RID: 72
		None = 0,
		/// <summary>
		/// AUDCLNT_STREAMFLAGS_CROSSPROCESS
		/// </summary>
		// Token: 0x04000049 RID: 73
		CrossProcess = 65536,
		/// <summary>
		/// AUDCLNT_STREAMFLAGS_LOOPBACK
		/// </summary>
		// Token: 0x0400004A RID: 74
		Loopback = 131072,
		/// <summary>
		/// AUDCLNT_STREAMFLAGS_EVENTCALLBACK 
		/// </summary>
		// Token: 0x0400004B RID: 75
		EventCallback = 262144,
		/// <summary>
		/// AUDCLNT_STREAMFLAGS_NOPERSIST     
		/// </summary>
		// Token: 0x0400004C RID: 76
		NoPersist = 524288
	}
}
