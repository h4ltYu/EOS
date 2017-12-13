using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// A buffer of Wave samples
	/// </summary>
	// Token: 0x020001FF RID: 511
	internal class WaveInBuffer : IDisposable
	{
		/// <summary>
		/// creates a new wavebuffer
		/// </summary>
		/// <param name="waveInHandle">WaveIn device to write to</param>
		/// <param name="bufferSize">Buffer size in bytes</param>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00022D0C File Offset: 0x00020F0C
		public WaveInBuffer(IntPtr waveInHandle, int bufferSize)
		{
			this.bufferSize = bufferSize;
			this.buffer = new byte[bufferSize];
			this.hBuffer = GCHandle.Alloc(this.buffer, GCHandleType.Pinned);
			this.waveInHandle = waveInHandle;
			this.header = new WaveHeader();
			this.hHeader = GCHandle.Alloc(this.header, GCHandleType.Pinned);
			this.header.dataBuffer = this.hBuffer.AddrOfPinnedObject();
			this.header.bufferLength = bufferSize;
			this.header.loops = 1;
			this.hThis = GCHandle.Alloc(this);
			this.header.userData = (IntPtr)this.hThis;
			MmException.Try(WaveInterop.waveInPrepareHeader(waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInPrepareHeader");
		}

		/// <summary>
		/// Place this buffer back to record more audio
		/// </summary>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00022DDC File Offset: 0x00020FDC
		public void Reuse()
		{
			MmException.Try(WaveInterop.waveInUnprepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveUnprepareHeader");
			MmException.Try(WaveInterop.waveInPrepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInPrepareHeader");
			MmException.Try(WaveInterop.waveInAddBuffer(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInAddBuffer");
		}

		/// <summary>
		/// Finalizer for this wave buffer
		/// </summary>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x00022E5C File Offset: 0x0002105C
		~WaveInBuffer()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Releases resources held by this WaveBuffer
		/// </summary>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00022E8C File Offset: 0x0002108C
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>
		/// Releases resources held by this WaveBuffer
		/// </summary>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00022E9C File Offset: 0x0002109C
		protected void Dispose(bool disposing)
		{
			if (this.waveInHandle != IntPtr.Zero)
			{
				WaveInterop.waveInUnprepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header));
				this.waveInHandle = IntPtr.Zero;
			}
			if (this.hHeader.IsAllocated)
			{
				this.hHeader.Free();
			}
			if (this.hBuffer.IsAllocated)
			{
				this.hBuffer.Free();
			}
			if (this.hThis.IsAllocated)
			{
				this.hThis.Free();
			}
		}

		/// <summary>
		/// Provides access to the actual record buffer (for reading only)
		/// </summary>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00022F2D File Offset: 0x0002112D
		public byte[] Data
		{
			get
			{
				return this.buffer;
			}
		}

		/// <summary>
		/// Indicates whether the Done flag is set on this buffer
		/// </summary>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00022F35 File Offset: 0x00021135
		public bool Done
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.Done) == WaveHeaderFlags.Done;
			}
		}

		/// <summary>
		/// Indicates whether the InQueue flag is set on this buffer
		/// </summary>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00022F47 File Offset: 0x00021147
		public bool InQueue
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;
			}
		}

		/// <summary>
		/// Number of bytes recorded
		/// </summary>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00022F5B File Offset: 0x0002115B
		public int BytesRecorded
		{
			get
			{
				return this.header.bytesRecorded;
			}
		}

		/// <summary>
		/// The buffer size in bytes
		/// </summary>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00022F68 File Offset: 0x00021168
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x04000BFC RID: 3068
		private readonly WaveHeader header;

		// Token: 0x04000BFD RID: 3069
		private readonly int bufferSize;

		// Token: 0x04000BFE RID: 3070
		private readonly byte[] buffer;

		// Token: 0x04000BFF RID: 3071
		private GCHandle hBuffer;

		// Token: 0x04000C00 RID: 3072
		private IntPtr waveInHandle;

		// Token: 0x04000C01 RID: 3073
		private GCHandle hHeader;

		// Token: 0x04000C02 RID: 3074
		private GCHandle hThis;
	}
}
