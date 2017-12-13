using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents an individual MIDI event
	/// </summary>
	// Token: 0x020000D3 RID: 211
	public class MidiEvent
	{
		/// <summary>
		/// Creates a MidiEvent from a raw message received using
		/// the MME MIDI In APIs
		/// </summary>
		/// <param name="rawMessage">The short MIDI message</param>
		/// <returns>A new MIDI Event</returns>
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
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

		/// <summary>
		/// Constructs a MidiEvent from a BinaryStream
		/// </summary>
		/// <param name="br">The binary stream of MIDI data</param>
		/// <param name="previous">The previous MIDI event (pass null for first event)</param>
		/// <returns>A new MidiEvent</returns>
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000FF78 File Offset: 0x0000E178
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

		/// <summary>
		/// Converts this MIDI event to a short message (32 bit integer) that
		/// can be sent by the Windows MIDI out short message APIs
		/// Cannot be implemented for all MIDI messages
		/// </summary>
		/// <returns>A short message</returns>
		// Token: 0x060004C9 RID: 1225 RVA: 0x00010114 File Offset: 0x0000E314
		public virtual int GetAsShortMessage()
		{
			return (int)((byte)(this.channel - 1) + this.commandCode);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		// Token: 0x060004CA RID: 1226 RVA: 0x00010125 File Offset: 0x0000E325
		protected MidiEvent()
		{
		}

		/// <summary>
		/// Creates a MIDI event with specified parameters
		/// </summary>
		/// <param name="absoluteTime">Absolute time of this event</param>
		/// <param name="channel">MIDI channel number</param>
		/// <param name="commandCode">MIDI command code</param>
		// Token: 0x060004CB RID: 1227 RVA: 0x0001012D File Offset: 0x0000E32D
		public MidiEvent(long absoluteTime, int channel, MidiCommandCode commandCode)
		{
			this.absoluteTime = absoluteTime;
			this.Channel = channel;
			this.commandCode = commandCode;
		}

		/// <summary>
		/// The MIDI Channel Number for this event (1-16)
		/// </summary>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001014A File Offset: 0x0000E34A
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00010152 File Offset: 0x0000E352
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

		/// <summary>
		/// The Delta time for this event
		/// </summary>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010185 File Offset: 0x0000E385
		public int DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		/// <summary>
		/// The absolute time for this event
		/// </summary>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001018D File Offset: 0x0000E38D
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00010195 File Offset: 0x0000E395
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

		/// <summary>
		/// The command code for this event
		/// </summary>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001019E File Offset: 0x0000E39E
		public MidiCommandCode CommandCode
		{
			get
			{
				return this.commandCode;
			}
		}

		/// <summary>
		/// Whether this is a note off event
		/// </summary>
		// Token: 0x060004D2 RID: 1234 RVA: 0x000101A8 File Offset: 0x0000E3A8
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

		/// <summary>
		/// Whether this is a note on event
		/// </summary>
		// Token: 0x060004D3 RID: 1235 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public static bool IsNoteOn(MidiEvent midiEvent)
		{
			if (midiEvent != null && midiEvent.CommandCode == MidiCommandCode.NoteOn)
			{
				NoteEvent noteEvent = (NoteEvent)midiEvent;
				return noteEvent.Velocity > 0;
			}
			return false;
		}

		/// <summary>
		/// Determines if this is an end track event
		/// </summary>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00010218 File Offset: 0x0000E418
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

		/// <summary>
		/// Displays a summary of the MIDI event
		/// </summary>
		/// <returns>A string containing a brief description of this MIDI event</returns>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00010240 File Offset: 0x0000E440
		public override string ToString()
		{
			if (this.commandCode >= MidiCommandCode.Sysex)
			{
				return string.Format("{0} {1}", this.absoluteTime, this.commandCode);
			}
			return string.Format("{0} {1} Ch: {2}", this.absoluteTime, this.commandCode, this.channel);
		}

		/// <summary>
		/// Utility function that can read a variable length integer from a binary stream
		/// </summary>
		/// <param name="br">The binary stream</param>
		/// <returns>The integer read</returns>
		// Token: 0x060004D6 RID: 1238 RVA: 0x000102A8 File Offset: 0x0000E4A8
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

		/// <summary>
		/// Writes a variable length integer to a binary stream
		/// </summary>
		/// <param name="writer">Binary stream</param>
		/// <param name="value">The value to write</param>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000102EC File Offset: 0x0000E4EC
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

		/// <summary>
		/// Exports this MIDI event's data
		/// Overriden in derived classes, but they should call this version
		/// </summary>
		/// <param name="absoluteTime">Absolute time used to calculate delta. 
		/// Is updated ready for the next delta calculation</param>
		/// <param name="writer">Stream to write to</param>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00010378 File Offset: 0x0000E578
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

		/// <summary>The MIDI command code</summary>
		// Token: 0x04000565 RID: 1381
		private MidiCommandCode commandCode;

		// Token: 0x04000566 RID: 1382
		private int channel;

		// Token: 0x04000567 RID: 1383
		private int deltaTime;

		// Token: 0x04000568 RID: 1384
		private long absoluteTime;
	}
}
