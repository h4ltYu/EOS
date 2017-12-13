using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI pitch wheel change event
	/// </summary>
	// Token: 0x020000F1 RID: 241
	public class PitchWheelChangeEvent : MidiEvent
	{
		/// <summary>
		/// Reads a pitch wheel change event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream to read from</param>
		// Token: 0x06000597 RID: 1431 RVA: 0x00012468 File Offset: 0x00010668
		public PitchWheelChangeEvent(BinaryReader br)
		{
			byte b = br.ReadByte();
			byte b2 = br.ReadByte();
			if ((b & 128) != 0)
			{
				throw new FormatException("Invalid pitchwheelchange byte 1");
			}
			if ((b2 & 128) != 0)
			{
				throw new FormatException("Invalid pitchwheelchange byte 2");
			}
			this.pitch = (int)b + ((int)b2 << 7);
		}

		/// <summary>
		/// Creates a new pitch wheel change event
		/// </summary>
		/// <param name="absoluteTime">Absolute event time</param>
		/// <param name="channel">Channel</param>
		/// <param name="pitchWheel">Pitch wheel value</param>
		// Token: 0x06000598 RID: 1432 RVA: 0x000124BC File Offset: 0x000106BC
		public PitchWheelChangeEvent(long absoluteTime, int channel, int pitchWheel) : base(absoluteTime, channel, MidiCommandCode.PitchWheelChange)
		{
			this.Pitch = pitchWheel;
		}

		/// <summary>
		/// Describes this pitch wheel change event
		/// </summary>
		/// <returns>String describing this pitch wheel change event</returns>
		// Token: 0x06000599 RID: 1433 RVA: 0x000124D2 File Offset: 0x000106D2
		public override string ToString()
		{
			return string.Format("{0} Pitch {1} ({2})", base.ToString(), this.pitch, this.pitch - 8192);
		}

		/// <summary>
		/// Pitch Wheel Value 0 is minimum, 0x2000 (8192) is default, 0x4000 (16384) is maximum
		/// </summary>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012500 File Offset: 0x00010700
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00012508 File Offset: 0x00010708
		public int Pitch
		{
			get
			{
				return this.pitch;
			}
			set
			{
				if (value < 0 || value > 16384)
				{
					throw new ArgumentOutOfRangeException("value", "Pitch value must be in the range 0 - 0x4000");
				}
				this.pitch = value;
			}
		}

		/// <summary>
		/// Gets a short message
		/// </summary>
		/// <returns>Integer to sent as short message</returns>
		// Token: 0x0600059C RID: 1436 RVA: 0x0001252D File Offset: 0x0001072D
		public override int GetAsShortMessage()
		{
			return base.GetAsShortMessage() + ((this.pitch & 127) << 8) + ((this.pitch >> 7 & 127) << 16);
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x0600059D RID: 1437 RVA: 0x00012550 File Offset: 0x00010750
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)(this.pitch & 127));
			writer.Write((byte)(this.pitch >> 7 & 127));
		}

		// Token: 0x040005F7 RID: 1527
		private int pitch;
	}
}
