using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Class to read the SoundFont file presets chunk
	/// </summary>
	// Token: 0x020000C0 RID: 192
	public class PresetsChunk
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000E058 File Offset: 0x0000C258
		internal PresetsChunk(RiffChunk chunk)
		{
			string text = chunk.ReadChunkID();
			if (text != "pdta")
			{
				throw new InvalidDataException(string.Format("Not a presets data chunk ({0})", text));
			}
			RiffChunk nextSubChunk;
			while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
			{
				string chunkID;
				switch (chunkID = nextSubChunk.ChunkID)
				{
				case "PHDR":
				case "phdr":
					nextSubChunk.GetDataAsStructureArray<Preset>(this.presetHeaders);
					continue;
				case "PBAG":
				case "pbag":
					nextSubChunk.GetDataAsStructureArray<Zone>(this.presetZones);
					continue;
				case "PMOD":
				case "pmod":
					nextSubChunk.GetDataAsStructureArray<Modulator>(this.presetZoneModulators);
					continue;
				case "PGEN":
				case "pgen":
					nextSubChunk.GetDataAsStructureArray<Generator>(this.presetZoneGenerators);
					continue;
				case "INST":
				case "inst":
					nextSubChunk.GetDataAsStructureArray<Instrument>(this.instruments);
					continue;
				case "IBAG":
				case "ibag":
					nextSubChunk.GetDataAsStructureArray<Zone>(this.instrumentZones);
					continue;
				case "IMOD":
				case "imod":
					nextSubChunk.GetDataAsStructureArray<Modulator>(this.instrumentZoneModulators);
					continue;
				case "IGEN":
				case "igen":
					nextSubChunk.GetDataAsStructureArray<Generator>(this.instrumentZoneGenerators);
					continue;
				case "SHDR":
				case "shdr":
					nextSubChunk.GetDataAsStructureArray<SampleHeader>(this.sampleHeaders);
					continue;
				}
				throw new InvalidDataException(string.Format("Unknown chunk type {0}", nextSubChunk.ChunkID));
			}
			this.instrumentZoneGenerators.Load(this.sampleHeaders.SampleHeaders);
			this.instrumentZones.Load(this.instrumentZoneModulators.Modulators, this.instrumentZoneGenerators.Generators);
			this.instruments.LoadZones(this.instrumentZones.Zones);
			this.presetZoneGenerators.Load(this.instruments.Instruments);
			this.presetZones.Load(this.presetZoneModulators.Modulators, this.presetZoneGenerators.Generators);
			this.presetHeaders.LoadZones(this.presetZones.Zones);
			this.sampleHeaders.RemoveEOS();
		}

		/// <summary>
		/// The Presets contained in this chunk
		/// </summary>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
		public Preset[] Presets
		{
			get
			{
				return this.presetHeaders.Presets;
			}
		}

		/// <summary>
		/// The instruments contained in this chunk
		/// </summary>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public Instrument[] Instruments
		{
			get
			{
				return this.instruments.Instruments;
			}
		}

		/// <summary>
		/// The sample headers contained in this chunk
		/// </summary>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.sampleHeaders.SampleHeaders;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Preset Headers:\r\n");
			foreach (Preset arg in this.presetHeaders.Presets)
			{
				stringBuilder.AppendFormat("{0}\r\n", arg);
			}
			stringBuilder.Append("Instruments:\r\n");
			foreach (Instrument arg2 in this.instruments.Instruments)
			{
				stringBuilder.AppendFormat("{0}\r\n", arg2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400050A RID: 1290
		private PresetBuilder presetHeaders = new PresetBuilder();

		// Token: 0x0400050B RID: 1291
		private ZoneBuilder presetZones = new ZoneBuilder();

		// Token: 0x0400050C RID: 1292
		private ModulatorBuilder presetZoneModulators = new ModulatorBuilder();

		// Token: 0x0400050D RID: 1293
		private GeneratorBuilder presetZoneGenerators = new GeneratorBuilder();

		// Token: 0x0400050E RID: 1294
		private InstrumentBuilder instruments = new InstrumentBuilder();

		// Token: 0x0400050F RID: 1295
		private ZoneBuilder instrumentZones = new ZoneBuilder();

		// Token: 0x04000510 RID: 1296
		private ModulatorBuilder instrumentZoneModulators = new ModulatorBuilder();

		// Token: 0x04000511 RID: 1297
		private GeneratorBuilder instrumentZoneGenerators = new GeneratorBuilder();

		// Token: 0x04000512 RID: 1298
		private SampleHeaderBuilder sampleHeaders = new SampleHeaderBuilder();
	}
}
