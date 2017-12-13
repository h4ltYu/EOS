using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// defined in MMDeviceAPI.h
	/// </summary>
	// Token: 0x0200012B RID: 299
	[Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IMMEndpoint
	{
		// Token: 0x0600069B RID: 1691
		int GetDataFlow(out DataFlow dataFlow);
	}
}
