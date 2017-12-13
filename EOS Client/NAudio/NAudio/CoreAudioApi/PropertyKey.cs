using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// PROPERTYKEY is defined in wtypes.h
	/// </summary>
	// Token: 0x02000130 RID: 304
	public struct PropertyKey
	{
		/// <summary>
		/// <param name="formatId"></param>
		/// <param name="propertyId"></param>
		/// </summary>
		// Token: 0x060006A7 RID: 1703 RVA: 0x00014A00 File Offset: 0x00012C00
		public PropertyKey(Guid formatId, int propertyId)
		{
			this.formatId = formatId;
			this.propertyId = propertyId;
		}

		/// <summary>
		/// Format ID
		/// </summary>
		// Token: 0x04000722 RID: 1826
		public Guid formatId;

		/// <summary>
		/// Property ID
		/// </summary>
		// Token: 0x04000723 RID: 1827
		public int propertyId;
	}
}
