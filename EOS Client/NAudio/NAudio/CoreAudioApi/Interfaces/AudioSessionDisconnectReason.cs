using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// Defines constants that indicate a reason for an audio session being disconnected.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: Unknown
	/// </remarks>
	// Token: 0x02000030 RID: 48
	public enum AudioSessionDisconnectReason
	{
		/// <summary>
		/// The user removed the audio endpoint device.
		/// </summary>
		// Token: 0x040000A0 RID: 160
		DisconnectReasonDeviceRemoval,
		/// <summary>
		/// The Windows audio service has stopped.
		/// </summary>
		// Token: 0x040000A1 RID: 161
		DisconnectReasonServerShutdown,
		/// <summary>
		/// The stream format changed for the device that the audio session is connected to.
		/// </summary>
		// Token: 0x040000A2 RID: 162
		DisconnectReasonFormatChanged,
		/// <summary>
		/// The user logged off the WTS session that the audio session was running in.
		/// </summary>
		// Token: 0x040000A3 RID: 163
		DisconnectReasonSessionLogoff,
		/// <summary>
		/// The WTS session that the audio session was running in was disconnected.
		/// </summary>
		// Token: 0x040000A4 RID: 164
		DisconnectReasonSessionDisconnected,
		/// <summary>
		/// The (shared-mode) audio session was disconnected to make the audio endpoint device available for an exclusive-mode connection.
		/// </summary>
		// Token: 0x040000A5 RID: 165
		DisconnectReasonExclusiveModeOverride
	}
}
