using System;

namespace NAudio.Utils
{
	/// <summary>
	/// Allows us to add descriptions to interop members
	/// </summary>
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Field)]
	public class FieldDescriptionAttribute : Attribute
	{
		/// <summary>
		/// The description
		/// </summary>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006DAD File Offset: 0x00004FAD
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006DB5 File Offset: 0x00004FB5
		public string Description { get; private set; }

		/// <summary>
		/// Field description
		/// </summary>
		// Token: 0x06000128 RID: 296 RVA: 0x00006DBE File Offset: 0x00004FBE
		public FieldDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		/// <summary>
		/// String representation
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000129 RID: 297 RVA: 0x00006DCD File Offset: 0x00004FCD
		public override string ToString()
		{
			return this.Description;
		}
	}
}
