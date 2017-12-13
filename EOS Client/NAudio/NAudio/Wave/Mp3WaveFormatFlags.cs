using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Format Padding Flags
	/// </summary>
	// Token: 0x020001A5 RID: 421
	[Flags]
	public enum Mp3WaveFormatFlags
	{
		/// <summary>
		/// MPEGLAYER3_FLAG_PADDING_ISO
		/// </summary>
		// Token: 0x040009C5 RID: 2501
		PaddingIso = 0,
		/// <summary>
		/// MPEGLAYER3_FLAG_PADDING_ON
		/// </summary>
		// Token: 0x040009C6 RID: 2502
		PaddingOn = 1,
		/// <summary>
		/// MPEGLAYER3_FLAG_PADDING_OFF
		/// </summary>
		// Token: 0x040009C7 RID: 2503
		PaddingOff = 2
	}
}
