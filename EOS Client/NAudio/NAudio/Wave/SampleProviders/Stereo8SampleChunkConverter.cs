using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000197 RID: 407
	internal class Stereo8SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x000180D7 File Offset: 0x000162D7
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 2;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000180F8 File Offset: 0x000162F8
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00018138 File Offset: 0x00016338
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)this.sourceBuffer[this.offset++] / 256f;
				sampleRight = (float)this.sourceBuffer[this.offset++] / 256f;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400099F RID: 2463
		private int offset;

		// Token: 0x040009A0 RID: 2464
		private byte[] sourceBuffer;

		// Token: 0x040009A1 RID: 2465
		private int sourceBytes;
	}
}
