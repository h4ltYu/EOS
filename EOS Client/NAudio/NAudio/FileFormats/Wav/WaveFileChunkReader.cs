using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.FileFormats.Wav
{
	// Token: 0x0200003E RID: 62
	internal class WaveFileChunkReader
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00006891 File Offset: 0x00004A91
		public WaveFileChunkReader()
		{
			this.storeAllChunks = true;
			this.strictMode = false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000068A8 File Offset: 0x00004AA8
		public void ReadWaveHeader(Stream stream)
		{
			this.dataChunkPosition = -1L;
			this.waveFormat = null;
			this.riffChunks = new List<RiffChunk>();
			this.dataChunkLength = 0L;
			BinaryReader binaryReader = new BinaryReader(stream);
			this.ReadRiffHeader(binaryReader);
			this.riffSize = (long)((ulong)binaryReader.ReadUInt32());
			if (binaryReader.ReadInt32() != ChunkIdentifier.ChunkIdentifierToInt32("WAVE"))
			{
				throw new FormatException("Not a WAVE file - no WAVE header");
			}
			if (this.isRf64)
			{
				this.ReadDs64Chunk(binaryReader);
			}
			int num = ChunkIdentifier.ChunkIdentifierToInt32("data");
			int num2 = ChunkIdentifier.ChunkIdentifierToInt32("fmt ");
			long num3 = Math.Min(this.riffSize + 8L, stream.Length);
			while (stream.Position <= num3 - 8L)
			{
				int num4 = binaryReader.ReadInt32();
				uint num5 = binaryReader.ReadUInt32();
				if (num4 == num)
				{
					this.dataChunkPosition = stream.Position;
					if (!this.isRf64)
					{
						this.dataChunkLength = (long)((ulong)num5);
					}
					stream.Position += (long)((ulong)num5);
				}
				else if (num4 == num2)
				{
					if (num5 > 2147483647u)
					{
						throw new InvalidDataException(string.Format("Format chunk length must be between 0 and {0}.", int.MaxValue));
					}
					this.waveFormat = WaveFormat.FromFormatChunk(binaryReader, (int)num5);
				}
				else if ((ulong)num5 > (ulong)(stream.Length - stream.Position))
				{
					if (this.strictMode)
					{
						break;
					}
					break;
				}
				else
				{
					if (this.storeAllChunks)
					{
						if (num5 > 2147483647u)
						{
							throw new InvalidDataException(string.Format("RiffChunk chunk length must be between 0 and {0}.", int.MaxValue));
						}
						this.riffChunks.Add(WaveFileChunkReader.GetRiffChunk(stream, num4, (int)num5));
					}
					stream.Position += (long)((ulong)num5);
				}
			}
			if (this.waveFormat == null)
			{
				throw new FormatException("Invalid WAV file - No fmt chunk found");
			}
			if (this.dataChunkPosition == -1L)
			{
				throw new FormatException("Invalid WAV file - No data chunk found");
			}
		}

		/// <summary>
		/// http://tech.ebu.ch/docs/tech/tech3306-2009.pdf
		/// </summary>
		// Token: 0x06000118 RID: 280 RVA: 0x00006A70 File Offset: 0x00004C70
		private void ReadDs64Chunk(BinaryReader reader)
		{
			int num = ChunkIdentifier.ChunkIdentifierToInt32("ds64");
			int num2 = reader.ReadInt32();
			if (num2 != num)
			{
				throw new FormatException("Invalid RF64 WAV file - No ds64 chunk found");
			}
			int num3 = reader.ReadInt32();
			this.riffSize = reader.ReadInt64();
			this.dataChunkLength = reader.ReadInt64();
			reader.ReadInt64();
			reader.ReadBytes(num3 - 24);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006ACF File Offset: 0x00004CCF
		private static RiffChunk GetRiffChunk(Stream stream, int chunkIdentifier, int chunkLength)
		{
			return new RiffChunk(chunkIdentifier, chunkLength, stream.Position);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006AE0 File Offset: 0x00004CE0
		private void ReadRiffHeader(BinaryReader br)
		{
			int num = br.ReadInt32();
			if (num == ChunkIdentifier.ChunkIdentifierToInt32("RF64"))
			{
				this.isRf64 = true;
				return;
			}
			if (num != ChunkIdentifier.ChunkIdentifierToInt32("RIFF"))
			{
				throw new FormatException("Not a WAVE file - no RIFF header");
			}
		}

		/// <summary>
		/// WaveFormat
		/// </summary>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006B21 File Offset: 0x00004D21
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Data Chunk Position
		/// </summary>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006B29 File Offset: 0x00004D29
		public long DataChunkPosition
		{
			get
			{
				return this.dataChunkPosition;
			}
		}

		/// <summary>
		/// Data Chunk Length
		/// </summary>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006B31 File Offset: 0x00004D31
		public long DataChunkLength
		{
			get
			{
				return this.dataChunkLength;
			}
		}

		/// <summary>
		/// Riff Chunks
		/// </summary>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006B39 File Offset: 0x00004D39
		public List<RiffChunk> RiffChunks
		{
			get
			{
				return this.riffChunks;
			}
		}

		// Token: 0x040000E9 RID: 233
		private WaveFormat waveFormat;

		// Token: 0x040000EA RID: 234
		private long dataChunkPosition;

		// Token: 0x040000EB RID: 235
		private long dataChunkLength;

		// Token: 0x040000EC RID: 236
		private List<RiffChunk> riffChunks;

		// Token: 0x040000ED RID: 237
		private readonly bool strictMode;

		// Token: 0x040000EE RID: 238
		private bool isRf64;

		// Token: 0x040000EF RID: 239
		private readonly bool storeAllChunks;

		// Token: 0x040000F0 RID: 240
		private long riffSize;
	}
}
