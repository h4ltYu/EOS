using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000196 RID: 406
	internal class Stereo24SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00017FAC File Offset: 0x000161AC
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 24 && waveFormat.Channels == 2;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00017FCC File Offset: 0x000161CC
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 6;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001800C File Offset: 0x0001620C
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16 | (int)this.sourceBuffer[this.offset + 1] << 8 | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				sampleRight = (float)((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16 | (int)this.sourceBuffer[this.offset + 1] << 8 | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400099C RID: 2460
		private int offset;

		// Token: 0x0400099D RID: 2461
		private byte[] sourceBuffer;

		// Token: 0x0400099E RID: 2462
		private int sourceBytes;
	}
}
