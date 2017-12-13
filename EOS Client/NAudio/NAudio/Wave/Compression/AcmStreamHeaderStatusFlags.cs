using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000177 RID: 375
	[Flags]
	internal enum AcmStreamHeaderStatusFlags
	{
		/// <summary>
		/// ACMSTREAMHEADER_STATUSF_DONE
		/// </summary>
		// Token: 0x0400083F RID: 2111
		Done = 65536,
		/// <summary>
		/// ACMSTREAMHEADER_STATUSF_PREPARED
		/// </summary>
		// Token: 0x04000840 RID: 2112
		Prepared = 131072,
		/// <summary>
		/// ACMSTREAMHEADER_STATUSF_INQUEUE
		/// </summary>
		// Token: 0x04000841 RID: 2113
		InQueue = 1048576
	}
}
