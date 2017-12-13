using System;
using System.IO;

namespace NAudio.Utils
{
	/// <summary>
	/// Pass-through stream that ignores Dispose
	/// Useful for dealing with MemoryStreams that you want to re-use
	/// </summary>
	// Token: 0x02000119 RID: 281
	public class IgnoreDisposeStream : Stream
	{
		/// <summary>
		/// The source stream all other methods fall through to
		/// </summary>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001435D File Offset: 0x0001255D
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00014365 File Offset: 0x00012565
		public Stream SourceStream { get; private set; }

		/// <summary>
		/// If true the Dispose will be ignored, if false, will pass through to the SourceStream
		/// Set to true by default
		/// </summary>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001436E File Offset: 0x0001256E
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00014376 File Offset: 0x00012576
		public bool IgnoreDispose { get; set; }

		/// <summary>
		/// Creates a new IgnoreDisposeStream
		/// </summary>
		/// <param name="sourceStream">The source stream</param>
		// Token: 0x06000638 RID: 1592 RVA: 0x0001437F File Offset: 0x0001257F
		public IgnoreDisposeStream(Stream sourceStream)
		{
			this.SourceStream = sourceStream;
			this.IgnoreDispose = true;
		}

		/// <summary>
		/// Can Read
		/// </summary>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00014395 File Offset: 0x00012595
		public override bool CanRead
		{
			get
			{
				return this.SourceStream.CanRead;
			}
		}

		/// <summary>
		/// Can Seek
		/// </summary>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000143A2 File Offset: 0x000125A2
		public override bool CanSeek
		{
			get
			{
				return this.SourceStream.CanSeek;
			}
		}

		/// <summary>
		/// Can write to the underlying stream
		/// </summary>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x000143AF File Offset: 0x000125AF
		public override bool CanWrite
		{
			get
			{
				return this.SourceStream.CanWrite;
			}
		}

		/// <summary>
		/// Flushes the underlying stream
		/// </summary>
		// Token: 0x0600063C RID: 1596 RVA: 0x000143BC File Offset: 0x000125BC
		public override void Flush()
		{
			this.SourceStream.Flush();
		}

		/// <summary>
		/// Gets the length of the underlying stream
		/// </summary>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x000143C9 File Offset: 0x000125C9
		public override long Length
		{
			get
			{
				return this.SourceStream.Length;
			}
		}

		/// <summary>
		/// Gets or sets the position of the underlying stream
		/// </summary>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000143D6 File Offset: 0x000125D6
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x000143E3 File Offset: 0x000125E3
		public override long Position
		{
			get
			{
				return this.SourceStream.Position;
			}
			set
			{
				this.SourceStream.Position = value;
			}
		}

		/// <summary>
		/// Reads from the underlying stream
		/// </summary>
		// Token: 0x06000640 RID: 1600 RVA: 0x000143F1 File Offset: 0x000125F1
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.SourceStream.Read(buffer, offset, count);
		}

		/// <summary>
		/// Seeks on the underlying stream
		/// </summary>
		// Token: 0x06000641 RID: 1601 RVA: 0x00014401 File Offset: 0x00012601
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.SourceStream.Seek(offset, origin);
		}

		/// <summary>
		/// Sets the length of the underlying stream
		/// </summary>
		// Token: 0x06000642 RID: 1602 RVA: 0x00014410 File Offset: 0x00012610
		public override void SetLength(long value)
		{
			this.SourceStream.SetLength(value);
		}

		/// <summary>
		/// Writes to the underlying stream
		/// </summary>
		// Token: 0x06000643 RID: 1603 RVA: 0x0001441E File Offset: 0x0001261E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.SourceStream.Write(buffer, offset, count);
		}

		/// <summary>
		/// Dispose - by default (IgnoreDispose = true) will do nothing,
		/// leaving the underlying stream undisposed
		/// </summary>
		// Token: 0x06000644 RID: 1604 RVA: 0x0001442E File Offset: 0x0001262E
		protected override void Dispose(bool disposing)
		{
			if (!this.IgnoreDispose)
			{
				this.SourceStream.Dispose();
				this.SourceStream = null;
			}
		}
	}
}
