using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>A read-only stream of AIFF data based on an aiff file
	/// with an associated WaveFormat
	/// originally contributed to NAudio by Giawa
	/// </summary>
	// Token: 0x020001E6 RID: 486
	public class AiffFileReader : WaveStream
	{
		/// <summary>Supports opening a AIF file</summary>
		/// <remarks>The AIF is of similar nastiness to the WAV format.
		/// This supports basic reading of uncompressed PCM AIF files,
		/// with 8, 16, 24 and 32 bit PCM data.
		/// </remarks>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001F585 File Offset: 0x0001D785
		public AiffFileReader(string aiffFile) : this(File.OpenRead(aiffFile))
		{
			this.ownInput = true;
		}

		/// <summary>
		/// Creates an Aiff File Reader based on an input stream
		/// </summary>
		/// <param name="inputStream">The input stream containing a AIF file including header</param>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001F59C File Offset: 0x0001D79C
		public AiffFileReader(Stream inputStream)
		{
			this.waveStream = inputStream;
			AiffFileReader.ReadAiffHeader(this.waveStream, out this.waveFormat, out this.dataPosition, out this.dataChunkLength, this.chunks);
			this.Position = 0L;
		}

		/// <summary>
		/// Ensures valid AIFF header and then finds data offset.
		/// </summary>
		/// <param name="stream">The stream, positioned at the start of audio data</param>
		/// <param name="format">The format found</param>
		/// <param name="dataChunkPosition">The position of the data chunk</param>
		/// <param name="dataChunkLength">The length of the data chunk</param>
		/// <param name="chunks">Additional chunks found</param>
		// Token: 0x06000ABA RID: 2746 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public static void ReadAiffHeader(Stream stream, out WaveFormat format, out long dataChunkPosition, out int dataChunkLength, List<AiffFileReader.AiffChunk> chunks)
		{
			dataChunkPosition = -1L;
			format = null;
			BinaryReader binaryReader = new BinaryReader(stream);
			if (AiffFileReader.ReadChunkName(binaryReader) != "FORM")
			{
				throw new FormatException("Not an AIFF file - no FORM header.");
			}
			AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
			string a = AiffFileReader.ReadChunkName(binaryReader);
			if (a != "AIFC" && a != "AIFF")
			{
				throw new FormatException("Not an AIFF file - no AIFF/AIFC header.");
			}
			dataChunkLength = 0;
			while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
			{
				AiffFileReader.AiffChunk item = AiffFileReader.ReadChunkHeader(binaryReader);
				if (item.ChunkName == "COMM")
				{
					short channels = AiffFileReader.ConvertShort(binaryReader.ReadBytes(2));
					AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					short bits = AiffFileReader.ConvertShort(binaryReader.ReadBytes(2));
					double num = IEEE.ConvertFromIeeeExtended(binaryReader.ReadBytes(10));
					format = new WaveFormat((int)num, (int)bits, (int)channels);
					if (item.ChunkLength > 18u && a == "AIFC")
					{
						string a2 = new string(binaryReader.ReadChars(4)).ToLower();
						if (a2 != "none")
						{
							throw new FormatException("Compressed AIFC is not supported.");
						}
						binaryReader.ReadBytes((int)(item.ChunkLength - 22u));
					}
					else
					{
						binaryReader.ReadBytes((int)(item.ChunkLength - 18u));
					}
				}
				else if (item.ChunkName == "SSND")
				{
					uint num2 = AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					dataChunkPosition = (long)((ulong)(item.ChunkStart + 16u + num2));
					dataChunkLength = (int)(item.ChunkLength - 8u);
					binaryReader.ReadBytes((int)(item.ChunkLength - 8u));
				}
				else
				{
					if (chunks != null)
					{
						chunks.Add(item);
					}
					binaryReader.ReadBytes((int)item.ChunkLength);
				}
				if (item.ChunkName == "\0\0\0\0")
				{
					break;
				}
			}
			if (format == null)
			{
				throw new FormatException("Invalid AIFF file - No COMM chunk found.");
			}
			if (dataChunkPosition == -1L)
			{
				throw new FormatException("Invalid AIFF file - No SSND chunk found.");
			}
		}

		/// <summary>
		/// Cleans up the resources associated with this AiffFileReader
		/// </summary>
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001F802 File Offset: 0x0001DA02
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.waveStream != null)
			{
				if (this.ownInput)
				{
					this.waveStream.Close();
				}
				this.waveStream = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0001F830 File Offset: 0x0001DA30
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001F838 File Offset: 0x0001DA38
		public override long Length
		{
			get
			{
				return (long)this.dataChunkLength;
			}
		}

		/// <summary>
		/// Number of Samples (if possible to calculate)
		/// </summary>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0001F844 File Offset: 0x0001DA44
		public long SampleCount
		{
			get
			{
				if (this.waveFormat.Encoding == WaveFormatEncoding.Pcm || this.waveFormat.Encoding == WaveFormatEncoding.Extensible || this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					return (long)(this.dataChunkLength / this.BlockAlign);
				}
				throw new FormatException("Sample count is calculated only for the standard encodings");
			}
		}

		/// <summary>
		/// Position in the AIFF file
		/// <see cref="P:System.IO.Stream.Position" />
		/// </summary>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001F898 File Offset: 0x0001DA98
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		public override long Position
		{
			get
			{
				return this.waveStream.Position - this.dataPosition;
			}
			set
			{
				lock (this.lockObject)
				{
					value = Math.Min(value, this.Length);
					value -= value % (long)this.waveFormat.BlockAlign;
					this.waveStream.Position = value + this.dataPosition;
				}
			}
		}

		/// <summary>
		/// Reads bytes from the AIFF File
		/// <see cref="M:System.IO.Stream.Read(System.Byte[],System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001F914 File Offset: 0x0001DB14
		public override int Read(byte[] array, int offset, int count)
		{
			if (count % this.waveFormat.BlockAlign != 0)
			{
				throw new ArgumentException(string.Format("Must read complete blocks: requested {0}, block align is {1}", count, this.WaveFormat.BlockAlign));
			}
			int result;
			lock (this.lockObject)
			{
				if (this.Position + (long)count > (long)this.dataChunkLength)
				{
					count = this.dataChunkLength - (int)this.Position;
				}
				byte[] array2 = new byte[count];
				int num = this.waveStream.Read(array2, offset, count);
				int num2 = this.WaveFormat.BitsPerSample / 8;
				for (int i = 0; i < num; i += num2)
				{
					if (this.WaveFormat.BitsPerSample == 8)
					{
						array[i] = array2[i];
					}
					else if (this.WaveFormat.BitsPerSample == 16)
					{
						array[i] = array2[i + 1];
						array[i + 1] = array2[i];
					}
					else if (this.WaveFormat.BitsPerSample == 24)
					{
						array[i] = array2[i + 2];
						array[i + 1] = array2[i + 1];
						array[i + 2] = array2[i];
					}
					else
					{
						if (this.WaveFormat.BitsPerSample != 32)
						{
							throw new FormatException("Unsupported PCM format.");
						}
						array[i] = array2[i + 3];
						array[i + 1] = array2[i + 2];
						array[i + 2] = array2[i + 1];
						array[i + 3] = array2[i];
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		private static uint ConvertInt(byte[] buffer)
		{
			if (buffer.Length != 4)
			{
				throw new Exception("Incorrect length for long.");
			}
			return (uint)((int)buffer[0] << 24 | (int)buffer[1] << 16 | (int)buffer[2] << 8 | (int)buffer[3]);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001FAB6 File Offset: 0x0001DCB6
		private static short ConvertShort(byte[] buffer)
		{
			if (buffer.Length != 2)
			{
				throw new Exception("Incorrect length for int.");
			}
			return (short)((int)buffer[0] << 8 | (int)buffer[1]);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		private static AiffFileReader.AiffChunk ReadChunkHeader(BinaryReader br)
		{
			AiffFileReader.AiffChunk result = new AiffFileReader.AiffChunk((uint)br.BaseStream.Position, AiffFileReader.ReadChunkName(br), AiffFileReader.ConvertInt(br.ReadBytes(4)));
			return result;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001FB07 File Offset: 0x0001DD07
		private static string ReadChunkName(BinaryReader br)
		{
			return new string(br.ReadChars(4));
		}

		// Token: 0x04000B80 RID: 2944
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B81 RID: 2945
		private readonly bool ownInput;

		// Token: 0x04000B82 RID: 2946
		private readonly long dataPosition;

		// Token: 0x04000B83 RID: 2947
		private readonly int dataChunkLength;

		// Token: 0x04000B84 RID: 2948
		private readonly List<AiffFileReader.AiffChunk> chunks = new List<AiffFileReader.AiffChunk>();

		// Token: 0x04000B85 RID: 2949
		private Stream waveStream;

		// Token: 0x04000B86 RID: 2950
		private readonly object lockObject = new object();

		/// <summary>
		/// AIFF Chunk
		/// </summary>
		// Token: 0x020001E7 RID: 487
		public struct AiffChunk
		{
			/// <summary>
			/// Creates a new AIFF Chunk
			/// </summary>
			// Token: 0x06000AC6 RID: 2758 RVA: 0x0001FB15 File Offset: 0x0001DD15
			public AiffChunk(uint start, string name, uint length)
			{
				this.ChunkStart = start;
				this.ChunkName = name;
				this.ChunkLength = length + ((length % 2u == 1u) ? 1u : 0u);
			}

			/// <summary>
			/// Chunk Name
			/// </summary>
			// Token: 0x04000B87 RID: 2951
			public string ChunkName;

			/// <summary>
			/// Chunk Length
			/// </summary>
			// Token: 0x04000B88 RID: 2952
			public uint ChunkLength;

			/// <summary>
			/// Chunk start
			/// </summary>
			// Token: 0x04000B89 RID: 2953
			public uint ChunkStart;
		}
	}
}
