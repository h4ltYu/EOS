using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NAudio.Dmo
{
	// Token: 0x0200008A RID: 138
	internal static class DmoInterop
	{
		// Token: 0x060002F6 RID: 758
		[DllImport("msdmo.dll")]
		public static extern int DMOEnum([In] ref Guid guidCategory, DmoEnumFlags flags, int inTypes, [In] DmoPartialMediaType[] inTypesArray, int outTypes, [In] DmoPartialMediaType[] outTypesArray, out IEnumDmo enumDmo);

		// Token: 0x060002F7 RID: 759
		[DllImport("msdmo.dll")]
		public static extern int MoFreeMediaType([In] ref DmoMediaType mediaType);

		// Token: 0x060002F8 RID: 760
		[DllImport("msdmo.dll")]
		public static extern int MoInitMediaType([In] [Out] ref DmoMediaType mediaType, int formatBlockBytes);

		// Token: 0x060002F9 RID: 761
		[DllImport("msdmo.dll")]
		public static extern int DMOGetName([In] ref Guid clsidDMO, [Out] StringBuilder name);
	}
}
