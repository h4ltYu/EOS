using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioRenderClient : IDisposable
    {
        internal AudioRenderClient(IAudioRenderClient audioRenderClientInterface)
        {
            this.audioRenderClientInterface = audioRenderClientInterface;
        }

        public IntPtr GetBuffer(int numFramesRequested)
        {
            IntPtr result;
            Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.GetBuffer(numFramesRequested, out result));
            return result;
        }

        public void ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags)
        {
            Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.ReleaseBuffer(numFramesWritten, bufferFlags));
        }

        public void Dispose()
        {
            if (this.audioRenderClientInterface != null)
            {
                Marshal.ReleaseComObject(this.audioRenderClientInterface);
                this.audioRenderClientInterface = null;
                GC.SuppressFinalize(this);
            }
        }

        private IAudioRenderClient audioRenderClientInterface;
    }
}
