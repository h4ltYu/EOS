using System;
using System.Collections.Generic;
using System.IO;
using NAudio.FileFormats.Wav;

namespace NAudio.Wave
{
    public class WaveFileReader : WaveStream
    {
        public WaveFileReader(string waveFile) : this(File.OpenRead(waveFile))
        {
            this.ownInput = true;
        }

        public WaveFileReader(Stream inputStream)
        {
            this.waveStream = inputStream;
            WaveFileChunkReader waveFileChunkReader = new WaveFileChunkReader();
            waveFileChunkReader.ReadWaveHeader(inputStream);
            this.waveFormat = waveFileChunkReader.WaveFormat;
            this.dataPosition = waveFileChunkReader.DataChunkPosition;
            this.dataChunkLength = waveFileChunkReader.DataChunkLength;
            this.chunks = waveFileChunkReader.RiffChunks;
            this.Position = 0L;
        }

        public List<RiffChunk> ExtraChunks
        {
            get
            {
                return this.chunks;
            }
        }

        public byte[] GetChunkData(RiffChunk chunk)
        {
            long position = this.waveStream.Position;
            this.waveStream.Position = chunk.StreamPosition;
            byte[] array = new byte[chunk.Length];
            this.waveStream.Read(array, 0, array.Length);
            this.waveStream.Position = position;
            return array;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.waveStream != null)
            {
                if (this.ownInput)
                {
                    this.waveStream.Close();
                }
                this.waveStream = null;
            }
            base.Dispose(disposing);
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
                return this.dataChunkLength;
            }
        }

        public long SampleCount
        {
            get
            {
                if (this.waveFormat.Encoding == WaveFormatEncoding.Pcm || this.waveFormat.Encoding == WaveFormatEncoding.Extensible || this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
                {
                    return this.dataChunkLength / (long)this.BlockAlign;
                }
                throw new InvalidOperationException("Sample count is calculated only for the standard encodings");
            }
        }

        public override long Position
        {
            get
            {
                return this.waveStream.Position - this.dataPosition;
            }
            set
            {
                lock (this.lockObject)
                {
                    value = Math.Min(value, this.Length);
                    value -= value % (long)this.waveFormat.BlockAlign;
                    this.waveStream.Position = value + this.dataPosition;
                }
            }
        }

        public override int Read(byte[] array, int offset, int count)
        {
            if (count % this.waveFormat.BlockAlign != 0)
            {
                throw new ArgumentException(string.Format("Must read complete blocks: requested {0}, block align is {1}", count, this.WaveFormat.BlockAlign));
            }
            int result;
            lock (this.lockObject)
            {
                if (this.Position + (long)count > this.dataChunkLength)
                {
                    count = (int)(this.dataChunkLength - this.Position);
                }
                result = this.waveStream.Read(array, offset, count);
            }
            return result;
        }

        public float[] ReadNextSampleFrame()
        {
            WaveFormatEncoding encoding = this.waveFormat.Encoding;
            switch (encoding)
            {
                case WaveFormatEncoding.Pcm:
                case WaveFormatEncoding.IeeeFloat:
                    goto IL_36;
                case WaveFormatEncoding.Adpcm:
                    break;
                default:
                    if (encoding == WaveFormatEncoding.Extensible)
                    {
                        goto IL_36;
                    }
                    break;
            }
            throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
            IL_36:
            float[] array = new float[this.waveFormat.Channels];
            int num = this.waveFormat.Channels * (this.waveFormat.BitsPerSample / 8);
            byte[] array2 = new byte[num];
            int num2 = this.Read(array2, 0, num);
            if (num2 == 0)
            {
                return null;
            }
            if (num2 < num)
            {
                throw new InvalidDataException("Unexpected end of file");
            }
            int num3 = 0;
            for (int i = 0; i < this.waveFormat.Channels; i++)
            {
                if (this.waveFormat.BitsPerSample == 16)
                {
                    array[i] = (float)BitConverter.ToInt16(array2, num3) / 32768f;
                    num3 += 2;
                }
                else if (this.waveFormat.BitsPerSample == 24)
                {
                    array[i] = (float)((int)((sbyte)array2[num3 + 2]) << 16 | (int)array2[num3 + 1] << 8 | (int)array2[num3]) / 8388608f;
                    num3 += 3;
                }
                else if (this.waveFormat.BitsPerSample == 32 && this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
                {
                    array[i] = BitConverter.ToSingle(array2, num3);
                    num3 += 4;
                }
                else
                {
                    if (this.waveFormat.BitsPerSample != 32)
                    {
                        throw new InvalidOperationException("Unsupported bit depth");
                    }
                    array[i] = (float)BitConverter.ToInt32(array2, num3) / 2.14748365E+09f;
                    num3 += 4;
                }
            }
            return array;
        }

        [Obsolete("Use ReadNextSampleFrame instead (this version does not support stereo properly)")]
        public bool TryReadFloat(out float sampleValue)
        {
            float[] array = this.ReadNextSampleFrame();
            sampleValue = ((array != null) ? array[0] : 0f);
            return array != null;
        }

        private readonly WaveFormat waveFormat;

        private readonly bool ownInput;

        private readonly long dataPosition;

        private readonly long dataChunkLength;

        private readonly List<RiffChunk> chunks = new List<RiffChunk>();

        private readonly object lockObject = new object();

        private Stream waveStream;
    }
}
