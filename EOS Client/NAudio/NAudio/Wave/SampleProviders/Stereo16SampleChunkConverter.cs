using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000195 RID: 405
	internal class Stereo16SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00017EB7 File Offset: 0x000160B7
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 16 && waveFormat.Channels == 2;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00017ED8 File Offset: 0x000160D8
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 4;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 2;
			this.sourceSample = 0;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00017F2C File Offset: 0x0001612C
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				sampleRight = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000998 RID: 2456
		private int sourceSample;

		// Token: 0x04000999 RID: 2457
		private byte[] sourceBuffer;

		// Token: 0x0400099A RID: 2458
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x0400099B RID: 2459
		private int sourceSamples;
	}
}
