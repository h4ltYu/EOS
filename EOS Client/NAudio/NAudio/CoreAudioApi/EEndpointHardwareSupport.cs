using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Endpoint Hardware Support
	/// </summary>
	// Token: 0x02000121 RID: 289
	[Flags]
	public enum EEndpointHardwareSupport
	{
		/// <summary>
		/// Volume
		/// </summary>
		// Token: 0x04000700 RID: 1792
		Volume = 1,
		/// <summary>
		/// Mute
		/// </summary>
		// Token: 0x04000701 RID: 1793
		Mute = 2,
		/// <summary>
		/// Meter
		/// </summary>
		// Token: 0x04000702 RID: 1794
		Meter = 4
	}
}
