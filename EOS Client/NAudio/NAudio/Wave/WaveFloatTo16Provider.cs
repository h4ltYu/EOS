using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Converts IEEE float to 16 bit PCM, optionally clipping and adjusting volume along the way
	/// </summary>
	// Token: 0x020001DE RID: 478
	public class WaveFloatTo16Provider : IWaveProvider
	{
		/// <summary>
		/// Creates a new WaveFloatTo16Provider
		/// </summary>
		/// <param name="sourceProvider">the source provider</param>
		// Token: 0x06000A89 RID: 2697 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		public WaveFloatTo16Provider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input wave provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input wave provider must be 32 bit", "sourceProvider");
			}
			this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 16, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="destBuffer">The destination buffer</param>
		/// <param name="offset">Offset into the destination buffer</param>
		/// <param name="numBytes">Number of bytes read</param>
		/// <returns>Number of bytes read.</returns>
		// Token: 0x06000A8A RID: 2698 RVA: 0x0001F044 File Offset: 0x0001D244
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(destBuffer);
			int num3 = num2 / 4;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i++)
			{
				float num5 = waveBuffer.FloatBuffer[i] * this.volume;
				if (num5 > 1f)
				{
					num5 = 1f;
				}
				if (num5 < -1f)
				{
					num5 = -1f;
				}
				waveBuffer2.ShortBuffer[num4++] = (short)(num5 * 32767f);
			}
			return num3 * 2;
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.IWaveProvider.WaveFormat" />
		/// </summary>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Volume of this channel. 1.0 = full scale
		/// </summary>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001F100 File Offset: 0x0001D300
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0001F10A File Offset: 0x0001D30A
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x04000B72 RID: 2930
		private readonly IWaveProvider sourceProvider;

		// Token: 0x04000B73 RID: 2931
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B74 RID: 2932
		private volatile float volume;

		// Token: 0x04000B75 RID: 2933
		private byte[] sourceBuffer;
	}
}
