using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Header Flags enumeration
	/// </summary>
	// Token: 0x02000181 RID: 385
	[Flags]
	public enum WaveHeaderFlags
	{
		/// <summary>
		/// WHDR_BEGINLOOP
		/// This buffer is the first buffer in a loop.  This flag is used only with output buffers.
		/// </summary>
		// Token: 0x0400092D RID: 2349
		BeginLoop = 4,
		/// <summary>
		/// WHDR_DONE
		/// Set by the device driver to indicate that it is finished with the buffer and is returning it to the application.
		/// </summary>
		// Token: 0x0400092E RID: 2350
		Done = 1,
		/// <summary>
		/// WHDR_ENDLOOP
		/// This buffer is the last buffer in a loop.  This flag is used only with output buffers.
		/// </summary>
		// Token: 0x0400092F RID: 2351
		EndLoop = 8,
		/// <summary>
		/// WHDR_INQUEUE
		/// Set by Windows to indicate that the buffer is queued for playback.
		/// </summary>
		// Token: 0x04000930 RID: 2352
		InQueue = 16,
		/// <summary>
		/// WHDR_PREPARED
		/// Set by Windows to indicate that the buffer has been prepared with the waveInPrepareHeader or waveOutPrepareHeader function.
		/// </summary>
		// Token: 0x04000931 RID: 2353
		Prepared = 2
	}
}
