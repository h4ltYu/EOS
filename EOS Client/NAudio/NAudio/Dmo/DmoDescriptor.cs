using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// Contains the name and CLSID of a DirectX Media Object
	/// </summary>
	// Token: 0x02000084 RID: 132
	public class DmoDescriptor
	{
		/// <summary>
		/// Name
		/// </summary>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009F12 File Offset: 0x00008112
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00009F1A File Offset: 0x0000811A
		public string Name { get; private set; }

		/// <summary>
		/// Clsid
		/// </summary>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009F23 File Offset: 0x00008123
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00009F2B File Offset: 0x0000812B
		public Guid Clsid { get; private set; }

		/// <summary>
		/// Initializes a new instance of DmoDescriptor
		/// </summary>
		// Token: 0x060002EE RID: 750 RVA: 0x00009F34 File Offset: 0x00008134
		public DmoDescriptor(string name, Guid clsid)
		{
			this.Name = name;
			this.Clsid = clsid;
		}
	}
}
