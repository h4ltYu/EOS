using System;

namespace NAudio.Midi
{
	/// <summary>
	/// MidiController enumeration
	/// http://www.midi.org/techspecs/midimessages.php#3
	/// </summary>
	// Token: 0x020000DA RID: 218
	public enum MidiController : byte
	{
		/// <summary>Bank Select (MSB)</summary>
		// Token: 0x04000595 RID: 1429
		BankSelect,
		/// <summary>Modulation (MSB)</summary>
		// Token: 0x04000596 RID: 1430
		Modulation,
		/// <summary>Breath Controller</summary>
		// Token: 0x04000597 RID: 1431
		BreathController,
		/// <summary>Foot controller (MSB)</summary>
		// Token: 0x04000598 RID: 1432
		FootController = 4,
		/// <summary>Main volume</summary>
		// Token: 0x04000599 RID: 1433
		MainVolume = 7,
		/// <summary>Pan</summary>
		// Token: 0x0400059A RID: 1434
		Pan = 10,
		/// <summary>Expression</summary>
		// Token: 0x0400059B RID: 1435
		Expression,
		/// <summary>Bank Select LSB</summary>
		// Token: 0x0400059C RID: 1436
		BankSelectLsb = 32,
		/// <summary>Sustain</summary>
		// Token: 0x0400059D RID: 1437
		Sustain = 64,
		/// <summary>Portamento On/Off</summary>
		// Token: 0x0400059E RID: 1438
		Portamento,
		/// <summary>Sostenuto On/Off</summary>
		// Token: 0x0400059F RID: 1439
		Sostenuto,
		/// <summary>Soft Pedal On/Off</summary>
		// Token: 0x040005A0 RID: 1440
		SoftPedal,
		/// <summary>Legato Footswitch</summary>
		// Token: 0x040005A1 RID: 1441
		LegatoFootswitch,
		/// <summary>Reset all controllers</summary>
		// Token: 0x040005A2 RID: 1442
		ResetAllControllers = 121,
		/// <summary>All notes off</summary>
		// Token: 0x040005A3 RID: 1443
		AllNotesOff = 123
	}
}
