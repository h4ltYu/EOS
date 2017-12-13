using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// DirectX Media Object Enumerator
	/// </summary>
	// Token: 0x02000085 RID: 133
	public class DmoEnumerator
	{
		/// <summary>
		/// Get audio effect names
		/// </summary>
		/// <returns>Audio effect names</returns>
		// Token: 0x060002EF RID: 751 RVA: 0x00009F4A File Offset: 0x0000814A
		public static IEnumerable<DmoDescriptor> GetAudioEffectNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_EFFECT);
		}

		/// <summary>
		/// Get audio encoder names
		/// </summary>
		/// <returns>Audio encoder names</returns>
		// Token: 0x060002F0 RID: 752 RVA: 0x00009F56 File Offset: 0x00008156
		public static IEnumerable<DmoDescriptor> GetAudioEncoderNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_ENCODER);
		}

		/// <summary>
		/// Get audio decoder names
		/// </summary>
		/// <returns>Audio decoder names</returns>
		// Token: 0x060002F1 RID: 753 RVA: 0x00009F62 File Offset: 0x00008162
		public static IEnumerable<DmoDescriptor> GetAudioDecoderNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_DECODER);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A0C4 File Offset: 0x000082C4
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
