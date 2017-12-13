using System;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Midi
{
    public class TextEvent : MetaEvent
    {
        public TextEvent(BinaryReader br, int length)
        {
            Encoding instance = ByteEncoding.Instance;
            this.text = instance.GetString(br.ReadBytes(length));
        }

        public TextEvent(string text, MetaEventType metaEventType, long absoluteTime) : base(metaEventType, text.Length, absoluteTime)
        {
            this.text = text;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.metaDataLength = this.text.Length;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", base.ToString(), this.text);
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            Encoding instance = ByteEncoding.Instance;
            byte[] bytes = instance.GetBytes(this.text);
            writer.Write(bytes);
        }

        private string text;
    }
}
