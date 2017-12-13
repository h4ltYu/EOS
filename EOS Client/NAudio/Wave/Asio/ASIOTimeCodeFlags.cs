using System;

namespace NAudio.Wave.Asio
{
    [Flags]
    internal enum ASIOTimeCodeFlags
    {
        kTcValid = 1,
        kTcRunning = 2,
        kTcReverse = 4,
        kTcOnspeed = 8,
        kTcStill = 16,
        kTcSpeedValid = 256
    }
}
