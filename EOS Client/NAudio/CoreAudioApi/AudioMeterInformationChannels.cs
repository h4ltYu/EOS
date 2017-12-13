using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioMeterInformationChannels
    {
        public int Count
        {
            get
            {
                int result;
                Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetMeteringChannelCount(out result));
                return result;
            }
        }

        public float this[int index]
        {
            get
            {
                float[] array = new float[this.Count];
                GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetChannelsPeakValues(array.Length, gchandle.AddrOfPinnedObject()));
                gchandle.Free();
                return array[index];
            }
        }

        internal AudioMeterInformationChannels(IAudioMeterInformation parent)
        {
            this.audioMeterInformation = parent;
        }

        private readonly IAudioMeterInformation audioMeterInformation;
    }
}
