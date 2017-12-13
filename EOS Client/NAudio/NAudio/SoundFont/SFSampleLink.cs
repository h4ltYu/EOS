using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Sample Link Type
	/// </summary>
	// Token: 0x020000C6 RID: 198
	public enum SFSampleLink : ushort
	{
		/// <summary>
		/// Mono Sample
		/// </summary>
		// Token: 0x04000528 RID: 1320
		MonoSample = 1,
		/// <summary>
		/// Right Sample
		/// </summary>
		// Token: 0x04000529 RID: 1321
		RightSample,
		/// <summary>
		/// Left Sample
		/// </summary>
		// Token: 0x0400052A RID: 1322
		LeftSample = 4,
		/// <summary>
		/// Linked Sample
		/// </summary>
		// Token: 0x0400052B RID: 1323
		LinkedSample = 8,
		/// <summary>
		/// ROM Mono Sample
		/// </summary>
		// Token: 0x0400052C RID: 1324
		RomMonoSample = 32769,
		/// <summary>
		/// ROM Right Sample
		/// </summary>
		// Token: 0x0400052D RID: 1325
		RomRightSample,
		/// <summary>
		/// ROM Left Sample
		/// </summary>
		// Token: 0x0400052E RID: 1326
		RomLeftSample = 32772,
		/// <summary>
		/// ROM Linked Sample
		/// </summary>
		// Token: 0x0400052F RID: 1327
		RomLinkedSample = 32776
	}
}
