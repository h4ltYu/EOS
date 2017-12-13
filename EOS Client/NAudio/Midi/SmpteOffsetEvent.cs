using System;
using System.IO;

namespace NAudio.Midi
{
    internal class SmpteOffsetEvent : MetaEvent
    {
        public SmpteOffsetEvent(BinaryReader br, int length)
        {
            if (length != 5)
            {
                throw new FormatException(string.Format("Invalid SMPTE Offset length: Got {0}, expected 5", length));
            }
            this.hours = br.ReadByte();
            this.minutes = br.ReadByte();
            this.seconds = br.ReadByte();
            this.frames = br.ReadByte();
            this.subFrames = br.ReadByte();
        }

        public int Hours
        {
            get
            {
                return (int)this.hours;
            }
        }

        public int Minutes
        {
            get
            {
                return (int)this.minutes;
            }
        }

        public int Seconds
        {
            get
            {
                return (int)this.seconds;
            }
        }

        public int Frames
        {
            get
            {
                return (int)this.frames;
            }
        }

        public int SubFrames
        {
            get
            {
                return (int)this.subFrames;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}:{2}:{3}:{4}:{5}", new object[]
            {
                base.ToString(),
                this.hours,
                this.minutes,
                this.seconds,
                this.frames,
                this.subFrames
            });
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            writer.Write(this.hours);
            writer.Write(this.minutes);
            writer.Write(this.seconds);
            writer.Write(this.frames);
            writer.Write(this.subFrames);
        }

        private byte hours;

        private byte minutes;

        private byte seconds;

        private byte frames;

        private byte subFrames;
    }
}
