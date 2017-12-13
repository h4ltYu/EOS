using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Windows CoreAudio IAudioSessionControl interface
	/// Defined in AudioPolicy.h
	/// </summary>
	// Token: 0x0200001C RID: 28
	[Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioSessionEvents
	{
		/// <summary>
		/// Notifies the client that the display name for the session has changed.
		/// </summary>
		/// <param name="displayName">The new display name for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000075 RID: 117
		[PreserveSig]
		int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string displayName, [In] ref Guid eventContext);

		/// <summary>
		/// Notifies the client that the display icon for the session has changed.
		/// </summary>
		/// <param name="iconPath">The path for the new display icon for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000076 RID: 118
		[PreserveSig]
		int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string iconPath, [In] ref Guid eventContext);

		/// <summary>
		/// Notifies the client that the volume level or muting state of the session has changed.
		/// </summary>
		/// <param name="volume">The new volume level for the audio session.</param>
		/// <param name="isMuted">The new muting state.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000077 RID: 119
		[PreserveSig]
		int OnSimpleVolumeChanged([MarshalAs(UnmanagedType.R4)] [In] float volume, [MarshalAs(UnmanagedType.Bool)] [In] bool isMuted, [In] ref Guid eventContext);

		/// <summary>
		/// Notifies the client that the volume level of an audio channel in the session submix has changed.
		/// </summary>
		/// <param name="channelCount">The channel count.</param>
		/// <param name="newVolumes">An array of volumnes cooresponding with each channel index.</param>
		/// <param name="channelIndex">The number of the channel whose volume level changed.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000078 RID: 120
		[PreserveSig]
		int OnChannelVolumeChanged([MarshalAs(UnmanagedType.U4)] [In] uint channelCount, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr newVolumes, [MarshalAs(UnmanagedType.U4)] [In] uint channelIndex, [In] ref Guid eventContext);

		/// <summary>
		/// Notifies the client that the grouping parameter for the session has changed.
		/// </summary>
		/// <param name="groupingId">The new grouping parameter for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000079 RID: 121
		[PreserveSig]
		int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext);

		/// <summary>
		/// Notifies the client that the stream-activity state of the session has changed.
		/// </summary>
		/// <param name="state">The new session state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x0600007A RID: 122
		[PreserveSig]
		int OnStateChanged([In] AudioSessionState state);

		/// <summary>
		/// Notifies the client that the session has been disconnected.
		/// </summary>
		/// <param name="disconnectReason">The reason that the audio session was disconnected.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x0600007B RID: 123
		[PreserveSig]
		int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason);
	}
}
