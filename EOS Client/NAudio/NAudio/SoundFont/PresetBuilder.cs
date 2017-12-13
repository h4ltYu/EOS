using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
	// Token: 0x020000BF RID: 191
	internal class PresetBuilder : StructureBuilder<Preset>
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public override Preset Read(BinaryReader br)
		{
			Preset preset = new Preset();
			string text = Encoding.UTF8.GetString(br.ReadBytes(20), 0, 20);
			if (text.IndexOf('\0') >= 0)
			{
				text = text.Substring(0, text.IndexOf('\0'));
			}
			preset.Name = text;
			preset.PatchNumber = br.ReadUInt16();
			preset.Bank = br.ReadUInt16();
			preset.startPresetZoneIndex = br.ReadUInt16();
			preset.library = br.ReadUInt32();
			preset.genre = br.ReadUInt32();
			preset.morphology = br.ReadUInt32();
			if (this.lastPreset != null)
			{
				this.lastPreset.endPresetZoneIndex = preset.startPresetZoneIndex - 1;
			}
			this.data.Add(preset);
			this.lastPreset = preset;
			return preset;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DFBD File Offset: 0x0000C1BD
		public override void Write(BinaryWriter bw, Preset preset)
		{
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000DFBF File Offset: 0x0000C1BF
		public override int Length
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public void LoadZones(Zone[] presetZones)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Preset preset = this.data[i];
				preset.Zones = new Zone[(int)(preset.endPresetZoneIndex - preset.startPresetZoneIndex + 1)];
				Array.Copy(presetZones, (int)preset.startPresetZoneIndex, preset.Zones, 0, preset.Zones.Length);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000E043 File Offset: 0x0000C243
		public Preset[] Presets
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x04000509 RID: 1289
		private Preset lastPreset;
	}
}
