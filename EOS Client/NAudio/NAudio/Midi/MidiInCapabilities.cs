using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	/// <summary>
	/// MIDI In Device Capabilities
	/// </summary>
	// Token: 0x020000DF RID: 223
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct MidiInCapabilities
	{
		/// <summary>
		/// Gets the manufacturer of this device
		/// </summary>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00011654 File Offset: 0x0000F854
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.manufacturerId;
			}
		}

		/// <summary>
		/// Gets the product identifier (manufacturer specific)
		/// </summary>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001165C File Offset: 0x0000F85C
		public int ProductId
		{
			get
			{
				return (int)this.productId;
			}
		}

		/// <summary>
		/// Gets the product name
		/// </summary>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00011664 File Offset: 0x0000F864
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x040005B1 RID: 1457
		private const int MaxProductNameLength = 32;

		/// <summary>
		/// wMid
		/// </summary>
		// Token: 0x040005B2 RID: 1458
		private ushort manufacturerId;

		/// <summary>
		/// wPid
		/// </summary>
		// Token: 0x040005B3 RID: 1459
		private ushort productId;

		/// <summary>
		/// vDriverVersion
		/// </summary>
		// Token: 0x040005B4 RID: 1460
		private uint driverVersion;

		/// <summary>
		/// Product Name
		/// </summary>
		// Token: 0x040005B5 RID: 1461
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string productName;

		/// <summary>
		/// Support - Reserved
		/// </summary>
		// Token: 0x040005B6 RID: 1462
		private int support;
	}
}
