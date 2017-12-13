using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Multimedia Device Collection
	/// </summary>
	// Token: 0x02000036 RID: 54
	public class MMDeviceCollection : IEnumerable<MMDevice>, IEnumerable
	{
		/// <summary>
		/// Device count
		/// </summary>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F0C File Offset: 0x0000310C
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(this._MMDeviceCollection.GetCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Get device by index
		/// </summary>
		/// <param name="index">Device index</param>
		/// <returns>Device at the specified index</returns>
		// Token: 0x17000037 RID: 55
		public MMDevice this[int index]
		{
			get
			{
				IMMDevice realDevice;
				this._MMDeviceCollection.Item(index, out realDevice);
				return new MMDevice(realDevice);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004F4E File Offset: 0x0000314E
		internal MMDeviceCollection(IMMDeviceCollection parent)
		{
			this._MMDeviceCollection = parent;
		}

		/// <summary>
		/// Get Enumerator
		/// </summary>
		/// <returns>Device enumerator</returns>
		// Token: 0x060000E5 RID: 229 RVA: 0x00005008 File Offset: 0x00003208
		public IEnumerator<MMDevice> GetEnumerator()
		{
			for (int index = 0; index < this.Count; index++)
			{
				yield return this[index];
			}
			yield break;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005024 File Offset: 0x00003224
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000A6 RID: 166
		private IMMDeviceCollection _MMDeviceCollection;
	}
}
