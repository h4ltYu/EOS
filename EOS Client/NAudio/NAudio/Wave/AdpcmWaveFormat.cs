using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// Microsoft ADPCM
	/// See http://icculus.org/SDL_sound/downloads/external_documentation/wavecomp.htm
	/// </summary>
	// Token: 0x020001A9 RID: 425
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class AdpcmWaveFormat : WaveFormat
	{
		/// <summary>
		/// Empty constructor needed for marshalling from a pointer
		/// </summary>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00018F50 File Offset: 0x00017150
		private AdpcmWaveFormat() : this(8000, 1)
		{
		}

		/// <summary>
		/// Samples per block
		/// </summary>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00018F5E File Offset: 0x0001715E
		public int SamplesPerBlock
		{
			get
			{
				return (int)this.samplesPerBlock;
			}
		}

		/// <summary>
		/// Number of coefficients
		/// </summary>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00018F66 File Offset: 0x00017166
		public int NumCoefficients
		{
			get
			{
				return (int)this.numCoeff;
			}
		}

		/// <summary>
		/// Coefficients
		/// </summary>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00018F6E File Offset: 0x0001716E
		public short[] Coefficients
		{
			get
			{
				return this.coefficients;
			}
		}

		/// <summary>
		/// Microsoft ADPCM  
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Channels</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00018F94 File Offset: 0x00017194
		public AdpcmWaveFormat(int sampleRate, int channels) : base(sampleRate, 0, channels)
		{
			this.waveFormatTag = WaveFormatEncoding.Adpcm;
			this.extraSize = 32;
			int sampleRate2 = this.sampleRate;
			if (sampleRate2 <= 11025)
			{
				if (sampleRate2 == 8000 || sampleRate2 == 11025)
				{
					this.blockAlign = 256;
					goto IL_70;
				}
			}
			else
			{
				if (sampleRate2 == 22050)
				{
					this.blockAlign = 512;
					goto IL_70;
				}
				if (sampleRate2 != 44100)
				{
				}
			}
			this.blockAlign = 1024;
			IL_70:
			this.bitsPerSample = 4;
			this.samplesPerBlock = (short)(((int)this.blockAlign - 7 * channels) * 8 / ((int)this.bitsPerSample * channels) + 2);
			this.averageBytesPerSecond = base.SampleRate * (int)this.blockAlign / (int)this.samplesPerBlock;
			this.numCoeff = 7;
			this.coefficients = new short[]
			{
				256,
				0,
				512,
				-256,
				0,
				0,
				192,
				64,
				240,
				0,
				460,
				-208,
				392,
				-232
			};
		}

		/// <summary>
		/// Serializes this wave format
		/// </summary>
		/// <param name="writer">Binary writer</param>
		// Token: 0x060008AA RID: 2218 RVA: 0x00019070 File Offset: 0x00017270
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.samplesPerBlock);
			writer.Write(this.numCoeff);
			foreach (short value in this.coefficients)
			{
				writer.Write(value);
			}
		}

		/// <summary>
		/// String Description of this WaveFormat
		/// </summary>
		// Token: 0x060008AB RID: 2219 RVA: 0x000190BC File Offset: 0x000172BC
		public override string ToString()
		{
			return string.Format("Microsoft ADPCM {0} Hz {1} channels {2} bits per sample {3} samples per block", new object[]
			{
				base.SampleRate,
				this.channels,
				this.bitsPerSample,
				this.samplesPerBlock
			});
		}

		// Token: 0x040009CF RID: 2511
		private short samplesPerBlock;

		// Token: 0x040009D0 RID: 2512
		private short numCoeff;

		// Token: 0x040009D1 RID: 2513
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
		private short[] coefficients;
	}
}
