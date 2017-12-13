using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Defined in AudioClient.h
	/// </summary>
	// Token: 0x0200002A RID: 42
	[Guid("6f49ff73-6727-49AC-A008-D98CF5E70048")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioClock2 : IAudioClock
	{
		// Token: 0x060000AD RID: 173
		[PreserveSig]
		int GetDevicePosition(out ulong devicePosition, out ulong qpcPosition);
	}
}
