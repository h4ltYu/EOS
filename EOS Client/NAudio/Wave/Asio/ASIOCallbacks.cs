using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct ASIOCallbacks
    {
        public ASIOCallbacks.ASIOBufferSwitchCallBack pbufferSwitch;

        public ASIOCallbacks.ASIOSampleRateDidChangeCallBack psampleRateDidChange;

        public ASIOCallbacks.ASIOAsioMessageCallBack pasioMessage;

        public ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack pbufferSwitchTimeInfo;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ASIOBufferSwitchCallBack(int doubleBufferIndex, bool directProcess);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ASIOSampleRateDidChangeCallBack(double sRate);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ASIOAsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess);
    }
}
