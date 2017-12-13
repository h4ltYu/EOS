using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Signal Generator type
	/// </summary>
	// Token: 0x0200007C RID: 124
	public enum SignalGeneratorType
	{
		/// <summary>
		/// Pink noise
		/// </summary>
		// Token: 0x040003A6 RID: 934
		Pink,
		/// <summary>
		/// White noise
		/// </summary>
		// Token: 0x040003A7 RID: 935
		White,
		/// <summary>
		/// Sweep
		/// </summary>
		// Token: 0x040003A8 RID: 936
		Sweep,
		/// <summary>
		/// Sine wave
		/// </summary>
		// Token: 0x040003A9 RID: 937
		Sin,
		/// <summary>
		/// Square wave
		/// </summary>
		// Token: 0x040003AA RID: 938
		Square,
		/// <summary>
		/// Triangle Wave
		/// </summary>
		// Token: 0x040003AB RID: 939
		Triangle,
		/// <summary>
		/// Sawtooth wave
		/// </summary>
		// Token: 0x040003AC RID: 940
		SawTooth
	}
}
