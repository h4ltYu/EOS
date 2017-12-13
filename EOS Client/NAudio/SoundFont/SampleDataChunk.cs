using System;
using System.IO;

namespace NAudio.SoundFont
{
    internal class SampleDataChunk
    {
        public SampleDataChunk(RiffChunk chunk)
        {
            string text = chunk.ReadChunkID();
            if (text != "sdta")
            {
                throw new InvalidDataException(string.Format("Not a sample data chunk ({0})", text));
            }
            this.sampleData = chunk.GetData();
        }

        public byte[] SampleData
        {
            get
            {
                return this.sampleData;
            }
        }

        private byte[] sampleData;
    }
}
