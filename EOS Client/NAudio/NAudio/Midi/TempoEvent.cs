using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI tempo event
	/// </summary>
	// Token: 0x020000F5 RID: 245
	public class TempoEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new tempo event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">the data length</param>
		// Token: 0x060005B0 RID: 1456 RVA: 0x00012884 File Offset: 0x00010A84
		public TempoEvent(BinaryReader br, int length)
		{
			if (length != 3)
			{
				throw new FormatException("Invalid tempo length");
			}
			this.microsecondsPerQuarterNote = ((int)br.ReadByte() << 16) + ((int)br.ReadByte() << 8) + (int)br.ReadByte();
		}

		/// <summary>
		/// Creates a new tempo event with specified settings
		/// </summary>
		/// <param name="microsecondsPerQuarterNote">Microseconds per quarter note</param>
		/// <param name="absoluteTime">Absolute time</param>
		// Token: 0x060005B1 RID: 1457 RVA: 0x000128BA File Offset: 0x00010ABA
		public TempoEvent(int microsecondsPerQuarterNote, long absoluteTime) : base(MetaEventType.SetTempo, 3, absoluteTime)
		{
			this.microsecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		/// <summary>
		/// Describes this tempo event
		/// </summary>
		/// <returns>String describing the tempo event</returns>
		// Token: 0x060005B2 RID: 1458 RVA: 0x000128CD File Offset: 0x00010ACD
		public override string ToString()
		{
			return string.Format("{0} {2}bpm ({1})", base.ToString(), this.microsecondsPerQuarterNote, 60000000 / this.microsecondsPerQuarterNote);
		}

		/// <summary>
		/// Microseconds per quarter note
		/// </summary>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000128FB File Offset: 0x00010AFB
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00012903 File Offset: 0x00010B03
		public int MicrosecondsPerQuarterNote
		{
			get
			{
				return this.microsecondsPerQuarterNote;
			}
			set
			{
				this.microsecondsPerQuarterNote = value;
			}
		}

		/// <summary>
		/// Tempo
		/// </summary>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001290C File Offset: 0x00010B0C
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001291F File Offset: 0x00010B1F
		public double Tempo
		{
			get
			{
				return 60000000.0 / (double)this.microsecondsPerQuarterNote;
			}
			set
			{
				this.microsecondsPerQuarterNote = (int)(60000000.0 / value);
			}
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005B7 RID: 1463 RVA: 0x00012934 File Offset: 0x00010B34
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)(this.microsecondsPerQuarterNote >> 16 & 255));
			writer.Write((byte)(this.microsecondsPerQuarterNote >> 8 & 255));
			writer.Write((byte)(this.microsecondsPerQuarterNote & 255));
		}

		// Token: 0x040005FF RID: 1535
		private int microsecondsPerQuarterNote;
	}
}
