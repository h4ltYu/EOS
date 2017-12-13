using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Store class, only supports reading properties at the moment.
	/// </summary>
	// Token: 0x02000135 RID: 309
	public class PropertyStore
	{
		/// <summary>
		/// Property Count
		/// </summary>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00015080 File Offset: 0x00013280
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(this.storeInterface.GetCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Gets property by index
		/// </summary>
		/// <param name="index">Property index</param>
		/// <returns>The property</returns>
		// Token: 0x17000173 RID: 371
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

		/// <summary>
		/// Contains property guid
		/// </summary>
		/// <param name="key">Looks for a specific key</param>
		/// <returns>True if found</returns>
		// Token: 0x060006CB RID: 1739 RVA: 0x000150D0 File Offset: 0x000132D0
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

		/// <summary>
		/// Indexer by guid
		/// </summary>
		/// <param name="key">Property Key</param>
		/// <returns>Property or null if not found</returns>
		// Token: 0x17000174 RID: 372
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

		/// <summary>
		/// Gets property key at sepecified index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Property key</returns>
		// Token: 0x060006CD RID: 1741 RVA: 0x00015188 File Offset: 0x00013388
		public PropertyKey Get(int index)
		{
			PropertyKey result;
			Marshal.ThrowExceptionForHR(this.storeInterface.GetAt(index, out result));
			return result;
		}

		/// <summary>
		/// Gets property value at specified index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Property value</returns>
		// Token: 0x060006CE RID: 1742 RVA: 0x000151AC File Offset: 0x000133AC
		public PropVariant GetValue(int index)
		{
			PropertyKey propertyKey = this.Get(index);
			PropVariant result;
			Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref propertyKey, out result));
			return result;
		}

		/// <summary>
		/// Creates a new property store
		/// </summary>
		/// <param name="store">IPropertyStore COM interface</param>
		// Token: 0x060006CF RID: 1743 RVA: 0x000151D6 File Offset: 0x000133D6
		internal PropertyStore(IPropertyStore store)
		{
			this.storeInterface = store;
		}

		// Token: 0x04000748 RID: 1864
		private readonly IPropertyStore storeInterface;
	}
}
