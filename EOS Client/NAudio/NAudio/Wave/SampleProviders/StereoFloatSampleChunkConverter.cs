using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000198 RID: 408
	internal class StereoFloatSampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x000181AE File Offset: 0x000163AE
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.IeeeFloat && waveFormat.Channels == 2;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000181C4 File Offset: 0x000163C4
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 8;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 4;
			this.sourceSample = 0;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00018218 File Offset: 0x00016418
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				sampleRight = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x040009A2 RID: 2466
		private int sourceSample;

		// Token: 0x040009A3 RID: 2467
		private byte[] sourceBuffer;

		// Token: 0x040009A4 RID: 2468
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x040009A5 RID: 2469
		private int sourceSamples;
	}
}
