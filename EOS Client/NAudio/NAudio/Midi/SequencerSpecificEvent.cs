using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a Sequencer Specific event
	/// </summary>
	// Token: 0x020000F2 RID: 242
	public class SequencerSpecificEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new sequencer specific event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">The data length</param>
		// Token: 0x0600059E RID: 1438 RVA: 0x0001257C File Offset: 0x0001077C
		public SequencerSpecificEvent(BinaryReader br, int length)
		{
			this.data = br.ReadBytes(length);
		}

		/// <summary>
		/// Creates a new Sequencer Specific event
		/// </summary>
		/// <param name="data">The sequencer specific data</param>
		/// <param name="absoluteTime">Absolute time of this event</param>
		// Token: 0x0600059F RID: 1439 RVA: 0x00012591 File Offset: 0x00010791
		public SequencerSpecificEvent(byte[] data, long absoluteTime) : base(MetaEventType.SequencerSpecific, data.Length, absoluteTime)
		{
			this.data = data;
		}

		/// <summary>
		/// The contents of this sequencer specific
		/// </summary>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000125A6 File Offset: 0x000107A6
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000125AE File Offset: 0x000107AE
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
				this.metaDataLength = this.data.Length;
			}
		}

		/// <summary>
		/// Describes this MIDI text event
		/// </summary>
		/// <returns>A string describing this event</returns>
		// Token: 0x060005A2 RID: 1442 RVA: 0x000125C8 File Offset: 0x000107C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(" ");
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			stringBuilder.Length--;
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005A3 RID: 1443 RVA: 0x00012633 File Offset: 0x00010833
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.data);
		}

		// Token: 0x040005F8 RID: 1528
		private byte[] data;
	}
}
