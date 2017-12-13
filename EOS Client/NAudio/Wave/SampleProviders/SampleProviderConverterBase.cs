using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
    public abstract class SampleProviderConverterBase : ISampleProvider
    {
        public SampleProviderConverterBase(IWaveProvider source)
        {
            this.source = source;
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, source.WaveFormat.Channels);
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        public abstract int Read(float[] buffer, int offset, int count);

        protected void EnsureSourceBuffer(int sourceBytesRequired)
        {
            this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, sourceBytesRequired);
        }

        protected IWaveProvider source;

        private WaveFormat waveFormat;

        protected byte[] sourceBuffer;
    }
}
