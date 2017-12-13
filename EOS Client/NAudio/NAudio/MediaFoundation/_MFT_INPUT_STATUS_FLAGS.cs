using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Indicates the status of an input stream on a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x0200005E RID: 94
	[Flags]
	public enum _MFT_INPUT_STATUS_FLAGS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x0400031F RID: 799
		None = 0,
		/// <summary>
		/// The input stream can receive more data at this time.
		/// </summary>
		// Token: 0x04000320 RID: 800
		MFT_INPUT_STATUS_ACCEPT_DATA = 1
	}
}
