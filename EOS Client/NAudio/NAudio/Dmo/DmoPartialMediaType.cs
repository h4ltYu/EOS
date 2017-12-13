using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO_PARTIAL_MEDIATYPE
	/// </summary>
	// Token: 0x0200008B RID: 139
	internal struct DmoPartialMediaType
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000A1E9 File Offset: 0x000083E9
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000A1F1 File Offset: 0x000083F1
		public Guid Type
		{
			get
			{
				return this.type;
			}
			internal set
			{
				this.type = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000A1FA File Offset: 0x000083FA
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000A202 File Offset: 0x00008402
		public Guid Subtype
		{
			get
			{
				return this.subtype;
			}
			internal set
			{
				this.subtype = value;
			}
		}

		// Token: 0x040003F8 RID: 1016
		private Guid type;

		// Token: 0x040003F9 RID: 1017
		private Guid subtype;
	}
}
