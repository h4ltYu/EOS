using System;
using System.IO;

namespace NAudio.Wave
{
    public class RawSourceWaveStream : WaveStream
    {
        public RawSourceWaveStream(Stream sourceStream, WaveFormat waveFormat)
        {
            this.sourceStream = sourceStream;
            this.waveFormat = waveFormat;
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        public override long Length
        {
            get
            {
                return this.sourceStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.sourceStream.Position;
            }
            set
            {
                this.sourceStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.sourceStream.Read(buffer, offset, count);
        }

        private Stream sourceStream;

        private WaveFormat waveFormat;
    }
}
