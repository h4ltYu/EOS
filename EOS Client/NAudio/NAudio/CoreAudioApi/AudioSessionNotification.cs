using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000021 RID: 33
	internal class AudioSessionNotification : IAudioSessionNotification
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004AAC File Offset: 0x00002CAC
		internal AudioSessionNotification(AudioSessionManager parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004ABB File Offset: 0x00002CBB
		[PreserveSig]
		public int OnSessionCreated(IAudioSessionControl newSession)
		{
			this.parent.FireSessionCreated(newSession);
			return 0;
		}

		// Token: 0x0400006A RID: 106
		private AudioSessionManager parent;
	}
}
