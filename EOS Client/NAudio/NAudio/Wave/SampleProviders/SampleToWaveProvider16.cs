using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts a sample provider to 16 bit PCM, optionally clipping and adjusting volume along the way
	/// </summary>
	// Token: 0x02000079 RID: 121
	public class SampleToWaveProvider16 : IWaveProvider
	{
		/// <summary>
		/// Converts from an ISampleProvider (IEEE float) to a 16 bit PCM IWaveProvider.
		/// Number of channels and sample rate remain unchanged.
		/// </summary>
		/// <param name="sourceProvider">The input source provider</param>
		// Token: 0x0600028A RID: 650 RVA: 0x000086C8 File Offset: 0x000068C8
		public SampleToWaveProvider16(ISampleProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input source provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input source provider must be 32 bit", "sourceProvider");
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
		// Token: 0x0600028B RID: 651 RVA: 0x00008750 File Offset: 0x00006950
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			WaveBuffer waveBuffer = new WaveBuffer(destBuffer);
			int num3 = offset / 2;
			for (int i = 0; i < num2; i++)
			{
				float num4 = this.sourceBuffer[i] * this.volume;
				if (num4 > 1f)
				{
					num4 = 1f;
				}
				if (num4 < -1f)
				{
					num4 = -1f;
				}
				waveBuffer.ShortBuffer[num3++] = (short)(num4 * 32767f);
			}
			return num2 * 2;
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.IWaveProvider.WaveFormat" />
		/// </summary>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000087EE File Offset: 0x000069EE
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
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000087F6 File Offset: 0x000069F6
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00008800 File Offset: 0x00006A00
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

		// Token: 0x04000391 RID: 913
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000392 RID: 914
		private readonly WaveFormat waveFormat;

		// Token: 0x04000393 RID: 915
		private volatile float volume;

		// Token: 0x04000394 RID: 916
		private float[] sourceBuffer;
	}
}
