using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.WaveFormats
{
	/// <summary>
	/// The WMA wave format. 
	/// May not be much use because WMA codec is a DirectShow DMO not an ACM
	/// </summary>
	// Token: 0x020001AE RID: 430
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal class WmaWaveFormat : WaveFormat
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001936A File Offset: 0x0001756A
		public WmaWaveFormat(int sampleRate, int bitsPerSample, int channels) : base(sampleRate, bitsPerSample, channels)
		{
			this.wValidBitsPerSample = (short)bitsPerSample;
			if (channels == 1)
			{
				this.dwChannelMask = 1;
			}
			else if (channels == 2)
			{
				this.dwChannelMask = 3;
			}
			this.waveFormatTag = WaveFormatEncoding.WindowsMediaAudio;
		}

		// Token: 0x04000A78 RID: 2680
		private short wValidBitsPerSample;

		// Token: 0x04000A79 RID: 2681
		private int dwChannelMask;

		// Token: 0x04000A7A RID: 2682
		private int dwReserved1;

		// Token: 0x04000A7B RID: 2683
		private int dwReserved2;

		// Token: 0x04000A7C RID: 2684
		private short wEncodeOptions;

		// Token: 0x04000A7D RID: 2685
		private short wReserved3;
	}
}
