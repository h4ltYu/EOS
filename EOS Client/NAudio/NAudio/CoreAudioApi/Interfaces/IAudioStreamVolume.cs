using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000034 RID: 52
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("93014887-242D-4068-8A15-CF5E93B90FE3")]
	internal interface IAudioStreamVolume
	{
		// Token: 0x060000D9 RID: 217
		[PreserveSig]
		int GetChannelCount(out uint dwCount);

		// Token: 0x060000DA RID: 218
		[PreserveSig]
		int SetChannelVolume([In] uint dwIndex, [In] float fLevel);

		// Token: 0x060000DB RID: 219
		[PreserveSig]
		int GetChannelVolume([In] uint dwIndex, out float fLevel);

		// Token: 0x060000DC RID: 220
		[PreserveSig]
		int SetAllVoumes([In] uint dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeParamIndex = 0)] [In] float[] fVolumes);

		// Token: 0x060000DD RID: 221
		[PreserveSig]
		int GetAllVolumes([In] uint dwCount, [MarshalAs(UnmanagedType.LPArray)] float[] pfVolumes);
	}
}
