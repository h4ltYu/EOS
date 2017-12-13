using System;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// This class writes audio data to a .aif file on disk
	/// </summary>
	// Token: 0x020001AF RID: 431
	public class AiffFileWriter : Stream
	{
		/// <summary>
		/// Creates an Aiff file by reading all the data from a WaveProvider
		/// BEWARE: the WaveProvider MUST return 0 from its Read method when it is finished,
		/// or the Aiff File will grow indefinitely.
		/// </summary>
		/// <param name="filename">The filename to use</param>
		/// <param name="sourceProvider">The source WaveProvider</param>
		// Token: 0x060008C0 RID: 2240 RVA: 0x000193A0 File Offset: 0x000175A0
		public static void CreateAiffFile(string filename, WaveStream sourceProvider)
		{
			using (AiffFileWriter aiffFileWriter = new AiffFileWriter(filename, sourceProvider.WaveFormat))
			{
				byte[] array = new byte[16384];
				while (sourceProvider.Position < sourceProvider.Length)
				{
					int count = Math.Min((int)(sourceProvider.Length - sourceProvider.Position), array.Length);
					int num = sourceProvider.Read(array, 0, count);
					if (num == 0)
					{
						break;
					}
					aiffFileWriter.Write(array, 0, num);
				}
			}
		}

		/// <summary>
		/// AiffFileWriter that actually writes to a stream
		/// </summary>
		/// <param name="outStream">Stream to be written to</param>
		/// <param name="format">Wave format to use</param>
		// Token: 0x060008C1 RID: 2241 RVA: 0x00019420 File Offset: 0x00017620
		public AiffFileWriter(Stream outStream, WaveFormat format)
		{
			this.outStream = outStream;
			this.format = format;
			this.writer = new BinaryWriter(outStream, Encoding.UTF8);
			this.writer.Write(Encoding.UTF8.GetBytes("FORM"));
			this.writer.Write(0);
			this.writer.Write(Encoding.UTF8.GetBytes("AIFF"));
			this.CreateCommChunk();
			this.WriteSsndChunkHeader();
		}

		/// <summary>
		/// Creates a new AiffFileWriter
		/// </summary>
		/// <param name="filename">The filename to write to</param>
		/// <param name="format">The Wave Format of the output data</param>
		// Token: 0x060008C2 RID: 2242 RVA: 0x000194B1 File Offset: 0x000176B1
		public AiffFileWriter(string filename, WaveFormat format) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), format)
		{
			this.filename = filename;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000194CC File Offset: 0x000176CC
		private void WriteSsndChunkHeader()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("SSND"));
			this.dataSizePos = this.outStream.Position;
			this.writer.Write(0);
			this.writer.Write(0);
			this.writer.Write(this.SwapEndian(this.format.BlockAlign));
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00019538 File Offset: 0x00017738
		private byte[] SwapEndian(short n)
		{
			return new byte[]
			{
				(byte)(n >> 8),
				(byte)(n & 255)
			};
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00019560 File Offset: 0x00017760
		private byte[] SwapEndian(int n)
		{
			return new byte[]
			{
				(byte)(n >> 24 & 255),
				(byte)(n >> 16 & 255),
				(byte)(n >> 8 & 255),
				(byte)(n & 255)
			};
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000195AC File Offset: 0x000177AC
		private void CreateCommChunk()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("COMM"));
			this.writer.Write(this.SwapEndian(18));
			this.writer.Write(this.SwapEndian((short)this.format.Channels));
			this.commSampleCountPos = this.outStream.Position;
			this.writer.Write(0);
			this.writer.Write(this.SwapEndian((short)this.format.BitsPerSample));
			this.writer.Write(IEEE.ConvertToIeeeExtended((double)this.format.SampleRate));
		}

		/// <summary>
		/// The aiff file name or null if not applicable
		/// </summary>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00019659 File Offset: 0x00017859
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>
		/// Number of bytes of audio in the data chunk
		/// </summary>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00019661 File Offset: 0x00017861
		public override long Length
		{
			get
			{
				return (long)this.dataChunkSize;
			}
		}

		/// <summary>
		/// WaveFormat of this aiff file
		/// </summary>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001966A File Offset: 0x0001786A
		public WaveFormat WaveFormat
		{
			get
			{
				return this.format;
			}
		}

		/// <summary>
		/// Returns false: Cannot read from a AiffFileWriter
		/// </summary>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00019672 File Offset: 0x00017872
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true: Can write to a AiffFileWriter
		/// </summary>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00019675 File Offset: 0x00017875
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Returns false: Cannot seek within a AiffFileWriter
		/// </summary>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00019678 File Offset: 0x00017878
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Read is not supported for a AiffFileWriter
		/// </summary>
		// Token: 0x060008CD RID: 2253 RVA: 0x0001967B File Offset: 0x0001787B
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Cannot read from an AiffFileWriter");
		}

		/// <summary>
		/// Seek is not supported for a AiffFileWriter
		/// </summary>
		// Token: 0x060008CE RID: 2254 RVA: 0x00019687 File Offset: 0x00017887
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Cannot seek within an AiffFileWriter");
		}

		/// <summary>
		/// SetLength is not supported for AiffFileWriter
		/// </summary>
		/// <param name="value"></param>
		// Token: 0x060008CF RID: 2255 RVA: 0x00019693 File Offset: 0x00017893
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("Cannot set length of an AiffFileWriter");
		}

		/// <summary>
		/// Gets the Position in the AiffFile (i.e. number of bytes written so far)
		/// </summary>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001969F File Offset: 0x0001789F
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x000196A8 File Offset: 0x000178A8
		public override long Position
		{
			get
			{
				return (long)this.dataChunkSize;
			}
			set
			{
				throw new InvalidOperationException("Repositioning an AiffFileWriter is not supported");
			}
		}

		/// <summary>
		/// Appends bytes to the AiffFile (assumes they are already in the correct format)
		/// </summary>
		/// <param name="data">the buffer containing the wave data</param>
		/// <param name="offset">the offset from which to start writing</param>
		/// <param name="count">the number of bytes to write</param>
		// Token: 0x060008D2 RID: 2258 RVA: 0x000196B4 File Offset: 0x000178B4
		public override void Write(byte[] data, int offset, int count)
		{
			byte[] array = new byte[data.Length];
			int num = this.format.BitsPerSample / 8;
			for (int i = 0; i < data.Length; i++)
			{
				int num2 = (int)Math.Floor((double)i / (double)num) * num + (num - i % num - 1);
				array[i] = data[num2];
			}
			this.outStream.Write(array, offset, count);
			this.dataChunkSize += count;
		}

		/// <summary>
		/// Writes a single sample to the Aiff file
		/// </summary>
		/// <param name="sample">the sample to write (assumed floating point with 1.0f as max value)</param>
		// Token: 0x060008D3 RID: 2259 RVA: 0x00019720 File Offset: 0x00017920
		public void WriteSample(float sample)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				this.writer.Write(this.SwapEndian((short)(32767f * sample)));
				this.dataChunkSize += 2;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				byte[] bytes = BitConverter.GetBytes((int)(2.14748365E+09f * sample));
				this.value24[2] = bytes[1];
				this.value24[1] = bytes[2];
				this.value24[0] = bytes[3];
				this.writer.Write(this.value24);
				this.dataChunkSize += 3;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				this.writer.Write(this.SwapEndian(65535 * (int)sample));
				this.dataChunkSize += 4;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		/// <summary>
		/// Writes 32 bit floating point samples to the Aiff file
		/// They will be converted to the appropriate bit depth depending on the WaveFormat of the AIF file
		/// </summary>
		/// <param name="samples">The buffer containing the floating point samples</param>
		/// <param name="offset">The offset from which to start writing</param>
		/// <param name="count">The number of floating point samples to write</param>
		// Token: 0x060008D4 RID: 2260 RVA: 0x00019818 File Offset: 0x00017A18
		public void WriteSamples(float[] samples, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteSample(samples[offset + i]);
			}
		}

		/// <summary>
		/// Writes 16 bit samples to the Aiff file
		/// </summary>
		/// <param name="samples">The buffer containing the 16 bit samples</param>
		/// <param name="offset">The offset from which to start writing</param>
		/// <param name="count">The number of 16 bit samples to write</param>
		// Token: 0x060008D5 RID: 2261 RVA: 0x0001983C File Offset: 0x00017A3C
		public void WriteSamples(short[] samples, int offset, int count)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				for (int i = 0; i < count; i++)
				{
					this.writer.Write(this.SwapEndian(samples[i + offset]));
				}
				this.dataChunkSize += count * 2;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				for (int j = 0; j < count; j++)
				{
					byte[] bytes = BitConverter.GetBytes(65535 * (int)samples[j + offset]);
					this.value24[2] = bytes[1];
					this.value24[1] = bytes[2];
					this.value24[0] = bytes[3];
					this.writer.Write(this.value24);
				}
				this.dataChunkSize += count * 3;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				for (int k = 0; k < count; k++)
				{
					this.writer.Write(this.SwapEndian(65535 * (int)samples[k + offset]));
				}
				this.dataChunkSize += count * 4;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM audio data supported");
		}

		/// <summary>
		/// Ensures data is written to disk
		/// </summary>
		// Token: 0x060008D6 RID: 2262 RVA: 0x0001995E File Offset: 0x00017B5E
		public override void Flush()
		{
			this.writer.Flush();
		}

		/// <summary>
		/// Actually performs the close,making sure the header contains the correct data
		/// </summary>
		/// <param name="disposing">True if called from <see>Dispose</see></param>
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001996C File Offset: 0x00017B6C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.outStream != null)
			{
				try
				{
					this.UpdateHeader(this.writer);
				}
				finally
				{
					this.outStream.Close();
					this.outStream = null;
				}
			}
		}

		/// <summary>
		/// Updates the header with file size information
		/// </summary>
		// Token: 0x060008D8 RID: 2264 RVA: 0x000199B8 File Offset: 0x00017BB8
		protected virtual void UpdateHeader(BinaryWriter writer)
		{
			this.Flush();
			writer.Seek(4, SeekOrigin.Begin);
			writer.Write(this.SwapEndian((int)(this.outStream.Length - 8L)));
			this.UpdateCommChunk(writer);
			this.UpdateSsndChunk(writer);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000199F2 File Offset: 0x00017BF2
		private void UpdateCommChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.commSampleCountPos, SeekOrigin.Begin);
			writer.Write(this.SwapEndian(this.dataChunkSize * 8 / this.format.BitsPerSample / this.format.Channels));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00019A2F File Offset: 0x00017C2F
		private void UpdateSsndChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.dataSizePos, SeekOrigin.Begin);
			writer.Write(this.SwapEndian(this.dataChunkSize));
		}

		/// <summary>
		/// Finaliser - should only be called if the user forgot to close this AiffFileWriter
		/// </summary>
		// Token: 0x060008DB RID: 2267 RVA: 0x00019A54 File Offset: 0x00017C54
		~AiffFileWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000A7E RID: 2686
		private Stream outStream;

		// Token: 0x04000A7F RID: 2687
		private BinaryWriter writer;

		// Token: 0x04000A80 RID: 2688
		private long dataSizePos;

		// Token: 0x04000A81 RID: 2689
		private long commSampleCountPos;

		// Token: 0x04000A82 RID: 2690
		private int dataChunkSize = 8;

		// Token: 0x04000A83 RID: 2691
		private WaveFormat format;

		// Token: 0x04000A84 RID: 2692
		private string filename;

		// Token: 0x04000A85 RID: 2693
		private byte[] value24 = new byte[3];
	}
}
