using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016D RID: 365
	internal struct AcmFormatTagDetails
	{
		/// <summary>
		/// ACMFORMATTAGDETAILS_FORMATTAG_CHARS
		/// </summary>
		// Token: 0x04000814 RID: 2068
		public const int FormatTagDescriptionChars = 48;

		/// <summary>
		/// DWORD cbStruct; 
		/// </summary>
		// Token: 0x04000815 RID: 2069
		public int structureSize;

		/// <summary>
		/// DWORD dwFormatTagIndex; 
		/// </summary>
		// Token: 0x04000816 RID: 2070
		public int formatTagIndex;

		/// <summary>
		/// DWORD dwFormatTag; 
		/// </summary>
		// Token: 0x04000817 RID: 2071
		public int formatTag;

		/// <summary>
		/// DWORD cbFormatSize; 
		/// </summary>
		// Token: 0x04000818 RID: 2072
		public int formatSize;

		/// <summary>
		/// DWORD fdwSupport;
		/// </summary>
		// Token: 0x04000819 RID: 2073
		public AcmDriverDetailsSupportFlags supportFlags;

		/// <summary>
		/// DWORD cStandardFormats; 
		/// </summary>
		// Token: 0x0400081A RID: 2074
		public int standardFormatsCount;

		/// <summary>
		/// TCHAR szFormatTag[ACMFORMATTAGDETAILS_FORMATTAG_CHARS]; 
		/// </summary>
		// Token: 0x0400081B RID: 2075
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
		public string formatDescription;
	}
}
