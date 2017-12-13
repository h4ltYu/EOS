using System;

namespace NAudio.Wave.SampleProviders
{
    public class MonoToStereoSampleProvider : ISampleProvider
    {
        public MonoToStereoSampleProvider(ISampleProvider source)
        {
            if (source.WaveFormat.Channels != 1)
            {
                throw new ArgumentException("Source must be mono");
            }
            this.source = source;
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int count2 = count / 2;
            int num = offset;
            this.EnsureSourceBuffer(count2);
            int num2 = this.source.Read(this.sourceBuffer, 0, count2);
            for (int i = 0; i < num2; i++)
            {
                buffer[num++] = this.sourceBuffer[i];
                buffer[num++] = this.sourceBuffer[i];
            }
            return num2 * 2;
        }

        private void EnsureSourceBuffer(int count)
        {
            if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
            {
                this.sourceBuffer = new float[count];
            }
        }

        private readonly ISampleProvider source;

        private readonly WaveFormat waveFormat;

        private float[] sourceBuffer;
    }
}
