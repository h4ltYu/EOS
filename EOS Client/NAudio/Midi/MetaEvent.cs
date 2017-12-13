using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
    public class MetaEvent : MidiEvent
    {
        public MetaEventType MetaEventType
        {
            get
            {
                return this.metaEvent;
            }
        }

        protected MetaEvent()
        {
        }

        public MetaEvent(MetaEventType metaEventType, int metaDataLength, long absoluteTime) : base(absoluteTime, 1, MidiCommandCode.MetaEvent)
        {
            this.metaEvent = metaEventType;
            this.metaDataLength = metaDataLength;
        }

        public static MetaEvent ReadMetaEvent(BinaryReader br)
        {
            MetaEventType metaEventType = (MetaEventType)br.ReadByte();
            int num = MidiEvent.ReadVarInt(br);
            MetaEvent metaEvent = new MetaEvent();
            MetaEventType metaEventType2 = metaEventType;
            if (metaEventType2 <= MetaEventType.SetTempo)
            {
                switch (metaEventType2)
                {
                    case MetaEventType.TrackSequenceNumber:
                        metaEvent = new TrackSequenceNumberEvent(br, num);
                        goto IL_E9;
                    case MetaEventType.TextEvent:
                    case MetaEventType.Copyright:
                    case MetaEventType.SequenceTrackName:
                    case MetaEventType.TrackInstrumentName:
                    case MetaEventType.Lyric:
                    case MetaEventType.Marker:
                    case MetaEventType.CuePoint:
                    case MetaEventType.ProgramName:
                    case MetaEventType.DeviceName:
                        metaEvent = new TextEvent(br, num);
                        goto IL_E9;
                    default:
                        if (metaEventType2 != MetaEventType.EndTrack)
                        {
                            if (metaEventType2 == MetaEventType.SetTempo)
                            {
                                metaEvent = new TempoEvent(br, num);
                                goto IL_E9;
                            }
                        }
                        else
                        {
                            if (num != 0)
                            {
                                throw new FormatException("End track length");
                            }
                            goto IL_E9;
                        }
                        break;
                }
            }
            else
            {
                if (metaEventType2 == MetaEventType.SmpteOffset)
                {
                    metaEvent = new SmpteOffsetEvent(br, num);
                    goto IL_E9;
                }
                switch (metaEventType2)
                {
                    case MetaEventType.TimeSignature:
                        metaEvent = new TimeSignatureEvent(br, num);
                        goto IL_E9;
                    case MetaEventType.KeySignature:
                        metaEvent = new KeySignatureEvent(br, num);
                        goto IL_E9;
                    default:
                        if (metaEventType2 == MetaEventType.SequencerSpecific)
                        {
                            metaEvent = new SequencerSpecificEvent(br, num);
                            goto IL_E9;
                        }
                        break;
                }
            }
            metaEvent.data = br.ReadBytes(num);
            if (metaEvent.data.Length != num)
            {
                throw new FormatException("Failed to read metaevent's data fully");
            }
            IL_E9:
            metaEvent.metaEvent = metaEventType;
            metaEvent.metaDataLength = num;
            return metaEvent;
        }

        public override string ToString()
        {
            if (this.data == null)
            {
                return string.Format("{0} {1}", base.AbsoluteTime, this.metaEvent);
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in this.data)
            {
                stringBuilder.AppendFormat("{0:X2} ", b);
            }
            return string.Format("{0} {1}\r\n{2}", base.AbsoluteTime, this.metaEvent, stringBuilder.ToString());
        }

        public override void Export(ref long absoluteTime, BinaryWriter writer)
        {
            base.Export(ref absoluteTime, writer);
            writer.Write((byte)this.metaEvent);
            MidiEvent.WriteVarInt(writer, this.metaDataLength);
            if (this.data != null)
            {
                writer.Write(this.data, 0, this.data.Length);
            }
        }

        private MetaEventType metaEvent;

        internal int metaDataLength;

        private byte[] data;
    }
}
