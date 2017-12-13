using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000192 RID: 402
	internal class Mono24SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00017C56 File Offset: 0x00015E56
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 24 && waveFormat.Channels == 1;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00017C78 File Offset: 0x00015E78
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 3;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00017CB8 File Offset: 0x00015EB8
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16 | (int)this.sourceBuffer[this.offset + 1] << 8 | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400098E RID: 2446
		private int offset;

		// Token: 0x0400098F RID: 2447
		private byte[] sourceBuffer;

		// Token: 0x04000990 RID: 2448
		private int sourceBytes;
	}
}
