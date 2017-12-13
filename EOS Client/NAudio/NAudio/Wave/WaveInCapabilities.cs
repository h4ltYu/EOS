using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveInCapabilities structure (based on WAVEINCAPS2 from mmsystem.h)
	/// http://msdn.microsoft.com/en-us/library/ms713726(VS.85).aspx
	/// </summary>
	// Token: 0x02000184 RID: 388
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WaveInCapabilities
	{
		/// <summary>
		/// Number of channels supported
		/// </summary>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000178EA File Offset: 0x00015AEA
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		/// <summary>
		/// The product name
		/// </summary>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000178F2 File Offset: 0x00015AF2
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		/// <summary>
		/// The device name Guid (if provided)
		/// </summary>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000178FA File Offset: 0x00015AFA
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
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00017902 File Offset: 0x00015B02
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
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001790A File Offset: 0x00015B0A
		public Guid ManufacturerGuid
		{
			get
			{
				return this.manufacturerGuid;
			}
		}

		/// <summary>
		/// Checks to see if a given SupportedWaveFormat is supported
		/// </summary>
		/// <param name="waveFormat">The SupportedWaveFormat</param>
		/// <returns>true if supported</returns>
		// Token: 0x06000812 RID: 2066 RVA: 0x00017912 File Offset: 0x00015B12
		public bool SupportsWaveFormat(SupportedWaveFormat waveFormat)
		{
			return (this.supportedFormats & waveFormat) == waveFormat;
		}

		// Token: 0x0400093F RID: 2367
		private const int MaxProductNameLength = 32;

		/// <summary>
		/// wMid
		/// </summary>
		// Token: 0x04000940 RID: 2368
		private short manufacturerId;

		/// <summary>
		/// wPid
		/// </summary>
		// Token: 0x04000941 RID: 2369
		private short productId;

		/// <summary>
		/// vDriverVersion
		/// </summary>
		// Token: 0x04000942 RID: 2370
		private int driverVersion;

		/// <summary>
		/// Product Name (szPname)
		/// </summary>
		// Token: 0x04000943 RID: 2371
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string productName;

		/// <summary>
		/// Supported formats (bit flags) dwFormats 
		/// </summary>
		// Token: 0x04000944 RID: 2372
		private SupportedWaveFormat supportedFormats;

		/// <summary>
		/// Supported channels (1 for mono 2 for stereo) (wChannels)
		/// Seems to be set to -1 on a lot of devices
		/// </summary>
		// Token: 0x04000945 RID: 2373
		private short channels;

		/// <summary>
		/// wReserved1
		/// </summary>
		// Token: 0x04000946 RID: 2374
		private short reserved;

		// Token: 0x04000947 RID: 2375
		private Guid manufacturerGuid;

		// Token: 0x04000948 RID: 2376
		private Guid productGuid;

		// Token: 0x04000949 RID: 2377
		private Guid nameGuid;
	}
}
