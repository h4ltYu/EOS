using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Interop structure for ACM stream headers.
	/// ACMSTREAMHEADER 
	/// http://msdn.microsoft.com/en-us/library/dd742926%28VS.85%29.aspx
	/// </summary>    
	// Token: 0x02000178 RID: 376
	[StructLayout(LayoutKind.Sequential, Size = 128)]
	internal class AcmStreamHeaderStruct
	{
		// Token: 0x04000842 RID: 2114
		public int cbStruct;

		// Token: 0x04000843 RID: 2115
		public AcmStreamHeaderStatusFlags fdwStatus;

		// Token: 0x04000844 RID: 2116
		public IntPtr userData;

		// Token: 0x04000845 RID: 2117
		public IntPtr sourceBufferPointer;

		// Token: 0x04000846 RID: 2118
		public int sourceBufferLength;

		// Token: 0x04000847 RID: 2119
		public int sourceBufferLengthUsed;

		// Token: 0x04000848 RID: 2120
		public IntPtr sourceUserData;

		// Token: 0x04000849 RID: 2121
		public IntPtr destBufferPointer;

		// Token: 0x0400084A RID: 2122
		public int destBufferLength;

		// Token: 0x0400084B RID: 2123
		public int destBufferLengthUsed;

		// Token: 0x0400084C RID: 2124
		public IntPtr destUserData;
	}
}
