using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.MediaFoundation;

namespace NAudio.Wave
{
	/// <summary>
	/// Class for reading any file that Media Foundation can play
	/// Will only work in Windows Vista and above
	/// Automatically converts to PCM
	/// If it is a video file with multiple audio streams, it will pick out the first audio stream
	/// </summary>
	// Token: 0x020001EF RID: 495
	public class MediaFoundationReader : WaveStream
	{
		/// <summary>
		/// Creates a new MediaFoundationReader based on the supplied file
		/// </summary>
		/// <param name="file">Filename (can also be a URL  e.g. http:// mms:// file://)</param>
		// Token: 0x06000B00 RID: 2816 RVA: 0x000209F3 File Offset: 0x0001EBF3
		public MediaFoundationReader(string file) : this(file, new MediaFoundationReader.MediaFoundationReaderSettings())
		{
		}

		/// <summary>
		/// Creates a new MediaFoundationReader based on the supplied file
		/// </summary>
		/// <param name="file">Filename</param>
		/// <param name="settings">Advanced settings</param>
		// Token: 0x06000B01 RID: 2817 RVA: 0x00020A04 File Offset: 0x0001EC04
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

		// Token: 0x06000B02 RID: 2818 RVA: 0x00020A6C File Offset: 0x0001EC6C
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

		/// <summary>
		/// Creates the reader (overridable by )
		/// </summary>
		// Token: 0x06000B03 RID: 2819 RVA: 0x00020AD0 File Offset: 0x0001ECD0
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

		// Token: 0x06000B04 RID: 2820 RVA: 0x00020B3C File Offset: 0x0001ED3C
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

		// Token: 0x06000B05 RID: 2821 RVA: 0x00020B95 File Offset: 0x0001ED95
		private void EnsureBuffer(int bytesRequired)
		{
			if (this.decoderOutputBuffer == null || this.decoderOutputBuffer.Length < bytesRequired)
			{
				this.decoderOutputBuffer = new byte[bytesRequired];
			}
		}

		/// <summary>
		/// Reads from this wave stream
		/// </summary>
		/// <param name="buffer">Buffer to read into</param>
		/// <param name="offset">Offset in buffer</param>
		/// <param name="count">Bytes required</param>
		/// <returns>Number of bytes read; 0 indicates end of stream</returns>
		// Token: 0x06000B06 RID: 2822 RVA: 0x00020BB8 File Offset: 0x0001EDB8
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

		// Token: 0x06000B07 RID: 2823 RVA: 0x00020CE8 File Offset: 0x0001EEE8
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

		/// <summary>
		/// WaveFormat of this stream (n.b. this is after converting to PCM)
		/// </summary>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00020D42 File Offset: 0x0001EF42
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// The bytesRequired of this stream in bytes (n.b may not be accurate)
		/// </summary>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00020D4A File Offset: 0x0001EF4A
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>
		/// Current position within this stream
		/// </summary>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00020D52 File Offset: 0x0001EF52
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00020D5A File Offset: 0x0001EF5A
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

		// Token: 0x06000B0C RID: 2828 RVA: 0x00020D94 File Offset: 0x0001EF94
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

		/// <summary>
		/// Cleans up after finishing with this reader
		/// </summary>
		/// <param name="disposing">true if called from Dispose</param>
		// Token: 0x06000B0D RID: 2829 RVA: 0x00020DF2 File Offset: 0x0001EFF2
		protected override void Dispose(bool disposing)
		{
			if (this.pReader != null)
			{
				Marshal.ReleaseComObject(this.pReader);
				this.pReader = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// WaveFormat has changed
		/// </summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000B0E RID: 2830 RVA: 0x00020E18 File Offset: 0x0001F018
		// (remove) Token: 0x06000B0F RID: 2831 RVA: 0x00020E50 File Offset: 0x0001F050
		public event EventHandler WaveFormatChanged;

		// Token: 0x06000B10 RID: 2832 RVA: 0x00020E88 File Offset: 0x0001F088
		private void OnWaveFormatChanged()
		{
			EventHandler waveFormatChanged = this.WaveFormatChanged;
			if (waveFormatChanged != null)
			{
				waveFormatChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000BA4 RID: 2980
		private WaveFormat waveFormat;

		// Token: 0x04000BA5 RID: 2981
		private readonly long length;

		// Token: 0x04000BA6 RID: 2982
		private readonly MediaFoundationReader.MediaFoundationReaderSettings settings;

		// Token: 0x04000BA7 RID: 2983
		private readonly string file;

		// Token: 0x04000BA8 RID: 2984
		private IMFSourceReader pReader;

		// Token: 0x04000BA9 RID: 2985
		private long position;

		// Token: 0x04000BAA RID: 2986
		private byte[] decoderOutputBuffer;

		// Token: 0x04000BAB RID: 2987
		private int decoderOutputOffset;

		// Token: 0x04000BAC RID: 2988
		private int decoderOutputCount;

		// Token: 0x04000BAD RID: 2989
		private long repositionTo = -1L;

		/// <summary>
		/// Allows customisation of this reader class
		/// </summary>
		// Token: 0x020001F0 RID: 496
		public class MediaFoundationReaderSettings
		{
			/// <summary>
			/// Sets up the default settings for MediaFoundationReader
			/// </summary>
			// Token: 0x06000B11 RID: 2833 RVA: 0x00020EAB File Offset: 0x0001F0AB
			public MediaFoundationReaderSettings()
			{
				this.RepositionInRead = true;
			}

			/// <summary>
			/// Allows us to request IEEE float output (n.b. no guarantee this will be accepted)
			/// </summary>
			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00020EBA File Offset: 0x0001F0BA
			// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00020EC2 File Offset: 0x0001F0C2
			public bool RequestFloatOutput { get; set; }

			/// <summary>
			/// If true, the reader object created in the constructor is used in Read
			/// Should only be set to true if you are working entirely on an STA thread, or 
			/// entirely with MTA threads.
			/// </summary>
			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00020ECB File Offset: 0x0001F0CB
			// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00020ED3 File Offset: 0x0001F0D3
			public bool SingleReaderObject { get; set; }

			/// <summary>
			/// If true, the reposition does not happen immediately, but waits until the
			/// next call to read to be processed.
			/// </summary>
			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00020EDC File Offset: 0x0001F0DC
			// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00020EE4 File Offset: 0x0001F0E4
			public bool RepositionInRead { get; set; }
		}
	}
}
