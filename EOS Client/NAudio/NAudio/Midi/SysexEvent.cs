using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI sysex message
	/// </summary>
	// Token: 0x020000F4 RID: 244
	public class SysexEvent : MidiEvent
	{
		/// <summary>
		/// Reads a sysex message from a MIDI stream
		/// </summary>
		/// <param name="br">Stream of MIDI data</param>
		/// <returns>a new sysex message</returns>
		// Token: 0x060005AC RID: 1452 RVA: 0x000127A4 File Offset: 0x000109A4
		public static SysexEvent ReadSysexEvent(BinaryReader br)
		{
			SysexEvent sysexEvent = new SysexEvent();
			List<byte> list = new List<byte>();
			bool flag = true;
			while (flag)
			{
				byte b = br.ReadByte();
				if (b == 247)
				{
					flag = false;
				}
				else
				{
					list.Add(b);
				}
			}
			sysexEvent.data = list.ToArray();
			return sysexEvent;
		}

		/// <summary>
		/// Describes this sysex message
		/// </summary>
		/// <returns>A string describing the sysex message</returns>
		// Token: 0x060005AD RID: 1453 RVA: 0x000127EC File Offset: 0x000109EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			return string.Format("{0} Sysex: {1} bytes\r\n{2}", base.AbsoluteTime, this.data.Length, stringBuilder.ToString());
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005AE RID: 1454 RVA: 0x00012852 File Offset: 0x00010A52
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.data, 0, this.data.Length);
			writer.Write(247);
		}

		// Token: 0x040005FE RID: 1534
		private byte[] data;
	}
}
