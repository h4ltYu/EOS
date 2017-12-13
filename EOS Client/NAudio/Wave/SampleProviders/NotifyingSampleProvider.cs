using System;

namespace NAudio.Wave.SampleProviders
{
    public class NotifyingSampleProvider : ISampleProvider, ISampleNotifier
    {
        public NotifyingSampleProvider(ISampleProvider source)
        {
            this.source = source;
            this.channels = this.WaveFormat.Channels;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this.source.WaveFormat;
            }
        }

        public int Read(float[] buffer, int offset, int sampleCount)
        {
            int num = this.source.Read(buffer, offset, sampleCount);
            if (this.Sample != null)
            {
                for (int i = 0; i < num; i += this.channels)
                {
                    this.sampleArgs.Left = buffer[offset + i];
                    this.sampleArgs.Right = ((this.channels > 1) ? buffer[offset + i + 1] : this.sampleArgs.Left);
                    this.Sample(this, this.sampleArgs);
                }
            }
            return num;
        }

        public event EventHandler<SampleEventArgs> Sample;

        private ISampleProvider source;

        private SampleEventArgs sampleArgs = new SampleEventArgs(0f, 0f);

        private int channels;
    }
}
