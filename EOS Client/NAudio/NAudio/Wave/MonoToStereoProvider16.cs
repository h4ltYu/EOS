using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Converts from mono to stereo, allowing freedom to route all, some, or none of the incoming signal to left or right channels
	/// </summary>
	// Token: 0x020001DC RID: 476
	public class MonoToStereoProvider16 : IWaveProvider
	{
		/// <summary>
		/// Creates a new stereo waveprovider based on a mono input
		/// </summary>
		/// <param name="sourceProvider">Mono 16 bit PCM input</param>
		// Token: 0x06000A7D RID: 2685 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		public MonoToStereoProvider16(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Source must be PCM");
			}
			if (sourceProvider.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source must be Mono");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Source must be 16 bit");
			}
			this.sourceProvider = sourceProvider;
			this.outputFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 2);
			this.RightVolume = 1f;
			this.LeftVolume = 1f;
		}

		/// <summary>
		/// 1.0 to copy the mono stream to the left channel without adjusting volume
		/// </summary>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001EDAF File Offset: 0x0001CFAF
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001EDB7 File Offset: 0x0001CFB7
		public float LeftVolume { get; set; }

		/// <summary>
		/// 1.0 to copy the mono stream to the right channel without adjusting volume
		/// </summary>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		public float RightVolume { get; set; }

		/// <summary>
		/// Output Wave Format
		/// </summary>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001EDD1 File Offset: 0x0001CFD1
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		/// <summary>
		/// Reads bytes from this WaveProvider
		/// </summary>
		// Token: 0x06000A83 RID: 2691 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = count / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(buffer);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 2;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i++)
			{
				short num5 = waveBuffer.ShortBuffer[i];
				waveBuffer2.ShortBuffer[num4++] = (short)(this.LeftVolume * (float)num5);
				waveBuffer2.ShortBuffer[num4++] = (short)(this.RightVolume * (float)num5);
			}
			return num3 * 4;
		}

		// Token: 0x04000B6B RID: 2923
		private IWaveProvider sourceProvider;

		// Token: 0x04000B6C RID: 2924
		private WaveFormat outputFormat;

		// Token: 0x04000B6D RID: 2925
		private byte[] sourceBuffer;
	}
}
