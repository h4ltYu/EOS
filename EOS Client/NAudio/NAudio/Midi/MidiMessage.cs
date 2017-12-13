using System;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI message
	/// </summary>
	// Token: 0x020000E9 RID: 233
	public class MidiMessage
	{
		/// <summary>
		/// Creates a new MIDI message
		/// </summary>
		/// <param name="status">Status</param>
		/// <param name="data1">Data parameter 1</param>
		/// <param name="data2">Data parameter 2</param>
		// Token: 0x06000557 RID: 1367 RVA: 0x00011674 File Offset: 0x0000F874
		public MidiMessage(int status, int data1, int data2)
		{
			this.rawData = status + (data1 << 8) + (data2 << 16);
		}

		/// <summary>
		/// Creates a new MIDI message from a raw message
		/// </summary>
		/// <param name="rawData">A packed MIDI message from an MMIO function</param>
		// Token: 0x06000558 RID: 1368 RVA: 0x0001168C File Offset: 0x0000F88C
		public MidiMessage(int rawData)
		{
			this.rawData = rawData;
		}

		/// <summary>
		/// Creates a Note On message
		/// </summary>
		/// <param name="note">Note number</param>
		/// <param name="volume">Volume</param>
		/// <param name="channel">MIDI channel</param>
		/// <returns>A new MidiMessage object</returns>
		// Token: 0x06000559 RID: 1369 RVA: 0x0001169B File Offset: 0x0000F89B
		public static MidiMessage StartNote(int note, int volume, int channel)
		{
			return new MidiMessage(144 + channel - 1, note, volume);
		}

		/// <summary>
		/// Creates a Note Off message
		/// </summary>
		/// <param name="note">Note number</param>
		/// <param name="volume">Volume</param>
		/// <param name="channel">MIDI channel (1-16)</param>
		/// <returns>A new MidiMessage object</returns>
		// Token: 0x0600055A RID: 1370 RVA: 0x000116AD File Offset: 0x0000F8AD
		public static MidiMessage StopNote(int note, int volume, int channel)
		{
			return new MidiMessage(128 + channel - 1, note, volume);
		}

		/// <summary>
		/// Creates a patch change message
		/// </summary>
		/// <param name="patch">The patch number</param>
		/// <param name="channel">The MIDI channel number (1-16)</param>
		/// <returns>A new MidiMessageObject</returns>
		// Token: 0x0600055B RID: 1371 RVA: 0x000116BF File Offset: 0x0000F8BF
		public static MidiMessage ChangePatch(int patch, int channel)
		{
			return new MidiMessage(192 + channel - 1, patch, 0);
		}

		/// <summary>
		/// Creates a Control Change message
		/// </summary>
		/// <param name="controller">The controller number to change</param>
		/// <param name="value">The value to set the controller to</param>
		/// <param name="channel">The MIDI channel number (1-16)</param>
		/// <returns>A new MidiMessageObject</returns>
		// Token: 0x0600055C RID: 1372 RVA: 0x000116D1 File Offset: 0x0000F8D1
		public static MidiMessage ChangeControl(int controller, int value, int channel)
		{
			return new MidiMessage(176 + channel - 1, controller, value);
		}

		/// <summary>
		/// Returns the raw MIDI message data
		/// </summary>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000116E3 File Offset: 0x0000F8E3
		public int RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x040005D6 RID: 1494
		private int rawData;
	}
}
