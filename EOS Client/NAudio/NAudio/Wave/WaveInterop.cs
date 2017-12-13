using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// MME Wave function interop
	/// </summary>
	// Token: 0x02000187 RID: 391
	internal class WaveInterop
	{
		// Token: 0x06000818 RID: 2072
		[DllImport("winmm.dll")]
		public static extern int mmioStringToFOURCC([MarshalAs(UnmanagedType.LPStr)] string s, int flags);

		// Token: 0x06000819 RID: 2073
		[DllImport("winmm.dll")]
		public static extern int waveOutGetNumDevs();

		// Token: 0x0600081A RID: 2074
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutPrepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x0600081B RID: 2075
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutUnprepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x0600081C RID: 2076
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutWrite(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

		// Token: 0x0600081D RID: 2077
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutOpen(out IntPtr hWaveOut, IntPtr uDeviceID, WaveFormat lpFormat, WaveInterop.WaveCallback dwCallback, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x0600081E RID: 2078
		[DllImport("winmm.dll", EntryPoint = "waveOutOpen")]
		public static extern MmResult waveOutOpenWindow(out IntPtr hWaveOut, IntPtr uDeviceID, WaveFormat lpFormat, IntPtr callbackWindowHandle, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x0600081F RID: 2079
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutReset(IntPtr hWaveOut);

		// Token: 0x06000820 RID: 2080
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutClose(IntPtr hWaveOut);

		// Token: 0x06000821 RID: 2081
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutPause(IntPtr hWaveOut);

		// Token: 0x06000822 RID: 2082
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutRestart(IntPtr hWaveOut);

		// Token: 0x06000823 RID: 2083
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutGetPosition(IntPtr hWaveOut, out MmTime mmTime, int uSize);

		// Token: 0x06000824 RID: 2084
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutSetVolume(IntPtr hWaveOut, int dwVolume);

		// Token: 0x06000825 RID: 2085
		[DllImport("winmm.dll")]
		public static extern MmResult waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

		// Token: 0x06000826 RID: 2086
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern MmResult waveOutGetDevCaps(IntPtr deviceID, out WaveOutCapabilities waveOutCaps, int waveOutCapsSize);

		// Token: 0x06000827 RID: 2087
		[DllImport("winmm.dll")]
		public static extern int waveInGetNumDevs();

		// Token: 0x06000828 RID: 2088
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern MmResult waveInGetDevCaps(IntPtr deviceID, out WaveInCapabilities waveInCaps, int waveInCapsSize);

		// Token: 0x06000829 RID: 2089
		[DllImport("winmm.dll")]
		public static extern MmResult waveInAddBuffer(IntPtr hWaveIn, WaveHeader pwh, int cbwh);

		// Token: 0x0600082A RID: 2090
		[DllImport("winmm.dll")]
		public static extern MmResult waveInClose(IntPtr hWaveIn);

		// Token: 0x0600082B RID: 2091
		[DllImport("winmm.dll")]
		public static extern MmResult waveInOpen(out IntPtr hWaveIn, IntPtr uDeviceID, WaveFormat lpFormat, WaveInterop.WaveCallback dwCallback, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x0600082C RID: 2092
		[DllImport("winmm.dll", EntryPoint = "waveInOpen")]
		public static extern MmResult waveInOpenWindow(out IntPtr hWaveIn, IntPtr uDeviceID, WaveFormat lpFormat, IntPtr callbackWindowHandle, IntPtr dwInstance, WaveInterop.WaveInOutOpenFlags dwFlags);

		// Token: 0x0600082D RID: 2093
		[DllImport("winmm.dll")]
		public static extern MmResult waveInPrepareHeader(IntPtr hWaveIn, WaveHeader lpWaveInHdr, int uSize);

		// Token: 0x0600082E RID: 2094
		[DllImport("winmm.dll")]
		public static extern MmResult waveInUnprepareHeader(IntPtr hWaveIn, WaveHeader lpWaveInHdr, int uSize);

		// Token: 0x0600082F RID: 2095
		[DllImport("winmm.dll")]
		public static extern MmResult waveInReset(IntPtr hWaveIn);

		// Token: 0x06000830 RID: 2096
		[DllImport("winmm.dll")]
		public static extern MmResult waveInStart(IntPtr hWaveIn);

		// Token: 0x06000831 RID: 2097
		[DllImport("winmm.dll")]
		public static extern MmResult waveInStop(IntPtr hWaveIn);

		// Token: 0x02000188 RID: 392
		[Flags]
		public enum WaveInOutOpenFlags
		{
			/// <summary>
			/// CALLBACK_NULL
			/// No callback
			/// </summary>
			// Token: 0x04000950 RID: 2384
			CallbackNull = 0,
			/// <summary>
			/// CALLBACK_FUNCTION
			/// dwCallback is a FARPROC 
			/// </summary>
			// Token: 0x04000951 RID: 2385
			CallbackFunction = 196608,
			/// <summary>
			/// CALLBACK_EVENT
			/// dwCallback is an EVENT handle 
			/// </summary>
			// Token: 0x04000952 RID: 2386
			CallbackEvent = 327680,
			/// <summary>
			/// CALLBACK_WINDOW
			/// dwCallback is a HWND 
			/// </summary>
			// Token: 0x04000953 RID: 2387
			CallbackWindow = 65536,
			/// <summary>
			/// CALLBACK_THREAD
			/// callback is a thread ID 
			/// </summary>
			// Token: 0x04000954 RID: 2388
			CallbackThread = 131072
		}

		// Token: 0x02000189 RID: 393
		public enum WaveMessage
		{
			/// <summary>
			/// WIM_OPEN
			/// </summary>
			// Token: 0x04000956 RID: 2390
			WaveInOpen = 958,
			/// <summary>
			/// WIM_CLOSE
			/// </summary>
			// Token: 0x04000957 RID: 2391
			WaveInClose,
			/// <summary>
			/// WIM_DATA
			/// </summary>
			// Token: 0x04000958 RID: 2392
			WaveInData,
			/// <summary>
			/// WOM_CLOSE
			/// </summary>
			// Token: 0x04000959 RID: 2393
			WaveOutClose = 956,
			/// <summary>
			/// WOM_DONE
			/// </summary>
			// Token: 0x0400095A RID: 2394
			WaveOutDone,
			/// <summary>
			/// WOM_OPEN
			/// </summary>
			// Token: 0x0400095B RID: 2395
			WaveOutOpen = 955
		}

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x06000834 RID: 2100
		public delegate void WaveCallback(IntPtr hWaveOut, WaveInterop.WaveMessage message, IntPtr dwInstance, WaveHeader wavhdr, IntPtr dwReserved);
	}
}
