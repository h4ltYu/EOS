using System;
using System.IO;

namespace NAudio.Wave
{
	/// <summary>
	/// Base class for all WaveStream classes. Derives from stream.
	/// </summary>
	// Token: 0x020001E5 RID: 485
	public abstract class WaveStream : Stream, IWaveProvider
	{
		/// <summary>
		/// Retrieves the WaveFormat for this stream
		/// </summary>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AA9 RID: 2729
		public abstract WaveFormat WaveFormat { get; }

		/// <summary>
		/// We can read from this stream
		/// </summary>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001F464 File Offset: 0x0001D664
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// We can seek within this stream
		/// </summary>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0001F467 File Offset: 0x0001D667
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// We can't write to this stream
		/// </summary>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001F46A File Offset: 0x0001D66A
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Flush does not need to do anything
		/// See <see cref="M:System.IO.Stream.Flush" />
		/// </summary>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0001F46D File Offset: 0x0001D66D
		public override void Flush()
		{
		}

		/// <summary>
		/// An alternative way of repositioning.
		/// See <see cref="M:System.IO.Stream.Seek(System.Int64,System.IO.SeekOrigin)" />
		/// </summary>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0001F46F File Offset: 0x0001D66F
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin)
			{
				this.Position = offset;
			}
			else if (origin == SeekOrigin.Current)
			{
				this.Position += offset;
			}
			else
			{
				this.Position = this.Length + offset;
			}
			return this.Position;
		}

		/// <summary>
		/// Sets the length of the WaveStream. Not Supported.
		/// </summary>
		/// <param name="length"></param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x0001F4A5 File Offset: 0x0001D6A5
		public override void SetLength(long length)
		{
			throw new NotSupportedException("Can't set length of a WaveFormatString");
		}

		/// <summary>
		/// Writes to the WaveStream. Not Supported.
		/// </summary>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001F4B1 File Offset: 0x0001D6B1
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Can't write to a WaveFormatString");
		}

		/// <summary>
		/// The block alignment for this wavestream. Do not modify the Position
		/// to anything that is not a whole multiple of this value
		/// </summary>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001F4BD File Offset: 0x0001D6BD
		public virtual int BlockAlign
		{
			get
			{
				return this.WaveFormat.BlockAlign;
			}
		}

		/// <summary>
		/// Moves forward or backwards the specified number of seconds in the stream
		/// </summary>
		/// <param name="seconds">Number of seconds to move, can be negative</param>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		public void Skip(int seconds)
		{
			long num = this.Position + (long)(this.WaveFormat.AverageBytesPerSecond * seconds);
			if (num > this.Length)
			{
				this.Position = this.Length;
				return;
			}
			if (num < 0L)
			{
				this.Position = 0L;
				return;
			}
			this.Position = num;
		}

		/// <summary>
		/// The current position in the stream in Time format
		/// </summary>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001F51A File Offset: 0x0001D71A
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0001F535 File Offset: 0x0001D735
		public virtual TimeSpan CurrentTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.Position / (double)this.WaveFormat.AverageBytesPerSecond);
			}
			set
			{
				this.Position = (long)(value.TotalSeconds * (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		/// <summary>
		/// Total length in real-time of the stream (may be an estimate for compressed files)
		/// </summary>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001F552 File Offset: 0x0001D752
		public virtual TimeSpan TotalTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.Length / (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		/// <summary>
		/// Whether the WaveStream has non-zero sample data at the current position for the 
		/// specified count
		/// </summary>
		/// <param name="count">Number of bytes to read</param>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001F56D File Offset: 0x0001D76D
		public virtual bool HasData(int count)
		{
			return this.Position < this.Length;
		}
	}
}
