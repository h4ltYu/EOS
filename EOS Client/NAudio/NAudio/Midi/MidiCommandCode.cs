using System;

namespace NAudio.Midi
{
	/// <summary>
	/// MIDI command codes
	/// </summary>
	// Token: 0x020000D9 RID: 217
	public enum MidiCommandCode : byte
	{
		/// <summary>Note Off</summary>
		// Token: 0x04000585 RID: 1413
		NoteOff = 128,
		/// <summary>Note On</summary>
		// Token: 0x04000586 RID: 1414
		NoteOn = 144,
		/// <summary>Key After-touch</summary>
		// Token: 0x04000587 RID: 1415
		KeyAfterTouch = 160,
		/// <summary>Control change</summary>
		// Token: 0x04000588 RID: 1416
		ControlChange = 176,
		/// <summary>Patch change</summary>
		// Token: 0x04000589 RID: 1417
		PatchChange = 192,
		/// <summary>Channel after-touch</summary>
		// Token: 0x0400058A RID: 1418
		ChannelAfterTouch = 208,
		/// <summary>Pitch wheel change</summary>
		// Token: 0x0400058B RID: 1419
		PitchWheelChange = 224,
		/// <summary>Sysex message</summary>
		// Token: 0x0400058C RID: 1420
		Sysex = 240,
		/// <summary>Eox (comes at end of a sysex message)</summary>
		// Token: 0x0400058D RID: 1421
		Eox = 247,
		/// <summary>Timing clock (used when synchronization is required)</summary>
		// Token: 0x0400058E RID: 1422
		TimingClock,
		/// <summary>Start sequence</summary>
		// Token: 0x0400058F RID: 1423
		StartSequence = 250,
		/// <summary>Continue sequence</summary>
		// Token: 0x04000590 RID: 1424
		ContinueSequence,
		/// <summary>Stop sequence</summary>
		// Token: 0x04000591 RID: 1425
		StopSequence,
		/// <summary>Auto-Sensing</summary>
		// Token: 0x04000592 RID: 1426
		AutoSensing = 254,
		/// <summary>Meta-event</summary>
		// Token: 0x04000593 RID: 1427
		MetaEvent
	}
}
