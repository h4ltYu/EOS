using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Helper stream that lets us read from compressed audio files with large block alignment
	/// as though we could read any amount and reposition anywhere
	/// </summary>
	// Token: 0x020001E9 RID: 489
	public class BlockAlignReductionStream : WaveStream
	{
		/// <summary>
		/// Creates a new BlockAlignReductionStream
		/// </summary>
		/// <param name="sourceStream">the input stream</param>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001FDC3 File Offset: 0x0001DFC3
		public BlockAlignReductionStream(WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.circularBuffer = new CircularBuffer(sourceStream.WaveFormat.AverageBytesPerSecond * 4);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001FDF5 File Offset: 0x0001DFF5
		private byte[] GetSourceBuffer(int size)
		{
			if (this.sourceBuffer == null || this.sourceBuffer.Length < size)
			{
				this.sourceBuffer = new byte[size * 2];
			}
			return this.sourceBuffer;
		}

		/// <summary>
		/// Block alignment of this stream
		/// </summary>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001FE1E File Offset: 0x0001E01E
		public override int BlockAlign
		{
			get
			{
				return this.WaveFormat.BitsPerSample / 8 * this.WaveFormat.Channels;
			}
		}

		/// <summary>
		/// Wave Format
		/// </summary>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001FE39 File Offset: 0x0001E039
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		/// <summary>
		/// Length of this Stream
		/// </summary>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001FE46 File Offset: 0x0001E046
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		/// <summary>
		/// Current position within stream
		/// </summary>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001FE53 File Offset: 0x0001E053
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0001FE5C File Offset: 0x0001E05C
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
					if (this.position != value)
					{
						if (this.position % (long)this.BlockAlign != 0L)
						{
							throw new ArgumentException("Position must be block aligned");
						}
						long num = value - value % (long)this.sourceStream.BlockAlign;
						if (this.sourceStream.Position != num)
						{
							this.sourceStream.Position = num;
							this.circularBuffer.Reset();
							this.bufferStartPosition = this.sourceStream.Position;
						}
						this.position = value;
					}
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001FF04 File Offset: 0x0001E104
		private long BufferEndPosition
		{
			get
			{
				return this.bufferStartPosition + (long)this.circularBuffer.Count;
			}
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000ADC RID: 2780 RVA: 0x0001FF19 File Offset: 0x0001E119
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
		/// Reads data from this stream
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0001FF40 File Offset: 0x0001E140
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			lock (this.lockObject)
			{
				while (this.BufferEndPosition < this.position + (long)count)
				{
					int num = count;
					if (num % this.sourceStream.BlockAlign != 0)
					{
						num = count + this.sourceStream.BlockAlign - count % this.sourceStream.BlockAlign;
					}
					int num2 = this.sourceStream.Read(this.GetSourceBuffer(num), 0, num);
					this.circularBuffer.Write(this.GetSourceBuffer(num), 0, num2);
					if (num2 == 0)
					{
						break;
					}
				}
				if (this.bufferStartPosition < this.position)
				{
					this.circularBuffer.Advance((int)(this.position - this.bufferStartPosition));
					this.bufferStartPosition = this.position;
				}
				int num3 = this.circularBuffer.Read(buffer, offset, count);
				this.position += (long)num3;
				this.bufferStartPosition = this.position;
				result = num3;
			}
			return result;
		}

		// Token: 0x04000B91 RID: 2961
		private WaveStream sourceStream;

		// Token: 0x04000B92 RID: 2962
		private long position;

		// Token: 0x04000B93 RID: 2963
		private readonly CircularBuffer circularBuffer;

		// Token: 0x04000B94 RID: 2964
		private long bufferStartPosition;

		// Token: 0x04000B95 RID: 2965
		private byte[] sourceBuffer;

		// Token: 0x04000B96 RID: 2966
		private readonly object lockObject = new object();
	}
}
