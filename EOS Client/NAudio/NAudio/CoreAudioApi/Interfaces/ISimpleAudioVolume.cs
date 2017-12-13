using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio ISimpleAudioVolume interface
	/// Defined in AudioClient.h
	/// </summary>
	// Token: 0x02000035 RID: 53
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
	internal interface ISimpleAudioVolume
	{
		/// <summary>
		/// Sets the master volume level for the audio session.
		/// </summary>
		/// <param name="levelNorm">The new volume level expressed as a normalized value between 0.0 and 1.0.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000DE RID: 222
		[PreserveSig]
		int SetMasterVolume([MarshalAs(UnmanagedType.R4)] [In] float levelNorm, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the client volume level for the audio session.
		/// </summary>
		/// <param name="levelNorm">Receives the volume level expressed as a normalized value between 0.0 and 1.0. </param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000DF RID: 223
		[PreserveSig]
		int GetMasterVolume([MarshalAs(UnmanagedType.R4)] out float levelNorm);

		/// <summary>
		/// Sets the muting state for the audio session.
		/// </summary>
		/// <param name="isMuted">The new muting state.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000E0 RID: 224
		[PreserveSig]
		int SetMute([MarshalAs(UnmanagedType.Bool)] [In] bool isMuted, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the current muting state for the audio session.
		/// </summary>
		/// <param name="isMuted">Receives the muting state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000E1 RID: 225
		[PreserveSig]
		int GetMute([MarshalAs(UnmanagedType.Bool)] out bool isMuted);
	}
}
