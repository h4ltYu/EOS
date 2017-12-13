using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains information about an output buffer for a Media Foundation transform. 
	/// </summary>
	// Token: 0x02000058 RID: 88
	public struct MFT_OUTPUT_DATA_BUFFER
	{
		/// <summary>
		/// Output stream identifier.
		/// </summary>
		// Token: 0x040002F0 RID: 752
		public int dwStreamID;

		/// <summary>
		/// Pointer to the IMFSample interface. 
		/// </summary>
		// Token: 0x040002F1 RID: 753
		public IMFSample pSample;

		/// <summary>
		/// Before calling ProcessOutput, set this member to zero.
		/// </summary>
		// Token: 0x040002F2 RID: 754
		public _MFT_OUTPUT_DATA_BUFFER_FLAGS dwStatus;

		/// <summary>
		/// Before calling ProcessOutput, set this member to NULL.
		/// </summary>
		// Token: 0x040002F3 RID: 755
		public IMFCollection pEvents;
	}
}
