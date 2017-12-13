using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
    {
        internal AudioEndpointVolumeCallback(AudioEndpointVolume parent)
        {
            this.parent = parent;
        }

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

        private readonly AudioEndpointVolume parent;
    }
}
