using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class MMDeviceEnumerator
    {
        public MMDeviceEnumerator()
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
            }
            this.realEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumeratorComObject();
        }

        public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
        {
            IMMDeviceCollection parent;
            Marshal.ThrowExceptionForHR(this.realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out parent));
            return new MMDeviceCollection(parent);
        }

        public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
        {
            IMMDevice realDevice = null;
            Marshal.ThrowExceptionForHR(this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out realDevice));
            return new MMDevice(realDevice);
        }

        public bool HasDefaultAudioEndpoint(DataFlow dataFlow, Role role)
        {
            IMMDevice o = null;
            int defaultAudioEndpoint = this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out o);
            if (defaultAudioEndpoint == 0)
            {
                Marshal.ReleaseComObject(o);
                return true;
            }
            if (defaultAudioEndpoint == -2147023728)
            {
                return false;
            }
            Marshal.ThrowExceptionForHR(defaultAudioEndpoint);
            return false;
        }

        public MMDevice GetDevice(string id)
        {
            IMMDevice realDevice = null;
            Marshal.ThrowExceptionForHR(this.realEnumerator.GetDevice(id, out realDevice));
            return new MMDevice(realDevice);
        }

        public int RegisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient client)
        {
            return this.realEnumerator.RegisterEndpointNotificationCallback(client);
        }

        public int UnregisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface)] [In] IMMNotificationClient client)
        {
            return this.realEnumerator.UnregisterEndpointNotificationCallback(client);
        }

        private readonly IMMDeviceEnumerator realEnumerator;
    }
}
