using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// A SoundFont zone
	/// </summary>
	// Token: 0x020000CA RID: 202
	public class Zone
	{
		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x06000466 RID: 1126 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public override string ToString()
		{
			return string.Format("Zone {0} Gens:{1} {2} Mods:{3}", new object[]
			{
				this.generatorCount,
				this.generatorIndex,
				this.modulatorCount,
				this.modulatorIndex
			});
		}

		/// <summary>
		/// Modulators for this Zone
		/// </summary>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000EAC7 File Offset: 0x0000CCC7
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000EACF File Offset: 0x0000CCCF
		public Modulator[] Modulators
		{
			get
			{
				return this.modulators;
			}
			set
			{
				this.modulators = value;
			}
		}

		/// <summary>
		/// Generators for this Zone
		/// </summary>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public Generator[] Generators
		{
			get
			{
				return this.generators;
			}
			set
			{
				this.generators = value;
			}
		}

		// Token: 0x04000535 RID: 1333
		internal ushort generatorIndex;

		// Token: 0x04000536 RID: 1334
		internal ushort modulatorIndex;

		// Token: 0x04000537 RID: 1335
		internal ushort generatorCount;

		// Token: 0x04000538 RID: 1336
		internal ushort modulatorCount;

		// Token: 0x04000539 RID: 1337
		private Modulator[] modulators;

		// Token: 0x0400053A RID: 1338
		private Generator[] generators;
	}
}
