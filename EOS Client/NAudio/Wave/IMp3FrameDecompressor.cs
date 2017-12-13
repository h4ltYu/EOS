using System;

namespace NAudio.Wave
{
    public interface IMp3FrameDecompressor : IDisposable
    {
        int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset);

        void Reset();

        WaveFormat OutputFormat { get; }
    }
}
