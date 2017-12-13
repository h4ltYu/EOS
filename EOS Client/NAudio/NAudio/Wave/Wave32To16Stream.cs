using System;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveStream that converts 32 bit audio back down to 16 bit, clipping if necessary
	/// </summary>
	// Token: 0x020001FB RID: 507
	public class Wave32To16Stream : WaveStream
	{
		/// <summary>
		/// Creates a new Wave32To16Stream
		/// </summary>
		/// <param name="sourceStream">the source stream</param>
		// Token: 0x06000B6E RID: 2926 RVA: 0x00022158 File Offset: 0x00020358
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

		/// <summary>
		/// Sets the volume for this stream. 1.0f is full scale
		/// </summary>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000221FC File Offset: 0x000203FC
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x00022204 File Offset: 0x00020404
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
		/// <see cref="P:NAudio.Wave.WaveStream.BlockAlign" />
		/// </summary>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002220D File Offset: 0x0002040D
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign / 2;
			}
		}

		/// <summary>
		/// Returns the stream length
		/// </summary>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002221C File Offset: 0x0002041C
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
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00022224 File Offset: 0x00020424
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0002222C File Offset: 0x0002042C
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

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="destBuffer">Destination buffer</param>
		/// <param name="offset">Offset into destination buffer</param>
		/// <param name="numBytes"></param>
		/// <returns>Number of bytes read.</returns>
		// Token: 0x06000B75 RID: 2933 RVA: 0x00022284 File Offset: 0x00020484
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

		/// <summary>
		/// Conversion to 16 bit and clipping
		/// </summary>
		// Token: 0x06000B76 RID: 2934 RVA: 0x000222F4 File Offset: 0x000204F4
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

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00022394 File Offset: 0x00020594
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Clip indicator. Can be reset.
		/// </summary>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002239C File Offset: 0x0002059C
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x000223A4 File Offset: 0x000205A4
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

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000B7A RID: 2938 RVA: 0x000223AD File Offset: 0x000205AD
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BDC RID: 3036
		private WaveStream sourceStream;

		// Token: 0x04000BDD RID: 3037
		private readonly WaveFormat waveFormat;

		// Token: 0x04000BDE RID: 3038
		private readonly long length;

		// Token: 0x04000BDF RID: 3039
		private long position;

		// Token: 0x04000BE0 RID: 3040
		private bool clip;

		// Token: 0x04000BE1 RID: 3041
		private float volume;

		// Token: 0x04000BE2 RID: 3042
		private readonly object lockObject = new object();
	}
}
