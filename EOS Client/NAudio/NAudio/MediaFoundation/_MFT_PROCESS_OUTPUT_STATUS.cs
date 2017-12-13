using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Process Output Status flags
	/// </summary>
	// Token: 0x02000064 RID: 100
	[Flags]
	public enum _MFT_PROCESS_OUTPUT_STATUS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000344 RID: 836
		None = 0,
		/// <summary>
		/// The Media Foundation transform (MFT) has created one or more new output streams.
		/// </summary>
		// Token: 0x04000345 RID: 837
		MFT_PROCESS_OUTPUT_STATUS_NEW_STREAMS = 256
	}
}
