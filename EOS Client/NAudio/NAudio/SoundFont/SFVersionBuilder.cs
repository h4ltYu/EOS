using System;
using System.IO;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Builds a SoundFont version
	/// </summary>
	// Token: 0x020000C8 RID: 200
	internal class SFVersionBuilder : StructureBuilder<SFVersion>
	{
		/// <summary>
		/// Reads a SoundFont Version structure
		/// </summary>
		// Token: 0x0600045A RID: 1114 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public override SFVersion Read(BinaryReader br)
		{
			SFVersion sfversion = new SFVersion();
			sfversion.Major = br.ReadInt16();
			sfversion.Minor = br.ReadInt16();
			this.data.Add(sfversion);
			return sfversion;
		}

		/// <summary>
		/// Writes a SoundFont Version structure
		/// </summary>
		// Token: 0x0600045B RID: 1115 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public override void Write(BinaryWriter bw, SFVersion v)
		{
			bw.Write(v.Major);
			bw.Write(v.Minor);
		}

		/// <summary>
		/// Gets the length of this structure
		/// </summary>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000E916 File Offset: 0x0000CB16
		public override int Length
		{
			get
			{
				return 4;
			}
		}
	}
}
