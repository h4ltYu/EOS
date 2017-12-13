using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI key signature event event
	/// </summary>
	// Token: 0x020000D7 RID: 215
	public class KeySignatureEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new track sequence number event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">the data length</param>
		// Token: 0x060004ED RID: 1261 RVA: 0x000107CC File Offset: 0x0000E9CC
		public KeySignatureEvent(BinaryReader br, int length)
		{
			if (length != 2)
			{
				throw new FormatException("Invalid key signature length");
			}
			this.sharpsFlats = br.ReadByte();
			this.majorMinor = br.ReadByte();
		}

		/// <summary>
		/// Creates a new Key signature event with the specified data
		/// </summary>
		// Token: 0x060004EE RID: 1262 RVA: 0x000107FB File Offset: 0x0000E9FB
		public KeySignatureEvent(int sharpsFlats, int majorMinor, long absoluteTime) : base(MetaEventType.KeySignature, 2, absoluteTime)
		{
			this.sharpsFlats = (byte)sharpsFlats;
			this.majorMinor = (byte)majorMinor;
		}

		/// <summary>
		/// Number of sharps or flats
		/// </summary>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00010817 File Offset: 0x0000EA17
		public int SharpsFlats
		{
			get
			{
				return (int)this.sharpsFlats;
			}
		}

		/// <summary>
		/// Major or Minor key
		/// </summary>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001081F File Offset: 0x0000EA1F
		public int MajorMinor
		{
			get
			{
				return (int)this.majorMinor;
			}
		}

		/// <summary>
		/// Describes this event
		/// </summary>
		/// <returns>String describing the event</returns>
		// Token: 0x060004F1 RID: 1265 RVA: 0x00010827 File Offset: 0x0000EA27
		public override string ToString()
		{
			return string.Format("{0} {1} {2}", base.ToString(), this.sharpsFlats, this.majorMinor);
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001084F File Offset: 0x0000EA4F
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.sharpsFlats);
			writer.Write(this.majorMinor);
		}

		// Token: 0x0400056F RID: 1391
		private byte sharpsFlats;

		// Token: 0x04000570 RID: 1392
		private byte majorMinor;
	}
}
