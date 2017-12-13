using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Defined in AudioClient.h
	/// </summary>
	// Token: 0x02000029 RID: 41
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CD63314F-3FBA-4a1b-812C-EF96358728E7")]
	internal interface IAudioClock
	{
		// Token: 0x060000AA RID: 170
		[PreserveSig]
		int GetFrequency(out ulong frequency);

		// Token: 0x060000AB RID: 171
		[PreserveSig]
		int GetPosition(out ulong devicePosition, out ulong qpcPosition);

		// Token: 0x060000AC RID: 172
		[PreserveSig]
		int GetCharacteristics(out uint characteristics);
	}
}
