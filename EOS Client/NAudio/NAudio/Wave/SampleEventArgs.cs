using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Sample event arguments
	/// </summary>
	// Token: 0x020001EE RID: 494
	public class SampleEventArgs : EventArgs
	{
		/// <summary>
		/// Left sample
		/// </summary>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000209BB File Offset: 0x0001EBBB
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000209C3 File Offset: 0x0001EBC3
		public float Left { get; set; }

		/// <summary>
		/// Right sample
		/// </summary>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000209CC File Offset: 0x0001EBCC
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x000209D4 File Offset: 0x0001EBD4
		public float Right { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		// Token: 0x06000AFF RID: 2815 RVA: 0x000209DD File Offset: 0x0001EBDD
		public SampleEventArgs(float left, float right)
		{
			this.Left = left;
			this.Right = right;
		}
	}
}
