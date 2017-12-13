using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2")]
    internal interface IAudioRenderClient
    {
        int GetBuffer(int numFramesRequested, out IntPtr dataBufferPointer);

        int ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags);
    }
}
