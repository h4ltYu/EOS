using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000156 RID: 342
	internal enum ASIOMessageSelector
	{
		// Token: 0x04000786 RID: 1926
		kAsioSelectorSupported = 1,
		// Token: 0x04000787 RID: 1927
		kAsioEngineVersion,
		// Token: 0x04000788 RID: 1928
		kAsioResetRequest,
		// Token: 0x04000789 RID: 1929
		kAsioBufferSizeChange,
		// Token: 0x0400078A RID: 1930
		kAsioResyncRequest,
		// Token: 0x0400078B RID: 1931
		kAsioLatenciesChanged,
		// Token: 0x0400078C RID: 1932
		kAsioSupportsTimeInfo,
		// Token: 0x0400078D RID: 1933
		kAsioSupportsTimeCode,
		// Token: 0x0400078E RID: 1934
		kAsioMMCCommand,
		// Token: 0x0400078F RID: 1935
		kAsioSupportsInputMonitor,
		// Token: 0x04000790 RID: 1936
		kAsioSupportsInputGain,
		// Token: 0x04000791 RID: 1937
		kAsioSupportsInputMeter,
		// Token: 0x04000792 RID: 1938
		kAsioSupportsOutputGain,
		// Token: 0x04000793 RID: 1939
		kAsioSupportsOutputMeter,
		// Token: 0x04000794 RID: 1940
		kAsioOverload
	}
}
