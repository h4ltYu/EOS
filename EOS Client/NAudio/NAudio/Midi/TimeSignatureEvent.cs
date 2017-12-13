using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI time signature event
	/// </summary>
	// Token: 0x020000F7 RID: 247
	public class TimeSignatureEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new time signature event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">The data length</param>
		// Token: 0x060005BE RID: 1470 RVA: 0x00012A38 File Offset: 0x00010C38
		public TimeSignatureEvent(BinaryReader br, int length)
		{
			if (length != 4)
			{
				throw new FormatException(string.Format("Invalid time signature length: Got {0}, expected 4", length));
			}
			this.numerator = br.ReadByte();
			this.denominator = br.ReadByte();
			this.ticksInMetronomeClick = br.ReadByte();
			this.no32ndNotesInQuarterNote = br.ReadByte();
		}

		/// <summary>
		/// Creates a new TimeSignatureEvent
		/// </summary>
		/// <param name="absoluteTime">Time at which to create this event</param>
		/// <param name="numerator">Numerator</param>
		/// <param name="denominator">Denominator</param>
		/// <param name="ticksInMetronomeClick">Ticks in Metronome Click</param>
		/// <param name="no32ndNotesInQuarterNote">No of 32nd Notes in Quarter Click</param>
		// Token: 0x060005BF RID: 1471 RVA: 0x00012A95 File Offset: 0x00010C95
		public TimeSignatureEvent(long absoluteTime, int numerator, int denominator, int ticksInMetronomeClick, int no32ndNotesInQuarterNote) : base(MetaEventType.TimeSignature, 4, absoluteTime)
		{
			this.numerator = (byte)numerator;
			this.denominator = (byte)denominator;
			this.ticksInMetronomeClick = (byte)ticksInMetronomeClick;
			this.no32ndNotesInQuarterNote = (byte)no32ndNotesInQuarterNote;
		}

		/// <summary>
		/// Creates a new time signature event with the specified parameters
		/// </summary>
		// Token: 0x060005C0 RID: 1472 RVA: 0x00012AC3 File Offset: 0x00010CC3
		[Obsolete("Use the constructor that has absolute time first")]
		public TimeSignatureEvent(int numerator, int denominator, int ticksInMetronomeClick, int no32ndNotesInQuarterNote, long absoluteTime) : base(MetaEventType.TimeSignature, 4, absoluteTime)
		{
			this.numerator = (byte)numerator;
			this.denominator = (byte)denominator;
			this.ticksInMetronomeClick = (byte)ticksInMetronomeClick;
			this.no32ndNotesInQuarterNote = (byte)no32ndNotesInQuarterNote;
		}

		/// <summary>
		/// Numerator (number of beats in a bar)
		/// </summary>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00012AF1 File Offset: 0x00010CF1
		public int Numerator
		{
			get
			{
				return (int)this.numerator;
			}
		}

		/// <summary>
		/// Denominator (Beat unit),
		/// 1 means 2, 2 means 4 (crochet), 3 means 8 (quaver), 4 means 16 and 5 means 32
		/// </summary>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00012AF9 File Offset: 0x00010CF9
		public int Denominator
		{
			get
			{
				return (int)this.denominator;
			}
		}

		/// <summary>
		/// Ticks in a metronome click
		/// </summary>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00012B01 File Offset: 0x00010D01
		public int TicksInMetronomeClick
		{
			get
			{
				return (int)this.ticksInMetronomeClick;
			}
		}

		/// <summary>
		/// Number of 32nd notes in a quarter note
		/// </summary>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00012B09 File Offset: 0x00010D09
		public int No32ndNotesInQuarterNote
		{
			get
			{
				return (int)this.no32ndNotesInQuarterNote;
			}
		}

		/// <summary>
		/// The time signature
		/// </summary>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00012B14 File Offset: 0x00010D14
		public string TimeSignature
		{
			get
			{
				string arg = string.Format("Unknown ({0})", this.denominator);
				switch (this.denominator)
				{
				case 1:
					arg = "2";
					break;
				case 2:
					arg = "4";
					break;
				case 3:
					arg = "8";
					break;
				case 4:
					arg = "16";
					break;
				case 5:
					arg = "32";
					break;
				}
				return string.Format("{0}/{1}", this.numerator, arg);
			}
		}

		/// <summary>
		/// Describes this time signature event
		/// </summary>
		/// <returns>A string describing this event</returns>
		// Token: 0x060005C6 RID: 1478 RVA: 0x00012B98 File Offset: 0x00010D98
		public override string ToString()
		{
			return string.Format("{0} {1} TicksInClick:{2} 32ndsInQuarterNote:{3}", new object[]
			{
				base.ToString(),
				this.TimeSignature,
				this.ticksInMetronomeClick,
				this.no32ndNotesInQuarterNote
			});
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005C7 RID: 1479 RVA: 0x00012BE5 File Offset: 0x00010DE5
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.numerator);
			writer.Write(this.denominator);
			writer.Write(this.ticksInMetronomeClick);
			writer.Write(this.no32ndNotesInQuarterNote);
		}

		// Token: 0x04000601 RID: 1537
		private byte numerator;

		// Token: 0x04000602 RID: 1538
		private byte denominator;

		// Token: 0x04000603 RID: 1539
		private byte ticksInMetronomeClick;

		// Token: 0x04000604 RID: 1540
		private byte no32ndNotesInQuarterNote;
	}
}
