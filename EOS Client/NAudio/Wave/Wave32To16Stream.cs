using System;

namespace NAudio.Wave
{
    public class Wave32To16Stream : WaveStream
    {
        public Wave32To16Stream(WaveStream sourceStream)
        {
            if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Only 32 bit Floating point supported");
            }
            if (sourceStream.WaveFormat.BitsPerSample != 32)
            {
                throw new ArgumentException("Only 32 bit Floating point supported");
            }
            this.waveFormat = new WaveFormat(sourceStream.WaveFormat.SampleRate, 16, sourceStream.WaveFormat.Channels);
            this.volume = 1f;
            this.sourceStream = sourceStream;
            this.length = sourceStream.Length / 2L;
            this.position = sourceStream.Position / 2L;
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

        public override int BlockAlign
        {
            get
            {
                return this.sourceStream.BlockAlign / 2;
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
                return this.position;
            }
            set
            {
                lock (this.lockObject)
                {
                    value -= value % (long)this.BlockAlign;
                    this.sourceStream.Position = value * 2L;
                    this.position = value;
                }
            }
        }

        public override int Read(byte[] destBuffer, int offset, int numBytes)
        {
            int result;
            lock (this.lockObject)
            {
                byte[] array = new byte[numBytes * 2];
                int num = this.sourceStream.Read(array, 0, numBytes * 2);
                this.Convert32To16(destBuffer, offset, array, num);
                this.position += (long)(num / 2);
                result = num / 2;
            }
            return result;
        }

        private unsafe void Convert32To16(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
        {
            fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
            {
                short* ptr3 = (short*)ptr;
                float* ptr4 = (float*)ptr2;
                int num = bytesRead / 4;
                for (int i = 0; i < num; i++)
                {
                    float num2 = ptr4[i] * this.volume;
                    if (num2 > 1f)
                    {
                        ptr3[i] = short.MaxValue;
                        this.clip = true;
                    }
                    else if (num2 < -1f)
                    {
                        ptr3[i] = short.MinValue;
                        this.clip = true;
                    }
                    else
                    {
                        ptr3[i] = (short)(num2 * 32767f);
                    }
                }
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return this.waveFormat;
            }
        }

        public bool Clip
        {
            get
            {
                return this.clip;
            }
            set
            {
                this.clip = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.sourceStream != null)
            {
                this.sourceStream.Dispose();
                this.sourceStream = null;
            }
            base.Dispose(disposing);
        }

        private WaveStream sourceStream;

        private readonly WaveFormat waveFormat;

        private readonly long length;

        private long position;

        private bool clip;

        private float volume;

        private readonly object lockObject = new object();
    }
}
