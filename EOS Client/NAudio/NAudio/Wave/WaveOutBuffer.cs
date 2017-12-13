using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// A buffer of Wave samples for streaming to a Wave Output device
	/// </summary>
	// Token: 0x02000202 RID: 514
	internal class WaveOutBuffer : IDisposable
	{
		/// <summary>
		/// creates a new wavebuffer
		/// </summary>
		/// <param name="hWaveOut">WaveOut device to write to</param>
		/// <param name="bufferSize">Buffer size in bytes</param>
		/// <param name="bufferFillStream">Stream to provide more data</param>
		/// <param name="waveOutLock">Lock to protect WaveOut API's from being called on &gt;1 thread</param>
		// Token: 0x06000BCD RID: 3021 RVA: 0x00023868 File Offset: 0x00021A68
		public WaveOutBuffer(IntPtr hWaveOut, int bufferSize, IWaveProvider bufferFillStream, object waveOutLock)
		{
			this.bufferSize = bufferSize;
			this.buffer = new byte[bufferSize];
			this.hBuffer = GCHandle.Alloc(this.buffer, GCHandleType.Pinned);
			this.hWaveOut = hWaveOut;
			this.waveStream = bufferFillStream;
			this.waveOutLock = waveOutLock;
			this.header = new WaveHeader();
			this.hHeader = GCHandle.Alloc(this.header, GCHandleType.Pinned);
			this.header.dataBuffer = this.hBuffer.AddrOfPinnedObject();
			this.header.bufferLength = bufferSize;
			this.header.loops = 1;
			this.hThis = GCHandle.Alloc(this);
			this.header.userData = (IntPtr)this.hThis;
			lock (waveOutLock)
			{
				MmException.Try(WaveInterop.waveOutPrepareHeader(hWaveOut, this.header, Marshal.SizeOf(this.header)), "waveOutPrepareHeader");
			}
		}

		/// <summary>
		/// Finalizer for this wave buffer
		/// </summary>
		// Token: 0x06000BCE RID: 3022 RVA: 0x00023968 File Offset: 0x00021B68
		~WaveOutBuffer()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Releases resources held by this WaveBuffer
		/// </summary>
		// Token: 0x06000BCF RID: 3023 RVA: 0x00023998 File Offset: 0x00021B98
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>
		/// Releases resources held by this WaveBuffer
		/// </summary>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x000239A8 File Offset: 0x00021BA8
		protected void Dispose(bool disposing)
		{
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
			if (this.hWaveOut != IntPtr.Zero)
			{
				lock (this.waveOutLock)
				{
					WaveInterop.waveOutUnprepareHeader(this.hWaveOut, this.header, Marshal.SizeOf(this.header));
				}
				this.hWaveOut = IntPtr.Zero;
			}
		}

		/// this is called by the WAVE callback and should be used to refill the buffer
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00023A60 File Offset: 0x00021C60
		internal bool OnDone()
		{
			int num;
			lock (this.waveStream)
			{
				num = this.waveStream.Read(this.buffer, 0, this.buffer.Length);
			}
			if (num == 0)
			{
				return false;
			}
			for (int i = num; i < this.buffer.Length; i++)
			{
				this.buffer[i] = 0;
			}
			this.WriteToWaveOut();
			return true;
		}

		/// <summary>
		/// Whether the header's in queue flag is set
		/// </summary>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00023AD8 File Offset: 0x00021CD8
		public bool InQueue
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;
			}
		}

		/// <summary>
		/// The buffer size in bytes
		/// </summary>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00023AEC File Offset: 0x00021CEC
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00023AF4 File Offset: 0x00021CF4
		private void WriteToWaveOut()
		{
			MmResult mmResult;
			lock (this.waveOutLock)
			{
				mmResult = WaveInterop.waveOutWrite(this.hWaveOut, this.header, Marshal.SizeOf(this.header));
			}
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "waveOutWrite");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x04000C15 RID: 3093
		private readonly WaveHeader header;

		// Token: 0x04000C16 RID: 3094
		private readonly int bufferSize;

		// Token: 0x04000C17 RID: 3095
		private readonly byte[] buffer;

		// Token: 0x04000C18 RID: 3096
		private readonly IWaveProvider waveStream;

		// Token: 0x04000C19 RID: 3097
		private readonly object waveOutLock;

		// Token: 0x04000C1A RID: 3098
		private GCHandle hBuffer;

		// Token: 0x04000C1B RID: 3099
		private IntPtr hWaveOut;

		// Token: 0x04000C1C RID: 3100
		private GCHandle hHeader;

		// Token: 0x04000C1D RID: 3101
		private GCHandle hThis;
	}
}
