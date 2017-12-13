using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Output Data Buffer Flags
	/// </summary>
	// Token: 0x02000207 RID: 519
	[Flags]
	public enum DmoOutputDataBufferFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000C34 RID: 3124
		None = 0,
		/// <summary>
		/// DMO_OUTPUT_DATA_BUFFERF_SYNCPOINT
		/// </summary>
		// Token: 0x04000C35 RID: 3125
		SyncPoint = 1,
		/// <summary>
		/// DMO_OUTPUT_DATA_BUFFERF_TIME
		/// </summary>
		// Token: 0x04000C36 RID: 3126
		Time = 2,
		/// <summary>
		/// DMO_OUTPUT_DATA_BUFFERF_TIMELENGTH
		/// </summary>
		// Token: 0x04000C37 RID: 3127
		TimeLength = 4,
		/// <summary>
		/// DMO_OUTPUT_DATA_BUFFERF_INCOMPLETE
		/// </summary>
		// Token: 0x04000C38 RID: 3128
		Incomplete = 16777216
	}
}
