using System;

namespace NAudio.Wave.SampleProviders
{
    public class SampleToWaveProvider : IWaveProvider
    {
        public SampleToWaveProvider(ISampleProvider source)
        {
            if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Must be already floating point");
            }
            this.source = source;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int count2 = count / 4;
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int num = this.source.Read(waveBuffer.FloatBuffer, offset / 4, count2);
            return num * 4;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this.source.WaveFormat;
            }
        }

        private ISampleProvider source;
    }
}
