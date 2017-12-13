using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// GSM 610
	/// </summary>
	// Token: 0x020001A2 RID: 418
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class Gsm610WaveFormat : WaveFormat
	{
		/// <summary>
		/// Creates a GSM 610 WaveFormat
		/// For now hardcoded to 13kbps
		/// </summary>
		// Token: 0x0600089C RID: 2204 RVA: 0x00018D60 File Offset: 0x00016F60
		public Gsm610WaveFormat()
		{
			this.waveFormatTag = WaveFormatEncoding.Gsm610;
			this.channels = 1;
			this.averageBytesPerSecond = 1625;
			this.bitsPerSample = 0;
			this.blockAlign = 65;
			this.sampleRate = 8000;
			this.extraSize = 2;
			this.samplesPerBlock = 320;
		}

		/// <summary>
		/// Samples per block
		/// </summary>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00018DB9 File Offset: 0x00016FB9
		public short SamplesPerBlock
		{
			get
			{
				return this.samplesPerBlock;
			}
		}

		/// <summary>
		/// Writes this structure to a BinaryWriter
		/// </summary>
		// Token: 0x0600089E RID: 2206 RVA: 0x00018DC1 File Offset: 0x00016FC1
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.samplesPerBlock);
		}

		// Token: 0x040009BC RID: 2492
		private short samplesPerBlock;
	}
}
