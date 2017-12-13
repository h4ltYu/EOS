using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Flags indicating what features this WaveOut device supports
	/// </summary>
	// Token: 0x0200018D RID: 397
	[Flags]
	internal enum WaveOutSupport
	{
		/// <summary>supports pitch control (WAVECAPS_PITCH)</summary>
		// Token: 0x04000982 RID: 2434
		Pitch = 1,
		/// <summary>supports playback rate control (WAVECAPS_PLAYBACKRATE)</summary>
		// Token: 0x04000983 RID: 2435
		PlaybackRate = 2,
		/// <summary>supports volume control (WAVECAPS_VOLUME)</summary>
		// Token: 0x04000984 RID: 2436
		Volume = 4,
		/// <summary>supports separate left-right volume control (WAVECAPS_LRVOLUME)</summary>
		// Token: 0x04000985 RID: 2437
		LRVolume = 8,
		/// <summary>(WAVECAPS_SYNC)</summary>
		// Token: 0x04000986 RID: 2438
		Sync = 16,
		/// <summary>(WAVECAPS_SAMPLEACCURATE)</summary>
		// Token: 0x04000987 RID: 2439
		SampleAccurate = 32
	}
}
