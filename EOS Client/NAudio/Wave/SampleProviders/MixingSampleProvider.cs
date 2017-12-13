using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
    public class MixingSampleProvider : ISampleProvider
    {
        public MixingSampleProvider(WaveFormat waveFormat)
        {
            if (waveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Mixer wave format must be IEEE float");
            }
            this.sources = new List<ISampleProvider>();
            this.waveFormat = waveFormat;
        }

        public MixingSampleProvider(IEnumerable<ISampleProvider> sources)
        {
            this.sources = new List<ISampleProvider>();
            foreach (ISampleProvider mixerInput in sources)
            {
                this.AddMixerInput(mixerInput);
            }
            if (this.sources.Count == 0)
            {
                throw new ArgumentException("Must provide at least one input in this constructor");
            }
        }

        public bool ReadFully { get; set; }

        public void AddMixerInput(IWaveProvider mixerInput)
        {
            this.AddMixerInput(SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(mixerInput));
        }

        public void AddMixerInput(ISampleProvider mixerInput)
        {
            lock (this.sources)
            {
                if (this.sources.Count >= 1024)
                {
                    throw new InvalidOperationException("Too many mixer inputs");
                }
                this.sources.Add(mixerInput);
            }
            if (this.waveFormat == null)
            {
                this.waveFormat = mixerInput.WaveFormat;
                return;
            }
            if (this.WaveFormat.SampleRate != mixerInput.WaveFormat.SampleRate || this.WaveFormat.Channels != mixerInput.WaveFormat.Channels)
            {
                throw new ArgumentException("All mixer inputs must have the same WaveFormat");
            }
        }

        public void RemoveMixerInput(ISampleProvider mixerInput)
        {
            lock (this.sources)
            {
                this.sources.Remove(mixerInput);
            }
        }

        public void RemoveAllMixerInputs()
        {
            lock (this.sources)
            {
                this.sources.Clear();
            }
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
            int num = 0;
            this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, count);
            lock (this.sources)
            {
                for (int i = this.sources.Count - 1; i >= 0; i--)
                {
                    ISampleProvider sampleProvider = this.sources[i];
                    int num2 = sampleProvider.Read(this.sourceBuffer, 0, count);
                    int num3 = offset;
                    for (int j = 0; j < num2; j++)
                    {
                        if (j >= num)
                        {
                            buffer[num3++] = this.sourceBuffer[j];
                        }
                        else
                        {
                            buffer[num3++] += this.sourceBuffer[j];
                        }
                    }
                    num = Math.Max(num2, num);
                    if (num2 == 0)
                    {
                        this.sources.RemoveAt(i);
                    }
                }
            }
            if (this.ReadFully && num < count)
            {
                int k = offset + num;
                while (k < offset + count)
                {
                    buffer[k++] = 0f;
                }
                num = count;
            }
            return num;
        }

        private const int maxInputs = 1024;

        private List<ISampleProvider> sources;

        private WaveFormat waveFormat;

        private float[] sourceBuffer;
    }
}
