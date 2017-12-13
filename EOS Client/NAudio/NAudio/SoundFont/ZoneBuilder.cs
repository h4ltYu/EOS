using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000CB RID: 203
	internal class ZoneBuilder : StructureBuilder<Zone>
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public override Zone Read(BinaryReader br)
		{
			Zone zone = new Zone();
			zone.generatorIndex = br.ReadUInt16();
			zone.modulatorIndex = br.ReadUInt16();
			if (this.lastZone != null)
			{
				this.lastZone.generatorCount = zone.generatorIndex - this.lastZone.generatorIndex;
				this.lastZone.modulatorCount = zone.modulatorIndex - this.lastZone.modulatorIndex;
			}
			this.data.Add(zone);
			this.lastZone = zone;
			return zone;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public override void Write(BinaryWriter bw, Zone zone)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		public void Load(Modulator[] modulators, Generator[] generators)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Zone zone = this.data[i];
				zone.Generators = new Generator[(int)zone.generatorCount];
				Array.Copy(generators, (int)zone.generatorIndex, zone.Generators, 0, (int)zone.generatorCount);
				zone.Modulators = new Modulator[(int)zone.modulatorCount];
				Array.Copy(modulators, (int)zone.modulatorIndex, zone.Modulators, 0, (int)zone.modulatorCount);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		public Zone[] Zones
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000EC27 File Offset: 0x0000CE27
		public override int Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0400053B RID: 1339
		private Zone lastZone;
	}
}
