using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioSessionManager
    {
        public event AudioSessionManager.SessionCreatedDelegate OnSessionCreated;

        internal AudioSessionManager(IAudioSessionManager audioSessionManager)
        {
            this.audioSessionInterface = audioSessionManager;
            this.audioSessionInterface2 = (audioSessionManager as IAudioSessionManager2);
            this.RefreshSessions();
        }

        public SimpleAudioVolume SimpleAudioVolume
        {
            get
            {
                if (this.simpleAudioVolume == null)
                {
                    ISimpleAudioVolume realSimpleVolume;
                    this.audioSessionInterface.GetSimpleAudioVolume(Guid.Empty, 0u, out realSimpleVolume);
                    this.simpleAudioVolume = new SimpleAudioVolume(realSimpleVolume);
                }
                return this.simpleAudioVolume;
            }
        }

        public AudioSessionControl AudioSessionControl
        {
            get
            {
                if (this.audioSessionControl == null)
                {
                    IAudioSessionControl audioSessionControl;
                    this.audioSessionInterface.GetAudioSessionControl(Guid.Empty, 0u, out audioSessionControl);
                    this.audioSessionControl = new AudioSessionControl(audioSessionControl);
                }
                return this.audioSessionControl;
            }
        }

        internal void FireSessionCreated(IAudioSessionControl newSession)
        {
            if (this.OnSessionCreated != null)
            {
                this.OnSessionCreated(this, newSession);
            }
        }

        public void RefreshSessions()
        {
            this.UnregisterNotifications();
            if (this.audioSessionInterface2 != null)
            {
                IAudioSessionEnumerator realEnumerator;
                Marshal.ThrowExceptionForHR(this.audioSessionInterface2.GetSessionEnumerator(out realEnumerator));
                this.sessions = new SessionCollection(realEnumerator);
                this.audioSessionNotification = new AudioSessionNotification(this);
                Marshal.ThrowExceptionForHR(this.audioSessionInterface2.RegisterSessionNotification(this.audioSessionNotification));
            }
        }

        public SessionCollection Sessions
        {
            get
            {
                return this.sessions;
            }
        }

        public void Dispose()
        {
            this.UnregisterNotifications();
            GC.SuppressFinalize(this);
        }

        private void UnregisterNotifications()
        {
            if (this.sessions != null)
            {
                this.sessions = null;
            }
            if (this.audioSessionNotification != null)
            {
                Marshal.ThrowExceptionForHR(this.audioSessionInterface2.UnregisterSessionNotification(this.audioSessionNotification));
            }
        }

        ~AudioSessionManager()
        {
            this.Dispose();
        }

        private readonly IAudioSessionManager audioSessionInterface;

        private readonly IAudioSessionManager2 audioSessionInterface2;

        private AudioSessionNotification audioSessionNotification;

        private SessionCollection sessions;

        private SimpleAudioVolume simpleAudioVolume;

        private AudioSessionControl audioSessionControl;

        public delegate void SessionCreatedDelegate(object sender, IAudioSessionControl newSession);
    }
}
