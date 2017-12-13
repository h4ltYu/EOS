using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// ACMFORMATCHOOSE
	/// http://msdn.microsoft.com/en-us/library/dd742911%28VS.85%29.aspx
	/// </summary>
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
	internal struct AcmFormatChoose
	{
		/// <summary>
		/// DWORD cbStruct; 
		/// </summary>
		// Token: 0x040007E2 RID: 2018
		public int structureSize;

		/// <summary>
		/// DWORD fdwStyle; 
		/// </summary>
		// Token: 0x040007E3 RID: 2019
		public AcmFormatChooseStyleFlags styleFlags;

		/// <summary>
		/// HWND hwndOwner; 
		/// </summary>
		// Token: 0x040007E4 RID: 2020
		public IntPtr ownerWindowHandle;

		/// <summary>
		/// LPWAVEFORMATEX pwfx; 
		/// </summary>
		// Token: 0x040007E5 RID: 2021
		public IntPtr selectedWaveFormatPointer;

		/// <summary>
		/// DWORD cbwfx; 
		/// </summary>
		// Token: 0x040007E6 RID: 2022
		public int selectedWaveFormatByteSize;

		/// <summary>
		/// LPCTSTR pszTitle; 
		/// </summary>
		// Token: 0x040007E7 RID: 2023
		[MarshalAs(UnmanagedType.LPTStr)]
		public string title;

		/// <summary>
		/// TCHAR szFormatTag[ACMFORMATTAGDETAILS_FORMATTAG_CHARS]; 
		/// </summary>
		// Token: 0x040007E8 RID: 2024
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
		public string formatTagDescription;

		/// <summary>
		/// TCHAR szFormat[ACMFORMATDETAILS_FORMAT_CHARS]; 
		/// </summary>
		// Token: 0x040007E9 RID: 2025
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string formatDescription;

		/// <summary>
		/// LPTSTR pszName; 
		/// n.b. can be written into
		/// </summary>
		// Token: 0x040007EA RID: 2026
		[MarshalAs(UnmanagedType.LPTStr)]
		public string name;

		/// <summary>
		/// DWORD cchName
		/// Should be at least 128 unless name is zero
		/// </summary>
		// Token: 0x040007EB RID: 2027
		public int nameByteSize;

		/// <summary>
		/// DWORD fdwEnum; 
		/// </summary>
		// Token: 0x040007EC RID: 2028
		public AcmFormatEnumFlags formatEnumFlags;

		/// <summary>
		/// LPWAVEFORMATEX pwfxEnum; 
		/// </summary>
		// Token: 0x040007ED RID: 2029
		public IntPtr waveFormatEnumPointer;

		/// <summary>
		/// HINSTANCE hInstance; 
		/// </summary>
		// Token: 0x040007EE RID: 2030
		public IntPtr instanceHandle;

		/// <summary>
		/// LPCTSTR pszTemplateName; 
		/// </summary>
		// Token: 0x040007EF RID: 2031
		[MarshalAs(UnmanagedType.LPTStr)]
		public string templateName;

		/// <summary>
		/// LPARAM lCustData; 
		/// </summary>
		// Token: 0x040007F0 RID: 2032
		public IntPtr customData;

		/// <summary>
		/// ACMFORMATCHOOSEHOOKPROC pfnHook; 
		/// </summary>
		// Token: 0x040007F1 RID: 2033
		public AcmInterop.AcmFormatChooseHookProc windowCallbackFunction;
	}
}
