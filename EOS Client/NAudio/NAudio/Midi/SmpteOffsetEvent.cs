using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000F3 RID: 243
	internal class SmpteOffsetEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new time signature event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">The data length</param>
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001264C File Offset: 0x0001084C
		public SmpteOffsetEvent(BinaryReader br, int length)
		{
			if (length != 5)
			{
				throw new FormatException(string.Format("Invalid SMPTE Offset length: Got {0}, expected 5", length));
			}
			this.hours = br.ReadByte();
			this.minutes = br.ReadByte();
			this.seconds = br.ReadByte();
			this.frames = br.ReadByte();
			this.subFrames = br.ReadByte();
		}

		/// <summary>
		/// Hours
		/// </summary>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000126B5 File Offset: 0x000108B5
		public int Hours
		{
			get
			{
				return (int)this.hours;
			}
		}

		/// <summary>
		/// Minutes
		/// </summary>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000126BD File Offset: 0x000108BD
		public int Minutes
		{
			get
			{
				return (int)this.minutes;
			}
		}

		/// <summary>
		/// Seconds
		/// </summary>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000126C5 File Offset: 0x000108C5
		public int Seconds
		{
			get
			{
				return (int)this.seconds;
			}
		}

		/// <summary>
		/// Frames
		/// </summary>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000126CD File Offset: 0x000108CD
		public int Frames
		{
			get
			{
				return (int)this.frames;
			}
		}

		/// <summary>
		/// SubFrames
		/// </summary>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000126D5 File Offset: 0x000108D5
		public int SubFrames
		{
			get
			{
				return (int)this.subFrames;
			}
		}

		/// <summary>
		/// Describes this time signature event
		/// </summary>
		/// <returns>A string describing this event</returns>
		// Token: 0x060005AA RID: 1450 RVA: 0x000126E0 File Offset: 0x000108E0
		public override string ToString()
		{
			return string.Format("{0} {1}:{2}:{3}:{4}:{5}", new object[]
			{
				base.ToString(),
				this.hours,
				this.minutes,
				this.seconds,
				this.frames,
				this.subFrames
			});
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005AB RID: 1451 RVA: 0x00012750 File Offset: 0x00010950
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.hours);
			writer.Write(this.minutes);
			writer.Write(this.seconds);
			writer.Write(this.frames);
			writer.Write(this.subFrames);
		}

		// Token: 0x040005F9 RID: 1529
		private byte hours;

		// Token: 0x040005FA RID: 1530
		private byte minutes;

		// Token: 0x040005FB RID: 1531
		private byte seconds;

		// Token: 0x040005FC RID: 1532
		private byte frames;

		// Token: 0x040005FD RID: 1533
		private byte subFrames;
	}
}
