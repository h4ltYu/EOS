using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// AudioSessionEvents callback implementation
	/// </summary>
	// Token: 0x0200001D RID: 29
	public class AudioSessionEventsCallback : IAudioSessionEvents
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="handler"></param>
		// Token: 0x0600007C RID: 124 RVA: 0x00004842 File Offset: 0x00002A42
		public AudioSessionEventsCallback(IAudioSessionEventsHandler handler)
		{
			this.audioSessionEventsHandler = handler;
		}

		/// <summary>
		/// Notifies the client that the display name for the session has changed.
		/// </summary>
		/// <param name="displayName">The new display name for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x0600007D RID: 125 RVA: 0x00004851 File Offset: 0x00002A51
		public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string displayName, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnDisplayNameChanged(displayName);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the display icon for the session has changed.
		/// </summary>
		/// <param name="iconPath">The path for the new display icon for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x0600007E RID: 126 RVA: 0x00004860 File Offset: 0x00002A60
		public int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] [In] string iconPath, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnIconPathChanged(iconPath);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the volume level or muting state of the session has changed.
		/// </summary>
		/// <param name="volume">The new volume level for the audio session.</param>
		/// <param name="isMuted">The new muting state.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x0600007F RID: 127 RVA: 0x0000486F File Offset: 0x00002A6F
		public int OnSimpleVolumeChanged([MarshalAs(UnmanagedType.R4)] [In] float volume, [MarshalAs(UnmanagedType.Bool)] [In] bool isMuted, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnVolumeChanged(volume, isMuted);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the volume level of an audio channel in the session submix has changed.
		/// </summary>
		/// <param name="channelCount">The channel count.</param>
		/// <param name="newVolumes">An array of volumnes cooresponding with each channel index.</param>
		/// <param name="channelIndex">The number of the channel whose volume level changed.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000080 RID: 128 RVA: 0x0000487F File Offset: 0x00002A7F
		public int OnChannelVolumeChanged([MarshalAs(UnmanagedType.U4)] [In] uint channelCount, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr newVolumes, [MarshalAs(UnmanagedType.U4)] [In] uint channelIndex, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnChannelVolumeChanged(channelCount, newVolumes, channelIndex);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the grouping parameter for the session has changed.
		/// </summary>
		/// <param name="groupingId">The new grouping parameter for the session.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000081 RID: 129 RVA: 0x00004890 File Offset: 0x00002A90
		public int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnGroupingParamChanged(ref groupingId);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the stream-activity state of the session has changed.
		/// </summary>
		/// <param name="state">The new session state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000082 RID: 130 RVA: 0x0000489F File Offset: 0x00002A9F
		public int OnStateChanged([In] AudioSessionState state)
		{
			this.audioSessionEventsHandler.OnStateChanged(state);
			return 0;
		}

		/// <summary>
		/// Notifies the client that the session has been disconnected.
		/// </summary>
		/// <param name="disconnectReason">The reason that the audio session was disconnected.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		// Token: 0x06000083 RID: 131 RVA: 0x000048AE File Offset: 0x00002AAE
		public int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason)
		{
			this.audioSessionEventsHandler.OnSessionDisconnected(disconnectReason);
			return 0;
		}

		// Token: 0x04000062 RID: 98
		private readonly IAudioSessionEventsHandler audioSessionEventsHandler;
	}
}
