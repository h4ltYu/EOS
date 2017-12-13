using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents Channel for the WaveMixerStream
	/// 32 bit output and 16 bit input
	/// It's output is always stereo
	/// The input stream can be panned
	/// </summary>
	// Token: 0x020001FC RID: 508
	public class WaveChannel32 : WaveStream, ISampleNotifier
	{
		/// <summary>
		/// Creates a new WaveChannel32
		/// </summary>
		/// <param name="sourceStream">the source stream</param>
		/// <param name="volume">stream volume (1 is 0dB)</param>
		/// <param name="pan">pan control (-1 to 1)</param>
		// Token: 0x06000B7B RID: 2939 RVA: 0x000223D4 File Offset: 0x000205D4
		public WaveChannel32(WaveStream sourceStream, float volume, float pan)
		{
			this.PadWithZeroes = true;
			ISampleChunkConverter[] array = new ISampleChunkConverter[]
			{
				new Mono8SampleChunkConverter(),
				new Stereo8SampleChunkConverter(),
				new Mono16SampleChunkConverter(),
				new Stereo16SampleChunkConverter(),
				new Mono24SampleChunkConverter(),
				new Stereo24SampleChunkConverter(),
				new MonoFloatSampleChunkConverter(),
				new StereoFloatSampleChunkConverter()
			};
			foreach (ISampleChunkConverter sampleChunkConverter in array)
			{
				if (sampleChunkConverter.Supports(sourceStream.WaveFormat))
				{
					this.sampleProvider = sampleChunkConverter;
					break;
				}
			}
			if (this.sampleProvider == null)
			{
				throw new ArgumentException("Unsupported sourceStream format");
			}
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceStream.WaveFormat.SampleRate, 2);
			this.destBytesPerSample = 8;
			this.sourceStream = sourceStream;
			this.volume = volume;
			this.pan = pan;
			this.sourceBytesPerSample = sourceStream.WaveFormat.Channels * sourceStream.WaveFormat.BitsPerSample / 8;
			this.length = this.SourceToDest(sourceStream.Length);
			this.position = 0L;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002250A File Offset: 0x0002070A
		private long SourceToDest(long sourceBytes)
		{
			return sourceBytes / (long)this.sourceBytesPerSample * (long)this.destBytesPerSample;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002251D File Offset: 0x0002071D
		private long DestToSource(long destBytes)
		{
			return destBytes / (long)this.destBytesPerSample * (long)this.sourceBytesPerSample;
		}

		/// <summary>
		/// Creates a WaveChannel32 with default settings
		/// </summary>
		/// <param name="sourceStream">The source stream</param>
		// Token: 0x06000B7E RID: 2942 RVA: 0x00022530 File Offset: 0x00020730
		public WaveChannel32(WaveStream sourceStream) : this(sourceStream, 1f, 0f)
		{
		}

		/// <summary>
		/// Gets the block alignment for this WaveStream
		/// </summary>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00022543 File Offset: 0x00020743
		public override int BlockAlign
		{
			get
			{
				return (int)this.SourceToDest((long)this.sourceStream.BlockAlign);
			}
		}

		/// <summary>
		/// Returns the stream length
		/// </summary>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00022558 File Offset: 0x00020758
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
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00022560 File Offset: 0x00020760
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x00022568 File Offset: 0x00020768
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
					if (value < 0L)
					{
						this.sourceStream.Position = 0L;
					}
					else
					{
						this.sourceStream.Position = this.DestToSource(value);
					}
					this.position = this.SourceToDest(this.sourceStream.Position);
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
		// Token: 0x06000B83 RID: 2947 RVA: 0x000225E8 File Offset: 0x000207E8
		public override int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int result;
			lock (this.lockObject)
			{
				int num = 0;
				WaveBuffer waveBuffer = new WaveBuffer(destBuffer);
				if (this.position < 0L)
				{
					num = (int)Math.Min((long)numBytes, -this.position);
					for (int i = 0; i < num; i++)
					{
						destBuffer[i + offset] = 0;
					}
				}
				if (num < numBytes)
				{
					this.sampleProvider.LoadNextChunk(this.sourceStream, (numBytes - num) / 8);
					int num2 = offset / 4 + num / 4;
					float num3;
					float num4;
					while (this.sampleProvider.GetNextSample(out num3, out num4) && num < numBytes)
					{
						num3 = ((this.pan <= 0f) ? num3 : (num3 * (1f - this.pan) / 2f));
						num4 = ((this.pan >= 0f) ? num4 : (num4 * (this.pan + 1f) / 2f));
						num3 *= this.volume;
						num4 *= this.volume;
						waveBuffer.FloatBuffer[num2++] = num3;
						waveBuffer.FloatBuffer[num2++] = num4;
						num += 8;
						if (this.Sample != null)
						{
							this.RaiseSample(num3, num4);
						}
					}
				}
				if (this.PadWithZeroes && num < numBytes)
				{
					Array.Clear(destBuffer, offset + num, numBytes - num);
					num = numBytes;
				}
				this.position += (long)num;
				result = num;
			}
			return result;
		}

		/// <summary>
		/// If true, Read always returns the number of bytes requested
		/// </summary>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00022770 File Offset: 0x00020970
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x00022778 File Offset: 0x00020978
		public bool PadWithZeroes { get; set; }

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00022781 File Offset: 0x00020981
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Volume of this channel. 1.0 = full scale
		/// </summary>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00022789 File Offset: 0x00020989
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00022793 File Offset: 0x00020993
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

		/// <summary>
		/// Pan of this channel (from -1 to 1)
		/// </summary>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002279E File Offset: 0x0002099E
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x000227A8 File Offset: 0x000209A8
		public float Pan
		{
			get
			{
				return this.pan;
			}
			set
			{
				this.pan = value;
			}
		}

		/// <summary>
		/// Determines whether this channel has any data to play
		/// to allow optimisation to not read, but bump position forward
		/// </summary>
		// Token: 0x06000B8B RID: 2955 RVA: 0x000227B4 File Offset: 0x000209B4
		public override bool HasData(int count)
		{
			bool flag = this.sourceStream.HasData(count);
			return flag && this.position + (long)count >= 0L && this.position < this.length && this.volume != 0f;
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000B8C RID: 2956 RVA: 0x00022804 File Offset: 0x00020A04
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Sample
		/// </summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B8D RID: 2957 RVA: 0x0002282C File Offset: 0x00020A2C
		// (remove) Token: 0x06000B8E RID: 2958 RVA: 0x00022864 File Offset: 0x00020A64
		public event EventHandler<SampleEventArgs> Sample;

		/// <summary>
		/// Raise the sample event (no check for null because it has already been done)
		/// </summary>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00022899 File Offset: 0x00020A99
		private void RaiseSample(float left, float right)
		{
			this.sampleEventArgs.Left = left;
			this.sampleEventArgs.Right = right;
			this.Sample(this, this.sampleEventArgs);
		}

		// Token: 0x04000BE3 RID: 3043
		private WaveStream sourceStream;

		// Token: 0x04000BE4 RID: 3044
		private readonly WaveFormat waveFormat;

		// Token: 0x04000BE5 RID: 3045
		private readonly long length;

		// Token: 0x04000BE6 RID: 3046
		private readonly int destBytesPerSample;

		// Token: 0x04000BE7 RID: 3047
		private readonly int sourceBytesPerSample;

		// Token: 0x04000BE8 RID: 3048
		private volatile float volume;

		// Token: 0x04000BE9 RID: 3049
		private volatile float pan;

		// Token: 0x04000BEA RID: 3050
		private long position;

		// Token: 0x04000BEB RID: 3051
		private readonly ISampleChunkConverter sampleProvider;

		// Token: 0x04000BEC RID: 3052
		private readonly object lockObject = new object();

		// Token: 0x04000BEE RID: 3054
		private SampleEventArgs sampleEventArgs = new SampleEventArgs(0f, 0f);
	}
}
