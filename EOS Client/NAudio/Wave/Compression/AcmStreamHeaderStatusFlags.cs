using System;

namespace NAudio.Wave.Compression
{
    [Flags]
    internal enum AcmStreamHeaderStatusFlags
    {
        Done = 65536,
        Prepared = 131072,
        InQueue = 1048576
    }
}
