using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// A SoundFont Preset
	/// </summary>
	// Token: 0x020000BE RID: 190
	public class Preset
	{
		/// <summary>
		/// Preset name
		/// </summary>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000DE86 File Offset: 0x0000C086
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000DE8E File Offset: 0x0000C08E
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>
		/// Patch Number
		/// </summary>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000DE97 File Offset: 0x0000C097
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000DE9F File Offset: 0x0000C09F
		public ushort PatchNumber
		{
			get
			{
				return this.patchNumber;
			}
			set
			{
				this.patchNumber = value;
			}
		}

		/// <summary>
		/// Bank number
		/// </summary>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public ushort Bank
		{
			get
			{
				return this.bank;
			}
			set
			{
				this.bank = value;
			}
		}

		/// <summary>
		/// Zones
		/// </summary>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		public Zone[] Zones
		{
			get
			{
				return this.zones;
			}
			set
			{
				this.zones = value;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x06000430 RID: 1072 RVA: 0x0000DECA File Offset: 0x0000C0CA
		public override string ToString()
		{
			return string.Format("{0}-{1} {2}", this.bank, this.patchNumber, this.name);
		}

		// Token: 0x04000500 RID: 1280
		private string name;

		// Token: 0x04000501 RID: 1281
		private ushort patchNumber;

		// Token: 0x04000502 RID: 1282
		private ushort bank;

		// Token: 0x04000503 RID: 1283
		internal ushort startPresetZoneIndex;

		// Token: 0x04000504 RID: 1284
		internal ushort endPresetZoneIndex;

		// Token: 0x04000505 RID: 1285
		internal uint library;

		// Token: 0x04000506 RID: 1286
		internal uint genre;

		// Token: 0x04000507 RID: 1287
		internal uint morphology;

		// Token: 0x04000508 RID: 1288
		private Zone[] zones;
	}
}
