using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Format ID
	/// </summary>
	// Token: 0x020001A6 RID: 422
	public enum Mp3WaveFormatId : ushort
	{
		/// <summary>MPEGLAYER3_ID_UNKNOWN</summary>
		// Token: 0x040009C9 RID: 2505
		Unknown,
		/// <summary>MPEGLAYER3_ID_MPEG</summary>
		// Token: 0x040009CA RID: 2506
		Mpeg,
		/// <summary>MPEGLAYER3_ID_CONSTANTFRAMESIZE</summary>
		// Token: 0x040009CB RID: 2507
		ConstantFrameSize
	}
}
