using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Callback Strategy
	/// </summary>
	// Token: 0x020001CC RID: 460
	public enum WaveCallbackStrategy
	{
		/// <summary>
		/// Use a function
		/// </summary>
		// Token: 0x04000B19 RID: 2841
		FunctionCallback,
		/// <summary>
		/// Create a new window (should only be done if on GUI thread)
		/// </summary>
		// Token: 0x04000B1A RID: 2842
		NewWindow,
		/// <summary>
		/// Use an existing window handle
		/// </summary>
		// Token: 0x04000B1B RID: 2843
		ExistingWindow,
		/// <summary>
		/// Use an event handle
		/// </summary>
		// Token: 0x04000B1C RID: 2844
		Event
	}
}
