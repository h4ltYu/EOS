using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Output Data Buffer
	/// </summary>
	// Token: 0x02000206 RID: 518
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DmoOutputDataBuffer : IDisposable
	{
		/// <summary>
		/// Creates a new DMO Output Data Buffer structure
		/// </summary>
		/// <param name="maxBufferSize">Maximum buffer size</param>
		// Token: 0x06000BDF RID: 3039 RVA: 0x00023D48 File Offset: 0x00021F48
		public DmoOutputDataBuffer(int maxBufferSize)
		{
			this.pBuffer = new MediaBuffer(maxBufferSize);
			this.dwStatus = DmoOutputDataBufferFlags.None;
			this.rtTimestamp = 0L;
			this.referenceTimeDuration = 0L;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x00023D6D File Offset: 0x00021F6D
		public void Dispose()
		{
			if (this.pBuffer != null)
			{
				((MediaBuffer)this.pBuffer).Dispose();
				this.pBuffer = null;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Media Buffer
		/// </summary>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00023D9E File Offset: 0x00021F9E
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00023DA6 File Offset: 0x00021FA6
		public IMediaBuffer MediaBuffer
		{
			get
			{
				return this.pBuffer;
			}
			internal set
			{
				this.pBuffer = value;
			}
		}

		/// <summary>
		/// Length of data in buffer
		/// </summary>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00023DAF File Offset: 0x00021FAF
		public int Length
		{
			get
			{
				return ((MediaBuffer)this.pBuffer).Length;
			}
		}

		/// <summary>
		/// Status Flags
		/// </summary>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00023DC1 File Offset: 0x00021FC1
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00023DC9 File Offset: 0x00021FC9
		public DmoOutputDataBufferFlags StatusFlags
		{
			get
			{
				return this.dwStatus;
			}
			internal set
			{
				this.dwStatus = value;
			}
		}

		/// <summary>
		/// Timestamp
		/// </summary>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00023DD2 File Offset: 0x00021FD2
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00023DDA File Offset: 0x00021FDA
		public long Timestamp
		{
			get
			{
				return this.rtTimestamp;
			}
			internal set
			{
				this.rtTimestamp = value;
			}
		}

		/// <summary>
		/// Duration
		/// </summary>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00023DE3 File Offset: 0x00021FE3
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00023DEB File Offset: 0x00021FEB
		public long Duration
		{
			get
			{
				return this.referenceTimeDuration;
			}
			internal set
			{
				this.referenceTimeDuration = value;
			}
		}

		/// <summary>
		/// Retrives the data in this buffer
		/// </summary>
		/// <param name="data">Buffer to receive data</param>
		/// <param name="offset">Offset into buffer</param>
		// Token: 0x06000BEA RID: 3050 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void RetrieveData(byte[] data, int offset)
		{
			((MediaBuffer)this.pBuffer).RetrieveData(data, offset);
		}

		/// <summary>
		/// Is more data available
		/// If true, ProcessOuput should be called again
		/// </summary>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00023E08 File Offset: 0x00022008
		public bool MoreDataAvailable
		{
			get
			{
				return (this.StatusFlags & DmoOutputDataBufferFlags.Incomplete) == DmoOutputDataBufferFlags.Incomplete;
			}
		}

		// Token: 0x04000C2F RID: 3119
		[MarshalAs(UnmanagedType.Interface)]
		private IMediaBuffer pBuffer;

		// Token: 0x04000C30 RID: 3120
		private DmoOutputDataBufferFlags dwStatus;

		// Token: 0x04000C31 RID: 3121
		private long rtTimestamp;

		// Token: 0x04000C32 RID: 3122
		private long referenceTimeDuration;
	}
}
