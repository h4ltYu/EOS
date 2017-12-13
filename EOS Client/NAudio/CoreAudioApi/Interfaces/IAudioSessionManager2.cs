using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
    [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionManager2 : IAudioSessionManager
    {
        [PreserveSig]
        int GetAudioSessionControl([MarshalAs(UnmanagedType.LPStruct)] Guid AudioSessionGuid, int StreamFlags, out IAudioSessionControl SessionControl);

        [PreserveSig]
        int GetSimpleAudioVolume([MarshalAs(UnmanagedType.LPStruct)] Guid AudioSessionGuid, int StreamFlags, ISimpleAudioVolume AudioVolume);
        [PreserveSig]
        int GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum);

        [PreserveSig]
        int RegisterSessionNotification(IAudioSessionNotification sessionNotification);

        [PreserveSig]
        int UnregisterSessionNotification(IAudioSessionNotification sessionNotification);

        [PreserveSig]
        int RegisterDuckNotification(string sessionID, IAudioSessionNotification audioVolumeDuckNotification);

        [PreserveSig]
        int UnregisterDuckNotification(IntPtr audioVolumeDuckNotification);
    }
}
