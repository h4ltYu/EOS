using System;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class PropertyStoreProperty
    {
        internal PropertyStoreProperty(PropertyKey key, PropVariant value)
        {
            this.propertyKey = key;
            this.propertyValue = value;
        }

        public PropertyKey Key
        {
            get
            {
                return this.propertyKey;
            }
        }

        public object Value
        {
            get
            {
                return this.propertyValue.Value;
            }
        }

        private readonly PropertyKey propertyKey;

        private PropVariant propertyValue;
    }
}
