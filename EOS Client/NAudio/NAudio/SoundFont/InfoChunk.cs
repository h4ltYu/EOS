using System;
using System.IO;

namespace NAudio.SoundFont
{
	/// <summary>
	/// A soundfont info chunk
	/// </summary>
	// Token: 0x020000B5 RID: 181
	public class InfoChunk
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		internal InfoChunk(RiffChunk chunk)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (chunk.ReadChunkID() != "INFO")
			{
				throw new InvalidDataException("Not an INFO chunk");
			}
			RiffChunk nextSubChunk;
			while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
			{
				string chunkID;
				switch (chunkID = nextSubChunk.ChunkID)
				{
				case "ifil":
					flag = true;
					this.verSoundFont = nextSubChunk.GetDataAsStructure<SFVersion>(new SFVersionBuilder());
					continue;
				case "isng":
					flag2 = true;
					this.waveTableSoundEngine = nextSubChunk.GetDataAsString();
					continue;
				case "INAM":
					flag3 = true;
					this.bankName = nextSubChunk.GetDataAsString();
					continue;
				case "irom":
					this.dataROM = nextSubChunk.GetDataAsString();
					continue;
				case "iver":
					this.verROM = nextSubChunk.GetDataAsStructure<SFVersion>(new SFVersionBuilder());
					continue;
				case "ICRD":
					this.creationDate = nextSubChunk.GetDataAsString();
					continue;
				case "IENG":
					this.author = nextSubChunk.GetDataAsString();
					continue;
				case "IPRD":
					this.targetProduct = nextSubChunk.GetDataAsString();
					continue;
				case "ICOP":
					this.copyright = nextSubChunk.GetDataAsString();
					continue;
				case "ICMT":
					this.comments = nextSubChunk.GetDataAsString();
					continue;
				case "ISFT":
					this.tools = nextSubChunk.GetDataAsString();
					continue;
				}
				throw new InvalidDataException(string.Format("Unknown chunk type {0}", nextSubChunk.ChunkID));
			}
			if (!flag)
			{
				throw new InvalidDataException("Missing SoundFont version information");
			}
			if (!flag2)
			{
				throw new InvalidDataException("Missing wavetable sound engine information");
			}
			if (!flag3)
			{
				throw new InvalidDataException("Missing SoundFont name information");
			}
		}

		/// <summary>
		/// SoundFont Version
		/// </summary>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		public SFVersion SoundFontVersion
		{
			get
			{
				return this.verSoundFont;
			}
		}

		/// <summary>
		/// WaveTable sound engine
		/// </summary>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000DA00 File Offset: 0x0000BC00
		public string WaveTableSoundEngine
		{
			get
			{
				return this.waveTableSoundEngine;
			}
			set
			{
				this.waveTableSoundEngine = value;
			}
		}

		/// <summary>
		/// Bank name
		/// </summary>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000DA09 File Offset: 0x0000BC09
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000DA11 File Offset: 0x0000BC11
		public string BankName
		{
			get
			{
				return this.bankName;
			}
			set
			{
				this.bankName = value;
			}
		}

		/// <summary>
		/// Data ROM
		/// </summary>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000DA22 File Offset: 0x0000BC22
		public string DataROM
		{
			get
			{
				return this.dataROM;
			}
			set
			{
				this.dataROM = value;
			}
		}

		/// <summary>
		/// Creation Date
		/// </summary>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000DA2B File Offset: 0x0000BC2B
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000DA33 File Offset: 0x0000BC33
		public string CreationDate
		{
			get
			{
				return this.creationDate;
			}
			set
			{
				this.creationDate = value;
			}
		}

		/// <summary>
		/// Author
		/// </summary>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public string Author
		{
			get
			{
				return this.author;
			}
			set
			{
				this.author = value;
			}
		}

		/// <summary>
		/// Target Product
		/// </summary>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000DA4D File Offset: 0x0000BC4D
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000DA55 File Offset: 0x0000BC55
		public string TargetProduct
		{
			get
			{
				return this.targetProduct;
			}
			set
			{
				this.targetProduct = value;
			}
		}

		/// <summary>
		/// Copyright
		/// </summary>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000DA5E File Offset: 0x0000BC5E
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000DA66 File Offset: 0x0000BC66
		public string Copyright
		{
			get
			{
				return this.copyright;
			}
			set
			{
				this.copyright = value;
			}
		}

		/// <summary>
		/// Comments
		/// </summary>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000DA6F File Offset: 0x0000BC6F
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000DA77 File Offset: 0x0000BC77
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				this.comments = value;
			}
		}

		/// <summary>
		/// Tools
		/// </summary>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000DA80 File Offset: 0x0000BC80
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public string Tools
		{
			get
			{
				return this.tools;
			}
			set
			{
				this.tools = value;
			}
		}

		/// <summary>
		/// ROM Version
		/// </summary>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000DA91 File Offset: 0x0000BC91
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000DA99 File Offset: 0x0000BC99
		public SFVersion ROMVersion
		{
			get
			{
				return this.verROM;
			}
			set
			{
				this.verROM = value;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x06000408 RID: 1032 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public override string ToString()
		{
			return string.Format("Bank Name: {0}\r\nAuthor: {1}\r\nCopyright: {2}\r\nCreation Date: {3}\r\nTools: {4}\r\nComments: {5}\r\nSound Engine: {6}\r\nSoundFont Version: {7}\r\nTarget Product: {8}\r\nData ROM: {9}\r\nROM Version: {10}", new object[]
			{
				this.BankName,
				this.Author,
				this.Copyright,
				this.CreationDate,
				this.Tools,
				"TODO-fix comments",
				this.WaveTableSoundEngine,
				this.SoundFontVersion,
				this.TargetProduct,
				this.DataROM,
				this.ROMVersion
			});
		}

		// Token: 0x040004D6 RID: 1238
		private SFVersion verSoundFont;

		// Token: 0x040004D7 RID: 1239
		private string waveTableSoundEngine;

		// Token: 0x040004D8 RID: 1240
		private string bankName;

		// Token: 0x040004D9 RID: 1241
		private string dataROM;

		// Token: 0x040004DA RID: 1242
		private string creationDate;

		// Token: 0x040004DB RID: 1243
		private string author;

		// Token: 0x040004DC RID: 1244
		private string targetProduct;

		// Token: 0x040004DD RID: 1245
		private string copyright;

		// Token: 0x040004DE RID: 1246
		private string comments;

		// Token: 0x040004DF RID: 1247
		private string tools;

		// Token: 0x040004E0 RID: 1248
		private SFVersion verROM;
	}
}
