using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioSessionControl : IDisposable
    {
        public AudioSessionControl(IAudioSessionControl audioSessionControl)
        {
            this.audioSessionControlInterface = audioSessionControl;
            this.audioSessionControlInterface2 = (audioSessionControl as IAudioSessionControl2);
            IAudioMeterInformation audioMeterInformation = this.audioSessionControlInterface as IAudioMeterInformation;
            ISimpleAudioVolume simpleAudioVolume = this.audioSessionControlInterface as ISimpleAudioVolume;
            if (audioMeterInformation != null)
            {
                this.audioMeterInformation = new AudioMeterInformation(audioMeterInformation);
            }
            if (simpleAudioVolume != null)
            {
                this.simpleAudioVolume = new SimpleAudioVolume(simpleAudioVolume);
            }
        }

        public void Dispose()
        {
            if (this.audioSessionEventCallback != null)
            {
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification(this.audioSessionEventCallback));
            }
            GC.SuppressFinalize(this);
        }

        ~AudioSessionControl()
        {
            this.Dispose();
        }

        public AudioMeterInformation AudioMeterInformation
        {
            get
            {
                return this.audioMeterInformation;
            }
        }

        public SimpleAudioVolume SimpleAudioVolume
        {
            get
            {
                return this.simpleAudioVolume;
            }
        }

        public AudioSessionState State
        {
            get
            {
                AudioSessionState result;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetState(out result));
                return result;
            }
        }

        public string DisplayName
        {
            get
            {
                string empty = string.Empty;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetDisplayName(out empty));
                return empty;
            }
            set
            {
                if (value != string.Empty)
                {
                    Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetDisplayName(value, Guid.Empty));
                }
            }
        }

        public string IconPath
        {
            get
            {
                string empty = string.Empty;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetIconPath(out empty));
                return empty;
            }
            set
            {
                if (value != string.Empty)
                {
                    Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetIconPath(value, Guid.Empty));
                }
            }
        }

        public string GetSessionIdentifier
        {
            get
            {
                if (this.audioSessionControlInterface2 == null)
                {
                    throw new InvalidOperationException("Not supported on this version of Windows");
                }
                string result;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionIdentifier(out result));
                return result;
            }
        }

        public string GetSessionInstanceIdentifier
        {
            get
            {
                if (this.audioSessionControlInterface2 == null)
                {
                    throw new InvalidOperationException("Not supported on this version of Windows");
                }
                string result;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionInstanceIdentifier(out result));
                return result;
            }
        }

        public uint GetProcessID
        {
            get
            {
                if (this.audioSessionControlInterface2 == null)
                {
                    throw new InvalidOperationException("Not supported on this version of Windows");
                }
                uint result;
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetProcessId(out result));
                return result;
            }
        }

        public bool IsSystemSoundsSession
        {
            get
            {
                if (this.audioSessionControlInterface2 == null)
                {
                    throw new InvalidOperationException("Not supported on this version of Windows");
                }
                return this.audioSessionControlInterface2.IsSystemSoundsSession() == 0;
            }
        }

        public Guid GetGroupingParam()
        {
            Guid empty = Guid.Empty;
            Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetGroupingParam(out empty));
            return empty;
        }

        public void SetGroupingParam(Guid groupingId, Guid context)
        {
            Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetGroupingParam(groupingId, context));
        }

        public void RegisterEventClient(IAudioSessionEventsHandler eventClient)
        {
            this.audioSessionEventCallback = new AudioSessionEventsCallback(eventClient);
            Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.RegisterAudioSessionNotification(this.audioSessionEventCallback));
        }

        public void UnRegisterEventClient(IAudioSessionEventsHandler eventClient)
        {
            if (this.audioSessionEventCallback != null)
            {
                Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification(this.audioSessionEventCallback));
            }
        }

        private readonly IAudioSessionControl audioSessionControlInterface;

        private readonly IAudioSessionControl2 audioSessionControlInterface2;

        private AudioSessionEventsCallback audioSessionEventCallback;

        internal AudioMeterInformation audioMeterInformation;

        internal SimpleAudioVolume simpleAudioVolume;
    }
}
