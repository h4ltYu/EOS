using System;

namespace NAudio.Wave.Asio
{
    internal enum ASIOMessageSelector
    {
        kAsioSelectorSupported = 1,
        kAsioEngineVersion,
        kAsioResetRequest,
        kAsioBufferSizeChange,
        kAsioResyncRequest,
        kAsioLatenciesChanged,
        kAsioSupportsTimeInfo,
        kAsioSupportsTimeCode,
        kAsioMMCCommand,
        kAsioSupportsInputMonitor,
        kAsioSupportsInputGain,
        kAsioSupportsInputMeter,
        kAsioSupportsOutputGain,
        kAsioSupportsOutputMeter,
        kAsioOverload
    }
}
