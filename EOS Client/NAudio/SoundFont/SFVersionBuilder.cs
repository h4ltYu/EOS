using System;
using System.IO;

namespace NAudio.SoundFont
{
    internal class SFVersionBuilder : StructureBuilder<SFVersion>
    {
        public override SFVersion Read(BinaryReader br)
        {
            SFVersion sfversion = new SFVersion();
            sfversion.Major = br.ReadInt16();
            sfversion.Minor = br.ReadInt16();
            this.data.Add(sfversion);
            return sfversion;
        }

        public override void Write(BinaryWriter bw, SFVersion v)
        {
            bw.Write(v.Major);
            bw.Write(v.Minor);
        }

        public override int Length
        {
            get
            {
                return 4;
            }
        }
    }
}
