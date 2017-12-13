using System;

namespace NAudio.CoreAudioApi.Interfaces
{
    internal struct Blob
    {
        private void FixCS0649()
        {
            this.Length = 0;
            this.Data = IntPtr.Zero;
        }

        public int Length;

        public IntPtr Data;
    }
}
