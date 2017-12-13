using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// MP_PARAMINFO
	/// </summary>
	// Token: 0x02000092 RID: 146
	internal struct MediaParamInfo
	{
		// Token: 0x04000403 RID: 1027
		public MediaParamType mpType;

		// Token: 0x04000404 RID: 1028
		public MediaParamCurveType mopCaps;

		// Token: 0x04000405 RID: 1029
		public float mpdMinValue;

		// Token: 0x04000406 RID: 1030
		public float mpdMaxValue;

		// Token: 0x04000407 RID: 1031
		public float mpdNeutralValue;

		// Token: 0x04000408 RID: 1032
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string szUnitText;

		// Token: 0x04000409 RID: 1033
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string szLabel;
	}
}
