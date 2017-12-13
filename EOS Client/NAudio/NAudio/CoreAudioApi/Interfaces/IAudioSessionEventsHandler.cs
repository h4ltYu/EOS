using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// interface to receive session related events
	/// </summary>
	// Token: 0x02000031 RID: 49
	public interface IAudioSessionEventsHandler
	{
		/// <summary>
		/// notification of volume changes including muting of audio session
		/// </summary>
		/// <param name="volume">the current volume</param>
		/// <param name="isMuted">the current mute state, true muted, false otherwise</param>
		// Token: 0x060000C9 RID: 201
		void OnVolumeChanged(float volume, bool isMuted);

		/// <summary>
		/// notification of display name changed
		/// </summary>
		/// <param name="displayName">the current display name</param>
		// Token: 0x060000CA RID: 202
		void OnDisplayNameChanged(string displayName);

		/// <summary>
		/// notification of icon path changed
		/// </summary>
		/// <param name="iconPath">the current icon path</param>
		// Token: 0x060000CB RID: 203
		void OnIconPathChanged(string iconPath);

		/// <summary>
		/// notification of the client that the volume level of an audio channel in the session submix has changed
		/// </summary>
		/// <param name="channelCount">The channel count.</param>
		/// <param name="newVolumes">An array of volumnes cooresponding with each channel index.</param>
		/// <param name="channelIndex">The number of the channel whose volume level changed.</param>
		// Token: 0x060000CC RID: 204
		void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex);

		/// <summary>
		/// notification of the client that the grouping parameter for the session has changed
		/// </summary>
		/// <param name="groupingId">&gt;The new grouping parameter for the session.</param>
		// Token: 0x060000CD RID: 205
		void OnGroupingParamChanged(ref Guid groupingId);

		/// <summary>
		/// notification of the client that the stream-activity state of the session has changed
		/// </summary>
		/// <param name="state">The new session state.</param>
		// Token: 0x060000CE RID: 206
		void OnStateChanged(AudioSessionState state);

		/// <summary>
		/// notification of the client that the session has been disconnected
		/// </summary>
		/// <param name="disconnectReason">The reason that the audio session was disconnected.</param>
		// Token: 0x060000CF RID: 207
		void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason);
	}
}
