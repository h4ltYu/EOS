using System;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Defines messages for a Media Foundation transform (MFT).
	/// </summary>
	// Token: 0x0200005A RID: 90
	public enum MFT_MESSAGE_TYPE
	{
		/// <summary>
		/// Requests the MFT to flush all stored data. 
		/// </summary>
		// Token: 0x040002F8 RID: 760
		MFT_MESSAGE_COMMAND_FLUSH,
		/// <summary>
		/// Requests the MFT to drain any stored data.
		/// </summary>
		// Token: 0x040002F9 RID: 761
		MFT_MESSAGE_COMMAND_DRAIN,
		/// <summary>
		/// Sets or clears the Direct3D Device Manager for DirectX Video Accereration (DXVA). 
		/// </summary>
		// Token: 0x040002FA RID: 762
		MFT_MESSAGE_SET_D3D_MANAGER,
		/// <summary>
		/// Drop samples - requires Windows 7
		/// </summary>
		// Token: 0x040002FB RID: 763
		MFT_MESSAGE_DROP_SAMPLES,
		/// <summary>
		/// Command Tick - requires Windows 8
		/// </summary>
		// Token: 0x040002FC RID: 764
		MFT_MESSAGE_COMMAND_TICK,
		/// <summary>
		/// Notifies the MFT that streaming is about to begin. 
		/// </summary>
		// Token: 0x040002FD RID: 765
		MFT_MESSAGE_NOTIFY_BEGIN_STREAMING = 268435456,
		/// <summary>
		/// Notifies the MFT that streaming is about to end. 
		/// </summary>
		// Token: 0x040002FE RID: 766
		MFT_MESSAGE_NOTIFY_END_STREAMING,
		/// <summary>
		/// Notifies the MFT that an input stream has ended. 
		/// </summary>
		// Token: 0x040002FF RID: 767
		MFT_MESSAGE_NOTIFY_END_OF_STREAM,
		/// <summary>
		/// Notifies the MFT that the first sample is about to be processed. 
		/// </summary>
		// Token: 0x04000300 RID: 768
		MFT_MESSAGE_NOTIFY_START_OF_STREAM,
		/// <summary>
		/// Marks a point in the stream. This message applies only to asynchronous MFTs. Requires Windows 7 
		/// </summary>
		// Token: 0x04000301 RID: 769
		MFT_MESSAGE_COMMAND_MARKER = 536870912
	}
}
