using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    internal class AudioSessionNotification : IAudioSessionNotification
    {
        internal AudioSessionNotification(AudioSessionManager parent)
        {
            this.parent = parent;
        }

        [PreserveSig]
        public int OnSessionCreated(IAudioSessionControl newSession)
        {
            this.parent.FireSessionCreated(newSession);
            return 0;
        }

        private AudioSessionManager parent;
    }
}
