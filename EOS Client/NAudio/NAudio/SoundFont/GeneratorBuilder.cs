using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000B3 RID: 179
	internal class GeneratorBuilder : StructureBuilder<Generator>
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		public override Generator Read(BinaryReader br)
		{
			Generator generator = new Generator();
			generator.GeneratorType = (GeneratorEnum)br.ReadUInt16();
			generator.UInt16Amount = br.ReadUInt16();
			this.data.Add(generator);
			return generator;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D720 File Offset: 0x0000B920
		public override void Write(BinaryWriter bw, Generator o)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000D722 File Offset: 0x0000B922
		public override int Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000D725 File Offset: 0x0000B925
		public Generator[] Generators
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D734 File Offset: 0x0000B934
		public void Load(Instrument[] instruments)
		{
			foreach (Generator generator in this.Generators)
			{
				if (generator.GeneratorType == GeneratorEnum.Instrument)
				{
					generator.Instrument = instruments[(int)generator.UInt16Amount];
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D774 File Offset: 0x0000B974
		public void Load(SampleHeader[] sampleHeaders)
		{
			foreach (Generator generator in this.Generators)
			{
				if (generator.GeneratorType == GeneratorEnum.SampleID)
				{
					generator.SampleHeader = sampleHeaders[(int)generator.UInt16Amount];
				}
			}
		}
	}
}
