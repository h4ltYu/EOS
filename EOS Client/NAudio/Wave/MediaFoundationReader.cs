using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.MediaFoundation;

namespace NAudio.Wave
{
    public class MediaFoundationReader : WaveStream
    {
        public MediaFoundationReader(string file) : this(file, new MediaFoundationReader.MediaFoundationReaderSettings())
        {
        }

        public MediaFoundationReader(string file, MediaFoundationReader.MediaFoundationReaderSettings settings)
        {
            MediaFoundationApi.Startup();
            this.settings = settings;
            this.file = file;
            IMFSourceReader imfsourceReader = this.CreateReader(settings);
            this.waveFormat = this.GetCurrentWaveFormat(imfsourceReader);
            imfsourceReader.SetStreamSelection(-3, true);
            this.length = this.GetLength(imfsourceReader);
            if (settings.SingleReaderObject)
            {
                this.pReader = imfsourceReader;
            }
        }

        private WaveFormat GetCurrentWaveFormat(IMFSourceReader reader)
        {
            IMFMediaType mediaType;
            reader.GetCurrentMediaType(-3, out mediaType);
            MediaType mediaType2 = new MediaType(mediaType);
            Guid majorType = mediaType2.MajorType;
            Guid subType = mediaType2.SubType;
            int channelCount = mediaType2.ChannelCount;
            int bitsPerSample = mediaType2.BitsPerSample;
            int sampleRate = mediaType2.SampleRate;
            if (!(subType == AudioSubtypes.MFAudioFormat_PCM))
            {
                return WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
            }
            return new WaveFormat(sampleRate, bitsPerSample, channelCount);
        }

        protected virtual IMFSourceReader CreateReader(MediaFoundationReader.MediaFoundationReaderSettings settings)
        {
            IMFSourceReader imfsourceReader;
            MediaFoundationInterop.MFCreateSourceReaderFromURL(this.file, null, out imfsourceReader);
            imfsourceReader.SetStreamSelection(-2, false);
            imfsourceReader.SetStreamSelection(-3, true);
            MediaType mediaType = new MediaType();
            mediaType.MajorType = MediaTypes.MFMediaType_Audio;
            mediaType.SubType = (settings.RequestFloatOutput ? AudioSubtypes.MFAudioFormat_Float : AudioSubtypes.MFAudioFormat_PCM);
            imfsourceReader.SetCurrentMediaType(-3, IntPtr.Zero, mediaType.MediaFoundationObject);
            return imfsourceReader;
        }

        private long GetLength(IMFSourceReader reader)
        {
            PropVariant propVariant;
            int presentationAttribute = reader.GetPresentationAttribute(-1, MediaFoundationAttributes.MF_PD_DURATION, out propVariant);
            if (presentationAttribute == -1072875802)
            {
                return 0L;
            }
            if (presentationAttribute != 0)
            {
                Marshal.ThrowExceptionForHR(presentationAttribute);
            }
            long result = (long)propVariant.Value * (long)this.waveFormat.AverageBytesPerSecond / 10000000L;
            propVariant.Clear();
            return result;
        }

        private void EnsureBuffer(int bytesRequired)
        {
            if (this.decoderOutputBuffer == null || this.decoderOutputBuffer.Length < bytesRequired)
            {
                this.decoderOutputBuffer = new byte[bytesRequired];
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.pReader == null)
            {
                this.pReader = this.CreateReader(this.settings);
            }
            if (this.repositionTo != -1L)
            {
                this.Reposition(this.repositionTo);
            }
            int i = 0;
            if (this.decoderOutputCount > 0)
            {
                i += this.ReadFromDecoderBuffer(buffer, offset, count - i);
            }
            while (i < count)
            {
                int num;
                MF_SOURCE_READER_FLAG mf_SOURCE_READER_FLAG;
                ulong num2;
                IMFSample imfsample;
                this.pReader.ReadSample(-3, 0, out num, out mf_SOURCE_READER_FLAG, out num2, out imfsample);
                if ((mf_SOURCE_READER_FLAG & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_ENDOFSTREAM) != MF_SOURCE_READER_FLAG.None)
                {
                    break;
                }
                if ((mf_SOURCE_READER_FLAG & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED) != MF_SOURCE_READER_FLAG.None)
                {
                    this.waveFormat = this.GetCurrentWaveFormat(this.pReader);
                    this.OnWaveFormatChanged();
                }
                else if (mf_SOURCE_READER_FLAG != MF_SOURCE_READER_FLAG.None)
                {
                    throw new InvalidOperationException(string.Format("MediaFoundationReadError {0}", mf_SOURCE_READER_FLAG));
                }
                IMFMediaBuffer imfmediaBuffer;
                imfsample.ConvertToContiguousBuffer(out imfmediaBuffer);
                IntPtr source;
                int num3;
                int bytesRequired;
                imfmediaBuffer.Lock(out source, out num3, out bytesRequired);
                this.EnsureBuffer(bytesRequired);
                Marshal.Copy(source, this.decoderOutputBuffer, 0, bytesRequired);
                this.decoderOutputOffset = 0;
                this.decoderOutputCount = bytesRequired;
                i += this.ReadFromDecoderBuffer(buffer, offset + i, count - i);
                imfmediaBuffer.Unlock();
                Marshal.ReleaseComObject(imfmediaBuffer);
                Marshal.ReleaseComObject(imfsample);
            }
            this.position += (long)i;
            return i;
        }

        private int ReadFromDecoderBuffer(byte[] buffer, int offset, int needed)
        {
            int num = Math.Min(needed, this.decoderOutputCount);
            Array.Copy(this.decoderOutputBuffer, this.decoderOutputOffset, buffer, offset, num);
            this.decoderOutputOffset += num;
            this.decoderOutputCount -= num;
            if (this.decoderOutputCount == 0)
            {
                this.decoderOutputOffset = 0;
            }
            return num;
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
                if (value < 0L)
                {
                    throw new ArgumentOutOfRangeException("value", "Position cannot be less than 0");
                }
                if (this.settings.RepositionInRead)
                {
                    this.repositionTo = value;
                    this.position = value;
                    return;
                }
                this.Reposition(value);
            }
        }

        private void Reposition(long desiredPosition)
        {
            long value = 10000000L * this.repositionTo / (long)this.waveFormat.AverageBytesPerSecond;
            PropVariant propVariant = PropVariant.FromLong(value);
            this.pReader.SetCurrentPosition(Guid.Empty, ref propVariant);
            this.decoderOutputCount = 0;
            this.decoderOutputOffset = 0;
            this.position = desiredPosition;
            this.repositionTo = -1L;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.pReader != null)
            {
                Marshal.ReleaseComObject(this.pReader);
                this.pReader = null;
            }
            base.Dispose(disposing);
        }

        public event EventHandler WaveFormatChanged;

        private void OnWaveFormatChanged()
        {
            EventHandler waveFormatChanged = this.WaveFormatChanged;
            if (waveFormatChanged != null)
            {
                waveFormatChanged(this, EventArgs.Empty);
            }
        }

        private WaveFormat waveFormat;

        private readonly long length;

        private readonly MediaFoundationReader.MediaFoundationReaderSettings settings;

        private readonly string file;

        private IMFSourceReader pReader;

        private long position;

        private byte[] decoderOutputBuffer;

        private int decoderOutputOffset;

        private int decoderOutputCount;

        private long repositionTo = -1L;

        public class MediaFoundationReaderSettings
        {
            public MediaFoundationReaderSettings()
            {
                this.RepositionInRead = true;
            }

            public bool RequestFloatOutput { get; set; }

            public bool SingleReaderObject { get; set; }

            public bool RepositionInRead { get; set; }
        }
    }
}
