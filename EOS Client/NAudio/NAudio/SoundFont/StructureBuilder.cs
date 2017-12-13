using System;
using System.Collections.Generic;
using System.IO;

namespace NAudio.SoundFont
{
	/// <summary>
	/// base class for structures that can read themselves
	/// </summary>
	// Token: 0x020000B2 RID: 178
	internal abstract class StructureBuilder<T>
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public StructureBuilder()
		{
			this.Reset();
		}

		// Token: 0x060003E6 RID: 998
		public abstract T Read(BinaryReader br);

		// Token: 0x060003E7 RID: 999
		public abstract void Write(BinaryWriter bw, T o);

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003E8 RID: 1000
		public abstract int Length { get; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		public void Reset()
		{
			this.data = new List<T>();
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		public T[] Data
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x04000497 RID: 1175
		protected List<T> data;
	}
}
