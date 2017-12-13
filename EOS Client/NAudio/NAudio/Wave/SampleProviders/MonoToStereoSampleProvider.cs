using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// No nonsense mono to stereo provider, no volume adjustment,
	/// just copies input to left and right. 
	/// </summary>
	// Token: 0x020001D6 RID: 470
	public class MonoToStereoSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Initializes a new instance of MonoToStereoSampleProvider
		/// </summary>
		/// <param name="source">Source sample provider</param>
		// Token: 0x06000A4A RID: 2634 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public MonoToStereoSampleProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source must be mono");
			}
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
		}

		/// <summary>
		/// WaveFormat of this provider
		/// </summary>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001DDBF File Offset: 0x0001BFBF
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Reads samples from this provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public int Read(float[] buffer, int offset, int count)
		{
			int count2 = count / 2;
			int num = offset;
			this.EnsureSourceBuffer(count2);
			int num2 = this.source.Read(this.sourceBuffer, 0, count2);
			for (int i = 0; i < num2; i++)
			{
				buffer[num++] = this.sourceBuffer[i];
				buffer[num++] = this.sourceBuffer[i];
			}
			return num2 * 2;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DE23 File Offset: 0x0001C023
		private void EnsureSourceBuffer(int count)
		{
			if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
			{
				this.sourceBuffer = new float[count];
			}
		}

		// Token: 0x04000B4A RID: 2890
		private readonly ISampleProvider source;

		// Token: 0x04000B4B RID: 2891
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B4C RID: 2892
		private float[] sourceBuffer;
	}
}
