using System;
using System.IO;
using NAudio.Utils;

namespace NAudio.SoundFont
{
	// Token: 0x020000C4 RID: 196
	internal class SampleHeaderBuilder : StructureBuilder<SampleHeader>
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public override SampleHeader Read(BinaryReader br)
		{
			SampleHeader sampleHeader = new SampleHeader();
			byte[] array = br.ReadBytes(20);
			sampleHeader.SampleName = ByteEncoding.Instance.GetString(array, 0, array.Length);
			sampleHeader.Start = br.ReadUInt32();
			sampleHeader.End = br.ReadUInt32();
			sampleHeader.StartLoop = br.ReadUInt32();
			sampleHeader.EndLoop = br.ReadUInt32();
			sampleHeader.SampleRate = br.ReadUInt32();
			sampleHeader.OriginalPitch = br.ReadByte();
			sampleHeader.PitchCorrection = br.ReadSByte();
			sampleHeader.SampleLink = br.ReadUInt16();
			sampleHeader.SFSampleLink = (SFSampleLink)br.ReadUInt16();
			this.data.Add(sampleHeader);
			return sampleHeader;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000E862 File Offset: 0x0000CA62
		public override void Write(BinaryWriter bw, SampleHeader sampleHeader)
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000E864 File Offset: 0x0000CA64
		public override int Length
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E868 File Offset: 0x0000CA68
		internal void RemoveEOS()
		{
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000E882 File Offset: 0x0000CA82
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.data.ToArray();
			}
		}
	}
}
