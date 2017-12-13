using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioEndpointVolumeStepInformation
    {
        internal AudioEndpointVolumeStepInformation(IAudioEndpointVolume parent)
        {
            Marshal.ThrowExceptionForHR(parent.GetVolumeStepInfo(out this.step, out this.stepCount));
        }

        public uint Step
        {
            get
            {
                return this.step;
            }
        }

        public uint StepCount
        {
            get
            {
                return this.stepCount;
            }
        }

        private readonly uint step;

        private readonly uint stepCount;
    }
}
