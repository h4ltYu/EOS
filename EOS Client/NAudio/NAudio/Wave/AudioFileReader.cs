using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	/// <summary>
	/// AudioFileReader simplifies opening an audio file in NAudio
	/// Simply pass in the filename, and it will attempt to open the
	/// file and set up a conversion path that turns into PCM IEEE float.
	/// ACM codecs will be used for conversion.
	/// It provides a volume property and implements both WaveStream and
	/// ISampleProvider, making it possibly the only stage in your audio
	/// pipeline necessary for simple playback scenarios
	/// </summary>
	// Token: 0x020001E8 RID: 488
	public class AudioFileReader : WaveStream, ISampleProvider
	{
		/// <summary>
		/// Initializes a new instance of AudioFileReader
		/// </summary>
		/// <param name="fileName">The file to open</param>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001FB38 File Offset: 0x0001DD38
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

		/// <summary>
		/// Creates the reader stream, supporting all filetypes in the core NAudio library,
		/// and ensuring we are in PCM format
		/// </summary>
		/// <param name="fileName">File Name</param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
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

		/// <summary>
		/// WaveFormat of this stream
		/// </summary>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001FC81 File Offset: 0x0001DE81
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sampleChannel.WaveFormat;
			}
		}

		/// <summary>
		/// Length of this stream (in bytes)
		/// </summary>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>
		/// Position of this stream (in bytes)
		/// </summary>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001FC96 File Offset: 0x0001DE96
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0001FCAC File Offset: 0x0001DEAC
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

		/// <summary>
		/// Reads from this wave stream
		/// </summary>
		/// <param name="buffer">Audio buffer</param>
		/// <param name="offset">Offset into buffer</param>
		/// <param name="count">Number of bytes required</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000ACD RID: 2765 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int count2 = count / 4;
			int num = this.Read(waveBuffer.FloatBuffer, offset / 4, count2);
			return num * 4;
		}

		/// <summary>
		/// Reads audio from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public int Read(float[] buffer, int offset, int count)
		{
			int result;
			lock (this.lockObject)
			{
				result = this.sampleChannel.Read(buffer, offset, count);
			}
			return result;
		}

		/// <summary>
		/// Gets or Sets the Volume of this AudioFileReader. 1.0f is full volume
		/// </summary>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001FD64 File Offset: 0x0001DF64
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0001FD71 File Offset: 0x0001DF71
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

		/// <summary>
		/// Helper to convert source to dest bytes
		/// </summary>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001FD7F File Offset: 0x0001DF7F
		private long SourceToDest(long sourceBytes)
		{
			return (long)this.destBytesPerSample * (sourceBytes / (long)this.sourceBytesPerSample);
		}

		/// <summary>
		/// Helper to convert dest to source bytes
		/// </summary>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001FD92 File Offset: 0x0001DF92
		private long DestToSource(long destBytes)
		{
			return (long)this.sourceBytesPerSample * (destBytes / (long)this.destBytesPerSample);
		}

		/// <summary>
		/// Disposes this AudioFileReader
		/// </summary>
		/// <param name="disposing">True if called from Dispose</param>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001FDA5 File Offset: 0x0001DFA5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.readerStream.Dispose();
				this.readerStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B8A RID: 2954
		private string fileName;

		// Token: 0x04000B8B RID: 2955
		private WaveStream readerStream;

		// Token: 0x04000B8C RID: 2956
		private readonly SampleChannel sampleChannel;

		// Token: 0x04000B8D RID: 2957
		private readonly int destBytesPerSample;

		// Token: 0x04000B8E RID: 2958
		private readonly int sourceBytesPerSample;

		// Token: 0x04000B8F RID: 2959
		private readonly long length;

		// Token: 0x04000B90 RID: 2960
		private readonly object lockObject;
	}
}
