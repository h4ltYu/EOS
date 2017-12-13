using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000025 RID: 37
	internal struct AudioVolumeNotificationDataStruct
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004D45 File Offset: 0x00002F45
		private void FixCS0649()
		{
			this.guidEventContext = Guid.Empty;
			this.bMuted = false;
			this.fMasterVolume = 0f;
			this.nChannels = 0u;
			this.ChannelVolume = 0f;
		}

		// Token: 0x0400007A RID: 122
		public Guid guidEventContext;

		// Token: 0x0400007B RID: 123
		public bool bMuted;

		// Token: 0x0400007C RID: 124
		public float fMasterVolume;

		// Token: 0x0400007D RID: 125
		public uint nChannels;

		// Token: 0x0400007E RID: 126
		public float ChannelVolume;
	}
}
