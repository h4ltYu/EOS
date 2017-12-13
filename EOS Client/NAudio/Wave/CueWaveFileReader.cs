using System;

namespace NAudio.Wave
{
    public class CueWaveFileReader : WaveFileReader
    {
        public CueWaveFileReader(string fileName) : base(fileName)
        {
        }

        public CueList Cues
        {
            get
            {
                if (this.cues == null)
                {
                    this.cues = CueList.FromChunks(this);
                }
                return this.cues;
            }
        }

        private CueList cues;
    }
}
