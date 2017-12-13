using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveBuffer class use to store wave datas. Data can be manipulated with arrays
	/// (<see cref="P:NAudio.Wave.WaveBuffer.ByteBuffer" />,<see cref="P:NAudio.Wave.WaveBuffer.FloatBuffer" />,<see cref="P:NAudio.Wave.WaveBuffer.ShortBuffer" />,<see cref="P:NAudio.Wave.WaveBuffer.IntBuffer" /> ) that are pointing to the
	/// same memory buffer. Use the associated Count property based on the type of buffer to get the number of 
	/// data in the buffer.
	/// Implicit casting is now supported to float[], byte[], int[], short[].
	/// You must not use Length on returned arrays.
	///
	/// n.b. FieldOffset is 8 now to allow it to work natively on 64 bit
	/// </summary>
	// Token: 0x020001CA RID: 458
	[StructLayout(LayoutKind.Explicit, Pack = 2)]
	public class WaveBuffer : IWaveBuffer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.WaveBuffer" /> class.
		/// </summary>
		/// <param name="sizeToAllocateInBytes">The number of bytes. The size of the final buffer will be aligned on 4 Bytes (upper bound)</param>
		// Token: 0x060009C6 RID: 2502 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public WaveBuffer(int sizeToAllocateInBytes)
		{
			int num = sizeToAllocateInBytes % 4;
			sizeToAllocateInBytes = ((num == 0) ? sizeToAllocateInBytes : (sizeToAllocateInBytes + 4 - num));
			this.byteBuffer = new byte[sizeToAllocateInBytes];
			this.numberOfBytes = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.WaveBuffer" /> class binded to a specific byte buffer.
		/// </summary>
		/// <param name="bufferToBoundTo">A byte buffer to bound the WaveBuffer to.</param>
		// Token: 0x060009C7 RID: 2503 RVA: 0x0001C5E7 File Offset: 0x0001A7E7
		public WaveBuffer(byte[] bufferToBoundTo)
		{
			this.BindTo(bufferToBoundTo);
		}

		/// <summary>
		/// Binds this WaveBuffer instance to a specific byte buffer.
		/// </summary>
		/// <param name="bufferToBoundTo">A byte buffer to bound the WaveBuffer to.</param>
		// Token: 0x060009C8 RID: 2504 RVA: 0x0001C5F6 File Offset: 0x0001A7F6
		public void BindTo(byte[] bufferToBoundTo)
		{
			this.byteBuffer = bufferToBoundTo;
			this.numberOfBytes = 0;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:NAudio.Wave.WaveBuffer" /> to <see cref="T:System.Byte" />.
		/// </summary>
		/// <param name="waveBuffer">The wave buffer.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x060009C9 RID: 2505 RVA: 0x0001C606 File Offset: 0x0001A806
		public static implicit operator byte[](WaveBuffer waveBuffer)
		{
			return waveBuffer.byteBuffer;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:NAudio.Wave.WaveBuffer" /> to <see cref="T:System.Single" />.
		/// </summary>
		/// <param name="waveBuffer">The wave buffer.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x060009CA RID: 2506 RVA: 0x0001C60E File Offset: 0x0001A80E
		public static implicit operator float[](WaveBuffer waveBuffer)
		{
			return waveBuffer.floatBuffer;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:NAudio.Wave.WaveBuffer" /> to <see cref="T:System.Int32" />.
		/// </summary>
		/// <param name="waveBuffer">The wave buffer.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x060009CB RID: 2507 RVA: 0x0001C616 File Offset: 0x0001A816
		public static implicit operator int[](WaveBuffer waveBuffer)
		{
			return waveBuffer.intBuffer;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:NAudio.Wave.WaveBuffer" /> to <see cref="T:System.Int16" />.
		/// </summary>
		/// <param name="waveBuffer">The wave buffer.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x060009CC RID: 2508 RVA: 0x0001C61E File Offset: 0x0001A81E
		public static implicit operator short[](WaveBuffer waveBuffer)
		{
			return waveBuffer.shortBuffer;
		}

		/// <summary>
		/// Gets the byte buffer.
		/// </summary>
		/// <value>The byte buffer.</value>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0001C626 File Offset: 0x0001A826
		public byte[] ByteBuffer
		{
			get
			{
				return this.byteBuffer;
			}
		}

		/// <summary>
		/// Gets the float buffer.
		/// </summary>
		/// <value>The float buffer.</value>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001C62E File Offset: 0x0001A82E
		public float[] FloatBuffer
		{
			get
			{
				return this.floatBuffer;
			}
		}

		/// <summary>
		/// Gets the short buffer.
		/// </summary>
		/// <value>The short buffer.</value>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001C636 File Offset: 0x0001A836
		public short[] ShortBuffer
		{
			get
			{
				return this.shortBuffer;
			}
		}

		/// <summary>
		/// Gets the int buffer.
		/// </summary>
		/// <value>The int buffer.</value>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001C63E File Offset: 0x0001A83E
		public int[] IntBuffer
		{
			get
			{
				return this.intBuffer;
			}
		}

		/// <summary>
		/// Gets the max size in bytes of the byte buffer..
		/// </summary>
		/// <value>Maximum number of bytes in the buffer.</value>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001C646 File Offset: 0x0001A846
		public int MaxSize
		{
			get
			{
				return this.byteBuffer.Length;
			}
		}

		/// <summary>
		/// Gets or sets the byte buffer count.
		/// </summary>
		/// <value>The byte buffer count.</value>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001C650 File Offset: 0x0001A850
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0001C658 File Offset: 0x0001A858
		public int ByteBufferCount
		{
			get
			{
				return this.numberOfBytes;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("ByteBufferCount", value, 1);
			}
		}

		/// <summary>
		/// Gets or sets the float buffer count.
		/// </summary>
		/// <value>The float buffer count.</value>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001C66D File Offset: 0x0001A86D
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0001C677 File Offset: 0x0001A877
		public int FloatBufferCount
		{
			get
			{
				return this.numberOfBytes / 4;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("FloatBufferCount", value, 4);
			}
		}

		/// <summary>
		/// Gets or sets the short buffer count.
		/// </summary>
		/// <value>The short buffer count.</value>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001C68C File Offset: 0x0001A88C
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001C696 File Offset: 0x0001A896
		public int ShortBufferCount
		{
			get
			{
				return this.numberOfBytes / 2;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("ShortBufferCount", value, 2);
			}
		}

		/// <summary>
		/// Gets or sets the int buffer count.
		/// </summary>
		/// <value>The int buffer count.</value>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001C6AB File Offset: 0x0001A8AB
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001C6B5 File Offset: 0x0001A8B5
		public int IntBufferCount
		{
			get
			{
				return this.numberOfBytes / 4;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("IntBufferCount", value, 4);
			}
		}

		/// <summary>
		/// Clears the associated buffer.
		/// </summary>
		// Token: 0x060009DA RID: 2522 RVA: 0x0001C6CA File Offset: 0x0001A8CA
		public void Clear()
		{
			Array.Clear(this.byteBuffer, 0, this.byteBuffer.Length);
		}

		/// <summary>
		/// Copy this WaveBuffer to a destination buffer up to ByteBufferCount bytes.
		/// </summary>
		// Token: 0x060009DB RID: 2523 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public void Copy(Array destinationArray)
		{
			Array.Copy(this.byteBuffer, destinationArray, this.numberOfBytes);
		}

		/// <summary>
		/// Checks the validity of the count parameters.
		/// </summary>
		/// <param name="argName">Name of the arg.</param>
		/// <param name="value">The value.</param>
		/// <param name="sizeOfValue">The size of value.</param>
		// Token: 0x060009DC RID: 2524 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		private int CheckValidityCount(string argName, int value, int sizeOfValue)
		{
			int num = value * sizeOfValue;
			if (num % 4 != 0)
			{
				throw new ArgumentOutOfRangeException(argName, string.Format("{0} cannot set a count ({1}) that is not 4 bytes aligned ", argName, num));
			}
			if (value < 0 || value > this.byteBuffer.Length / sizeOfValue)
			{
				throw new ArgumentOutOfRangeException(argName, string.Format("{0} cannot set a count that exceed max count {1}", argName, this.byteBuffer.Length / sizeOfValue));
			}
			return num;
		}

		/// <summary>
		/// Number of Bytes
		/// </summary>
		// Token: 0x04000B0F RID: 2831
		[FieldOffset(0)]
		public int numberOfBytes;

		// Token: 0x04000B10 RID: 2832
		[FieldOffset(8)]
		private byte[] byteBuffer;

		// Token: 0x04000B11 RID: 2833
		[FieldOffset(8)]
		private float[] floatBuffer;

		// Token: 0x04000B12 RID: 2834
		[FieldOffset(8)]
		private short[] shortBuffer;

		// Token: 0x04000B13 RID: 2835
		[FieldOffset(8)]
		private int[] intBuffer;
	}
}
