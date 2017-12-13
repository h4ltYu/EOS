using System;

namespace NAudio.Codecs
{
	/// <summary>
	/// G722 Flags
	/// </summary>
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum G722Flags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x0400002C RID: 44
		None = 0,
		/// <summary>
		/// Using a G722 sample rate of 8000
		/// </summary>
		// Token: 0x0400002D RID: 45
		SampleRate8000 = 1,
		/// <summary>
		/// Packed
		/// </summary>
		// Token: 0x0400002E RID: 46
		Packed = 2
	}
}
