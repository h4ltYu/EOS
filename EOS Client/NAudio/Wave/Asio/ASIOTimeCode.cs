using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct ASIOTimeCode
    {
        public double speed;

        public ASIO64Bit timeCodeSamples;

        public ASIOTimeCodeFlags flags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string future;
    }
}
