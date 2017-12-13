using System;
using System.IO;
using NAudio.Utils;

namespace NAudio.SoundFont
{
	// Token: 0x020000C1 RID: 193
	internal class RiffChunk
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0000E474 File Offset: 0x0000C674
		public static RiffChunk GetTopLevelChunk(BinaryReader file)
		{
			RiffChunk riffChunk = new RiffChunk(file);
			riffChunk.ReadChunk();
			return riffChunk;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000E48F File Offset: 0x0000C68F
		private RiffChunk(BinaryReader file)
		{
			this.riffFile = file;
			this.chunkID = "????";
			this.chunkSize = 0u;
			this.dataOffset = 0L;
		}

		/// <summary>
		/// just reads a chunk ID at the current position
		/// </summary>
		/// <returns>chunk ID</returns>
		// Token: 0x0600043F RID: 1087 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		public string ReadChunkID()
		{
			byte[] array = this.riffFile.ReadBytes(4);
			if (array.Length != 4)
			{
				throw new InvalidDataException("Couldn't read Chunk ID");
			}
			return ByteEncoding.Instance.GetString(array, 0, array.Length);
		}

		/// <summary>
		/// reads a chunk at the current position
		/// </summary>
		// Token: 0x06000440 RID: 1088 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		private void ReadChunk()
		{
			this.chunkID = this.ReadChunkID();
			this.chunkSize = this.riffFile.ReadUInt32();
			this.dataOffset = this.riffFile.BaseStream.Position;
		}

		/// <summary>
		/// creates a new riffchunk from current position checking that we're not
		/// at the end of this chunk first
		/// </summary>
		/// <returns>the new chunk</returns>
		// Token: 0x06000441 RID: 1089 RVA: 0x0000E528 File Offset: 0x0000C728
		public RiffChunk GetNextSubChunk()
		{
			if (this.riffFile.BaseStream.Position + 8L < this.dataOffset + (long)((ulong)this.chunkSize))
			{
				RiffChunk riffChunk = new RiffChunk(this.riffFile);
				riffChunk.ReadChunk();
				return riffChunk;
			}
			return null;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000E570 File Offset: 0x0000C770
		public byte[] GetData()
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			byte[] array = this.riffFile.ReadBytes((int)this.chunkSize);
			if ((long)array.Length != (long)((ulong)this.chunkSize))
			{
				throw new InvalidDataException(string.Format("Couldn't read chunk's data Chunk: {0}, read {1} bytes", this, array.Length));
			}
			return array;
		}

		/// <summary>
		/// useful for chunks that just contain a string
		/// </summary>
		/// <returns>chunk as string</returns>
		// Token: 0x06000443 RID: 1091 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public string GetDataAsString()
		{
			byte[] data = this.GetData();
			if (data == null)
			{
				return null;
			}
			return ByteEncoding.Instance.GetString(data, 0, data.Length);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public T GetDataAsStructure<T>(StructureBuilder<T> s)
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			if ((long)s.Length != (long)((ulong)this.chunkSize))
			{
				throw new InvalidDataException(string.Format("Chunk size is: {0} so can't read structure of: {1}", this.chunkSize, s.Length));
			}
			return s.Read(this.riffFile);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000E65C File Offset: 0x0000C85C
		public T[] GetDataAsStructureArray<T>(StructureBuilder<T> s)
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			if ((ulong)this.chunkSize % (ulong)((long)s.Length) != 0UL)
			{
				throw new InvalidDataException(string.Format("Chunk size is: {0} not a multiple of structure size: {1}", this.chunkSize, s.Length));
			}
			int num = (int)((ulong)this.chunkSize / (ulong)((long)s.Length));
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = s.Read(this.riffFile);
			}
			return array;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		public string ChunkID
		{
			get
			{
				return this.chunkID;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ChunkID may not be null");
				}
				if (value.Length != 4)
				{
					throw new ArgumentException("ChunkID must be four characters");
				}
				this.chunkID = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000E723 File Offset: 0x0000C923
		public uint ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000E72B File Offset: 0x0000C92B
		public long DataOffset
		{
			get
			{
				return this.dataOffset;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000E733 File Offset: 0x0000C933
		public override string ToString()
		{
			return string.Format("RiffChunk ID: {0} Size: {1} Data Offset: {2}", this.ChunkID, this.ChunkSize, this.DataOffset);
		}

		// Token: 0x04000513 RID: 1299
		private string chunkID;

		// Token: 0x04000514 RID: 1300
		private uint chunkSize;

		// Token: 0x04000515 RID: 1301
		private long dataOffset;

		// Token: 0x04000516 RID: 1302
		private BinaryReader riffFile;
	}
}
