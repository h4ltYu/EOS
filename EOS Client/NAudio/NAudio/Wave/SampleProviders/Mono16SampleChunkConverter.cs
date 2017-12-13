using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000191 RID: 401
	internal class Mono16SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x00017B83 File Offset: 0x00015D83
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 16 && waveFormat.Channels == 1;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 2;
			this.sourceSample = 0;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 2;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400098A RID: 2442
		private int sourceSample;

		// Token: 0x0400098B RID: 2443
		private byte[] sourceBuffer;

		// Token: 0x0400098C RID: 2444
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x0400098D RID: 2445
		private int sourceSamples;
	}
}
