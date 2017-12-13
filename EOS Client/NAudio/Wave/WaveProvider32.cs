using System;

namespace NAudio.Wave
{
    public abstract class WaveProvider32 : IWaveProvider, ISampleProvider
    {
        public WaveProvider32() : this(44100, 1)
        {
        }

        public WaveProvider32(int sampleRate, int channels)
        {
            this.SetWaveFormat(sampleRate, channels);
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int sampleCount = count / 4;
            int num = this.Read(waveBuffer.FloatBuffer, offset / 4, sampleCount);
            return num * 4;
        }

        public abstract int Read(float[] buffer, int offset, int sampleCount);

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
