using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Takes a stereo 16 bit input and turns it mono, allowing you to select left or right channel only or mix them together
	/// </summary>
	// Token: 0x020001DB RID: 475
	public class StereoToMonoProvider16 : IWaveProvider
	{
		/// <summary>
		/// Creates a new mono waveprovider based on a stereo input
		/// </summary>
		/// <param name="sourceProvider">Stereo 16 bit PCM input</param>
		// Token: 0x06000A76 RID: 2678 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		public StereoToMonoProvider16(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Source must be PCM");
			}
			if (sourceProvider.WaveFormat.Channels != 2)
			{
				throw new ArgumentException("Source must be stereo");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Source must be 16 bit");
			}
			this.sourceProvider = sourceProvider;
			this.outputFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 1);
		}

		/// <summary>
		/// 1.0 to mix the mono source entirely to the left channel
		/// </summary>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001EC29 File Offset: 0x0001CE29
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001EC31 File Offset: 0x0001CE31
		public float LeftVolume { get; set; }

		/// <summary>
		/// 1.0 to mix the mono source entirely to the right channel
		/// </summary>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001EC3A File Offset: 0x0001CE3A
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0001EC42 File Offset: 0x0001CE42
		public float RightVolume { get; set; }

		/// <summary>
		/// Output Wave Format
		/// </summary>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0001EC4B File Offset: 0x0001CE4B
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
		// Token: 0x06000A7C RID: 2684 RVA: 0x0001EC54 File Offset: 0x0001CE54
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = count * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(buffer);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 2;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i += 2)
			{
				short num5 = waveBuffer.ShortBuffer[i];
				short num6 = waveBuffer.ShortBuffer[i + 1];
				float num7 = (float)num5 * this.LeftVolume + (float)num6 * this.RightVolume;
				if (num7 > 32767f)
				{
					num7 = 32767f;
				}
				if (num7 < -32768f)
				{
					num7 = -32768f;
				}
				waveBuffer2.ShortBuffer[num4++] = (short)num7;
			}
			return num2 / 2;
		}

		// Token: 0x04000B66 RID: 2918
		private IWaveProvider sourceProvider;

		// Token: 0x04000B67 RID: 2919
		private WaveFormat outputFormat;

		// Token: 0x04000B68 RID: 2920
		private byte[] sourceBuffer;
	}
}
