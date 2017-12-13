using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200002E RID: 46
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8")]
	internal interface IAudioSessionEnumerator
	{
		// Token: 0x060000C7 RID: 199
		int GetCount(out int sessionCount);

		// Token: 0x060000C8 RID: 200
		int GetSession(int sessionCount, out IAudioSessionControl session);
	}
}
