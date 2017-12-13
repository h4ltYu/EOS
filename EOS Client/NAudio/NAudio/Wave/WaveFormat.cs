using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents a Wave file format
	/// </summary>
	// Token: 0x020001A1 RID: 417
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class WaveFormat
	{
		/// <summary>
		/// Creates a new PCM 44.1Khz stereo 16 bit format
		/// </summary>
		// Token: 0x06000884 RID: 2180 RVA: 0x00018877 File Offset: 0x00016A77
		public WaveFormat() : this(44100, 16, 2)
		{
		}

		/// <summary>
		/// Creates a new 16 bit wave format with the specified sample
		/// rate and channel count
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Number of channels</param>
		// Token: 0x06000885 RID: 2181 RVA: 0x00018887 File Offset: 0x00016A87
		public WaveFormat(int sampleRate, int channels) : this(sampleRate, 16, channels)
		{
		}

		/// <summary>
		/// Gets the size of a wave buffer equivalent to the latency in milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		/// <returns></returns>
		// Token: 0x06000886 RID: 2182 RVA: 0x00018894 File Offset: 0x00016A94
		public int ConvertLatencyToByteSize(int milliseconds)
		{
			int num = (int)((double)this.AverageBytesPerSecond / 1000.0 * (double)milliseconds);
			if (num % this.BlockAlign != 0)
			{
				num = num + this.BlockAlign - num % this.BlockAlign;
			}
			return num;
		}

		/// <summary>
		/// Creates a WaveFormat with custom members
		/// </summary>
		/// <param name="tag">The encoding</param>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Number of channels</param>
		/// <param name="averageBytesPerSecond">Average Bytes Per Second</param>
		/// <param name="blockAlign">Block Align</param>
		/// <param name="bitsPerSample">Bits Per Sample</param>
		/// <returns></returns>
		// Token: 0x06000887 RID: 2183 RVA: 0x000188D4 File Offset: 0x00016AD4
		public static WaveFormat CreateCustomFormat(WaveFormatEncoding tag, int sampleRate, int channels, int averageBytesPerSecond, int blockAlign, int bitsPerSample)
		{
			return new WaveFormat
			{
				waveFormatTag = tag,
				channels = (short)channels,
				sampleRate = sampleRate,
				averageBytesPerSecond = averageBytesPerSecond,
				blockAlign = (short)blockAlign,
				bitsPerSample = (short)bitsPerSample,
				extraSize = 0
			};
		}

		/// <summary>
		/// Creates an A-law wave format
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Number of Channels</param>
		/// <returns>Wave Format</returns>
		// Token: 0x06000888 RID: 2184 RVA: 0x0001891E File Offset: 0x00016B1E
		public static WaveFormat CreateALawFormat(int sampleRate, int channels)
		{
			return WaveFormat.CreateCustomFormat(WaveFormatEncoding.ALaw, sampleRate, channels, sampleRate * channels, channels, 8);
		}

		/// <summary>
		/// Creates a Mu-law wave format
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="channels">Number of Channels</param>
		/// <returns>Wave Format</returns>
		// Token: 0x06000889 RID: 2185 RVA: 0x0001892D File Offset: 0x00016B2D
		public static WaveFormat CreateMuLawFormat(int sampleRate, int channels)
		{
			return WaveFormat.CreateCustomFormat(WaveFormatEncoding.MuLaw, sampleRate, channels, sampleRate * channels, channels, 8);
		}

		/// <summary>
		/// Creates a new PCM format with the specified sample rate, bit depth and channels
		/// </summary>
		// Token: 0x0600088A RID: 2186 RVA: 0x0001893C File Offset: 0x00016B3C
		public WaveFormat(int rate, int bits, int channels)
		{
			if (channels < 1)
			{
				throw new ArgumentOutOfRangeException("channels", "Channels must be 1 or greater");
			}
			this.waveFormatTag = WaveFormatEncoding.Pcm;
			this.channels = (short)channels;
			this.sampleRate = rate;
			this.bitsPerSample = (short)bits;
			this.extraSize = 0;
			this.blockAlign = (short)(channels * (bits / 8));
			this.averageBytesPerSecond = this.sampleRate * (int)this.blockAlign;
		}

		/// <summary>
		/// Creates a new 32 bit IEEE floating point wave format
		/// </summary>
		/// <param name="sampleRate">sample rate</param>
		/// <param name="channels">number of channels</param>
		// Token: 0x0600088B RID: 2187 RVA: 0x000189A8 File Offset: 0x00016BA8
		public static WaveFormat CreateIeeeFloatWaveFormat(int sampleRate, int channels)
		{
			WaveFormat waveFormat = new WaveFormat();
			waveFormat.waveFormatTag = WaveFormatEncoding.IeeeFloat;
			waveFormat.channels = (short)channels;
			waveFormat.bitsPerSample = 32;
			waveFormat.sampleRate = sampleRate;
			waveFormat.blockAlign = (short)(4 * channels);
			waveFormat.averageBytesPerSecond = sampleRate * (int)waveFormat.blockAlign;
			waveFormat.extraSize = 0;
			return waveFormat;
		}

		/// <summary>
		/// Helper function to retrieve a WaveFormat structure from a pointer
		/// </summary>
		/// <param name="pointer">WaveFormat structure</param>
		/// <returns></returns>
		// Token: 0x0600088C RID: 2188 RVA: 0x000189FC File Offset: 0x00016BFC
		public static WaveFormat MarshalFromPtr(IntPtr pointer)
		{
			WaveFormat waveFormat = (WaveFormat)Marshal.PtrToStructure(pointer, typeof(WaveFormat));
			WaveFormatEncoding encoding = waveFormat.Encoding;
			switch (encoding)
			{
			case WaveFormatEncoding.Pcm:
				waveFormat.extraSize = 0;
				break;
			case WaveFormatEncoding.Adpcm:
				waveFormat = (AdpcmWaveFormat)Marshal.PtrToStructure(pointer, typeof(AdpcmWaveFormat));
				break;
			default:
				if (encoding != WaveFormatEncoding.Gsm610)
				{
					if (encoding != WaveFormatEncoding.Extensible)
					{
						if (waveFormat.ExtraSize > 0)
						{
							waveFormat = (WaveFormatExtraData)Marshal.PtrToStructure(pointer, typeof(WaveFormatExtraData));
						}
					}
					else
					{
						waveFormat = (WaveFormatExtensible)Marshal.PtrToStructure(pointer, typeof(WaveFormatExtensible));
					}
				}
				else
				{
					waveFormat = (Gsm610WaveFormat)Marshal.PtrToStructure(pointer, typeof(Gsm610WaveFormat));
				}
				break;
			}
			return waveFormat;
		}

		/// <summary>
		/// Helper function to marshal WaveFormat to an IntPtr
		/// </summary>
		/// <param name="format">WaveFormat</param>
		/// <returns>IntPtr to WaveFormat structure (needs to be freed by callee)</returns>
		// Token: 0x0600088D RID: 2189 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public static IntPtr MarshalToPtr(WaveFormat format)
		{
			int cb = Marshal.SizeOf(format);
			IntPtr intPtr = Marshal.AllocHGlobal(cb);
			Marshal.StructureToPtr(format, intPtr, false);
			return intPtr;
		}

		/// <summary>
		/// Reads in a WaveFormat (with extra data) from a fmt chunk (chunk identifier and
		/// length should already have been read)
		/// </summary>
		/// <param name="br">Binary reader</param>
		/// <param name="formatChunkLength">Format chunk length</param>
		/// <returns>A WaveFormatExtraData</returns>
		// Token: 0x0600088E RID: 2190 RVA: 0x00018ADC File Offset: 0x00016CDC
		public static WaveFormat FromFormatChunk(BinaryReader br, int formatChunkLength)
		{
			WaveFormatExtraData waveFormatExtraData = new WaveFormatExtraData();
			waveFormatExtraData.ReadWaveFormat(br, formatChunkLength);
			waveFormatExtraData.ReadExtraData(br);
			return waveFormatExtraData;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00018B00 File Offset: 0x00016D00
		private void ReadWaveFormat(BinaryReader br, int formatChunkLength)
		{
			if (formatChunkLength < 16)
			{
				throw new InvalidDataException("Invalid WaveFormat Structure");
			}
			this.waveFormatTag = (WaveFormatEncoding)br.ReadUInt16();
			this.channels = br.ReadInt16();
			this.sampleRate = br.ReadInt32();
			this.averageBytesPerSecond = br.ReadInt32();
			this.blockAlign = br.ReadInt16();
			this.bitsPerSample = br.ReadInt16();
			if (formatChunkLength > 16)
			{
				this.extraSize = br.ReadInt16();
				if ((int)this.extraSize != formatChunkLength - 18)
				{
					this.extraSize = (short)(formatChunkLength - 18);
				}
			}
		}

		/// <summary>
		/// Reads a new WaveFormat object from a stream
		/// </summary>
		/// <param name="br">A binary reader that wraps the stream</param>
		// Token: 0x06000890 RID: 2192 RVA: 0x00018B90 File Offset: 0x00016D90
		public WaveFormat(BinaryReader br)
		{
			int formatChunkLength = br.ReadInt32();
			this.ReadWaveFormat(br, formatChunkLength);
		}

		/// <summary>
		/// Reports this WaveFormat as a string
		/// </summary>
		/// <returns>String describing the wave format</returns>
		// Token: 0x06000891 RID: 2193 RVA: 0x00018BB4 File Offset: 0x00016DB4
		public override string ToString()
		{
			WaveFormatEncoding waveFormatEncoding = this.waveFormatTag;
			if (waveFormatEncoding == WaveFormatEncoding.Pcm || waveFormatEncoding == WaveFormatEncoding.Extensible)
			{
				return string.Format("{0} bit PCM: {1}kHz {2} channels", this.bitsPerSample, this.sampleRate / 1000, this.channels);
			}
			return this.waveFormatTag.ToString();
		}

		/// <summary>
		/// Compares with another WaveFormat object
		/// </summary>
		/// <param name="obj">Object to compare to</param>
		/// <returns>True if the objects are the same</returns>
		// Token: 0x06000892 RID: 2194 RVA: 0x00018C18 File Offset: 0x00016E18
		public override bool Equals(object obj)
		{
			WaveFormat waveFormat = obj as WaveFormat;
			return waveFormat != null && (this.waveFormatTag == waveFormat.waveFormatTag && this.channels == waveFormat.channels && this.sampleRate == waveFormat.sampleRate && this.averageBytesPerSecond == waveFormat.averageBytesPerSecond && this.blockAlign == waveFormat.blockAlign) && this.bitsPerSample == waveFormat.bitsPerSample;
		}

		/// <summary>
		/// Provides a Hashcode for this WaveFormat
		/// </summary>
		/// <returns>A hashcode</returns>
		// Token: 0x06000893 RID: 2195 RVA: 0x00018C87 File Offset: 0x00016E87
		public override int GetHashCode()
		{
			return (int)(this.waveFormatTag ^ (WaveFormatEncoding)this.channels) ^ this.sampleRate ^ this.averageBytesPerSecond ^ (int)this.blockAlign ^ (int)this.bitsPerSample;
		}

		/// <summary>
		/// Returns the encoding type used
		/// </summary>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00018CB2 File Offset: 0x00016EB2
		public WaveFormatEncoding Encoding
		{
			get
			{
				return this.waveFormatTag;
			}
		}

		/// <summary>
		/// Writes this WaveFormat object to a stream
		/// </summary>
		/// <param name="writer">the output stream</param>
		// Token: 0x06000895 RID: 2197 RVA: 0x00018CBC File Offset: 0x00016EBC
		public virtual void Serialize(BinaryWriter writer)
		{
			writer.Write((int)(18 + this.extraSize));
			writer.Write((short)this.Encoding);
			writer.Write((short)this.Channels);
			writer.Write(this.SampleRate);
			writer.Write(this.AverageBytesPerSecond);
			writer.Write((short)this.BlockAlign);
			writer.Write((short)this.BitsPerSample);
			writer.Write(this.extraSize);
		}

		/// <summary>
		/// Returns the number of channels (1=mono,2=stereo etc)
		/// </summary>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00018D30 File Offset: 0x00016F30
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		/// <summary>
		/// Returns the sample rate (samples per second)
		/// </summary>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00018D38 File Offset: 0x00016F38
		public int SampleRate
		{
			get
			{
				return this.sampleRate;
			}
		}

		/// <summary>
		/// Returns the average number of bytes used per second
		/// </summary>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00018D40 File Offset: 0x00016F40
		public int AverageBytesPerSecond
		{
			get
			{
				return this.averageBytesPerSecond;
			}
		}

		/// <summary>
		/// Returns the block alignment
		/// </summary>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00018D48 File Offset: 0x00016F48
		public virtual int BlockAlign
		{
			get
			{
				return (int)this.blockAlign;
			}
		}

		/// <summary>
		/// Returns the number of bits per sample (usually 16 or 32, sometimes 24 or 8)
		/// Can be 0 for some codecs
		/// </summary>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00018D50 File Offset: 0x00016F50
		public int BitsPerSample
		{
			get
			{
				return (int)this.bitsPerSample;
			}
		}

		/// <summary>
		/// Returns the number of extra bytes used by this waveformat. Often 0,
		/// except for compressed formats which store extra data after the WAVEFORMATEX header
		/// </summary>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00018D58 File Offset: 0x00016F58
		public int ExtraSize
		{
			get
			{
				return (int)this.extraSize;
			}
		}

		/// <summary>format type</summary>
		// Token: 0x040009B5 RID: 2485
		protected WaveFormatEncoding waveFormatTag;

		/// <summary>number of channels</summary>
		// Token: 0x040009B6 RID: 2486
		protected short channels;

		/// <summary>sample rate</summary>
		// Token: 0x040009B7 RID: 2487
		protected int sampleRate;

		/// <summary>for buffer estimation</summary>
		// Token: 0x040009B8 RID: 2488
		protected int averageBytesPerSecond;

		/// <summary>block size of data</summary>
		// Token: 0x040009B9 RID: 2489
		protected short blockAlign;

		/// <summary>number of bits per sample of mono data</summary>
		// Token: 0x040009BA RID: 2490
		protected short bitsPerSample;

		/// <summary>number of following bytes</summary>
		// Token: 0x040009BB RID: 2491
		protected short extraSize;
	}
}
