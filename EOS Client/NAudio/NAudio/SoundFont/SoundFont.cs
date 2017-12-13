using System;
using System.IO;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Represents a SoundFont
	/// </summary>
	// Token: 0x020000C9 RID: 201
	public class SoundFont
	{
		/// <summary>
		/// Loads a SoundFont from a file
		/// </summary>
		/// <param name="fileName">Filename of the SoundFont</param>
		// Token: 0x0600045E RID: 1118 RVA: 0x0000E921 File Offset: 0x0000CB21
		public SoundFont(string fileName) : this(new FileStream(fileName, FileMode.Open, FileAccess.Read))
		{
		}

		/// <summary>
		/// Loads a SoundFont from a stream
		/// </summary>
		/// <param name="sfFile">stream</param>
		// Token: 0x0600045F RID: 1119 RVA: 0x0000E934 File Offset: 0x0000CB34
		public SoundFont(Stream sfFile)
		{
			try
			{
				RiffChunk topLevelChunk = RiffChunk.GetTopLevelChunk(new BinaryReader(sfFile));
				if (!(topLevelChunk.ChunkID == "RIFF"))
				{
					throw new InvalidDataException("Not a RIFF file");
				}
				string text = topLevelChunk.ReadChunkID();
				if (text != "sfbk")
				{
					throw new InvalidDataException(string.Format("Not a SoundFont ({0})", text));
				}
				RiffChunk nextSubChunk = topLevelChunk.GetNextSubChunk();
				if (!(nextSubChunk.ChunkID == "LIST"))
				{
					throw new InvalidDataException(string.Format("Not info list found ({0})", nextSubChunk.ChunkID));
				}
				this.info = new InfoChunk(nextSubChunk);
				RiffChunk nextSubChunk2 = topLevelChunk.GetNextSubChunk();
				this.sampleData = new SampleDataChunk(nextSubChunk2);
				nextSubChunk2 = topLevelChunk.GetNextSubChunk();
				this.presetsChunk = new PresetsChunk(nextSubChunk2);
			}
			finally
			{
				if (sfFile != null)
				{
					((IDisposable)sfFile).Dispose();
				}
			}
		}

		/// <summary>
		/// The File Info Chunk
		/// </summary>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public InfoChunk FileInfo
		{
			get
			{
				return this.info;
			}
		}

		/// <summary>
		/// The Presets
		/// </summary>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public Preset[] Presets
		{
			get
			{
				return this.presetsChunk.Presets;
			}
		}

		/// <summary>
		/// The Instruments
		/// </summary>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000EA31 File Offset: 0x0000CC31
		public Instrument[] Instruments
		{
			get
			{
				return this.presetsChunk.Instruments;
			}
		}

		/// <summary>
		/// The Sample Headers
		/// </summary>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000EA3E File Offset: 0x0000CC3E
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.presetsChunk.SampleHeaders;
			}
		}

		/// <summary>
		/// The Sample Data
		/// </summary>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		public byte[] SampleData
		{
			get
			{
				return this.sampleData.SampleData;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x06000465 RID: 1125 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public override string ToString()
		{
			return string.Format("Info Chunk:\r\n{0}\r\nPresets Chunk:\r\n{1}", this.info, this.presetsChunk);
		}

		// Token: 0x04000532 RID: 1330
		private InfoChunk info;

		// Token: 0x04000533 RID: 1331
		private PresetsChunk presetsChunk;

		// Token: 0x04000534 RID: 1332
		private SampleDataChunk sampleData;
	}
}
