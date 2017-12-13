using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// Attempting to implement the COM IMediaBuffer interface as a .NET object
	/// Not sure what will happen when I pass this to an unmanaged object
	/// </summary>
	// Token: 0x0200008F RID: 143
	public class MediaBuffer : IMediaBuffer, IDisposable
	{
		/// <summary>
		/// Creates a new Media Buffer
		/// </summary>
		/// <param name="maxLength">Maximum length in bytes</param>
		// Token: 0x06000309 RID: 777 RVA: 0x0000A20B File Offset: 0x0000840B
		public MediaBuffer(int maxLength)
		{
			this.buffer = Marshal.AllocCoTaskMem(maxLength);
			this.maxLength = maxLength;
		}

		/// <summary>
		/// Dispose and free memory for buffer
		/// </summary>
		// Token: 0x0600030A RID: 778 RVA: 0x0000A226 File Offset: 0x00008426
		public void Dispose()
		{
			if (this.buffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.buffer);
				this.buffer = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		// Token: 0x0600030B RID: 779 RVA: 0x0000A258 File Offset: 0x00008458
		~MediaBuffer()
		{
			this.Dispose();
		}

		/// <summary>
		/// Set length of valid data in the buffer
		/// </summary>
		/// <param name="length">length</param>
		/// <returns>HRESULT</returns>
		// Token: 0x0600030C RID: 780 RVA: 0x0000A284 File Offset: 0x00008484
		int IMediaBuffer.SetLength(int length)
		{
			if (length > this.maxLength)
			{
				return -2147483645;
			}
			this.length = length;
			return 0;
		}

		/// <summary>
		/// Gets the maximum length of the buffer
		/// </summary>
		/// <param name="maxLength">Max length (output parameter)</param>
		/// <returns>HRESULT</returns>
		// Token: 0x0600030D RID: 781 RVA: 0x0000A29D File Offset: 0x0000849D
		int IMediaBuffer.GetMaxLength(out int maxLength)
		{
			maxLength = this.maxLength;
			return 0;
		}

		/// <summary>
		/// Gets buffer and / or length
		/// </summary>
		/// <param name="bufferPointerPointer">Pointer to variable into which buffer pointer should be written</param>
		/// <param name="validDataLengthPointer">Pointer to variable into which valid data length should be written</param>
		/// <returns>HRESULT</returns>
		// Token: 0x0600030E RID: 782 RVA: 0x0000A2A8 File Offset: 0x000084A8
		int IMediaBuffer.GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer)
		{
			if (bufferPointerPointer != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(bufferPointerPointer, this.buffer);
			}
			if (validDataLengthPointer != IntPtr.Zero)
			{
				Marshal.WriteInt32(validDataLengthPointer, this.length);
			}
			return 0;
		}

		/// <summary>
		/// Length of data in the media buffer
		/// </summary>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000A2DD File Offset: 0x000084DD
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000A2E5 File Offset: 0x000084E5
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				if (this.length > this.maxLength)
				{
					throw new ArgumentException("Cannot be greater than maximum buffer size");
				}
				this.length = value;
			}
		}

		/// <summary>
		/// Loads data into this buffer
		/// </summary>
		/// <param name="data">Data to load</param>
		/// <param name="bytes">Number of bytes to load</param>
		// Token: 0x06000311 RID: 785 RVA: 0x0000A307 File Offset: 0x00008507
		public void LoadData(byte[] data, int bytes)
		{
			this.Length = bytes;
			Marshal.Copy(data, 0, this.buffer, bytes);
		}

		/// <summary>
		/// Retrieves the data in the output buffer
		/// </summary>
		/// <param name="data">buffer to retrieve into</param>
		/// <param name="offset">offset within that buffer</param>
		// Token: 0x06000312 RID: 786 RVA: 0x0000A31E File Offset: 0x0000851E
		public void RetrieveData(byte[] data, int offset)
		{
			Marshal.Copy(this.buffer, data, offset, this.Length);
		}

		// Token: 0x040003FA RID: 1018
		private IntPtr buffer;

		// Token: 0x040003FB RID: 1019
		private int length;

		// Token: 0x040003FC RID: 1020
		private int maxLength;
	}
}
