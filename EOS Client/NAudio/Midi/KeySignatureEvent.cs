using System;
using System.IO;

namespace NAudio.Midi
{
    public class KeySignatureEvent : MetaEvent
    {
        public KeySignatureEvent(BinaryReader br, int length)
        {
            if (length != 2)
            {
                throw new FormatException("Invalid key signature length");
            }
            this.sharpsFlats = br.ReadByte();
            this.majorMinor = br.ReadByte();
        }

        public KeySignatureEvent(int sharpsFlats, int majorMinor, long absoluteTime) : base(MetaEventType.KeySignature, 2, absoluteTime)
        {
            this.sharpsFlats = (byte)sharpsFlats;
            this.majorMinor = (byte)majorMinor;
        }

        public int SharpsFlats
        {
            get
            {
                return (int)this.sharpsFlats;
            }
        }

        public int MajorMinor
        {
            get
            {
                return (int)this.majorMinor;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", base.ToString(), this.sharpsFlats, this.majorMinor);
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            writer.Write(this.sharpsFlats);
            writer.Write(this.majorMinor);
        }

        private byte sharpsFlats;

        private byte majorMinor;
    }
}
