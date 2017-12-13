using System;

namespace NAudio.Wave.Asio
{
    internal class AsioDriverCapability
    {
        public string DriverName;

        public int NbInputChannels;

        public int NbOutputChannels;

        public int InputLatency;

        public int OutputLatency;

        public int BufferMinSize;

        public int BufferMaxSize;

        public int BufferPreferredSize;

        public int BufferGranularity;

        public double SampleRate;

        public ASIOChannelInfo[] InputChannelInfos;

        public ASIOChannelInfo[] OutputChannelInfos;
    }
}
