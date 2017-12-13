using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000033 RID: 51
	[Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioSessionManager2 : IAudioSessionManager
	{
		/// <summary>
		/// Retrieves an audio session control.
		/// </summary>
		/// <param name="sessionId">A new or existing session ID.</param>
		/// <param name="streamFlags">Audio session flags.</param>
		/// <param name="sessionControl">Receives an <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionControl" /> interface for the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000D2 RID: 210
		[PreserveSig]
		int GetAudioSessionControl([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId = default(Guid), [MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl sessionControl);

		/// <summary>
		/// Retrieves a simple audio volume control.
		/// </summary>
		/// <param name="sessionId">A new or existing session ID.</param>
		/// <param name="streamFlags">Audio session flags.</param>
		/// <param name="audioVolume">Receives an <see cref="T:NAudio.CoreAudioApi.Interfaces.ISimpleAudioVolume" /> interface for the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000D3 RID: 211
		[PreserveSig]
		int GetSimpleAudioVolume([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId = default(Guid), [MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out ISimpleAudioVolume audioVolume);

		// Token: 0x060000D4 RID: 212
		[PreserveSig]
		int GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum);

		// Token: 0x060000D5 RID: 213
		[PreserveSig]
		int RegisterSessionNotification(IAudioSessionNotification sessionNotification);

		// Token: 0x060000D6 RID: 214
		[PreserveSig]
		int UnregisterSessionNotification(IAudioSessionNotification sessionNotification);

		// Token: 0x060000D7 RID: 215
		[PreserveSig]
		int RegisterDuckNotification(string sessionID, IAudioSessionNotification audioVolumeDuckNotification);

		// Token: 0x060000D8 RID: 216
		[PreserveSig]
		int UnregisterDuckNotification(IntPtr audioVolumeDuckNotification);
	}
}
