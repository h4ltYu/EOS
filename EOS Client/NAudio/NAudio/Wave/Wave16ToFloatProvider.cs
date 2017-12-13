using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Converts 16 bit PCM to IEEE float, optionally adjusting volume along the way
	/// </summary>
	// Token: 0x020001DF RID: 479
	public class Wave16ToFloatProvider : IWaveProvider
	{
		/// <summary>
		/// Creates a new Wave16toFloatProvider
		/// </summary>
		/// <param name="sourceProvider">the source provider</param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x0001F118 File Offset: 0x0001D318
		public Wave16ToFloatProvider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Only PCM supported");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Only 16 bit audio supported");
			}
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceProvider.WaveFormat.SampleRate, sourceProvider.WaveFormat.Channels);
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
		// Token: 0x06000A8F RID: 2703 RVA: 0x0001F194 File Offset: 0x0001D394
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, offset, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(destBuffer);
			int num3 = num2 / 2;
			int num4 = offset / 4;
			for (int i = 0; i < num3; i++)
			{
				waveBuffer2.FloatBuffer[num4++] = (float)waveBuffer.ShortBuffer[i] / 32768f * this.volume;
			}
			return num3 * 4;
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.IWaveProvider.WaveFormat" />
		/// </summary>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001F224 File Offset: 0x0001D424
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
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001F22C File Offset: 0x0001D42C
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0001F236 File Offset: 0x0001D436
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

		// Token: 0x04000B76 RID: 2934
		private IWaveProvider sourceProvider;

		// Token: 0x04000B77 RID: 2935
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B78 RID: 2936
		private volatile float volume;

		// Token: 0x04000B79 RID: 2937
		private byte[] sourceBuffer;
	}
}
