using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Interop definitions for Windows ACM (Audio Compression Manager) API
	/// </summary>
	// Token: 0x0200016E RID: 366
	internal class AcmInterop
	{
		// Token: 0x060007A7 RID: 1959
		[DllImport("msacm32.dll")]
		public static extern MmResult acmDriverAdd(out IntPtr driverHandle, IntPtr driverModule, IntPtr driverFunctionAddress, int priority, AcmDriverAddFlags flags);

		// Token: 0x060007A8 RID: 1960
		[DllImport("msacm32.dll")]
		public static extern MmResult acmDriverRemove(IntPtr driverHandle, int removeFlags);

		// Token: 0x060007A9 RID: 1961
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverClose(IntPtr hAcmDriver, int closeFlags);

		// Token: 0x060007AA RID: 1962
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverEnum(AcmInterop.AcmDriverEnumCallback fnCallback, IntPtr dwInstance, AcmDriverEnumFlags flags);

		// Token: 0x060007AB RID: 1963
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverDetails(IntPtr hAcmDriver, ref AcmDriverDetails driverDetails, int reserved);

		// Token: 0x060007AC RID: 1964
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverOpen(out IntPtr pAcmDriver, IntPtr hAcmDriverId, int openFlags);

		// Token: 0x060007AD RID: 1965
		[DllImport("Msacm32.dll", EntryPoint = "acmFormatChooseW")]
		public static extern MmResult acmFormatChoose(ref AcmFormatChoose formatChoose);

		// Token: 0x060007AE RID: 1966
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatEnum(IntPtr hAcmDriver, ref AcmFormatDetails formatDetails, AcmInterop.AcmFormatEnumCallback callback, IntPtr instance, AcmFormatEnumFlags flags);

		/// <summary>
		/// http://msdn.microsoft.com/en-us/library/dd742916%28VS.85%29.aspx
		/// MMRESULT acmFormatSuggest(
		/// HACMDRIVER had,          
		/// LPWAVEFORMATEX pwfxSrc,  
		/// LPWAVEFORMATEX pwfxDst,  
		/// DWORD cbwfxDst,          
		/// DWORD fdwSuggest);
		/// </summary>
		// Token: 0x060007AF RID: 1967
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatSuggest(IntPtr hAcmDriver, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WaveFormatCustomMarshaler))] [In] WaveFormat sourceFormat, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WaveFormatCustomMarshaler))] [In] [Out] WaveFormat destFormat, int sizeDestFormat, AcmFormatSuggestFlags suggestFlags);

		// Token: 0x060007B0 RID: 1968
		[DllImport("Msacm32.dll", EntryPoint = "acmFormatSuggest")]
		public static extern MmResult acmFormatSuggest2(IntPtr hAcmDriver, IntPtr sourceFormatPointer, IntPtr destFormatPointer, int sizeDestFormat, AcmFormatSuggestFlags suggestFlags);

		// Token: 0x060007B1 RID: 1969
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatTagEnum(IntPtr hAcmDriver, ref AcmFormatTagDetails formatTagDetails, AcmInterop.AcmFormatTagEnumCallback callback, IntPtr instance, int reserved);

		// Token: 0x060007B2 RID: 1970
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmMetrics(IntPtr hAcmObject, AcmMetrics metric, out int output);

		/// <summary>
		/// http://msdn.microsoft.com/en-us/library/dd742928%28VS.85%29.aspx
		/// MMRESULT acmStreamOpen(
		///   LPHACMSTREAM    phas,       
		///   HACMDRIVER      had,        
		///   LPWAVEFORMATEX  pwfxSrc,    
		///   LPWAVEFORMATEX  pwfxDst,    
		///   LPWAVEFILTER    pwfltr,     
		///   DWORD_PTR       dwCallback, 
		///   DWORD_PTR       dwInstance, 
		///   DWORD           fdwOpen     
		/// </summary>
		// Token: 0x060007B3 RID: 1971
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamOpen(out IntPtr hAcmStream, IntPtr hAcmDriver, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WaveFormatCustomMarshaler))] [In] WaveFormat sourceFormat, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WaveFormatCustomMarshaler))] [In] WaveFormat destFormat, [In] WaveFilter waveFilter, IntPtr callback, IntPtr instance, AcmStreamOpenFlags openFlags);

		/// <summary>
		/// A version with pointers for troubleshooting
		/// </summary>
		// Token: 0x060007B4 RID: 1972
		[DllImport("Msacm32.dll", EntryPoint = "acmStreamOpen")]
		public static extern MmResult acmStreamOpen2(out IntPtr hAcmStream, IntPtr hAcmDriver, IntPtr sourceFormatPointer, IntPtr destFormatPointer, [In] WaveFilter waveFilter, IntPtr callback, IntPtr instance, AcmStreamOpenFlags openFlags);

		// Token: 0x060007B5 RID: 1973
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamClose(IntPtr hAcmStream, int closeFlags);

		// Token: 0x060007B6 RID: 1974
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamConvert(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, AcmStreamConvertFlags streamConvertFlags);

		// Token: 0x060007B7 RID: 1975
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamPrepareHeader(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, int prepareFlags);

		// Token: 0x060007B8 RID: 1976
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamReset(IntPtr hAcmStream, int resetFlags);

		// Token: 0x060007B9 RID: 1977
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamSize(IntPtr hAcmStream, int inputBufferSize, out int outputBufferSize, AcmStreamSizeFlags flags);

		// Token: 0x060007BA RID: 1978
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamUnprepareHeader(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, int flags);

		// Token: 0x0200016F RID: 367
		// (Invoke) Token: 0x060007BD RID: 1981
		public delegate bool AcmDriverEnumCallback(IntPtr hAcmDriverId, IntPtr instance, AcmDriverDetailsSupportFlags flags);

		// Token: 0x02000170 RID: 368
		// (Invoke) Token: 0x060007C1 RID: 1985
		public delegate bool AcmFormatEnumCallback(IntPtr hAcmDriverId, ref AcmFormatDetails formatDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags);

		// Token: 0x02000171 RID: 369
		// (Invoke) Token: 0x060007C5 RID: 1989
		public delegate bool AcmFormatTagEnumCallback(IntPtr hAcmDriverId, ref AcmFormatTagDetails formatTagDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags);

		/// <summary>
		/// http://msdn.microsoft.com/en-us/library/dd742910%28VS.85%29.aspx
		/// UINT ACMFORMATCHOOSEHOOKPROC acmFormatChooseHookProc(
		///   HWND hwnd,     
		///   UINT uMsg,     
		///   WPARAM wParam, 
		///   LPARAM lParam  
		/// </summary>        
		// Token: 0x02000172 RID: 370
		// (Invoke) Token: 0x060007C9 RID: 1993
		public delegate bool AcmFormatChooseHookProc(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam);
	}
}
