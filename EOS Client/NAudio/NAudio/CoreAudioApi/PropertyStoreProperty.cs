using System;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Store Property
	/// </summary>
	// Token: 0x02000136 RID: 310
	public class PropertyStoreProperty
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x000151E5 File Offset: 0x000133E5
		internal PropertyStoreProperty(PropertyKey key, PropVariant value)
		{
			this.propertyKey = key;
			this.propertyValue = value;
		}

		/// <summary>
		/// Property Key
		/// </summary>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000151FB File Offset: 0x000133FB
		public PropertyKey Key
		{
			get
			{
				return this.propertyKey;
			}
		}

		/// <summary>
		/// Property Value
		/// </summary>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00015203 File Offset: 0x00013403
		public object Value
		{
			get
			{
				return this.propertyValue.Value;
			}
		}

		// Token: 0x04000749 RID: 1865
		private readonly PropertyKey propertyKey;

		// Token: 0x0400074A RID: 1866
		private PropVariant propertyValue;
	}
}
