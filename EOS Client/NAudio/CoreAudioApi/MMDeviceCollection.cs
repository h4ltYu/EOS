using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class MMDeviceCollection : IEnumerable<MMDevice>, IEnumerable
    {
        public int Count
        {
            get
            {
                int result;
                Marshal.ThrowExceptionForHR(this._MMDeviceCollection.GetCount(out result));
                return result;
            }
        }

        public MMDevice this[int index]
        {
            get
            {
                IMMDevice realDevice;
                this._MMDeviceCollection.Item(index, out realDevice);
                return new MMDevice(realDevice);
            }
        }

        internal MMDeviceCollection(IMMDeviceCollection parent)
        {
            this._MMDeviceCollection = parent;
        }

        public IEnumerator<MMDevice> GetEnumerator()
        {
            for (int index = 0; index < this.Count; index++)
            {
                yield return this[index];
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IMMDeviceCollection _MMDeviceCollection;
    }
}
