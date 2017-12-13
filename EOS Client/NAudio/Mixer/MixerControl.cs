using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
    public abstract class MixerControl
    {
        public static IList<MixerControl> GetMixerControls(IntPtr mixerHandle, MixerLine mixerLine, MixerFlags mixerHandleType)
        {
            List<MixerControl> list = new List<MixerControl>();
            if (mixerLine.ControlsCount > 0)
            {
                int num = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
                MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
                IntPtr intPtr = Marshal.AllocHGlobal(num * mixerLine.ControlsCount);
                mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
                mixerlinecontrols.dwLineID = mixerLine.LineId;
                mixerlinecontrols.cControls = mixerLine.ControlsCount;
                mixerlinecontrols.pamxctrl = intPtr;
                mixerlinecontrols.cbmxctrl = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
                try
                {
                    MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, mixerHandleType);
                    if (mmResult != MmResult.NoError)
                    {
                        throw new MmException(mmResult, "mixerGetLineControls");
                    }
                    for (int i = 0; i < mixerlinecontrols.cControls; i++)
                    {
                        long value = intPtr.ToInt64() + (long)(num * i);
                        MixerInterop.MIXERCONTROL mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure((IntPtr)value, typeof(MixerInterop.MIXERCONTROL));
                        MixerControl item = MixerControl.GetMixerControl(mixerHandle, mixerLine.LineId, mixercontrol.dwControlID, mixerLine.Channels, mixerHandleType);
                        list.Add(item);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(intPtr);
                }
            }
            return list;
        }

        public static MixerControl GetMixerControl(IntPtr mixerHandle, int nLineID, int controlId, int nChannels, MixerFlags mixerFlags)
        {
            MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
            MixerInterop.MIXERCONTROL mixercontrol = default(MixerInterop.MIXERCONTROL);
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mixercontrol));
            mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
            mixerlinecontrols.cControls = 1;
            mixerlinecontrols.dwControlID = controlId;
            mixerlinecontrols.cbmxctrl = Marshal.SizeOf(mixercontrol);
            mixerlinecontrols.pamxctrl = intPtr;
            mixerlinecontrols.dwLineID = nLineID;
            MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, MixerFlags.ListText | mixerFlags);
            if (mmResult != MmResult.NoError)
            {
                Marshal.FreeCoTaskMem(intPtr);
                throw new MmException(mmResult, "mixerGetLineControls");
            }
            mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure(mixerlinecontrols.pamxctrl, typeof(MixerInterop.MIXERCONTROL));
            Marshal.FreeCoTaskMem(intPtr);
            if (MixerControl.IsControlBoolean(mixercontrol.dwControlType))
            {
                return new BooleanMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
            }
            if (MixerControl.IsControlSigned(mixercontrol.dwControlType))
            {
                return new SignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
            }
            if (MixerControl.IsControlUnsigned(mixercontrol.dwControlType))
            {
                return new UnsignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
            }
            if (MixerControl.IsControlListText(mixercontrol.dwControlType))
            {
                return new ListTextMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
            }
            if (MixerControl.IsControlCustom(mixercontrol.dwControlType))
            {
                return new CustomMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
            }
            throw new InvalidOperationException(string.Format("Unknown mixer control type {0}", mixercontrol.dwControlType));
        }

        protected void GetControlDetails()
        {
            this.mixerControlDetails.cbStruct = Marshal.SizeOf(this.mixerControlDetails);
            this.mixerControlDetails.dwControlID = this.mixerControl.dwControlID;
            if (this.IsCustom)
            {
                this.mixerControlDetails.cChannels = 0;
            }
            else if ((this.mixerControl.fdwControl & 1u) != 0u)
            {
                this.mixerControlDetails.cChannels = 1;
            }
            else
            {
                this.mixerControlDetails.cChannels = this.nChannels;
            }
            if ((this.mixerControl.fdwControl & 2u) != 0u)
            {
                this.mixerControlDetails.hwndOwner = (IntPtr)((long)((ulong)this.mixerControl.cMultipleItems));
            }
            else if (this.IsCustom)
            {
                this.mixerControlDetails.hwndOwner = IntPtr.Zero;
            }
            else
            {
                this.mixerControlDetails.hwndOwner = IntPtr.Zero;
            }
            if (this.IsBoolean)
            {
                this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
            }
            else if (this.IsListText)
            {
                this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_LISTTEXT));
            }
            else if (this.IsSigned)
            {
                this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_SIGNED));
            }
            else if (this.IsUnsigned)
            {
                this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
            }
            else
            {
                this.mixerControlDetails.cbDetails = this.mixerControl.Metrics.customData;
            }
            int num = this.mixerControlDetails.cbDetails * this.mixerControlDetails.cChannels;
            if ((this.mixerControl.fdwControl & 2u) != 0u)
            {
                num *= (int)this.mixerControl.cMultipleItems;
            }
            IntPtr intPtr = Marshal.AllocCoTaskMem(num);
            this.mixerControlDetails.paDetails = intPtr;
            MmResult mmResult = MixerInterop.mixerGetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType);
            if (mmResult == MmResult.NoError)
            {
                this.GetDetails(this.mixerControlDetails.paDetails);
            }
            Marshal.FreeCoTaskMem(intPtr);
            if (mmResult != MmResult.NoError)
            {
                throw new MmException(mmResult, "mixerGetControlDetails");
            }
        }

        protected abstract void GetDetails(IntPtr pDetails);

        public string Name
        {
            get
            {
                return this.mixerControl.szName;
            }
        }

        public MixerControlType ControlType
        {
            get
            {
                return this.mixerControl.dwControlType;
            }
        }

        private static bool IsControlBoolean(MixerControlType controlType)
        {
            if (controlType <= MixerControlType.StereoEnhance)
            {
                if (controlType != MixerControlType.BooleanMeter)
                {
                    switch (controlType)
                    {
                        case MixerControlType.Boolean:
                        case MixerControlType.OnOff:
                        case MixerControlType.Mute:
                        case MixerControlType.Mono:
                        case MixerControlType.Loudness:
                        case MixerControlType.StereoEnhance:
                            break;
                        default:
                            return false;
                    }
                }
            }
            else if (controlType != MixerControlType.Button)
            {
                switch (controlType)
                {
                    case MixerControlType.SingleSelect:
                    case MixerControlType.Mux:
                        break;
                    default:
                        switch (controlType)
                        {
                            case MixerControlType.MultipleSelect:
                            case MixerControlType.Mixer:
                                break;
                            default:
                                return false;
                        }
                        break;
                }
            }
            return true;
        }

        public bool IsBoolean
        {
            get
            {
                return MixerControl.IsControlBoolean(this.mixerControl.dwControlType);
            }
        }

        private static bool IsControlListText(MixerControlType controlType)
        {
            if (controlType != MixerControlType.Equalizer)
            {
                switch (controlType)
                {
                    case MixerControlType.SingleSelect:
                    case MixerControlType.Mux:
                        break;
                    default:
                        switch (controlType)
                        {
                            case MixerControlType.MultipleSelect:
                            case MixerControlType.Mixer:
                                break;
                            default:
                                return false;
                        }
                        break;
                }
            }
            return true;
        }

        public bool IsListText
        {
            get
            {
                return MixerControl.IsControlListText(this.mixerControl.dwControlType);
            }
        }

        private static bool IsControlSigned(MixerControlType controlType)
        {
            if (controlType <= MixerControlType.Signed)
            {
                switch (controlType)
                {
                    case MixerControlType.SignedMeter:
                    case MixerControlType.PeakMeter:
                        break;
                    default:
                        if (controlType != MixerControlType.Signed)
                        {
                            return false;
                        }
                        break;
                }
            }
            else if (controlType != MixerControlType.Decibels)
            {
                switch (controlType)
                {
                    case MixerControlType.Slider:
                    case MixerControlType.Pan:
                    case MixerControlType.QSoundPan:
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        public bool IsSigned
        {
            get
            {
                return MixerControl.IsControlSigned(this.mixerControl.dwControlType);
            }
        }

        private static bool IsControlUnsigned(MixerControlType controlType)
        {
            if (controlType <= MixerControlType.Percent)
            {
                if (controlType != MixerControlType.UnsignedMeter && controlType != MixerControlType.Unsigned && controlType != MixerControlType.Percent)
                {
                    return false;
                }
            }
            else
            {
                switch (controlType)
                {
                    case MixerControlType.Fader:
                    case MixerControlType.Volume:
                    case MixerControlType.Bass:
                    case MixerControlType.Treble:
                    case MixerControlType.Equalizer:
                        break;
                    default:
                        if (controlType != MixerControlType.MicroTime && controlType != MixerControlType.MilliTime)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        public bool IsUnsigned
        {
            get
            {
                return MixerControl.IsControlUnsigned(this.mixerControl.dwControlType);
            }
        }

        private static bool IsControlCustom(MixerControlType controlType)
        {
            return controlType == MixerControlType.Custom;
        }

        public bool IsCustom
        {
            get
            {
                return MixerControl.IsControlCustom(this.mixerControl.dwControlType);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Name, this.ControlType);
        }

        internal MixerInterop.MIXERCONTROL mixerControl;

        internal MixerInterop.MIXERCONTROLDETAILS mixerControlDetails;

        protected IntPtr mixerHandle;

        protected int nChannels;

        protected MixerFlags mixerHandleType;
    }
}
