using System;

namespace NAudio.Wave
{
    public interface IWavePosition
    {
        long GetPosition();

        WaveFormat OutputWaveFormat { get; }
    }
}
