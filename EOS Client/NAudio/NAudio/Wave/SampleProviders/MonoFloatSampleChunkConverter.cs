using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000194 RID: 404
	internal class MonoFloatSampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.IeeeFloat && waveFormat.Channels == 1;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00017E0C File Offset: 0x0001600C
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 4;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 4;
			this.sourceSample = 0;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00017E60 File Offset: 0x00016060
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000994 RID: 2452
		private int sourceSample;

		// Token: 0x04000995 RID: 2453
		private byte[] sourceBuffer;

		// Token: 0x04000996 RID: 2454
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x04000997 RID: 2455
		private int sourceSamples;
	}
}
