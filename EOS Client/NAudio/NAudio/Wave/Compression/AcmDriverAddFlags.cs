using System;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Flags for use with acmDriverAdd
	/// </summary>
	// Token: 0x0200006E RID: 110
	internal enum AcmDriverAddFlags
	{
		/// <summary>
		/// ACM_DRIVERADDF_LOCAL
		/// </summary>
		// Token: 0x0400036F RID: 879
		Local,
		/// <summary>
		/// ACM_DRIVERADDF_GLOBAL
		/// </summary>
		// Token: 0x04000370 RID: 880
		Global = 8,
		/// <summary>
		/// ACM_DRIVERADDF_FUNCTION
		/// </summary>
		// Token: 0x04000371 RID: 881
		Function = 3,
		/// <summary>
		/// ACM_DRIVERADDF_NOTIFYHWND
		/// </summary>
		// Token: 0x04000372 RID: 882
		NotifyWindowHandle
	}
}
