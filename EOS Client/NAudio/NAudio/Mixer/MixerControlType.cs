using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// Mixer control types
	/// </summary>
	// Token: 0x02000101 RID: 257
	public enum MixerControlType
	{
		/// <summary>Custom</summary>
		// Token: 0x0400062B RID: 1579
		Custom,
		/// <summary>Boolean meter</summary>
		// Token: 0x0400062C RID: 1580
		BooleanMeter = 268500992,
		/// <summary>Signed meter</summary>
		// Token: 0x0400062D RID: 1581
		SignedMeter = 268566528,
		/// <summary>Peak meter</summary>
		// Token: 0x0400062E RID: 1582
		PeakMeter,
		/// <summary>Unsigned meter</summary>
		// Token: 0x0400062F RID: 1583
		UnsignedMeter = 268632064,
		/// <summary>Boolean</summary>
		// Token: 0x04000630 RID: 1584
		Boolean = 536936448,
		/// <summary>On Off</summary>
		// Token: 0x04000631 RID: 1585
		OnOff,
		/// <summary>Mute</summary>
		// Token: 0x04000632 RID: 1586
		Mute,
		/// <summary>Mono</summary>
		// Token: 0x04000633 RID: 1587
		Mono,
		/// <summary>Loudness</summary>
		// Token: 0x04000634 RID: 1588
		Loudness,
		/// <summary>Stereo Enhance</summary>
		// Token: 0x04000635 RID: 1589
		StereoEnhance,
		/// <summary>Button</summary>
		// Token: 0x04000636 RID: 1590
		Button = 553713664,
		/// <summary>Decibels</summary>
		// Token: 0x04000637 RID: 1591
		Decibels = 805568512,
		/// <summary>Signed</summary>
		// Token: 0x04000638 RID: 1592
		Signed = 805437440,
		/// <summary>Unsigned</summary>
		// Token: 0x04000639 RID: 1593
		Unsigned = 805502976,
		/// <summary>Percent</summary>
		// Token: 0x0400063A RID: 1594
		Percent = 805634048,
		/// <summary>Slider</summary>
		// Token: 0x0400063B RID: 1595
		Slider = 1073872896,
		/// <summary>Pan</summary>
		// Token: 0x0400063C RID: 1596
		Pan,
		/// <summary>Q-sound pan</summary>
		// Token: 0x0400063D RID: 1597
		QSoundPan,
		/// <summary>Fader</summary>
		// Token: 0x0400063E RID: 1598
		Fader = 1342373888,
		/// <summary>Volume</summary>
		// Token: 0x0400063F RID: 1599
		Volume,
		/// <summary>Bass</summary>
		// Token: 0x04000640 RID: 1600
		Bass,
		/// <summary>Treble</summary>
		// Token: 0x04000641 RID: 1601
		Treble,
		/// <summary>Equaliser</summary>
		// Token: 0x04000642 RID: 1602
		Equalizer,
		/// <summary>Single Select</summary>
		// Token: 0x04000643 RID: 1603
		SingleSelect = 1879113728,
		/// <summary>Mux</summary>
		// Token: 0x04000644 RID: 1604
		Mux,
		/// <summary>Multiple select</summary>
		// Token: 0x04000645 RID: 1605
		MultipleSelect = 1895890944,
		/// <summary>Mixer</summary>
		// Token: 0x04000646 RID: 1606
		Mixer,
		/// <summary>Micro time</summary>
		// Token: 0x04000647 RID: 1607
		MicroTime = 1610809344,
		/// <summary>Milli time</summary>
		// Token: 0x04000648 RID: 1608
		MilliTime = 1627586560
	}
}
