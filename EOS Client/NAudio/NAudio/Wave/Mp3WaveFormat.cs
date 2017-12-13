using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// MP3 WaveFormat, MPEGLAYER3WAVEFORMAT from mmreg.h
	/// </summary>
	// Token: 0x020001A4 RID: 420
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class Mp3WaveFormat : WaveFormat
	{
		/// <summary>
		/// Creates a new MP3 WaveFormat
		/// </summary>
		// Token: 0x060008A1 RID: 2209 RVA: 0x00018E30 File Offset: 0x00017030
		public Mp3WaveFormat(int sampleRate, int channels, int blockSize, int bitRate)
		{
			this.waveFormatTag = WaveFormatEncoding.MpegLayer3;
			this.channels = (short)channels;
			this.averageBytesPerSecond = bitRate / 8;
			this.bitsPerSample = 0;
			this.blockAlign = 1;
			this.sampleRate = sampleRate;
			this.extraSize = 12;
			this.id = Mp3WaveFormatId.Mpeg;
			this.flags = Mp3WaveFormatFlags.PaddingIso;
			this.blockSize = (ushort)blockSize;
			this.framesPerBlock = 1;
			this.codecDelay = 0;
		}

		// Token: 0x040009BE RID: 2494
		private const short Mp3WaveFormatExtraBytes = 12;

		/// <summary>
		/// Wave format ID (wID)
		/// </summary>
		// Token: 0x040009BF RID: 2495
		public Mp3WaveFormatId id;

		/// <summary>
		/// Padding flags (fdwFlags)
		/// </summary>
		// Token: 0x040009C0 RID: 2496
		public Mp3WaveFormatFlags flags;

		/// <summary>
		/// Block Size (nBlockSize)
		/// </summary>
		// Token: 0x040009C1 RID: 2497
		public ushort blockSize;

		/// <summary>
		/// Frames per block (nFramesPerBlock)
		/// </summary>
		// Token: 0x040009C2 RID: 2498
		public ushort framesPerBlock;

		/// <summary>
		/// Codec Delay (nCodecDelay)
		/// </summary>
		// Token: 0x040009C3 RID: 2499
		public ushort codecDelay;
	}
}
