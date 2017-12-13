using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Buffered WaveProvider taking source data from WaveIn
	/// </summary>
	// Token: 0x020001E0 RID: 480
	public class WaveInProvider : IWaveProvider
	{
		/// <summary>
		/// Creates a new WaveInProvider
		/// n.b. Should make sure the WaveFormat is set correctly on IWaveIn before calling
		/// </summary>
		/// <param name="waveIn">The source of wave data</param>
		// Token: 0x06000A93 RID: 2707 RVA: 0x0001F241 File Offset: 0x0001D441
		public WaveInProvider(IWaveIn waveIn)
		{
			this.waveIn = waveIn;
			waveIn.DataAvailable += this.waveIn_DataAvailable;
			this.bufferedWaveProvider = new BufferedWaveProvider(this.WaveFormat);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001F273 File Offset: 0x0001D473
		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			this.bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
		}

		/// <summary>
		/// Reads data from the WaveInProvider
		/// </summary>
		// Token: 0x06000A95 RID: 2709 RVA: 0x0001F28D File Offset: 0x0001D48D
		public int Read(byte[] buffer, int offset, int count)
		{
			return this.bufferedWaveProvider.Read(buffer, 0, count);
		}

		/// <summary>
		/// The WaveFormat
		/// </summary>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0001F29D File Offset: 0x0001D49D
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveIn.WaveFormat;
			}
		}

		// Token: 0x04000B7A RID: 2938
		private IWaveIn waveIn;

		// Token: 0x04000B7B RID: 2939
		private BufferedWaveProvider bufferedWaveProvider;
	}
}
