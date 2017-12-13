using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000168 RID: 360
	[Flags]
	internal enum AcmFormatChooseStyleFlags
	{
		/// <summary>
		/// None
		/// </summary>
		// Token: 0x040007F3 RID: 2035
		None = 0,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_SHOWHELP
		/// </summary>
		// Token: 0x040007F4 RID: 2036
		ShowHelp = 4,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_ENABLEHOOK
		/// </summary>
		// Token: 0x040007F5 RID: 2037
		EnableHook = 8,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_ENABLETEMPLATE
		/// </summary>
		// Token: 0x040007F6 RID: 2038
		EnableTemplate = 16,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_ENABLETEMPLATEHANDLE
		/// </summary>
		// Token: 0x040007F7 RID: 2039
		EnableTemplateHandle = 32,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_INITTOWFXSTRUCT
		/// </summary>
		// Token: 0x040007F8 RID: 2040
		InitToWfxStruct = 64,
		/// <summary>
		/// ACMFORMATCHOOSE_STYLEF_CONTEXTHELP
		/// </summary>
		// Token: 0x040007F9 RID: 2041
		ContextHelp = 128
	}
}
