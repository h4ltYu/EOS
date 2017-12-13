using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioClient interface
	/// Defined in AudioClient.h
	/// </summary>
	// Token: 0x02000125 RID: 293
	[Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioClient
	{
		// Token: 0x0600066E RID: 1646
		[PreserveSig]
		int Initialize(AudioClientShareMode shareMode, AudioClientStreamFlags StreamFlags, long hnsBufferDuration, long hnsPeriodicity, [In] WaveFormat pFormat, [In] ref Guid AudioSessionGuid);

		/// <summary>
		/// The GetBufferSize method retrieves the size (maximum capacity) of the endpoint buffer.
		/// </summary>
		// Token: 0x0600066F RID: 1647
		int GetBufferSize(out uint bufferSize);

		// Token: 0x06000670 RID: 1648
		[return: MarshalAs(UnmanagedType.I8)]
		long GetStreamLatency();

		// Token: 0x06000671 RID: 1649
		int GetCurrentPadding(out int currentPadding);

		// Token: 0x06000672 RID: 1650
		[PreserveSig]
		int IsFormatSupported(AudioClientShareMode shareMode, [In] WaveFormat pFormat, [MarshalAs(UnmanagedType.LPStruct)] out WaveFormatExtensible closestMatchFormat);

		// Token: 0x06000673 RID: 1651
		int GetMixFormat(out IntPtr deviceFormatPointer);

		// Token: 0x06000674 RID: 1652
		int GetDevicePeriod(out long defaultDevicePeriod, out long minimumDevicePeriod);

		// Token: 0x06000675 RID: 1653
		int Start();

		// Token: 0x06000676 RID: 1654
		int Stop();

		// Token: 0x06000677 RID: 1655
		int Reset();

		// Token: 0x06000678 RID: 1656
		int SetEventHandle(IntPtr eventHandle);

		/// <summary>
		/// The GetService method accesses additional services from the audio client object.
		/// </summary>
		/// <param name="interfaceId">The interface ID for the requested service.</param>
		/// <param name="interfacePointer">Pointer to a pointer variable into which the method writes the address of an instance of the requested interface. </param>
		// Token: 0x06000679 RID: 1657
		[PreserveSig]
		int GetService([MarshalAs(UnmanagedType.LPStruct)] [In] Guid interfaceId, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);
	}
}
