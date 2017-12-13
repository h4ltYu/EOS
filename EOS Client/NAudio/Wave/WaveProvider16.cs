using System;

namespace NAudio.Wave
{
    public abstract class WaveProvider16 : IWaveProvider
    {
        public WaveProvider16() : this(44100, 1)
        {
        }

        public WaveProvider16(int sampleRate, int channels)
        {
            this.SetWaveFormat(sampleRate, channels);
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = new WaveFormat(sampleRate, 16, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int sampleCount = count / 2;
            int num = this.Read(waveBuffer.ShortBuffer, offset / 2, sampleCount);
            return num * 2;
        }

        public abstract int Read(short[] buffer, int offset, int sampleCount);

        public WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        private WaveFormat waveFormat;
    }
}
