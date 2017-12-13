using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// SoundFont instrument
	/// </summary>
	// Token: 0x020000B6 RID: 182
	public class Instrument
	{
		/// <summary>
		/// instrument name
		/// </summary>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000DB28 File Offset: 0x0000BD28
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000DB30 File Offset: 0x0000BD30
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
		/// Zones
		/// </summary>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000DB39 File Offset: 0x0000BD39
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000DB41 File Offset: 0x0000BD41
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
		// Token: 0x0600040D RID: 1037 RVA: 0x0000DB4A File Offset: 0x0000BD4A
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x040004E1 RID: 1249
		private string name;

		// Token: 0x040004E2 RID: 1250
		internal ushort startInstrumentZoneIndex;

		// Token: 0x040004E3 RID: 1251
		internal ushort endInstrumentZoneIndex;

		// Token: 0x040004E4 RID: 1252
		private Zone[] zones;
	}
}
