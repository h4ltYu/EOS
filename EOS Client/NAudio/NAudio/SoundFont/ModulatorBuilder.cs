using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000BA RID: 186
	internal class ModulatorBuilder : StructureBuilder<Modulator>
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public override Modulator Read(BinaryReader br)
		{
			Modulator modulator = new Modulator();
			modulator.SourceModulationData = new ModulatorType(br.ReadUInt16());
			modulator.DestinationGenerator = (GeneratorEnum)br.ReadUInt16();
			modulator.Amount = br.ReadInt16();
			modulator.SourceModulationAmount = new ModulatorType(br.ReadUInt16());
			modulator.SourceTransform = (TransformEnum)br.ReadUInt16();
			this.data.Add(modulator);
			return modulator;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DD9E File Offset: 0x0000BF9E
		public override void Write(BinaryWriter bw, Modulator o)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public override int Length
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public Modulator[] Modulators
		{
			get
			{
				return this.data.ToArray();
			}
		}
	}
}
