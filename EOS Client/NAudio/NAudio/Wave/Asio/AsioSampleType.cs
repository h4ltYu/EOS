using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// ASIO Sample Type
	/// </summary>
	// Token: 0x0200006D RID: 109
	public enum AsioSampleType
	{
		/// <summary>
		/// Int 16 MSB
		/// </summary>
		// Token: 0x04000359 RID: 857
		Int16MSB,
		/// <summary>
		/// Int 24 MSB (used for 20 bits as well)
		/// </summary>
		// Token: 0x0400035A RID: 858
		Int24MSB,
		/// <summary>
		/// Int 32 MSB
		/// </summary>
		// Token: 0x0400035B RID: 859
		Int32MSB,
		/// <summary>
		/// IEEE 754 32 bit float
		/// </summary>
		// Token: 0x0400035C RID: 860
		Float32MSB,
		/// <summary>
		/// IEEE 754 64 bit double float
		/// </summary>
		// Token: 0x0400035D RID: 861
		Float64MSB,
		/// <summary>
		/// 32 bit data with 16 bit alignment
		/// </summary>
		// Token: 0x0400035E RID: 862
		Int32MSB16 = 8,
		/// <summary>
		/// 32 bit data with 18 bit alignment
		/// </summary>
		// Token: 0x0400035F RID: 863
		Int32MSB18,
		/// <summary>
		/// 32 bit data with 20 bit alignment
		/// </summary>
		// Token: 0x04000360 RID: 864
		Int32MSB20,
		/// <summary>
		/// 32 bit data with 24 bit alignment
		/// </summary>
		// Token: 0x04000361 RID: 865
		Int32MSB24,
		/// <summary>
		/// Int 16 LSB
		/// </summary>
		// Token: 0x04000362 RID: 866
		Int16LSB = 16,
		/// <summary>
		/// Int 24 LSB
		/// used for 20 bits as well
		/// </summary>
		// Token: 0x04000363 RID: 867
		Int24LSB,
		/// <summary>
		/// Int 32 LSB
		/// </summary>
		// Token: 0x04000364 RID: 868
		Int32LSB,
		/// <summary>
		/// IEEE 754 32 bit float, as found on Intel x86 architecture
		/// </summary>
		// Token: 0x04000365 RID: 869
		Float32LSB,
		/// <summary>
		/// IEEE 754 64 bit double float, as found on Intel x86 architecture
		/// </summary>
		// Token: 0x04000366 RID: 870
		Float64LSB,
		/// <summary>
		/// 32 bit data with 16 bit alignment
		/// </summary>
		// Token: 0x04000367 RID: 871
		Int32LSB16 = 24,
		/// <summary>
		/// 32 bit data with 18 bit alignment
		/// </summary>
		// Token: 0x04000368 RID: 872
		Int32LSB18,
		/// <summary>
		/// 32 bit data with 20 bit alignment
		/// </summary>
		// Token: 0x04000369 RID: 873
		Int32LSB20,
		/// <summary>
		/// 32 bit data with 24 bit alignment
		/// </summary>
		// Token: 0x0400036A RID: 874
		Int32LSB24,
		/// <summary>
		/// DSD 1 bit data, 8 samples per byte. First sample in Least significant bit.
		/// </summary>
		// Token: 0x0400036B RID: 875
		DSDInt8LSB1 = 32,
		/// <summary>
		/// DSD 1 bit data, 8 samples per byte. First sample in Most significant bit.
		/// </summary>
		// Token: 0x0400036C RID: 876
		DSDInt8MSB1,
		/// <summary>
		/// DSD 8 bit data, 1 sample per byte. No Endianness required.
		/// </summary>
		// Token: 0x0400036D RID: 877
		DSDInt8NER8 = 40
	}
}
