using System;

namespace NAudio.Utils
{
	/// <summary>
	/// A very basic circular buffer implementation
	/// </summary>
	// Token: 0x02000116 RID: 278
	public class CircularBuffer
	{
		/// <summary>
		/// Create a new circular buffer
		/// </summary>
		/// <param name="size">Max buffer size in bytes</param>
		// Token: 0x06000628 RID: 1576 RVA: 0x000140C3 File Offset: 0x000122C3
		public CircularBuffer(int size)
		{
			this.buffer = new byte[size];
			this.lockObject = new object();
		}

		/// <summary>
		/// Write data to the buffer
		/// </summary>
		/// <param name="data">Data to write</param>
		/// <param name="offset">Offset into data</param>
		/// <param name="count">Number of bytes to write</param>
		/// <returns>number of bytes written</returns>
		// Token: 0x06000629 RID: 1577 RVA: 0x000140E4 File Offset: 0x000122E4
		public int Write(byte[] data, int offset, int count)
		{
			int result;
			lock (this.lockObject)
			{
				int num = 0;
				if (count > this.buffer.Length - this.byteCount)
				{
					count = this.buffer.Length - this.byteCount;
				}
				int num2 = Math.Min(this.buffer.Length - this.writePosition, count);
				Array.Copy(data, offset, this.buffer, this.writePosition, num2);
				this.writePosition += num2;
				this.writePosition %= this.buffer.Length;
				num += num2;
				if (num < count)
				{
					Array.Copy(data, offset + num, this.buffer, this.writePosition, count - num);
					this.writePosition += count - num;
					num = count;
				}
				this.byteCount += num;
				result = num;
			}
			return result;
		}

		/// <summary>
		/// Read from the buffer
		/// </summary>
		/// <param name="data">Buffer to read into</param>
		/// <param name="offset">Offset into read buffer</param>
		/// <param name="count">Bytes to read</param>
		/// <returns>Number of bytes actually read</returns>
		// Token: 0x0600062A RID: 1578 RVA: 0x000141CC File Offset: 0x000123CC
		public int Read(byte[] data, int offset, int count)
		{
			int result;
			lock (this.lockObject)
			{
				if (count > this.byteCount)
				{
					count = this.byteCount;
				}
				int num = 0;
				int num2 = Math.Min(this.buffer.Length - this.readPosition, count);
				Array.Copy(this.buffer, this.readPosition, data, offset, num2);
				num += num2;
				this.readPosition += num2;
				this.readPosition %= this.buffer.Length;
				if (num < count)
				{
					Array.Copy(this.buffer, this.readPosition, data, offset + num, count - num);
					this.readPosition += count - num;
					num = count;
				}
				this.byteCount -= num;
				result = num;
			}
			return result;
		}

		/// <summary>
		/// Maximum length of this circular buffer
		/// </summary>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000142A4 File Offset: 0x000124A4
		public int MaxLength
		{
			get
			{
				return this.buffer.Length;
			}
		}

		/// <summary>
		/// Number of bytes currently stored in the circular buffer
		/// </summary>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000142AE File Offset: 0x000124AE
		public int Count
		{
			get
			{
				return this.byteCount;
			}
		}

		/// <summary>
		/// Resets the buffer
		/// </summary>
		// Token: 0x0600062D RID: 1581 RVA: 0x000142B6 File Offset: 0x000124B6
		public void Reset()
		{
			this.byteCount = 0;
			this.readPosition = 0;
			this.writePosition = 0;
		}

		/// <summary>
		/// Advances the buffer, discarding bytes
		/// </summary>
		/// <param name="count">Bytes to advance</param>
		// Token: 0x0600062E RID: 1582 RVA: 0x000142D0 File Offset: 0x000124D0
		public void Advance(int count)
		{
			if (count >= this.byteCount)
			{
				this.Reset();
				return;
			}
			this.byteCount -= count;
			this.readPosition += count;
			this.readPosition %= this.MaxLength;
		}

		// Token: 0x040006C6 RID: 1734
		private readonly byte[] buffer;

		// Token: 0x040006C7 RID: 1735
		private readonly object lockObject;

		// Token: 0x040006C8 RID: 1736
		private int writePosition;

		// Token: 0x040006C9 RID: 1737
		private int readPosition;

		// Token: 0x040006CA RID: 1738
		private int byteCount;
	}
}
