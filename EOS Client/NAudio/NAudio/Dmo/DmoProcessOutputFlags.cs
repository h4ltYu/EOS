using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Process Output Flags
	/// </summary>
	// Token: 0x02000208 RID: 520
	[Flags]
	public enum DmoProcessOutputFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000C3A RID: 3130
		None = 0,
		/// <summary>
		/// DMO_PROCESS_OUTPUT_DISCARD_WHEN_NO_BUFFER
		/// </summary>
		// Token: 0x04000C3B RID: 3131
		DiscardWhenNoBuffer = 1
	}
}
