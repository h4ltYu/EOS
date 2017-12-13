using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// MP_CURVE_TYPE
	/// </summary>
	// Token: 0x02000094 RID: 148
	[Flags]
	internal enum MediaParamCurveType
	{
		// Token: 0x04000411 RID: 1041
		MP_CURVE_JUMP = 1,
		// Token: 0x04000412 RID: 1042
		MP_CURVE_LINEAR = 2,
		// Token: 0x04000413 RID: 1043
		MP_CURVE_SQUARE = 4,
		// Token: 0x04000414 RID: 1044
		MP_CURVE_INVSQUARE = 8,
		// Token: 0x04000415 RID: 1045
		MP_CURVE_SINE = 16
	}
}
