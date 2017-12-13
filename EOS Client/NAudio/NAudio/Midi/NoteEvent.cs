using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a note MIDI event
	/// </summary>
	// Token: 0x020000EE RID: 238
	public class NoteEvent : MidiEvent
	{
		/// <summary>
		/// Reads a NoteEvent from a stream of MIDI data
		/// </summary>
		/// <param name="br">Binary Reader for the stream</param>
		// Token: 0x06000578 RID: 1400 RVA: 0x000119BB File Offset: 0x0000FBBB
		public NoteEvent(BinaryReader br)
		{
			this.NoteNumber = (int)br.ReadByte();
			this.velocity = (int)br.ReadByte();
			if (this.velocity > 127)
			{
				this.velocity = 127;
			}
		}

		/// <summary>
		/// Creates a MIDI Note Event with specified parameters
		/// </summary>
		/// <param name="absoluteTime">Absolute time of this event</param>
		/// <param name="channel">MIDI channel number</param>
		/// <param name="commandCode">MIDI command code</param>
		/// <param name="noteNumber">MIDI Note Number</param>
		/// <param name="velocity">MIDI Note Velocity</param>
		// Token: 0x06000579 RID: 1401 RVA: 0x000119ED File Offset: 0x0000FBED
		public NoteEvent(long absoluteTime, int channel, MidiCommandCode commandCode, int noteNumber, int velocity) : base(absoluteTime, channel, commandCode)
		{
			this.NoteNumber = noteNumber;
			this.Velocity = velocity;
		}

		/// <summary>
		/// <see cref="M:NAudio.Midi.MidiEvent.GetAsShortMessage" />
		/// </summary>
		// Token: 0x0600057A RID: 1402 RVA: 0x00011A08 File Offset: 0x0000FC08
		public override int GetAsShortMessage()
		{
			return base.GetAsShortMessage() + (this.noteNumber << 8) + (this.velocity << 16);
		}

		/// <summary>
		/// The MIDI note number
		/// </summary>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00011A2B File Offset: 0x0000FC2B
		public virtual int NoteNumber
		{
			get
			{
				return this.noteNumber;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "Note number must be in the range 0-127");
				}
				this.noteNumber = value;
			}
		}

		/// <summary>
		/// The note velocity
		/// </summary>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00011A4D File Offset: 0x0000FC4D
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00011A55 File Offset: 0x0000FC55
		public int Velocity
		{
			get
			{
				return this.velocity;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "Velocity must be in the range 0-127");
				}
				this.velocity = value;
			}
		}

		/// <summary>
		/// The note name
		/// </summary>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00011A78 File Offset: 0x0000FC78
		public string NoteName
		{
			get
			{
				if (this.Channel != 16 && this.Channel != 10)
				{
					int num = this.noteNumber / 12;
					return string.Format("{0}{1}", NoteEvent.NoteNames[this.noteNumber % 12], num);
				}
				switch (this.noteNumber)
				{
				case 35:
					return "Acoustic Bass Drum";
				case 36:
					return "Bass Drum 1";
				case 37:
					return "Side Stick";
				case 38:
					return "Acoustic Snare";
				case 39:
					return "Hand Clap";
				case 40:
					return "Electric Snare";
				case 41:
					return "Low Floor Tom";
				case 42:
					return "Closed Hi-Hat";
				case 43:
					return "High Floor Tom";
				case 44:
					return "Pedal Hi-Hat";
				case 45:
					return "Low Tom";
				case 46:
					return "Open Hi-Hat";
				case 47:
					return "Low-Mid Tom";
				case 48:
					return "Hi-Mid Tom";
				case 49:
					return "Crash Cymbal 1";
				case 50:
					return "High Tom";
				case 51:
					return "Ride Cymbal 1";
				case 52:
					return "Chinese Cymbal";
				case 53:
					return "Ride Bell";
				case 54:
					return "Tambourine";
				case 55:
					return "Splash Cymbal";
				case 56:
					return "Cowbell";
				case 57:
					return "Crash Cymbal 2";
				case 58:
					return "Vibraslap";
				case 59:
					return "Ride Cymbal 2";
				case 60:
					return "Hi Bongo";
				case 61:
					return "Low Bongo";
				case 62:
					return "Mute Hi Conga";
				case 63:
					return "Open Hi Conga";
				case 64:
					return "Low Conga";
				case 65:
					return "High Timbale";
				case 66:
					return "Low Timbale";
				case 67:
					return "High Agogo";
				case 68:
					return "Low Agogo";
				case 69:
					return "Cabasa";
				case 70:
					return "Maracas";
				case 71:
					return "Short Whistle";
				case 72:
					return "Long Whistle";
				case 73:
					return "Short Guiro";
				case 74:
					return "Long Guiro";
				case 75:
					return "Claves";
				case 76:
					return "Hi Wood Block";
				case 77:
					return "Low Wood Block";
				case 78:
					return "Mute Cuica";
				case 79:
					return "Open Cuica";
				case 80:
					return "Mute Triangle";
				case 81:
					return "Open Triangle";
				default:
					return string.Format("Drum {0}", this.noteNumber);
				}
			}
		}

		/// <summary>
		/// Describes the Note Event
		/// </summary>
		/// <returns>Note event as a string</returns>
		// Token: 0x06000580 RID: 1408 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		public override string ToString()
		{
			return string.Format("{0} {1} Vel:{2}", base.ToString(), this.NoteName, this.Velocity);
		}

		/// <summary>
		/// <see cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)" />
		/// </summary>
		// Token: 0x06000581 RID: 1409 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)this.noteNumber);
			writer.Write((byte)this.velocity);
		}

		// Token: 0x040005F1 RID: 1521
		private int noteNumber;

		// Token: 0x040005F2 RID: 1522
		private int velocity;

		// Token: 0x040005F3 RID: 1523
		private static readonly string[] NoteNames = new string[]
		{
			"C",
			"C#",
			"D",
			"D#",
			"E",
			"F",
			"F#",
			"G",
			"G#",
			"A",
			"A#",
			"B"
		};
	}
}
