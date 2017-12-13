using System;

namespace NAudio.SoundFont
{
    public class SFVersion
    {
        public short Major
        {
            get
            {
                return this.major;
            }
            set
            {
                this.major = value;
            }
        }

        public short Minor
        {
            get
            {
                return this.minor;
            }
            set
            {
                this.minor = value;
            }
        }

        private short major;

        private short minor;
    }
}
