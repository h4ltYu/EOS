using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000013 RID: 19
	internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004265 File Offset: 0x00002465
		internal AudioEndpointVolumeCallback(AudioEndpointVolume parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004274 File Offset: 0x00002474
		public void OnNotify(IntPtr notifyData)
		{
			AudioVolumeNotificationDataStruct audioVolumeNotificationDataStruct = (AudioVolumeNotificationDataStruct)Marshal.PtrToStructure(notifyData, typeof(AudioVolumeNotificationDataStruct));
			IntPtr value = Marshal.OffsetOf(typeof(AudioVolumeNotificationDataStruct), "ChannelVolume");
			IntPtr ptr = (IntPtr)((long)notifyData + (long)value);
			float[] array = new float[audioVolumeNotificationDataStruct.nChannels];
			int num = 0;
			while ((long)num < (long)((ulong)audioVolumeNotificationDataStruct.nChannels))
			{
				array[num] = (float)Marshal.PtrToStructure(ptr, typeof(float));
				num++;
			}
			AudioVolumeNotificationData notificationData = new AudioVolumeNotificationData(audioVolumeNotificationDataStruct.guidEventContext, audioVolumeNotificationDataStruct.bMuted, audioVolumeNotificationDataStruct.fMasterVolume, array);
			this.parent.FireNotification(notificationData);
		}

		// Token: 0x04000051 RID: 81
		private readonly AudioEndpointVolume parent;
	}
}
