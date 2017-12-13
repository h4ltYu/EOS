using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Volume Notification Data
	/// </summary>
	// Token: 0x02000024 RID: 36
	public class AudioVolumeNotificationData
	{
		/// <summary>
		/// Event Context
		/// </summary>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004CEE File Offset: 0x00002EEE
		public Guid EventContext
		{
			get
			{
				return this.eventContext;
			}
		}

		/// <summary>
		/// Muted
		/// </summary>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004CF6 File Offset: 0x00002EF6
		public bool Muted
		{
			get
			{
				return this.muted;
			}
		}

		/// <summary>
		/// Master Volume
		/// </summary>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004CFE File Offset: 0x00002EFE
		public float MasterVolume
		{
			get
			{
				return this.masterVolume;
			}
		}

		/// <summary>
		/// Channels
		/// </summary>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004D06 File Offset: 0x00002F06
		public int Channels
		{
			get
			{
				return this.channels;
			}
		}

		/// <summary>
		/// Channel Volume
		/// </summary>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004D0E File Offset: 0x00002F0E
		public float[] ChannelVolume
		{
			get
			{
				return this.channelVolume;
			}
		}

		/// <summary>
		/// Audio Volume Notification Data
		/// </summary>
		/// <param name="eventContext"></param>
		/// <param name="muted"></param>
		/// <param name="masterVolume"></param>
		/// <param name="channelVolume"></param>
		// Token: 0x060000A4 RID: 164 RVA: 0x00004D16 File Offset: 0x00002F16
		public AudioVolumeNotificationData(Guid eventContext, bool muted, float masterVolume, float[] channelVolume)
		{
			this.eventContext = eventContext;
			this.muted = muted;
			this.masterVolume = masterVolume;
			this.channels = channelVolume.Length;
			this.channelVolume = channelVolume;
		}

		// Token: 0x04000075 RID: 117
		private readonly Guid eventContext;

		// Token: 0x04000076 RID: 118
		private readonly bool muted;

		// Token: 0x04000077 RID: 119
		private readonly float masterVolume;

		// Token: 0x04000078 RID: 120
		private readonly int channels;

		// Token: 0x04000079 RID: 121
		private readonly float[] channelVolume;
	}
}
