using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
    public class DmoEnumerator
    {
        public static IEnumerable<DmoDescriptor> GetAudioEffectNames()
        {
            return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_EFFECT);
        }

        public static IEnumerable<DmoDescriptor> GetAudioEncoderNames()
        {
            return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_ENCODER);
        }

        public static IEnumerable<DmoDescriptor> GetAudioDecoderNames()
        {
            return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_DECODER);
        }

        private static IEnumerable<DmoDescriptor> GetDmos(Guid category)
        {
            IEnumDmo enumDmo;
            int hresult = DmoInterop.DMOEnum(ref category, DmoEnumFlags.None, 0, null, 0, null, out enumDmo);
            Marshal.ThrowExceptionForHR(hresult);
            int itemsFetched;
            do
            {
                Guid guid;
                IntPtr namePointer;
                enumDmo.Next(1, out guid, out namePointer, out itemsFetched);
                if (itemsFetched == 1)
                {
                    string name = Marshal.PtrToStringUni(namePointer);
                    Marshal.FreeCoTaskMem(namePointer);
                    yield return new DmoDescriptor(name, guid);
                }
            }
            while (itemsFetched > 0);
            yield break;
        }
    }
}
