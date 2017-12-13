using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveOutCapabilities structure (based on WAVEOUTCAPS2 from mmsystem.h)
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/multimed/htm/_win32_waveoutcaps_str.asp
	/// </summary>
	// Token: 0x0200018B RID: 395
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WaveOutCapabilities
	{
		/// <summary>
		/// Number of channels supported
		/// </summary>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000179F9 File Offset: 0x00015BF9
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		/// <summary>
		/// Whether playback control is supported
		/// </summary>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00017A01 File Offset: 0x00015C01
		public bool SupportsPlaybackRateControl
		{
			get
			{
				return (this.support & WaveOutSupport.PlaybackRate) == WaveOutSupport.PlaybackRate;
			}
		}

		/// <summary>
		/// The product name
		/// </summary>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00017A0E File Offset: 0x00015C0E
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		/// <summary>
		/// Checks to see if a given SupportedWaveFormat is supported
		/// </summary>
		/// <param name="waveFormat">The SupportedWaveFormat</param>
		/// <returns>true if supported</returns>
		// Token: 0x0600083A RID: 2106 RVA: 0x00017A16 File Offset: 0x00015C16
		public bool SupportsWaveFormat(SupportedWaveFormat waveFormat)
		{
			return (this.supportedFormats & waveFormat) == waveFormat;
		}

		/// <summary>
		/// The device name Guid (if provided)
		/// </summary>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00017A23 File Offset: 0x00015C23
		public Guid NameGuid
		{
			get
			{
				return this.nameGuid;
			}
		}

		/// <summary>
		/// The product name Guid (if provided)
		/// </summary>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00017A2B File Offset: 0x00015C2B
		public Guid ProductGuid
		{
			get
			{
				return this.productGuid;
			}
		}

		/// <summary>
		/// The manufacturer guid (if provided)
		/// </summary>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00017A33 File Offset: 0x00015C33
		public Guid ManufacturerGuid
		{
			get
			{
				return this.manufacturerGuid;
			}
		}

		// Token: 0x0400095C RID: 2396
		private const int MaxProductNameLength = 32;

		/// <summary>
		/// wMid
		/// </summary>
		// Token: 0x0400095D RID: 2397
		private short manufacturerId;

		/// <summary>
		/// wPid
		/// </summary>
		// Token: 0x0400095E RID: 2398
		private short productId;

		/// <summary>
		/// vDriverVersion
		/// </summary>
		// Token: 0x0400095F RID: 2399
		private int driverVersion;

		/// <summary>
		/// Product Name (szPname)
		/// </summary>
		// Token: 0x04000960 RID: 2400
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string productName;

		/// <summary>
		/// Supported formats (bit flags) dwFormats 
		/// </summary>
		// Token: 0x04000961 RID: 2401
		private SupportedWaveFormat supportedFormats;

		/// <summary>
		/// Supported channels (1 for mono 2 for stereo) (wChannels)
		/// Seems to be set to -1 on a lot of devices
		/// </summary>
		// Token: 0x04000962 RID: 2402
		private short channels;

		/// <summary>
		/// wReserved1
		/// </summary>
		// Token: 0x04000963 RID: 2403
		private short reserved;

		/// <summary>
		/// Optional functionality supported by the device
		/// </summary>
		// Token: 0x04000964 RID: 2404
		private WaveOutSupport support;

		// Token: 0x04000965 RID: 2405
		private Guid manufacturerGuid;

		// Token: 0x04000966 RID: 2406
		private Guid productGuid;

		// Token: 0x04000967 RID: 2407
		private Guid nameGuid;
	}
}
