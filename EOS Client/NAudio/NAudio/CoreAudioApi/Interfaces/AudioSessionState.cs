using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Defines constants that indicate the current state of an audio session.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370792.aspx
	/// </remarks>
	// Token: 0x0200002F RID: 47
	public enum AudioSessionState
	{
		/// <summary>
		/// The audio session is inactive.
		/// </summary>
		// Token: 0x0400009C RID: 156
		AudioSessionStateInactive,
		/// <summary>
		/// The audio session is active.
		/// </summary>
		// Token: 0x0400009D RID: 157
		AudioSessionStateActive,
		/// <summary>
		/// The audio session has expired.
		/// </summary>
		// Token: 0x0400009E RID: 158
		AudioSessionStateExpired
	}
}
