using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Guids for use with DMOEnum
	/// dmoreg.h
	/// </summary>
	// Token: 0x02000087 RID: 135
	internal static class DmoGuids
	{
		// Token: 0x040003E2 RID: 994
		public static readonly Guid DMOCATEGORY_AUDIO_DECODER = new Guid("57f2db8b-e6bb-4513-9d43-dcd2a6593125");

		// Token: 0x040003E3 RID: 995
		public static readonly Guid DMOCATEGORY_AUDIO_ENCODER = new Guid("33D9A761-90C8-11d0-BD43-00A0C911CE86");

		// Token: 0x040003E4 RID: 996
		public static readonly Guid DMOCATEGORY_VIDEO_DECODER = new Guid("4a69b442-28be-4991-969c-b500adf5d8a8");

		// Token: 0x040003E5 RID: 997
		public static readonly Guid DMOCATEGORY_VIDEO_ENCODER = new Guid("33D9A760-90C8-11d0-BD43-00A0C911CE86");

		// Token: 0x040003E6 RID: 998
		public static readonly Guid DMOCATEGORY_AUDIO_EFFECT = new Guid("f3602b3f-0592-48df-a4cd-674721e7ebeb");

		// Token: 0x040003E7 RID: 999
		public static readonly Guid DMOCATEGORY_VIDEO_EFFECT = new Guid("d990ee14-776c-4723-be46-3da2f56f10b9");

		// Token: 0x040003E8 RID: 1000
		public static readonly Guid DMOCATEGORY_AUDIO_CAPTURE_EFFECT = new Guid("f665aaba-3e09-4920-aa5f-219811148f09");
	}
}
