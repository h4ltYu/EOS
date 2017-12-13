using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
    public class PresetsChunk
    {
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

        public Preset[] Presets
        {
            get
            {
                return this.presetHeaders.Presets;
            }
        }

        public Instrument[] Instruments
        {
            get
            {
                return this.instruments.Instruments;
            }
        }

        public SampleHeader[] SampleHeaders
        {
            get
            {
                return this.sampleHeaders.SampleHeaders;
            }
        }

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

        private PresetBuilder presetHeaders = new PresetBuilder();

        private ZoneBuilder presetZones = new ZoneBuilder();

        private ModulatorBuilder presetZoneModulators = new ModulatorBuilder();

        private GeneratorBuilder presetZoneGenerators = new GeneratorBuilder();

        private InstrumentBuilder instruments = new InstrumentBuilder();

        private ZoneBuilder instrumentZones = new ZoneBuilder();

        private ModulatorBuilder instrumentZoneModulators = new ModulatorBuilder();

        private GeneratorBuilder instrumentZoneGenerators = new GeneratorBuilder();

        private SampleHeaderBuilder sampleHeaders = new SampleHeaderBuilder();
    }
}
