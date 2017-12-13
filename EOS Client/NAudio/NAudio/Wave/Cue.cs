using System;
using System.Text.RegularExpressions;

namespace NAudio.Wave
{
	/// <summary>
	/// Holds information on a cue: a labeled position within a Wave file
	/// </summary>
	// Token: 0x020001EA RID: 490
	public class Cue
	{
		/// <summary>
		/// Cue position in samples
		/// </summary>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00020044 File Offset: 0x0001E244
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002004C File Offset: 0x0001E24C
		public int Position { get; private set; }

		/// <summary>
		/// Label of the cue
		/// </summary>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00020055 File Offset: 0x0001E255
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0002005D File Offset: 0x0001E25D
		public string Label { get; private set; }

		/// <summary>
		/// Creates a Cue based on a sample position and label 
		/// </summary>
		/// <param name="position"></param>
		/// <param name="label"></param>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x00020066 File Offset: 0x0001E266
		public Cue(int position, string label)
		{
			this.Position = position;
			if (label == null)
			{
				label = "";
			}
			this.Label = Regex.Replace(label, "[^\\u0000-\\u00FF]", "");
		}
	}
}
