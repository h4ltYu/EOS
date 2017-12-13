using System;

namespace NAudio.Midi
{
	/// <summary>
	/// MIDI In Message Information
	/// </summary>
	// Token: 0x02000066 RID: 102
	public class MidiInMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Create a new MIDI In Message EventArgs
		/// </summary>
		/// <param name="message"></param>
		/// <param name="timestamp"></param>
		// Token: 0x0600023C RID: 572 RVA: 0x000074B8 File Offset: 0x000056B8
		public MidiInMessageEventArgs(int message, int timestamp)
		{
			this.RawMessage = message;
			this.Timestamp = timestamp;
			try
			{
				this.MidiEvent = MidiEvent.FromRawMessage(message);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// The Raw message received from the MIDI In API
		/// </summary>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000074FC File Offset: 0x000056FC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00007504 File Offset: 0x00005704
		public int RawMessage { get; private set; }

		/// <summary>
		/// The raw message interpreted as a MidiEvent
		/// </summary>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000750D File Offset: 0x0000570D
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007515 File Offset: 0x00005715
		public MidiEvent MidiEvent { get; private set; }

		/// <summary>
		/// The timestamp in milliseconds for this message
		/// </summary>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000751E File Offset: 0x0000571E
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007526 File Offset: 0x00005726
		public int Timestamp { get; private set; }
	}
}
