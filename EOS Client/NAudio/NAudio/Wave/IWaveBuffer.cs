using System;

namespace NAudio.Wave
{
	/// <summary>
	/// IWaveBuffer interface use to store wave datas. 
	/// Data can be manipulated with arrays (<see cref="P:NAudio.Wave.IWaveBuffer.ByteBuffer" />,<see cref="P:NAudio.Wave.IWaveBuffer.FloatBuffer" />,
	/// <see cref="P:NAudio.Wave.IWaveBuffer.ShortBuffer" />,<see cref="P:NAudio.Wave.IWaveBuffer.IntBuffer" /> ) that are pointing to the same memory buffer.
	/// This is a requirement for all subclasses.
	///
	/// Use the associated Count property based on the type of buffer to get the number of data in the 
	/// buffer.
	///
	/// <see cref="T:NAudio.Wave.WaveBuffer" /> for the standard implementation using C# unions.
	/// </summary>
	// Token: 0x020001B8 RID: 440
	public interface IWaveBuffer
	{
		/// <summary>
		/// Gets the byte buffer.
		/// </summary>
		/// <value>The byte buffer.</value>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000961 RID: 2401
		byte[] ByteBuffer { get; }

		/// <summary>
		/// Gets the float buffer.
		/// </summary>
		/// <value>The float buffer.</value>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000962 RID: 2402
		float[] FloatBuffer { get; }

		/// <summary>
		/// Gets the short buffer.
		/// </summary>
		/// <value>The short buffer.</value>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000963 RID: 2403
		short[] ShortBuffer { get; }

		/// <summary>
		/// Gets the int buffer.
		/// </summary>
		/// <value>The int buffer.</value>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000964 RID: 2404
		int[] IntBuffer { get; }

		/// <summary>
		/// Gets the max size in bytes of the byte buffer..
		/// </summary>
		/// <value>Maximum number of bytes in the buffer.</value>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000965 RID: 2405
		int MaxSize { get; }

		/// <summary>
		/// Gets the byte buffer count.
		/// </summary>
		/// <value>The byte buffer count.</value>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000966 RID: 2406
		int ByteBufferCount { get; }

		/// <summary>
		/// Gets the float buffer count.
		/// </summary>
		/// <value>The float buffer count.</value>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000967 RID: 2407
		int FloatBufferCount { get; }

		/// <summary>
		/// Gets the short buffer count.
		/// </summary>
		/// <value>The short buffer count.</value>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000968 RID: 2408
		int ShortBufferCount { get; }

		/// <summary>
		/// Gets the int buffer count.
		/// </summary>
		/// <value>The int buffer count.</value>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000969 RID: 2409
		int IntBufferCount { get; }
	}
}
