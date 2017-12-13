using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// Callback used by the ASIODriverExt to get wave data
	/// </summary>
	// Token: 0x0200014E RID: 334
	// (Invoke) Token: 0x06000746 RID: 1862
	internal delegate void ASIOFillBufferCallback(IntPtr[] inputChannels, IntPtr[] outputChannels);
}
