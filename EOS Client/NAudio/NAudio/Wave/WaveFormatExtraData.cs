using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// This class used for marshalling from unmanaged code
	/// </summary>
	// Token: 0x020001AD RID: 429
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class WaveFormatExtraData : WaveFormat
	{
		/// <summary>
		/// Allows the extra data to be read
		/// </summary>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000192EC File Offset: 0x000174EC
		public byte[] ExtraData
		{
			get
			{
				return this.extraData;
			}
		}

		/// <summary>
		/// parameterless constructor for marshalling
		/// </summary>
		// Token: 0x060008BB RID: 2235 RVA: 0x000192F4 File Offset: 0x000174F4
		internal WaveFormatExtraData()
		{
		}

		/// <summary>
		/// Reads this structure from a BinaryReader
		/// </summary>
		// Token: 0x060008BC RID: 2236 RVA: 0x00019309 File Offset: 0x00017509
		public WaveFormatExtraData(BinaryReader reader) : base(reader)
		{
			this.ReadExtraData(reader);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00019326 File Offset: 0x00017526
		internal void ReadExtraData(BinaryReader reader)
		{
			if (this.extraSize > 0)
			{
				reader.Read(this.extraData, 0, (int)this.extraSize);
			}
		}

		/// <summary>
		/// Writes this structure to a BinaryWriter
		/// </summary>
		// Token: 0x060008BE RID: 2238 RVA: 0x00019345 File Offset: 0x00017545
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			if (this.extraSize > 0)
			{
				writer.Write(this.extraData, 0, (int)this.extraSize);
			}
		}

		// Token: 0x04000A77 RID: 2679
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		private byte[] extraData = new byte[100];
	}
}
