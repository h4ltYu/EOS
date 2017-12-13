using System;

namespace NAudio.Codecs
{
	/// <summary>
	/// Band data for G722 Codec
	/// </summary>
	// Token: 0x02000006 RID: 6
	public class Band
	{
		/// <summary>s</summary>
		// Token: 0x0400001E RID: 30
		public int s;

		/// <summary>sp</summary>
		// Token: 0x0400001F RID: 31
		public int sp;

		/// <summary>sz</summary>
		// Token: 0x04000020 RID: 32
		public int sz;

		/// <summary>r</summary>
		// Token: 0x04000021 RID: 33
		public int[] r = new int[3];

		/// <summary>a</summary>
		// Token: 0x04000022 RID: 34
		public int[] a = new int[3];

		/// <summary>ap</summary>
		// Token: 0x04000023 RID: 35
		public int[] ap = new int[3];

		/// <summary>p</summary>
		// Token: 0x04000024 RID: 36
		public int[] p = new int[3];

		/// <summary>d</summary>
		// Token: 0x04000025 RID: 37
		public int[] d = new int[7];

		/// <summary>b</summary>
		// Token: 0x04000026 RID: 38
		public int[] b = new int[7];

		/// <summary>bp</summary>
		// Token: 0x04000027 RID: 39
		public int[] bp = new int[7];

		/// <summary>sg</summary>
		// Token: 0x04000028 RID: 40
		public int[] sg = new int[7];

		/// <summary>nb</summary>
		// Token: 0x04000029 RID: 41
		public int nb;

		/// <summary>det</summary>
		// Token: 0x0400002A RID: 42
		public int det;
	}
}
