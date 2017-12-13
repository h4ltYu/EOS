using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioSessionControl interface
	/// Defined in AudioPolicy.h
	/// </summary>
	// Token: 0x0200002C RID: 44
	[Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioSessionControl
	{
		/// <summary>
		/// Retrieves the current state of the audio session.
		/// </summary>
		/// <param name="state">Receives the current session state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B0 RID: 176
		[PreserveSig]
		int GetState(out AudioSessionState state);

		/// <summary>
		/// Retrieves the display name for the audio session.
		/// </summary>
		/// <param name="displayName">Receives a string that contains the display name.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B1 RID: 177
		[PreserveSig]
		int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string displayName);

		/// <summary>
		/// Assigns a display name to the current audio session.
		/// </summary>
		/// <param name="displayName">A string that contains the new display name for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B2 RID: 178
		[PreserveSig]
		int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] [In] string displayName, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the path for the display icon for the audio session.
		/// </summary>
		/// <param name="iconPath">Receives a string that specifies the fully qualified path of the file that contains the icon.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B3 RID: 179
		[PreserveSig]
		int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string iconPath);

		/// <summary>
		/// Assigns a display icon to the current session.
		/// </summary>
		/// <param name="iconPath">A string that specifies the fully qualified path of the file that contains the new icon.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B4 RID: 180
		[PreserveSig]
		int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] [In] string iconPath, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Retrieves the grouping parameter of the audio session.
		/// </summary>
		/// <param name="groupingId">Receives the grouping parameter ID.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B5 RID: 181
		[PreserveSig]
		int GetGroupingParam(out Guid groupingId);

		/// <summary>
		/// Assigns a session to a grouping of sessions.
		/// </summary>
		/// <param name="groupingId">The new grouping parameter ID.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B6 RID: 182
		[PreserveSig]
		int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct)] [In] Guid groupingId, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid eventContext);

		/// <summary>
		/// Registers the client to receive notifications of session events, including changes in the session state.
		/// </summary>
		/// <param name="client">A client-implemented <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionEvents" /> interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B7 RID: 183
		[PreserveSig]
		int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

		/// <summary>
		/// Deletes a previous registration by the client to receive notifications.
		/// </summary>
		/// <param name="client">A client-implemented <see cref="T:NAudio.CoreAudioApi.Interfaces.IAudioSessionEvents" /> interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x060000B8 RID: 184
		[PreserveSig]
		int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);
	}
}
