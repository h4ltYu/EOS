using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// SoundFont sample modes
	/// </summary>
	// Token: 0x020000C5 RID: 197
	public enum SampleMode
	{
		/// <summary>
		/// No loop
		/// </summary>
		// Token: 0x04000523 RID: 1315
		NoLoop,
		/// <summary>
		/// Loop Continuously
		/// </summary>
		// Token: 0x04000524 RID: 1316
		LoopContinuously,
		/// <summary>
		/// Reserved no loop
		/// </summary>
		// Token: 0x04000525 RID: 1317
		ReservedNoLoop,
		/// <summary>
		/// Loop and continue
		/// </summary>
		// Token: 0x04000526 RID: 1318
		LoopAndContinue
	}
}
