using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// IMA/DVI ADPCM Wave Format
	/// Work in progress
	/// </summary>
	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class ImaAdpcmWaveFormat : WaveFormat
	{
		/// <summary>
		/// parameterless constructor for Marshalling
		/// </summary>
		// Token: 0x0600089F RID: 2207 RVA: 0x00018DD6 File Offset: 0x00016FD6
		private ImaAdpcmWaveFormat()
		{
		}

		/// <summary>
		/// Creates a new IMA / DVI ADPCM Wave Format
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Number of channels</param>
		/// <param name="bitsPerSample">Bits Per Sample</param>
		// Token: 0x060008A0 RID: 2208 RVA: 0x00018DE0 File Offset: 0x00016FE0
		public ImaAdpcmWaveFormat(int sampleRate, int channels, int bitsPerSample)
		{
			this.waveFormatTag = WaveFormatEncoding.DviAdpcm;
			this.sampleRate = sampleRate;
			this.channels = (short)channels;
			this.bitsPerSample = (short)bitsPerSample;
			this.extraSize = 2;
			this.blockAlign = 0;
			this.averageBytesPerSecond = 0;
			this.samplesPerBlock = 0;
		}

		// Token: 0x040009BD RID: 2493
		private short samplesPerBlock;
	}
}
