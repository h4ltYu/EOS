using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
    public class Mixer
    {
        public static int NumberOfDevices
        {
            get
            {
                return MixerInterop.mixerGetNumDevs();
            }
        }

        public Mixer(int mixerIndex)
        {
            if (mixerIndex < 0 || mixerIndex >= Mixer.NumberOfDevices)
            {
                throw new ArgumentOutOfRangeException("mixerID");
            }
            this.caps = default(MixerInterop.MIXERCAPS);
            MmException.Try(MixerInterop.mixerGetDevCaps((IntPtr)mixerIndex, ref this.caps, Marshal.SizeOf(this.caps)), "mixerGetDevCaps");
            this.mixerHandle = (IntPtr)mixerIndex;
            this.mixerHandleType = MixerFlags.Mixer;
        }

        public int DestinationCount
        {
            get
            {
                return (int)this.caps.cDestinations;
            }
        }

        public string Name
        {
            get
            {
                return this.caps.szPname;
            }
        }

        public Manufacturers Manufacturer
        {
            get
            {
                return (Manufacturers)this.caps.wMid;
            }
        }

        public int ProductID
        {
            get
            {
                return (int)this.caps.wPid;
            }
        }

        public MixerLine GetDestination(int destinationIndex)
        {
            if (destinationIndex < 0 || destinationIndex >= this.DestinationCount)
            {
                throw new ArgumentOutOfRangeException("destinationIndex");
            }
            return new MixerLine(this.mixerHandle, destinationIndex, this.mixerHandleType);
        }

        public IEnumerable<MixerLine> Destinations
        {
            get
            {
                for (int destination = 0; destination < this.DestinationCount; destination++)
                {
                    yield return this.GetDestination(destination);
                }
                yield break;
            }
        }

        public static IEnumerable<Mixer> Mixers
        {
            get
            {
                for (int device = 0; device < Mixer.NumberOfDevices; device++)
                {
                    yield return new Mixer(device);
                }
                yield break;
            }
        }

        private MixerInterop.MIXERCAPS caps;

        private IntPtr mixerHandle;

        private MixerFlags mixerHandleType;
    }
}
