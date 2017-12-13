using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Input Data Buffer Flags
	/// </summary>
	// Token: 0x02000203 RID: 515
	[Flags]
	public enum DmoInputDataBufferFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000C1F RID: 3103
		None = 0,
		/// <summary>
		/// DMO_INPUT_DATA_BUFFERF_SYNCPOINT
		/// </summary>
		// Token: 0x04000C20 RID: 3104
		SyncPoint = 1,
		/// <summary>
		/// DMO_INPUT_DATA_BUFFERF_TIME
		/// </summary>
		// Token: 0x04000C21 RID: 3105
		Time = 2,
		/// <summary>
		/// DMO_INPUT_DATA_BUFFERF_TIMELENGTH
		/// </summary>
		// Token: 0x04000C22 RID: 3106
		TimeLength = 4
	}
}
