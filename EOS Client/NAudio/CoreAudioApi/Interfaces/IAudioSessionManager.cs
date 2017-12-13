using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
    [Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionManager
    {
        [PreserveSig]
        int GetAudioSessionControl([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId  ,[MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl sessionControl);

        [PreserveSig]
        int GetSimpleAudioVolume([MarshalAs(UnmanagedType.LPStruct)] [In] Guid sessionId , [MarshalAs(UnmanagedType.U4)] [In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out ISimpleAudioVolume audioVolume);
    }
}
