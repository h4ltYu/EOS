using System;
using System.IO;

namespace NAudio.Wave
{
    public class CueWaveFileWriter : WaveFileWriter
    {
        public CueWaveFileWriter(string fileName, WaveFormat waveFormat) : base(fileName, waveFormat)
        {
        }

        public void AddCue(int position, string label)
        {
            if (this.cues == null)
            {
                this.cues = new CueList();
            }
            this.cues.Add(new Cue(position, label));
        }

        private void WriteCues(BinaryWriter w)
        {
            if (this.cues != null)
            {
                byte[] riffchunks = this.cues.GetRIFFChunks();
                int count = riffchunks.Length;
                w.Seek(0, SeekOrigin.End);
                if (w.BaseStream.Length % 2L == 1L)
                {
                    w.Write(0);
                }
                w.Write(this.cues.GetRIFFChunks(), 0, count);
                w.Seek(4, SeekOrigin.Begin);
                w.Write((int)(w.BaseStream.Length - 8L));
            }
        }

        protected override void UpdateHeader(BinaryWriter writer)
        {
            base.UpdateHeader(writer);
            this.WriteCues(writer);
        }

        private CueList cues;
    }
}
