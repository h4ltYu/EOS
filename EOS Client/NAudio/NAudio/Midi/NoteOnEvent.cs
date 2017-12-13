using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI note on event
	/// </summary>
	// Token: 0x020000EF RID: 239
	public class NoteOnEvent : NoteEvent
	{
		/// <summary>
		/// Reads a new Note On event from a stream of MIDI data
		/// </summary>
		/// <param name="br">Binary reader on the MIDI data stream</param>
		// Token: 0x06000583 RID: 1411 RVA: 0x00011D8E File Offset: 0x0000FF8E
		public NoteOnEvent(BinaryReader br) : base(br)
		{
		}

		/// <summary>
		/// Creates a NoteOn event with specified parameters
		/// </summary>
		/// <param name="absoluteTime">Absolute time of this event</param>
		/// <param name="channel">MIDI channel number</param>
		/// <param name="noteNumber">MIDI note number</param>
		/// <param name="velocity">MIDI note velocity</param>
		/// <param name="duration">MIDI note duration</param>
		// Token: 0x06000584 RID: 1412 RVA: 0x00011D97 File Offset: 0x0000FF97
		public NoteOnEvent(long absoluteTime, int channel, int noteNumber, int velocity, int duration) : base(absoluteTime, channel, MidiCommandCode.NoteOn, noteNumber, velocity)
		{
			this.OffEvent = new NoteEvent(absoluteTime, channel, MidiCommandCode.NoteOff, noteNumber, 0);
			this.NoteLength = duration;
		}

		/// <summary>
		/// The associated Note off event
		/// </summary>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00011DC5 File Offset: 0x0000FFC5
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public NoteEvent OffEvent
		{
			get
			{
				return this.offEvent;
			}
			set
			{
				if (!MidiEvent.IsNoteOff(value))
				{
					throw new ArgumentException("OffEvent must be a valid MIDI note off event");
				}
				if (value.NoteNumber != this.NoteNumber)
				{
					throw new ArgumentException("Note Off Event must be for the same note number");
				}
				if (value.Channel != this.Channel)
				{
					throw new ArgumentException("Note Off Event must be for the same channel");
				}
				this.offEvent = value;
			}
		}

		/// <summary>
		/// Get or set the Note Number, updating the off event at the same time
		/// </summary>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00011E29 File Offset: 0x00010029
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00011E31 File Offset: 0x00010031
		public override int NoteNumber
		{
			get
			{
				return base.NoteNumber;
			}
			set
			{
				base.NoteNumber = value;
				if (this.OffEvent != null)
				{
					this.OffEvent.NoteNumber = this.NoteNumber;
				}
			}
		}

		/// <summary>
		/// Get or set the channel, updating the off event at the same time
		/// </summary>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00011E53 File Offset: 0x00010053
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00011E5B File Offset: 0x0001005B
		public override int Channel
		{
			get
			{
				return base.Channel;
			}
			set
			{
				base.Channel = value;
				if (this.OffEvent != null)
				{
					this.OffEvent.Channel = this.Channel;
				}
			}
		}

		/// <summary>
		/// The duration of this note
		/// </summary>
		/// <remarks>
		/// There must be a note off event
		/// </remarks>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00011E7D File Offset: 0x0001007D
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00011E92 File Offset: 0x00010092
		public int NoteLength
		{
			get
			{
				return (int)(this.offEvent.AbsoluteTime - base.AbsoluteTime);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("NoteLength must be 0 or greater");
				}
				this.offEvent.AbsoluteTime = base.AbsoluteTime + (long)value;
			}
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x0600058D RID: 1421 RVA: 0x00011EB8 File Offset: 0x000100B8
		public override string ToString()
		{
			if (base.Velocity == 0 && this.OffEvent == null)
			{
				return string.Format("{0} (Note Off)", base.ToString());
			}
			return string.Format("{0} Len: {1}", base.ToString(), (this.OffEvent == null) ? "?" : this.NoteLength.ToString());
		}

		// Token: 0x040005F4 RID: 1524
		private NoteEvent offEvent;
	}
}
