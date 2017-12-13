using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// Media Object Size Info
	/// </summary>
	// Token: 0x02000091 RID: 145
	public class MediaObjectSizeInfo
	{
		/// <summary>
		/// Minimum Buffer Size, in bytes
		/// </summary>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A9FD File Offset: 0x00008BFD
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000AA05 File Offset: 0x00008C05
		public int Size { get; private set; }

		/// <summary>
		/// Max Lookahead
		/// </summary>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000AA0E File Offset: 0x00008C0E
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000AA16 File Offset: 0x00008C16
		public int MaxLookahead { get; private set; }

		/// <summary>
		/// Alignment
		/// </summary>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000AA1F File Offset: 0x00008C1F
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000AA27 File Offset: 0x00008C27
		public int Alignment { get; private set; }

		/// <summary>
		/// Media Object Size Info
		/// </summary>
		// Token: 0x06000337 RID: 823 RVA: 0x0000AA30 File Offset: 0x00008C30
		public MediaObjectSizeInfo(int size, int maxLookahead, int alignment)
		{
			this.Size = size;
			this.MaxLookahead = maxLookahead;
			this.Alignment = alignment;
		}

		/// <summary>
		/// ToString
		/// </summary>        
		// Token: 0x06000338 RID: 824 RVA: 0x0000AA4D File Offset: 0x00008C4D
		public override string ToString()
		{
			return string.Format("Size: {0}, Alignment {1}, MaxLookahead {2}", this.Size, this.Alignment, this.MaxLookahead);
		}
	}
}
