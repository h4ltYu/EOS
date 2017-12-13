using System;
using NAudio.Utils;

namespace NAudio.Wave
{
    public class Wave16ToFloatProvider : IWaveProvider
    {
        public Wave16ToFloatProvider(IWaveProvider sourceProvider)
        {
            if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
            {
                throw new ArgumentException("Only PCM supported");
            }
            if (sourceProvider.WaveFormat.BitsPerSample != 16)
            {
                throw new ArgumentException("Only 16 bit audio supported");
            }
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceProvider.WaveFormat.SampleRate, sourceProvider.WaveFormat.Channels);
            this.sourceProvider = sourceProvider;
            this.volume = 1f;
        }

        public int Read(byte[] destBuffer, int offset, int numBytes)
        {
            int num = numBytes / 2;
            this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
            int num2 = this.sourceProvider.Read(this.sourceBuffer, offset, num);
            WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
            WaveBuffer waveBuffer2 = new WaveBuffer(destBuffer);
            int num3 = num2 / 2;
            int num4 = offset / 4;
            for (int i = 0; i < num3; i++)
            {
                waveBuffer2.FloatBuffer[num4++] = (float)waveBuffer.ShortBuffer[i] / 32768f * this.volume;
            }
            return num3 * 4;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        public float Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                this.volume = value;
            }
        }

        private IWaveProvider sourceProvider;

        private readonly WaveFormat waveFormat;

        private volatile float volume;

        private byte[] sourceBuffer;
    }
}
