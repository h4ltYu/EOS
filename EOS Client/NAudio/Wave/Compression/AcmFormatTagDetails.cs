using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
    internal struct AcmFormatTagDetails
    {
        public const int FormatTagDescriptionChars = 48;

        public int structureSize;

        public int formatTagIndex;

        public int formatTag;

        public int formatSize;

        public AcmDriverDetailsSupportFlags supportFlags;

        public int standardFormatsCount;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
        public string formatDescription;
    }
}
