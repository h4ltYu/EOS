using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioSessionControl interface
	/// Defined in AudioPolicy.h
	/// </summary>
	// Token: 0x0200002D RID: 45
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d")]
	public interface IAudioSessionControl2 : IAudioSessionControl
	{
		/// <summary>
		/// Retrieves the current state of the audio session.
		/// </summary>
		/// <param name="state">Receives the current session state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B9 RID: 185
		[PreserveSig]
		int GetState(out AudioSessionState state);

		/// <summary>
		/// Retrieves the display name for the audio session.
		/// </summary>
		/// <param name="displayName">Receives a string that contains the display name.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BA RID: 186
		[PreserveSig]
		int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string displayName);

		/// <summary>
		/// Assigns a display name to the current audio session.
		/// </summary>
		/// <param name="displayName">A string that contains the new display name for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BB RID: 187
		[PreserveSig]
		int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] [In] string displayName, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the path for the display icon for the audio session.
		/// </summary>
		/// <param name="iconPath">Receives a string that specifies the fully qualified path of the file that contains the icon.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BC RID: 188
		[PreserveSig]
		int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string iconPath);

		/// <summary>
		/// Assigns a display icon to the current session.
		/// </summary>
		/// <param name="iconPath">A string that specifies the fully qualified path of the file that contains the new icon.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BD RID: 189
		[PreserveSig]
		int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] [In] string iconPath, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the grouping parameter of the audio session.
		/// </summary>
		/// <param name="groupingId">Receives the grouping parameter ID.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BE RID: 190
		[PreserveSig]
		int GetGroupingParam(out Guid groupingId);

		/// <summary>
		/// Assigns a session to a grouping of sessions.
		/// </summary>
		/// <param name="groupingId">The new grouping parameter ID.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000BF RID: 191
		[PreserveSig]
		int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct)] [In] Guid groupingId, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Registers the client to receive notifications of session events, including changes in the session state.
		/// </summary>
		/// <param name="client">A client-implemented <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionEvents" /> interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C0 RID: 192
		[PreserveSig]
		int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

		/// <summary>
		/// Deletes a previous registration by the client to receive notifications.
		/// </summary>
		/// <param name="client">A client-implemented <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionEvents" /> interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C1 RID: 193
		[PreserveSig]
		int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);

		/// <summary>
		/// Retrieves the identifier for the audio session.
		/// </summary>
		/// <param name="retVal">Receives the session identifier.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C2 RID: 194
		[PreserveSig]
		int GetSessionIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string retVal);

		/// <summary>
		/// Retrieves the identifier of the audio session instance.
		/// </summary>
		/// <param name="retVal">Receives the identifier of a particular instance.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C3 RID: 195
		[PreserveSig]
		int GetSessionInstanceIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string retVal);

		/// <summary>
		/// Retrieves the process identifier of the audio session.
		/// </summary>
		/// <param name="retVal">Receives the process identifier of the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C4 RID: 196
		[PreserveSig]
		int GetProcessId(out uint retVal);

		/// <summary>
		/// Indicates whether the session is a system sounds session.
		/// </summary>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C5 RID: 197
		[PreserveSig]
		int IsSystemSoundsSession();

		/// <summary>
		/// Enables or disables the default stream attenuation experience (auto-ducking) provided by the system.
		/// </summary>
		/// <param name="optOut">A variable that enables or disables system auto-ducking.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000C6 RID: 198
		[PreserveSig]
		int SetDuckingPreference(bool optOut);
	}
}
