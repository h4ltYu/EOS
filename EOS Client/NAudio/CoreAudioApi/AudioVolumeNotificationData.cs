using System;

namespace NAudio.CoreAudioApi
{
    public class AudioVolumeNotificationData
    {
        public Guid EventContext
        {
            get
            {
                return this.eventContext;
            }
        }

        public bool Muted
        {
            get
            {
                return this.muted;
            }
        }

        public float MasterVolume
        {
            get
            {
                return this.masterVolume;
            }
        }

        public int Channels
        {
            get
            {
                return this.channels;
            }
        }

        public float[] ChannelVolume
        {
            get
            {
                return this.channelVolume;
            }
        }

        public AudioVolumeNotificationData(Guid eventContext, bool muted, float masterVolume, float[] channelVolume)
        {
            this.eventContext = eventContext;
            this.muted = muted;
            this.masterVolume = masterVolume;
            this.channels = channelVolume.Length;
            this.channelVolume = channelVolume;
        }

        private readonly Guid eventContext;

        private readonly bool muted;

        private readonly float masterVolume;

        private readonly int channels;

        private readonly float[] channelVolume;
    }
}
