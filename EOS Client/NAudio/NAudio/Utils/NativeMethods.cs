using System;
using System.Runtime.InteropServices;

namespace NAudio.Utils
{
	/// <summary>
	/// General purpose native methods for internal NAudio use
	/// </summary>
	// Token: 0x0200006B RID: 107
	internal class NativeMethods
	{
		// Token: 0x0600024F RID: 591
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x06000250 RID: 592
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x06000251 RID: 593
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);
	}
}
