using System;

namespace NAudio.SoundFont
{
    public class SampleHeader
    {
        public override string ToString()
        {
            return this.SampleName;
        }

        public string SampleName;

        public uint Start;

        public uint End;

        public uint StartLoop;

        public uint EndLoop;

        public uint SampleRate;

        public byte OriginalPitch;

        public sbyte PitchCorrection;

        public ushort SampleLink;

        public SFSampleLink SFSampleLink;
    }
}
