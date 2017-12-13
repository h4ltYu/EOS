using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000028 RID: 40
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C8ADBD64-E71E-48a0-A4DE-185C395CD317")]
	internal interface IAudioCaptureClient
	{
		// Token: 0x060000A7 RID: 167
		int GetBuffer(out IntPtr dataBuffer, out int numFramesToRead, out AudioClientBufferFlags bufferFlags, out long devicePosition, out long qpcPosition);

		// Token: 0x060000A8 RID: 168
		int ReleaseBuffer(int numFramesRead);

		// Token: 0x060000A9 RID: 169
		int GetNextPacketSize(out int numFramesInNextPacket);
	}
}
