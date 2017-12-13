using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI track sequence number event event
	/// </summary>
	// Token: 0x020000F8 RID: 248
	public class TrackSequenceNumberEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new track sequence number event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">the data length</param>
		// Token: 0x060005C8 RID: 1480 RVA: 0x00012C1F File Offset: 0x00010E1F
		public TrackSequenceNumberEvent(BinaryReader br, int length)
		{
			if (length != 2)
			{
				throw new FormatException("Invalid sequence number length");
			}
			this.sequenceNumber = (ushort)(((int)br.ReadByte() << 8) + (int)br.ReadByte());
		}

		/// <summary>
		/// Describes this event
		/// </summary>
		/// <returns>String describing the event</returns>
		// Token: 0x060005C9 RID: 1481 RVA: 0x00012C4C File Offset: 0x00010E4C
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), this.sequenceNumber);
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005CA RID: 1482 RVA: 0x00012C69 File Offset: 0x00010E69
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)(this.sequenceNumber >> 8 & 255));
			writer.Write((byte)(this.sequenceNumber & 255));
		}

		// Token: 0x04000605 RID: 1541
		private ushort sequenceNumber;
	}
}
