using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000026 RID: 38
	internal enum AudioClientErrors
	{
		/// <summary>
		/// AUDCLNT_E_NOT_INITIALIZED
		/// </summary>
		// Token: 0x04000080 RID: 128
		NotInitialized = -2004287487,
		/// <summary>
		/// AUDCLNT_E_UNSUPPORTED_FORMAT
		/// </summary>
		// Token: 0x04000081 RID: 129
		UnsupportedFormat = -2004287480,
		/// <summary>
		/// AUDCLNT_E_DEVICE_IN_USE
		/// </summary>
		// Token: 0x04000082 RID: 130
		DeviceInUse = -2004287478
	}
}
