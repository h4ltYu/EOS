using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// DSP Group TrueSpeech
	/// </summary>
	// Token: 0x020001A8 RID: 424
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class TrueSpeechWaveFormat : WaveFormat
	{
		/// <summary>
		/// DSP Group TrueSpeech WaveFormat
		/// </summary>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00018EA8 File Offset: 0x000170A8
		public TrueSpeechWaveFormat()
		{
			this.waveFormatTag = WaveFormatEncoding.DspGroupTrueSpeech;
			this.channels = 1;
			this.averageBytesPerSecond = 1067;
			this.bitsPerSample = 1;
			this.blockAlign = 32;
			this.sampleRate = 8000;
			this.extraSize = 32;
			this.unknown = new short[16];
			this.unknown[0] = 1;
			this.unknown[1] = 240;
		}

		/// <summary>
		/// Writes this structure to a BinaryWriter
		/// </summary>
		// Token: 0x060008A4 RID: 2212 RVA: 0x00018F1C File Offset: 0x0001711C
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			foreach (short value in this.unknown)
			{
				writer.Write(value);
			}
		}

		// Token: 0x040009CE RID: 2510
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private short[] unknown;
	}
}
