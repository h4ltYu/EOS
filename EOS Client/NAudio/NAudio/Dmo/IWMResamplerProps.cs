using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// Windows Media Resampler Props
	/// wmcodecdsp.h
	/// </summary>
	// Token: 0x0200008D RID: 141
	[Guid("E7E9984F-F09F-4da4-903F-6E2E0EFE56B5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWMResamplerProps
	{
		/// <summary>
		/// Range is 1 to 60
		/// </summary>
		// Token: 0x06000304 RID: 772
		int SetHalfFilterLength(int outputQuality);

		/// <summary>
		///  Specifies the channel matrix.
		/// </summary>
		// Token: 0x06000305 RID: 773
		int SetUserChannelMtx([In] float[] channelConversionMatrix);
	}
}
