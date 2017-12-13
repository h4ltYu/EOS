using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Helper class for when you need to convert back to an IWaveProvider from
	/// an ISampleProvider. Keeps it as IEEE float
	/// </summary>
	// Token: 0x020001D4 RID: 468
	public class SampleToWaveProvider : IWaveProvider
	{
		/// <summary>
		/// Initializes a new instance of the WaveProviderFloatToWaveProvider class
		/// </summary>
		/// <param name="source">Source wave provider</param>
		// Token: 0x06000A3A RID: 2618 RVA: 0x0001DBCE File Offset: 0x0001BDCE
		public SampleToWaveProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
			this.source = source;
		}

		/// <summary>
		/// Reads from this provider
		/// </summary>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public int Read(byte[] buffer, int offset, int count)
		{
			int count2 = count / 4;
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num = this.source.Read(waveBuffer.FloatBuffer, offset / 4, count2);
			return num * 4;
		}

		/// <summary>
		/// The waveformat of this WaveProvider (same as the source)
		/// </summary>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001DC29 File Offset: 0x0001BE29
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x04000B45 RID: 2885
		private ISampleProvider source;
	}
}
