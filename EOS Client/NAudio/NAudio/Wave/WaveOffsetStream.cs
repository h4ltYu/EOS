using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Simply shifts the input stream in time, optionally
	/// clipping its start and end.
	/// (n.b. may include looping in the future)
	/// </summary>
	// Token: 0x02000201 RID: 513
	public class WaveOffsetStream : WaveStream
	{
		/// <summary>
		/// Creates a new WaveOffsetStream
		/// </summary>
		/// <param name="sourceStream">the source stream</param>
		/// <param name="startTime">the time at which we should start reading from the source stream</param>
		/// <param name="sourceOffset">amount to trim off the front of the source stream</param>
		/// <param name="sourceLength">length of time to play from source stream</param>
		// Token: 0x06000BBD RID: 3005 RVA: 0x0002345C File Offset: 0x0002165C
		public WaveOffsetStream(WaveStream sourceStream, TimeSpan startTime, TimeSpan sourceOffset, TimeSpan sourceLength)
		{
			if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Only PCM supported");
			}
			this.sourceStream = sourceStream;
			this.position = 0L;
			this.bytesPerSample = sourceStream.WaveFormat.BitsPerSample / 8 * sourceStream.WaveFormat.Channels;
			this.StartTime = startTime;
			this.SourceOffset = sourceOffset;
			this.SourceLength = sourceLength;
		}

		/// <summary>
		/// Creates a WaveOffsetStream with default settings (no offset or pre-delay,
		/// and whole length of source stream)
		/// </summary>
		/// <param name="sourceStream">The source stream</param>
		// Token: 0x06000BBE RID: 3006 RVA: 0x000234D7 File Offset: 0x000216D7
		public WaveOffsetStream(WaveStream sourceStream) : this(sourceStream, TimeSpan.Zero, TimeSpan.Zero, sourceStream.TotalTime)
		{
		}

		/// <summary>
		/// The length of time before which no audio will be played
		/// </summary>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000234F0 File Offset: 0x000216F0
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x000234F8 File Offset: 0x000216F8
		public TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				lock (this.lockObject)
				{
					this.startTime = value;
					this.audioStartPosition = (long)(this.startTime.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.length = this.audioStartPosition + this.sourceLengthBytes;
					this.Position = this.Position;
				}
			}
		}

		/// <summary>
		/// An offset into the source stream from which to start playing
		/// </summary>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00023580 File Offset: 0x00021780
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00023588 File Offset: 0x00021788
		public TimeSpan SourceOffset
		{
			get
			{
				return this.sourceOffset;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceOffset = value;
					this.sourceOffsetBytes = (long)(this.sourceOffset.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.Position = this.Position;
				}
			}
		}

		/// <summary>
		/// Length of time to read from the source stream
		/// </summary>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000235FC File Offset: 0x000217FC
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00023604 File Offset: 0x00021804
		public TimeSpan SourceLength
		{
			get
			{
				return this.sourceLength;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceLength = value;
					this.sourceLengthBytes = (long)(this.sourceLength.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.length = this.audioStartPosition + this.sourceLengthBytes;
					this.Position = this.Position;
				}
			}
		}

		/// <summary>
		/// Gets the block alignment for this WaveStream
		/// </summary>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002368C File Offset: 0x0002188C
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign;
			}
		}

		/// <summary>
		/// Returns the stream length
		/// </summary>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00023699 File Offset: 0x00021899
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>
		/// Gets or sets the current position in the stream
		/// </summary>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000236A1 File Offset: 0x000218A1
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x000236AC File Offset: 0x000218AC
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
					if (value < this.audioStartPosition)
					{
						this.sourceStream.Position = this.sourceOffsetBytes;
					}
					else
					{
						this.sourceStream.Position = this.sourceOffsetBytes + (value - this.audioStartPosition);
					}
					this.position = value;
				}
			}
		}

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="destBuffer">The destination buffer</param>
		/// <param name="offset">Offset into the destination buffer</param>
		/// <param name="numBytes">Number of bytes read</param>
		/// <returns>Number of bytes read.</returns>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002372C File Offset: 0x0002192C
		public override int Read(byte[] destBuffer, int offset, int numBytes)
		{
			lock (this.lockObject)
			{
				int num = 0;
				if (this.position < this.audioStartPosition)
				{
					num = (int)Math.Min((long)numBytes, this.audioStartPosition - this.position);
					for (int i = 0; i < num; i++)
					{
						destBuffer[i + offset] = 0;
					}
				}
				if (num < numBytes)
				{
					int count = (int)Math.Min((long)(numBytes - num), this.sourceLengthBytes + this.sourceOffsetBytes - this.sourceStream.Position);
					int num2 = this.sourceStream.Read(destBuffer, num + offset, count);
					num += num2;
				}
				for (int j = num; j < numBytes; j++)
				{
					destBuffer[offset + j] = 0;
				}
				this.position += (long)numBytes;
			}
			return numBytes;
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00023804 File Offset: 0x00021A04
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		/// <summary>
		/// Determines whether this channel has any data to play
		/// to allow optimisation to not read, but bump position forward
		/// </summary>
		// Token: 0x06000BCB RID: 3019 RVA: 0x00023811 File Offset: 0x00021A11
		public override bool HasData(int count)
		{
			return this.position + (long)count >= this.audioStartPosition && this.position < this.length && this.sourceStream.HasData(count);
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000BCC RID: 3020 RVA: 0x00023842 File Offset: 0x00021A42
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000C0A RID: 3082
		private WaveStream sourceStream;

		// Token: 0x04000C0B RID: 3083
		private long audioStartPosition;

		// Token: 0x04000C0C RID: 3084
		private long sourceOffsetBytes;

		// Token: 0x04000C0D RID: 3085
		private long sourceLengthBytes;

		// Token: 0x04000C0E RID: 3086
		private long length;

		// Token: 0x04000C0F RID: 3087
		private readonly int bytesPerSample;

		// Token: 0x04000C10 RID: 3088
		private long position;

		// Token: 0x04000C11 RID: 3089
		private TimeSpan startTime;

		// Token: 0x04000C12 RID: 3090
		private TimeSpan sourceOffset;

		// Token: 0x04000C13 RID: 3091
		private TimeSpan sourceLength;

		// Token: 0x04000C14 RID: 3092
		private readonly object lockObject = new object();
	}
}
