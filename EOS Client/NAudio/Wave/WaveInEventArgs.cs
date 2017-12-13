using System;

namespace NAudio.Wave
{
    public class WaveInEventArgs : EventArgs
    {
        public WaveInEventArgs(byte[] buffer, int bytes)
        {
            this.buffer = buffer;
            this.bytes = bytes;
        }

        public byte[] Buffer
        {
            get
            {
                return this.buffer;
            }
        }

        public int BytesRecorded
        {
            get
            {
                return this.bytes;
            }
        }

        private byte[] buffer;

        private int bytes;
    }
}
