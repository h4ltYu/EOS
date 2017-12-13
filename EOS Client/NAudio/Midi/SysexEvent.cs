using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
    public class SysexEvent : MidiEvent
    {
        public static SysexEvent ReadSysexEvent(BinaryReader br)
        {
            SysexEvent sysexEvent = new SysexEvent();
            List<byte> list = new List<byte>();
            bool flag = true;
            while (flag)
            {
                byte b = br.ReadByte();
                if (b == 247)
                {
                    flag = false;
                }
                else
                {
                    list.Add(b);
                }
            }
            sysexEvent.data = list.ToArray();
            return sysexEvent;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in this.data)
            {
                stringBuilder.AppendFormat("{0:X2} ", b);
            }
            return string.Format("{0} Sysex: {1} bytes\r\n{2}", base.AbsoluteTime, this.data.Length, stringBuilder.ToString());
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            writer.Write(this.data, 0, this.data.Length);
            writer.Write(247);
        }

        private byte[] data;
    }
}
