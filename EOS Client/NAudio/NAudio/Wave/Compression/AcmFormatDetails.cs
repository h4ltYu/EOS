using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// ACMFORMATDETAILS
	/// http://msdn.microsoft.com/en-us/library/dd742913%28VS.85%29.aspx
	/// </summary>
	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct AcmFormatDetails
	{
		/// <summary>
		/// ACMFORMATDETAILS_FORMAT_CHARS
		/// </summary>
		// Token: 0x040007FA RID: 2042
		public const int FormatDescriptionChars = 128;

		/// <summary>
		/// DWORD cbStruct; 
		/// </summary>
		// Token: 0x040007FB RID: 2043
		public int structSize;

		/// <summary>
		/// DWORD dwFormatIndex; 
		/// </summary>
		// Token: 0x040007FC RID: 2044
		public int formatIndex;

		/// <summary>
		/// DWORD dwFormatTag; 
		/// </summary>
		// Token: 0x040007FD RID: 2045
		public int formatTag;

		/// <summary>
		/// DWORD fdwSupport; 
		/// </summary>
		// Token: 0x040007FE RID: 2046
		public AcmDriverDetailsSupportFlags supportFlags;

		/// <summary>
		/// LPWAVEFORMATEX pwfx; 
		/// </summary>    
		// Token: 0x040007FF RID: 2047
		public IntPtr waveFormatPointer;

		/// <summary>
		/// DWORD cbwfx; 
		/// </summary>
		// Token: 0x04000800 RID: 2048
		public int waveFormatByteSize;

		/// <summary>
		/// TCHAR szFormat[ACMFORMATDETAILS_FORMAT_CHARS];
		/// </summary>
		// Token: 0x04000801 RID: 2049
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string formatDescription;
	}
}
