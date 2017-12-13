using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI Channel AfterTouch Event.
	/// </summary>
	// Token: 0x020000D4 RID: 212
	public class ChannelAfterTouchEvent : MidiEvent
	{
		/// <summary>
		/// Creates a new ChannelAfterTouchEvent from raw MIDI data
		/// </summary>
		/// <param name="br">A binary reader</param>
		// Token: 0x060004D9 RID: 1241 RVA: 0x000103D9 File Offset: 0x0000E5D9
		public ChannelAfterTouchEvent(BinaryReader br)
		{
			this.afterTouchPressure = br.ReadByte();
			if ((this.afterTouchPressure & 128) != 0)
			{
				throw new FormatException("Invalid afterTouchPressure");
			}
		}

		/// <summary>
		/// Creates a new Channel After-Touch Event
		/// </summary>
		/// <param name="absoluteTime">Absolute time</param>
		/// <param name="channel">Channel</param>
		/// <param name="afterTouchPressure">After-touch pressure</param>
		// Token: 0x060004DA RID: 1242 RVA: 0x00010406 File Offset: 0x0000E606
		public ChannelAfterTouchEvent(long absoluteTime, int channel, int afterTouchPressure) : base(absoluteTime, channel, MidiCommandCode.ChannelAfterTouch)
		{
			this.AfterTouchPressure = afterTouchPressure;
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060004DB RID: 1243 RVA: 0x0001041C File Offset: 0x0000E61C
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.afterTouchPressure);
		}

		/// <summary>
		/// The aftertouch pressure value
		/// </summary>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010432 File Offset: 0x0000E632
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0001043A File Offset: 0x0000E63A
		public int AfterTouchPressure
		{
			get
			{
				return (int)this.afterTouchPressure;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "After touch pressure must be in the range 0-127");
				}
				this.afterTouchPressure = (byte)value;
			}
		}

		// Token: 0x04000569 RID: 1385
		private byte afterTouchPressure;
	}
}
