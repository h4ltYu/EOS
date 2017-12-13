using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000193 RID: 403
	internal class Mono8SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00017D3A File Offset: 0x00015F3A
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 1;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00017D5C File Offset: 0x00015F5C
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, samplePairsRequired);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, samplePairsRequired);
			this.offset = 0;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00017D98 File Offset: 0x00015F98
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)this.sourceBuffer[this.offset] / 256f;
				this.offset++;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000991 RID: 2449
		private int offset;

		// Token: 0x04000992 RID: 2450
		private byte[] sourceBuffer;

		// Token: 0x04000993 RID: 2451
		private int sourceBytes;
	}
}
