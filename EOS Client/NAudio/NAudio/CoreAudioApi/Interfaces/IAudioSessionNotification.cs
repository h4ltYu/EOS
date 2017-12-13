using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioSessionNotification interface
	/// Defined in AudioPolicy.h
	/// </summary>
	// Token: 0x02000020 RID: 32
	[Guid("641DD20B-4D41-49CC-ABA3-174B9477BB08")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioSessionNotification
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="newSession">session being added</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000093 RID: 147
		[PreserveSig]
		int OnSessionCreated(IAudioSessionControl newSession);
	}
}
