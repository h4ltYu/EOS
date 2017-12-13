using System;
using System.IO;

namespace NAudio.Midi
{
    public class MidiEvent
    {
        public static MidiEvent FromRawMessage(int rawMessage)
        {
            long num = 0L;
            int num2 = rawMessage & 255;
            int num3 = rawMessage >> 8 & 255;
            int num4 = rawMessage >> 16 & 255;
            int num5 = 1;
            MidiCommandCode midiCommandCode;
            if ((num2 & 240) == 240)
            {
                midiCommandCode = (MidiCommandCode)num2;
            }
            else
            {
                midiCommandCode = (MidiCommandCode)(num2 & 240);
                num5 = (num2 & 15) + 1;
            }
            MidiCommandCode midiCommandCode2 = midiCommandCode;
            if (midiCommandCode2 <= MidiCommandCode.ControlChange)
            {
                if (midiCommandCode2 <= MidiCommandCode.NoteOn)
                {
                    if (midiCommandCode2 != MidiCommandCode.NoteOff && midiCommandCode2 != MidiCommandCode.NoteOn)
                    {
                        goto IL_177;
                    }
                }
                else if (midiCommandCode2 != MidiCommandCode.KeyAfterTouch)
                {
                    if (midiCommandCode2 != MidiCommandCode.ControlChange)
                    {
                        goto IL_177;
                    }
                    return new ControlChangeEvent(num, num5, (MidiController)num3, num4);
                }
                if (num4 > 0 && midiCommandCode == MidiCommandCode.NoteOn)
                {
                    return new NoteOnEvent(num, num5, num3, num4, 0);
                }
                return new NoteEvent(num, num5, midiCommandCode, num3, num4);
            }
            else if (midiCommandCode2 <= MidiCommandCode.ChannelAfterTouch)
            {
                if (midiCommandCode2 == MidiCommandCode.PatchChange)
                {
                    return new PatchChangeEvent(num, num5, num3);
                }
                if (midiCommandCode2 == MidiCommandCode.ChannelAfterTouch)
                {
                    return new ChannelAfterTouchEvent(num, num5, num3);
                }
            }
            else
            {
                if (midiCommandCode2 == MidiCommandCode.PitchWheelChange)
                {
                    return new PitchWheelChangeEvent(num, num5, num3 + (num4 << 7));
                }
                if (midiCommandCode2 != MidiCommandCode.Sysex)
                {
                    switch (midiCommandCode2)
                    {
                        case MidiCommandCode.TimingClock:
                        case MidiCommandCode.StartSequence:
                        case MidiCommandCode.ContinueSequence:
                        case MidiCommandCode.StopSequence:
                        case MidiCommandCode.AutoSensing:
                            return new MidiEvent(num, num5, midiCommandCode);
                    }
                }
            }
            IL_177:
            throw new FormatException(string.Format("Unsupported MIDI Command Code for Raw Message {0}", midiCommandCode));
        }

        public static MidiEvent ReadNextEvent(BinaryReader br, MidiEvent previous)
        {
            int num = MidiEvent.ReadVarInt(br);
            int num2 = 1;
            byte b = br.ReadByte();
            MidiCommandCode midiCommandCode;
            if ((b & 128) == 0)
            {
                midiCommandCode = previous.CommandCode;
                num2 = previous.Channel;
                br.BaseStream.Position -= 1L;
            }
            else if ((b & 240) == 240)
            {
                midiCommandCode = (MidiCommandCode)b;
            }
            else
            {
                midiCommandCode = (MidiCommandCode)(b & 240);
                num2 = (int)((b & 15) + 1);
            }
            MidiCommandCode midiCommandCode2 = midiCommandCode;
            MidiEvent midiEvent;
            if (midiCommandCode2 <= MidiCommandCode.ControlChange)
            {
                if (midiCommandCode2 <= MidiCommandCode.NoteOn)
                {
                    if (midiCommandCode2 != MidiCommandCode.NoteOff)
                    {
                        if (midiCommandCode2 != MidiCommandCode.NoteOn)
                        {
                            goto IL_15F;
                        }
                        midiEvent = new NoteOnEvent(br);
                        goto IL_175;
                    }
                }
                else if (midiCommandCode2 != MidiCommandCode.KeyAfterTouch)
                {
                    if (midiCommandCode2 != MidiCommandCode.ControlChange)
                    {
                        goto IL_15F;
                    }
                    midiEvent = new ControlChangeEvent(br);
                    goto IL_175;
                }
                midiEvent = new NoteEvent(br);
                goto IL_175;
            }
            if (midiCommandCode2 <= MidiCommandCode.ChannelAfterTouch)
            {
                if (midiCommandCode2 == MidiCommandCode.PatchChange)
                {
                    midiEvent = new PatchChangeEvent(br);
                    goto IL_175;
                }
                if (midiCommandCode2 == MidiCommandCode.ChannelAfterTouch)
                {
                    midiEvent = new ChannelAfterTouchEvent(br);
                    goto IL_175;
                }
            }
            else
            {
                if (midiCommandCode2 == MidiCommandCode.PitchWheelChange)
                {
                    midiEvent = new PitchWheelChangeEvent(br);
                    goto IL_175;
                }
                if (midiCommandCode2 == MidiCommandCode.Sysex)
                {
                    midiEvent = SysexEvent.ReadSysexEvent(br);
                    goto IL_175;
                }
                switch (midiCommandCode2)
                {
                    case MidiCommandCode.TimingClock:
                    case MidiCommandCode.StartSequence:
                    case MidiCommandCode.ContinueSequence:
                    case MidiCommandCode.StopSequence:
                        midiEvent = new MidiEvent();
                        goto IL_175;
                    case MidiCommandCode.MetaEvent:
                        midiEvent = MetaEvent.ReadMetaEvent(br);
                        goto IL_175;
                }
            }
            IL_15F:
            throw new FormatException(string.Format("Unsupported MIDI Command Code {0:X2}", (byte)midiCommandCode));
            IL_175:
            midiEvent.channel = num2;
            midiEvent.deltaTime = num;
            midiEvent.commandCode = midiCommandCode;
            return midiEvent;
        }

        public virtual int GetAsShortMessage()
        {
            return (int)((byte)(this.channel - 1) + this.commandCode);
        }

        protected MidiEvent()
        {
        }

        public MidiEvent(long absoluteTime, int channel, MidiCommandCode commandCode)
        {
            this.absoluteTime = absoluteTime;
            this.Channel = channel;
            this.commandCode = commandCode;
        }

        public virtual int Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                if (value < 1 || value > 16)
                {
                    throw new ArgumentOutOfRangeException("value", value, string.Format("Channel must be 1-16 (Got {0})", value));
                }
                this.channel = value;
            }
        }

        public int DeltaTime
        {
            get
            {
                return this.deltaTime;
            }
        }

        public long AbsoluteTime
        {
            get
            {
                return this.absoluteTime;
            }
            set
            {
                this.absoluteTime = value;
            }
        }

        public MidiCommandCode CommandCode
        {
            get
            {
                return this.commandCode;
            }
        }

        public static bool IsNoteOff(MidiEvent midiEvent)
        {
            if (midiEvent == null)
            {
                return false;
            }
            if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                NoteEvent noteEvent = (NoteEvent)midiEvent;
                return noteEvent.Velocity == 0;
            }
            return midiEvent.CommandCode == MidiCommandCode.NoteOff;
        }

        public static bool IsNoteOn(MidiEvent midiEvent)
        {
            if (midiEvent != null && midiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                NoteEvent noteEvent = (NoteEvent)midiEvent;
                return noteEvent.Velocity > 0;
            }
            return false;
        }

        public static bool IsEndTrack(MidiEvent midiEvent)
        {
            if (midiEvent != null)
            {
                MetaEvent metaEvent = midiEvent as MetaEvent;
                if (metaEvent != null)
                {
                    return metaEvent.MetaEventType == MetaEventType.EndTrack;
                }
            }
            return false;
        }

        public override string ToString()
        {
            if (this.commandCode >= MidiCommandCode.Sysex)
            {
                return string.Format("{0} {1}", this.absoluteTime, this.commandCode);
            }
            return string.Format("{0} {1} Ch: {2}", this.absoluteTime, this.commandCode, this.channel);
        }

        public static int ReadVarInt(BinaryReader br)
        {
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                byte b = br.ReadByte();
                num <<= 7;
                num += (int)(b & 127);
                if ((b & 128) == 0)
                {
                    return num;
                }
            }
            throw new FormatException("Invalid Var Int");
        }

        public static void WriteVarInt(BinaryWriter writer, int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", value, "Cannot write a negative Var Int");
            }
            if (value > 268435455)
            {
                throw new ArgumentOutOfRangeException("value", value, "Maximum allowed Var Int is 0x0FFFFFFF");
            }
            int i = 0;
            byte[] array = new byte[4];
            do
            {
                array[i++] = (byte)(value & 127);
                value >>= 7;
            }
            while (value > 0);
            while (i > 0)
            {
                i--;
                if (i > 0)
                {
                    writer.Write(array[i] | 128);
                }
                else
                {
                    writer.Write(array[i]);
                }
            }
        }

        public virtual void Export(ref long absoluteTime, BinaryWriter writer)
        {
            if (this.absoluteTime < absoluteTime)
            {
                throw new FormatException("Can't export unsorted MIDI events");
            }
            MidiEvent.WriteVarInt(writer, (int)(this.absoluteTime - absoluteTime));
            absoluteTime = this.absoluteTime;
            int num = (int)this.commandCode;
            if (this.commandCode != MidiCommandCode.MetaEvent)
            {
                num += this.channel - 1;
            }
            writer.Write((byte)num);
        }

        private MidiCommandCode commandCode;

        private int channel;

        private int deltaTime;

        private long absoluteTime;
    }
}
