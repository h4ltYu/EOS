using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Interop structure for ACM driver details (ACMDRIVERDETAILS)
	/// http://msdn.microsoft.com/en-us/library/dd742889%28VS.85%29.aspx
	/// </summary>
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct AcmDriverDetails
	{
		/// <summary>
		/// ACMDRIVERDETAILS_SHORTNAME_CHARS
		/// </summary>
		// Token: 0x040007C0 RID: 1984
		private const int ShortNameChars = 32;

		/// <summary>
		/// ACMDRIVERDETAILS_LONGNAME_CHARS
		/// </summary>
		// Token: 0x040007C1 RID: 1985
		private const int LongNameChars = 128;

		/// <summary>
		/// ACMDRIVERDETAILS_COPYRIGHT_CHARS
		/// </summary>
		// Token: 0x040007C2 RID: 1986
		private const int CopyrightChars = 80;

		/// <summary>
		/// ACMDRIVERDETAILS_LICENSING_CHARS 
		/// </summary>
		// Token: 0x040007C3 RID: 1987
		private const int LicensingChars = 128;

		/// <summary>
		/// ACMDRIVERDETAILS_FEATURES_CHARS
		/// </summary>
		// Token: 0x040007C4 RID: 1988
		private const int FeaturesChars = 512;

		/// <summary>
		/// DWORD cbStruct
		/// </summary>
		// Token: 0x040007C5 RID: 1989
		public int structureSize;

		/// <summary>
		/// FOURCC fccType
		/// </summary>
		// Token: 0x040007C6 RID: 1990
		public uint fccType;

		/// <summary>
		/// FOURCC fccComp
		/// </summary>
		// Token: 0x040007C7 RID: 1991
		public uint fccComp;

		/// <summary>
		/// WORD   wMid; 
		/// </summary>
		// Token: 0x040007C8 RID: 1992
		public ushort manufacturerId;

		/// <summary>
		/// WORD wPid
		/// </summary>
		// Token: 0x040007C9 RID: 1993
		public ushort productId;

		/// <summary>
		/// DWORD vdwACM
		/// </summary>
		// Token: 0x040007CA RID: 1994
		public uint acmVersion;

		/// <summary>
		/// DWORD vdwDriver
		/// </summary>
		// Token: 0x040007CB RID: 1995
		public uint driverVersion;

		/// <summary>
		/// DWORD  fdwSupport;
		/// </summary>
		// Token: 0x040007CC RID: 1996
		public AcmDriverDetailsSupportFlags supportFlags;

		/// <summary>
		/// DWORD cFormatTags
		/// </summary>
		// Token: 0x040007CD RID: 1997
		public int formatTagsCount;

		/// <summary>
		/// DWORD cFilterTags
		/// </summary>
		// Token: 0x040007CE RID: 1998
		public int filterTagsCount;

		/// <summary>
		/// HICON hicon
		/// </summary>
		// Token: 0x040007CF RID: 1999
		public IntPtr hicon;

		/// <summary>
		/// TCHAR  szShortName[ACMDRIVERDETAILS_SHORTNAME_CHARS]; 
		/// </summary>
		// Token: 0x040007D0 RID: 2000
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string shortName;

		/// <summary>
		/// TCHAR  szLongName[ACMDRIVERDETAILS_LONGNAME_CHARS];
		/// </summary>
		// Token: 0x040007D1 RID: 2001
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string longName;

		/// <summary>
		/// TCHAR  szCopyright[ACMDRIVERDETAILS_COPYRIGHT_CHARS]; 
		/// </summary>
		// Token: 0x040007D2 RID: 2002
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string copyright;

		/// <summary>
		/// TCHAR  szLicensing[ACMDRIVERDETAILS_LICENSING_CHARS]; 
		/// </summary>
		// Token: 0x040007D3 RID: 2003
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string licensing;

		/// <summary>
		/// TCHAR  szFeatures[ACMDRIVERDETAILS_FEATURES_CHARS];
		/// </summary>
		// Token: 0x040007D4 RID: 2004
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string features;
	}
}
