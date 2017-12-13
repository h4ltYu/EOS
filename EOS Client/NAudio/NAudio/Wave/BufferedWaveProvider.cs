using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Provides a buffered store of samples
	/// Read method will return queued samples or fill buffer with zeroes
	/// Now backed by a circular buffer
	/// </summary>
	// Token: 0x020001D5 RID: 469
	public class BufferedWaveProvider : IWaveProvider
	{
		/// <summary>
		/// Creates a new buffered WaveProvider
		/// </summary>
		/// <param name="waveFormat">WaveFormat</param>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0001DC36 File Offset: 0x0001BE36
		public BufferedWaveProvider(WaveFormat waveFormat)
		{
			this.waveFormat = waveFormat;
			this.BufferLength = waveFormat.AverageBytesPerSecond * 5;
		}

		/// <summary>
		/// Buffer length in bytes
		/// </summary>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001DC53 File Offset: 0x0001BE53
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0001DC5B File Offset: 0x0001BE5B
		public int BufferLength { get; set; }

		/// <summary>
		/// Buffer duration
		/// </summary>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001DC64 File Offset: 0x0001BE64
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0001DC7F File Offset: 0x0001BE7F
		public TimeSpan BufferDuration
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.BufferLength / (double)this.WaveFormat.AverageBytesPerSecond);
			}
			set
			{
				this.BufferLength = (int)(value.TotalSeconds * (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		/// <summary>
		/// If true, when the buffer is full, start throwing away data
		/// if false, AddSamples will throw an exception when buffer is full
		/// </summary>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0001DCA4 File Offset: 0x0001BEA4
		public bool DiscardOnBufferOverflow { get; set; }

		/// <summary>
		/// The number of buffered bytes
		/// </summary>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001DCAD File Offset: 0x0001BEAD
		public int BufferedBytes
		{
			get
			{
				if (this.circularBuffer != null)
				{
					return this.circularBuffer.Count;
				}
				return 0;
			}
		}

		/// <summary>
		/// Buffered Duration
		/// </summary>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		public TimeSpan BufferedDuration
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.BufferedBytes / (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		/// <summary>
		/// Gets the WaveFormat
		/// </summary>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001DCDF File Offset: 0x0001BEDF
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Adds samples. Takes a copy of buffer, so that buffer can be reused if necessary
		/// </summary>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		public void AddSamples(byte[] buffer, int offset, int count)
		{
			if (this.circularBuffer == null)
			{
				this.circularBuffer = new CircularBuffer(this.BufferLength);
			}
			int num = this.circularBuffer.Write(buffer, offset, count);
			if (num < count && !this.DiscardOnBufferOverflow)
			{
				throw new InvalidOperationException("Buffer full");
			}
		}

		/// <summary>
		/// Reads from this WaveProvider
		/// Will always return count bytes, since we will zero-fill the buffer if not enough available
		/// </summary>
		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DD34 File Offset: 0x0001BF34
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.circularBuffer != null)
			{
				num = this.circularBuffer.Read(buffer, offset, count);
			}
			if (num < count)
			{
				Array.Clear(buffer, offset + num, count - num);
			}
			return count;
		}

		/// <summary>
		/// Discards all audio from the buffer
		/// </summary>
		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		public void ClearBuffer()
		{
			if (this.circularBuffer != null)
			{
				this.circularBuffer.Reset();
			}
		}

		// Token: 0x04000B46 RID: 2886
		private CircularBuffer circularBuffer;

		// Token: 0x04000B47 RID: 2887
		private readonly WaveFormat waveFormat;
	}
}
