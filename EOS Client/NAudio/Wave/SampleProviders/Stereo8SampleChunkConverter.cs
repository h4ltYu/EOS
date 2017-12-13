using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
    internal class Stereo8SampleChunkConverter : ISampleChunkConverter
    {
        public bool Supports(WaveFormat waveFormat)
        {
            return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 2;
        }

        public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
        {
            int num = samplePairsRequired * 2;
            this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
            this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
            this.offset = 0;
        }

        public bool GetNextSample(out float sampleLeft, out float sampleRight)
        {
            if (this.offset < this.sourceBytes)
            {
                sampleLeft = (float)this.sourceBuffer[this.offset++] / 256f;
                sampleRight = (float)this.sourceBuffer[this.offset++] / 256f;
                return true;
            }
            sampleLeft = 0f;
            sampleRight = 0f;
            return false;
        }

        private int offset;

        private byte[] sourceBuffer;

        private int sourceBytes;
    }
}
