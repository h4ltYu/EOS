using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Event Args for WaveInStream event
	/// </summary>
	// Token: 0x02000186 RID: 390
	public class WaveInEventArgs : EventArgs
	{
		/// <summary>
		/// Creates new WaveInEventArgs
		/// </summary>
		// Token: 0x06000815 RID: 2069 RVA: 0x000179CB File Offset: 0x00015BCB
		public WaveInEventArgs(byte[] buffer, int bytes)
		{
			this.buffer = buffer;
			this.bytes = bytes;
		}

		/// <summary>
		/// Buffer containing recorded data. Note that it might not be completely
		/// full. <seealso cref="P:NAudio.Wave.WaveInEventArgs.BytesRecorded" />
		/// </summary>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x000179E1 File Offset: 0x00015BE1
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		/// <summary>
		/// The number of recorded bytes in Buffer. <seealso cref="P:NAudio.Wave.WaveInEventArgs.Buffer" />
		/// </summary>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000179E9 File Offset: 0x00015BE9
		public int BytesRecorded
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x0400094D RID: 2381
		private byte[] buffer;

		// Token: 0x0400094E RID: 2382
		private int bytes;
	}
}
