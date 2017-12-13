using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000127 RID: 295
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
	internal interface IAudioMeterInformation
	{
		// Token: 0x0600068C RID: 1676
		int GetPeakValue(out float pfPeak);

		// Token: 0x0600068D RID: 1677
		int GetMeteringChannelCount(out int pnChannelCount);

		// Token: 0x0600068E RID: 1678
		int GetChannelsPeakValues(int u32ChannelCount, [In] IntPtr afPeakValues);

		// Token: 0x0600068F RID: 1679
		int QueryHardwareSupport(out int pdwHardwareSupportMask);
	}
}
