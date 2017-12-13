using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Instrument Builder
	/// </summary>
	// Token: 0x020000B7 RID: 183
	internal class InstrumentBuilder : StructureBuilder<Instrument>
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public override Instrument Read(BinaryReader br)
		{
			Instrument instrument = new Instrument();
			string text = Encoding.UTF8.GetString(br.ReadBytes(20), 0, 20);
			if (text.IndexOf('\0') >= 0)
			{
				text = text.Substring(0, text.IndexOf('\0'));
			}
			instrument.Name = text;
			instrument.startInstrumentZoneIndex = br.ReadUInt16();
			if (this.lastInstrument != null)
			{
				this.lastInstrument.endInstrumentZoneIndex = instrument.startInstrumentZoneIndex - 1;
			}
			this.data.Add(instrument);
			this.lastInstrument = instrument;
			return instrument;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
		public override void Write(BinaryWriter bw, Instrument instrument)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000DBE3 File Offset: 0x0000BDE3
		public override int Length
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public void LoadZones(Zone[] zones)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Instrument instrument = this.data[i];
				instrument.Zones = new Zone[(int)(instrument.endInstrumentZoneIndex - instrument.startInstrumentZoneIndex + 1)];
				Array.Copy(zones, (int)instrument.startInstrumentZoneIndex, instrument.Zones, 0, instrument.Zones.Length);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000DC67 File Offset: 0x0000BE67
		public Instrument[] Instruments
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x040004E5 RID: 1253
		private Instrument lastInstrument;
	}
}
