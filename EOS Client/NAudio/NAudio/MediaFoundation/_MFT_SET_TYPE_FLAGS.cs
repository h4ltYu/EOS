using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Defines flags for the setting or testing the media type on a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x02000065 RID: 101
	[Flags]
	public enum _MFT_SET_TYPE_FLAGS
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x04000347 RID: 839
		None = 0,
		/// <summary>
		/// Test the proposed media type, but do not set it.
		/// </summary>
		// Token: 0x04000348 RID: 840
		MFT_SET_TYPE_TEST_ONLY = 1
	}
}
