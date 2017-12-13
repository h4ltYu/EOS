using System;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents the different types of technology used by a MIDI out device
	/// </summary>
	/// <remarks>from mmsystem.h</remarks>
	// Token: 0x020000ED RID: 237
	public enum MidiOutTechnology
	{
		/// <summary>The device is a MIDI port</summary>
		// Token: 0x040005EA RID: 1514
		MidiPort = 1,
		/// <summary>The device is a MIDI synth</summary>
		// Token: 0x040005EB RID: 1515
		Synth,
		/// <summary>The device is a square wave synth</summary>
		// Token: 0x040005EC RID: 1516
		SquareWaveSynth,
		/// <summary>The device is an FM synth</summary>
		// Token: 0x040005ED RID: 1517
		FMSynth,
		/// <summary>The device is a MIDI mapper</summary>
		// Token: 0x040005EE RID: 1518
		MidiMapper,
		/// <summary>The device is a WaveTable synth</summary>
		// Token: 0x040005EF RID: 1519
		WaveTableSynth,
		/// <summary>The device is a software synth</summary>
		// Token: 0x040005F0 RID: 1520
		SoftwareSynth
	}
}
