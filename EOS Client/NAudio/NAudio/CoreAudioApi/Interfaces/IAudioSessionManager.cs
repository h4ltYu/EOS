using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioSessionManager interface
	/// Defined in AudioPolicy.h
	/// </summary>
	// Token: 0x02000032 RID: 50
	[Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioSessionManager
	{
		/// <summary>
		/// Retrieves an audio session control.
		/// </summary>
		/// <param name="sessionId">A new or existing session ID.</param>
		/// <param name="streamFlags">Audio session flags.</param>
		/// <param name="sessionControl">Receives an <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionControl" /> interface for the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000D0 RID: 208
		[PreserveSig]
		int GetAudioSessionControl([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId = default(Guid), [MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl sessionControl);

		/// <summary>
		/// Retrieves a simple audio volume control.
		/// </summary>
		/// <param name="sessionId">A new or existing session ID.</param>
		/// <param name="streamFlags">Audio session flags.</param>
		/// <param name="audioVolume">Receives an <see cref="T:NAudio.CoreAudioApi.Interfaces.ISimpleAudioVolume" /> interface for the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000D1 RID: 209
		[PreserveSig]
		int GetSimpleAudioVolume([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId = default(Guid), [MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out ISimpleAudioVolume audioVolume);
	}
}
