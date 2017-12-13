using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000152 RID: 338
	internal enum ASIOError
	{
		// Token: 0x04000771 RID: 1905
		ASE_OK,
		// Token: 0x04000772 RID: 1906
		ASE_SUCCESS = 1061701536,
		// Token: 0x04000773 RID: 1907
		ASE_NotPresent = -1000,
		// Token: 0x04000774 RID: 1908
		ASE_HWMalfunction,
		// Token: 0x04000775 RID: 1909
		ASE_InvalidParameter,
		// Token: 0x04000776 RID: 1910
		ASE_InvalidMode,
		// Token: 0x04000777 RID: 1911
		ASE_SPNotAdvancing,
		// Token: 0x04000778 RID: 1912
		ASE_NoClock,
		// Token: 0x04000779 RID: 1913
		ASE_NoMemory
	}
}
