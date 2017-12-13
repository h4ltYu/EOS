using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200002B RID: 43
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2")]
	internal interface IAudioRenderClient
	{
		// Token: 0x060000AE RID: 174
		int GetBuffer(int numFramesRequested, out IntPtr dataBufferPointer);

		// Token: 0x060000AF RID: 175
		int ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags);
	}
}
