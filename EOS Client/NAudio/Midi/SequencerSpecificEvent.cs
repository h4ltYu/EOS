using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
    public class SequencerSpecificEvent : MetaEvent
    {
        public SequencerSpecificEvent(BinaryReader br, int length)
        {
            this.data = br.ReadBytes(length);
        }

        public SequencerSpecificEvent(byte[] data, long absoluteTime) : base(MetaEventType.SequencerSpecific, data.Length, absoluteTime)
        {
            this.data = data;
        }

        public byte[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                this.metaDataLength = this.data.Length;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.ToString());
            stringBuilder.Append(" ");
            foreach (byte b in this.data)
            {
                stringBuilder.AppendFormat("{0:X2} ", b);
            }
            stringBuilder.Length--;
            return stringBuilder.ToString();
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            writer.Write(this.data);
        }

        private byte[] data;
    }
}
