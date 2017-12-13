using System;

namespace NAudio.Wave.Asio
{
    internal delegate void ASIOFillBufferCallback(IntPtr[] inputChannels, IntPtr[] outputChannels);
}
