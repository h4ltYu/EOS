using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts a sample provider to 24 bit PCM, optionally clipping and adjusting volume along the way
	/// </summary>
	// Token: 0x0200007A RID: 122
	public class SampleToWaveProvider24 : IWaveProvider
	{
		/// <summary>
		/// Converts from an ISampleProvider (IEEE float) to a 16 bit PCM IWaveProvider.
		/// Number of channels and sample rate remain unchanged.
		/// </summary>
		/// <param name="sourceProvider">The input source provider</param>
		// Token: 0x0600028F RID: 655 RVA: 0x0000880C File Offset: 0x00006A0C
		public SampleToWaveProvider24(ISampleProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input source provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input source provider must be 32 bit", "sourceProvider");
			}
			this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 24, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		/// <summary>
		/// Reads bytes from this wave stream, clipping if necessary
		/// </summary>
		/// <param name="destBuffer">The destination buffer</param>
		/// <param name="offset">Offset into the destination buffer</param>
		/// <param name="numBytes">Number of bytes read</param>
		/// <returns>Number of bytes read.</returns>
		// Token: 0x06000290 RID: 656 RVA: 0x00008894 File Offset: 0x00006A94
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 3;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
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
				int num5 = (int)((double)num4 * 8388607.0);
				destBuffer[num3++] = (byte)num5;
				destBuffer[num3++] = (byte)(num5 >> 8);
				destBuffer[num3++] = (byte)(num5 >> 16);
			}
			return num2 * 3;
		}

		/// <summary>
		/// The Format of this IWaveProvider
		/// <see cref="P:NAudio.Wave.IWaveProvider.WaveFormat" />
		/// </summary>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00008942 File Offset: 0x00006B42
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Volume of this channel. 1.0 = full scale, 0.0 to mute
		/// </summary>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000894A File Offset: 0x00006B4A
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00008954 File Offset: 0x00006B54
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

		// Token: 0x04000395 RID: 917
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000396 RID: 918
		private readonly WaveFormat waveFormat;

		// Token: 0x04000397 RID: 919
		private volatile float volume;

		// Token: 0x04000398 RID: 920
		private float[] sourceBuffer;
	}
}
