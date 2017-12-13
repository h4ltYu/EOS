using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Supported wave formats for WaveOutCapabilities
	/// </summary>
	// Token: 0x0200018C RID: 396
	[Flags]
	public enum SupportedWaveFormat
	{
		/// <summary>
		/// 11.025 kHz, Mono,   8-bit
		/// </summary>
		// Token: 0x04000969 RID: 2409
		WAVE_FORMAT_1M08 = 1,
		/// <summary>
		/// 11.025 kHz, Stereo, 8-bit
		/// </summary>
		// Token: 0x0400096A RID: 2410
		WAVE_FORMAT_1S08 = 2,
		/// <summary>
		/// 11.025 kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x0400096B RID: 2411
		WAVE_FORMAT_1M16 = 4,
		/// <summary>
		/// 11.025 kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x0400096C RID: 2412
		WAVE_FORMAT_1S16 = 8,
		/// <summary>
		/// 22.05  kHz, Mono,   8-bit
		/// </summary>
		// Token: 0x0400096D RID: 2413
		WAVE_FORMAT_2M08 = 16,
		/// <summary>
		/// 22.05  kHz, Stereo, 8-bit 
		/// </summary>
		// Token: 0x0400096E RID: 2414
		WAVE_FORMAT_2S08 = 32,
		/// <summary>
		/// 22.05  kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x0400096F RID: 2415
		WAVE_FORMAT_2M16 = 64,
		/// <summary>
		/// 22.05  kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x04000970 RID: 2416
		WAVE_FORMAT_2S16 = 128,
		/// <summary>
		/// 44.1   kHz, Mono,   8-bit 
		/// </summary>
		// Token: 0x04000971 RID: 2417
		WAVE_FORMAT_4M08 = 256,
		/// <summary>
		/// 44.1   kHz, Stereo, 8-bit 
		/// </summary>
		// Token: 0x04000972 RID: 2418
		WAVE_FORMAT_4S08 = 512,
		/// <summary>
		/// 44.1   kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x04000973 RID: 2419
		WAVE_FORMAT_4M16 = 1024,
		/// <summary>
		///  44.1   kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x04000974 RID: 2420
		WAVE_FORMAT_4S16 = 2048,
		/// <summary>
		/// 44.1   kHz, Mono,   8-bit 
		/// </summary>
		// Token: 0x04000975 RID: 2421
		WAVE_FORMAT_44M08 = 256,
		/// <summary>
		/// 44.1   kHz, Stereo, 8-bit 
		/// </summary>
		// Token: 0x04000976 RID: 2422
		WAVE_FORMAT_44S08 = 512,
		/// <summary>
		/// 44.1   kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x04000977 RID: 2423
		WAVE_FORMAT_44M16 = 1024,
		/// <summary>
		/// 44.1   kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x04000978 RID: 2424
		WAVE_FORMAT_44S16 = 2048,
		/// <summary>
		/// 48     kHz, Mono,   8-bit 
		/// </summary>
		// Token: 0x04000979 RID: 2425
		WAVE_FORMAT_48M08 = 4096,
		/// <summary>
		///  48     kHz, Stereo, 8-bit
		/// </summary>
		// Token: 0x0400097A RID: 2426
		WAVE_FORMAT_48S08 = 8192,
		/// <summary>
		/// 48     kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x0400097B RID: 2427
		WAVE_FORMAT_48M16 = 16384,
		/// <summary>
		/// 48     kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x0400097C RID: 2428
		WAVE_FORMAT_48S16 = 32768,
		/// <summary>
		/// 96     kHz, Mono,   8-bit 
		/// </summary>
		// Token: 0x0400097D RID: 2429
		WAVE_FORMAT_96M08 = 65536,
		/// <summary>
		/// 96     kHz, Stereo, 8-bit
		/// </summary>
		// Token: 0x0400097E RID: 2430
		WAVE_FORMAT_96S08 = 131072,
		/// <summary>
		/// 96     kHz, Mono,   16-bit
		/// </summary>
		// Token: 0x0400097F RID: 2431
		WAVE_FORMAT_96M16 = 262144,
		/// <summary>
		/// 96     kHz, Stereo, 16-bit
		/// </summary>
		// Token: 0x04000980 RID: 2432
		WAVE_FORMAT_96S16 = 524288
	}
}
