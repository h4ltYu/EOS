using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class SessionCollection
    {
        internal SessionCollection(IAudioSessionEnumerator realEnumerator)
        {
            this.audioSessionEnumerator = realEnumerator;
        }

        public AudioSessionControl this[int index]
        {
            get
            {
                IAudioSessionControl audioSessionControl;
                Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetSession(index, out audioSessionControl));
                return new AudioSessionControl(audioSessionControl);
            }
        }

        public int Count
        {
            get
            {
                int result;
                Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetCount(out result));
                return result;
            }
        }

        private readonly IAudioSessionEnumerator audioSessionEnumerator;
    }
}
