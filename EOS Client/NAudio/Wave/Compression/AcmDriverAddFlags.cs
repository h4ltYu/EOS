using System;

namespace NAudio.Wave.Compression
{
    internal enum AcmDriverAddFlags
    {
        Local,
        Global = 8,
        Function = 3,
        NotifyWindowHandle
    }
}
