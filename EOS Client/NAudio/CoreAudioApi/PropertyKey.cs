using System;

namespace NAudio.CoreAudioApi
{
    public struct PropertyKey
    {
        public PropertyKey(Guid formatId, int propertyId)
        {
            this.formatId = formatId;
            this.propertyId = propertyId;
        }

        public Guid formatId;

        public int propertyId;
    }
}
