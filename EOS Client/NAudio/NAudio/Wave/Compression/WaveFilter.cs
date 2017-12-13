using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Summary description for WaveFilter.
	/// </summary>
	// Token: 0x0200017B RID: 379
	[StructLayout(LayoutKind.Sequential)]
	public class WaveFilter
	{
		/// <summary>
		/// cbStruct
		/// </summary>
		// Token: 0x0400085B RID: 2139
		public int StructureSize = Marshal.SizeOf(typeof(WaveFilter));

		/// <summary>
		/// dwFilterTag
		/// </summary>
		// Token: 0x0400085C RID: 2140
		public int FilterTag;

		/// <summary>
		/// fdwFilter
		/// </summary>
		// Token: 0x0400085D RID: 2141
		public int Filter;

		/// <summary>
		/// reserved
		/// </summary>
		// Token: 0x0400085E RID: 2142
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public int[] Reserved;
	}
}
