using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class PropertyStore
    {
        public int Count
        {
            get
            {
                int result;
                Marshal.ThrowExceptionForHR(this.storeInterface.GetCount(out result));
                return result;
            }
        }

        public PropertyStoreProperty this[int index]
        {
            get
            {
                PropertyKey key = this.Get(index);
                PropVariant value;
                Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref key, out value));
                return new PropertyStoreProperty(key, value);
            }
        }

        public bool Contains(PropertyKey key)
        {
            for (int i = 0; i < this.Count; i++)
            {
                PropertyKey propertyKey = this.Get(i);
                if (propertyKey.formatId == key.formatId && propertyKey.propertyId == key.propertyId)
                {
                    return true;
                }
            }
            return false;
        }

        public PropertyStoreProperty this[PropertyKey key]
        {
            get
            {
                for (int i = 0; i < this.Count; i++)
                {
                    PropertyKey key2 = this.Get(i);
                    if (key2.formatId == key.formatId && key2.propertyId == key.propertyId)
                    {
                        PropVariant value;
                        Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref key2, out value));
                        return new PropertyStoreProperty(key2, value);
                    }
                }
                return null;
            }
        }

        public PropertyKey Get(int index)
        {
            PropertyKey result;
            Marshal.ThrowExceptionForHR(this.storeInterface.GetAt(index, out result));
            return result;
        }

        public PropVariant GetValue(int index)
        {
            PropertyKey propertyKey = this.Get(index);
            PropVariant result;
            Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref propertyKey, out result));
            return result;
        }

        internal PropertyStore(IPropertyStore store)
        {
            this.storeInterface = store;
        }

        private readonly IPropertyStore storeInterface;
    }
}
