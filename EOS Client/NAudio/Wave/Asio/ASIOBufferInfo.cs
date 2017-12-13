using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct ASIOBufferInfo
    {
        public IntPtr Buffer(int bufferIndex)
        {
            if (bufferIndex != 0)
            {
                return this.pBuffer1;
            }
            return this.pBuffer0;
        }

        public bool isInput;

        public int channelNum;

        public IntPtr pBuffer0;

        public IntPtr pBuffer1;
    }
}
