using System;
using NAudio.Wave.Compression;

namespace NAudio.Wave
{
    public class WaveFormatConversionStream : WaveStream
    {
        public static WaveStream CreatePcmStream(WaveStream sourceStream)
        {
            if (sourceStream.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                return sourceStream;
            }
            WaveFormat waveFormat = AcmStream.SuggestPcmFormat(sourceStream.WaveFormat);
            if (waveFormat.SampleRate < 8000)
            {
                if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.G723)
                {
                    throw new InvalidOperationException("Invalid suggested output format, please explicitly provide a target format");
                }
                waveFormat = new WaveFormat(8000, 16, 1);
            }
            return new WaveFormatConversionStream(waveFormat, sourceStream);
        }

        public WaveFormatConversionStream(WaveFormat targetFormat, WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.targetFormat = targetFormat;
            this.conversionStream = new AcmStream(sourceStream.WaveFormat, targetFormat);
            this.length = this.EstimateSourceToDest((long)((int)sourceStream.Length));
            this.position = 0L;
            this.preferredSourceReadSize = Math.Min(sourceStream.WaveFormat.AverageBytesPerSecond, this.conversionStream.SourceBuffer.Length);
            this.preferredSourceReadSize -= this.preferredSourceReadSize % sourceStream.WaveFormat.BlockAlign;
        }

        [Obsolete("can be unreliable, use of this method not encouraged")]
        public int SourceToDest(int source)
        {
            return (int)this.EstimateSourceToDest((long)source);
        }

        private long EstimateSourceToDest(long source)
        {
            long num = source * (long)this.targetFormat.AverageBytesPerSecond / (long)this.sourceStream.WaveFormat.AverageBytesPerSecond;
            return num - num % (long)this.targetFormat.BlockAlign;
        }

        private long EstimateDestToSource(long dest)
        {
            long num = dest * (long)this.sourceStream.WaveFormat.AverageBytesPerSecond / (long)this.targetFormat.AverageBytesPerSecond;
            num -= num % (long)this.sourceStream.WaveFormat.BlockAlign;
            return (long)((int)num);
        }

        [Obsolete("can be unreliable, use of this method not encouraged")]
        public int DestToSource(int dest)
        {
            return (int)this.EstimateDestToSource((long)dest);
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
                return this.position;
            }
            set
            {
                value -= value % (long)this.BlockAlign;
                long num = this.EstimateDestToSource(value);
                this.sourceStream.Position = num;
                this.position = this.EstimateSourceToDest(this.sourceStream.Position);
                this.leftoverDestBytes = 0;
                this.leftoverDestOffset = 0;
                this.conversionStream.Reposition();
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return this.targetFormat;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int i = 0;
            if (count % this.BlockAlign != 0)
            {
                count -= count % this.BlockAlign;
            }
            while (i < count)
            {
                int num = Math.Min(count - i, this.leftoverDestBytes);
                if (num > 0)
                {
                    Array.Copy(this.conversionStream.DestBuffer, this.leftoverDestOffset, buffer, offset + i, num);
                    this.leftoverDestOffset += num;
                    this.leftoverDestBytes -= num;
                    i += num;
                }
                if (i >= count)
                {
                    break;
                }
                int num2 = this.leftoverSourceBytes;
                int num3 = this.sourceStream.Read(this.conversionStream.SourceBuffer, 0, this.preferredSourceReadSize);
                if (num3 == 0)
                {
                    break;
                }
                int num5;
                int num4 = this.conversionStream.Convert(num3, out num5);
                if (num5 == 0)
                {
                    break;
                }
                if (num5 < num3)
                {
                    this.sourceStream.Position -= (long)(num3 - num5);
                }
                if (num4 <= 0)
                {
                    break;
                }
                int val = count - i;
                int num6 = Math.Min(num4, val);
                if (num6 < num4)
                {
                    this.leftoverDestBytes = num4 - num6;
                    this.leftoverDestOffset = num6;
                }
                Array.Copy(this.conversionStream.DestBuffer, 0, buffer, i + offset, num6);
                i += num6;
            }
            this.position += (long)i;
            return i;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.conversionStream != null)
                {
                    this.conversionStream.Dispose();
                    this.conversionStream = null;
                }
                if (this.sourceStream != null)
                {
                    this.sourceStream.Dispose();
                    this.sourceStream = null;
                }
            }
            base.Dispose(disposing);
        }

        private AcmStream conversionStream;

        private WaveStream sourceStream;

        private WaveFormat targetFormat;

        private long length;

        private long position;

        private int preferredSourceReadSize;

        private int leftoverDestBytes;

        private int leftoverDestOffset;

        private int leftoverSourceBytes;
    }
}
