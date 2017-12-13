using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
    public class AudioFileReader : WaveStream, ISampleProvider
    {
        public AudioFileReader(string fileName)
        {
            this.lockObject = new object();
            this.fileName = fileName;
            this.CreateReaderStream(fileName);
            this.sourceBytesPerSample = this.readerStream.WaveFormat.BitsPerSample / 8 * this.readerStream.WaveFormat.Channels;
            this.sampleChannel = new SampleChannel(this.readerStream, false);
            this.destBytesPerSample = 4 * this.sampleChannel.WaveFormat.Channels;
            this.length = this.SourceToDest(this.readerStream.Length);
        }

        private void CreateReaderStream(string fileName)
        {
            if (fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                this.readerStream = new WaveFileReader(fileName);
                if (this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                {
                    this.readerStream = WaveFormatConversionStream.CreatePcmStream(this.readerStream);
                    this.readerStream = new BlockAlignReductionStream(this.readerStream);
                    return;
                }
            }
            else
            {
                if (fileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    this.readerStream = new Mp3FileReader(fileName);
                    return;
                }
                if (fileName.EndsWith(".aiff"))
                {
                    this.readerStream = new AiffFileReader(fileName);
                    return;
                }
                this.readerStream = new MediaFoundationReader(fileName);
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return this.sampleChannel.WaveFormat;
            }
        }

        public override long Length
        {
            get
            {
                return this.length;
            }
        }

        public override long Position
        {
            get
            {
                return this.SourceToDest(this.readerStream.Position);
            }
            set
            {
                lock (this.lockObject)
                {
                    this.readerStream.Position = this.DestToSource(value);
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int count2 = count / 4;
            int num = this.Read(waveBuffer.FloatBuffer, offset / 4, count2);
            return num * 4;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int result;
            lock (this.lockObject)
            {
                result = this.sampleChannel.Read(buffer, offset, count);
            }
            return result;
        }

        public float Volume
        {
            get
            {
                return this.sampleChannel.Volume;
            }
            set
            {
                this.sampleChannel.Volume = value;
            }
        }

        private long SourceToDest(long sourceBytes)
        {
            return (long)this.destBytesPerSample * (sourceBytes / (long)this.sourceBytesPerSample);
        }

        private long DestToSource(long destBytes)
        {
            return (long)this.sourceBytesPerSample * (destBytes / (long)this.destBytesPerSample);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.readerStream.Dispose();
                this.readerStream = null;
            }
            base.Dispose(disposing);
        }

        private string fileName;

        private WaveStream readerStream;

        private readonly SampleChannel sampleChannel;

        private readonly int destBytesPerSample;

        private readonly int sourceBytesPerSample;

        private readonly long length;

        private readonly object lockObject;
    }
}
