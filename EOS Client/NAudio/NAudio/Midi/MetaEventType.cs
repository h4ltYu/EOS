using System;

namespace NAudio.Midi
{
	/// <summary>
	/// MIDI MetaEvent Type
	/// </summary>
	// Token: 0x020000D8 RID: 216
	public enum MetaEventType : byte
	{
		/// <summary>Track sequence number</summary>
		// Token: 0x04000572 RID: 1394
		TrackSequenceNumber,
		/// <summary>Text event</summary>
		// Token: 0x04000573 RID: 1395
		TextEvent,
		/// <summary>Copyright</summary>
		// Token: 0x04000574 RID: 1396
		Copyright,
		/// <summary>Sequence track name</summary>
		// Token: 0x04000575 RID: 1397
		SequenceTrackName,
		/// <summary>Track instrument name</summary>
		// Token: 0x04000576 RID: 1398
		TrackInstrumentName,
		/// <summary>Lyric</summary>
		// Token: 0x04000577 RID: 1399
		Lyric,
		/// <summary>Marker</summary>
		// Token: 0x04000578 RID: 1400
		Marker,
		/// <summary>Cue point</summary>
		// Token: 0x04000579 RID: 1401
		CuePoint,
		/// <summary>Program (patch) name</summary>
		// Token: 0x0400057A RID: 1402
		ProgramName,
		/// <summary>Device (port) name</summary>
		// Token: 0x0400057B RID: 1403
		DeviceName,
		/// <summary>MIDI Channel (not official?)</summary>
		// Token: 0x0400057C RID: 1404
		MidiChannel = 32,
		/// <summary>MIDI Port (not official?)</summary>
		// Token: 0x0400057D RID: 1405
		MidiPort,
		/// <summary>End track</summary>
		// Token: 0x0400057E RID: 1406
		EndTrack = 47,
		/// <summary>Set tempo</summary>
		// Token: 0x0400057F RID: 1407
		SetTempo = 81,
		/// <summary>SMPTE offset</summary>
		// Token: 0x04000580 RID: 1408
		SmpteOffset = 84,
		/// <summary>Time signature</summary>
		// Token: 0x04000581 RID: 1409
		TimeSignature = 88,
		/// <summary>Key signature</summary>
		// Token: 0x04000582 RID: 1410
		KeySignature,
		/// <summary>Sequencer specific</summary>
		// Token: 0x04000583 RID: 1411
		SequencerSpecific = 127
	}
}
