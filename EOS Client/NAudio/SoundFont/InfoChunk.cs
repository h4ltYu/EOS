using System;
using System.IO;

namespace NAudio.SoundFont
{
    public class InfoChunk
    {
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

        public SFVersion SoundFontVersion
        {
            get
            {
                return this.verSoundFont;
            }
        }

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

        private SFVersion verSoundFont;

        private string waveTableSoundEngine;

        private string bankName;

        private string dataROM;

        private string creationDate;

        private string author;

        private string targetProduct;

        private string copyright;

        private string comments;

        private string tools;

        private SFVersion verROM;
    }
}
