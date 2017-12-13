using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Modulator
	/// </summary>
	// Token: 0x020000B9 RID: 185
	public class Modulator
	{
		/// <summary>
		/// Source Modulation data type
		/// </summary>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public ModulatorType SourceModulationData
		{
			get
			{
				return this.sourceModulationData;
			}
			set
			{
				this.sourceModulationData = value;
			}
		}

		/// <summary>
		/// Destination generator type
		/// </summary>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000DC8D File Offset: 0x0000BE8D
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000DC95 File Offset: 0x0000BE95
		public GeneratorEnum DestinationGenerator
		{
			get
			{
				return this.destinationGenerator;
			}
			set
			{
				this.destinationGenerator = value;
			}
		}

		/// <summary>
		/// Amount
		/// </summary>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000DCA6 File Offset: 0x0000BEA6
		public short Amount
		{
			get
			{
				return this.amount;
			}
			set
			{
				this.amount = value;
			}
		}

		/// <summary>
		/// Source Modulation Amount Type
		/// </summary>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000DCAF File Offset: 0x0000BEAF
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000DCB7 File Offset: 0x0000BEB7
		public ModulatorType SourceModulationAmount
		{
			get
			{
				return this.sourceModulationAmount;
			}
			set
			{
				this.sourceModulationAmount = value;
			}
		}

		/// <summary>
		/// Source Transform Type
		/// </summary>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public TransformEnum SourceTransform
		{
			get
			{
				return this.sourceTransform;
			}
			set
			{
				this.sourceTransform = value;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x0600041F RID: 1055 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public override string ToString()
		{
			return string.Format("Modulator {0} {1} {2} {3} {4}", new object[]
			{
				this.sourceModulationData,
				this.destinationGenerator,
				this.amount,
				this.sourceModulationAmount,
				this.sourceTransform
			});
		}

		// Token: 0x040004E8 RID: 1256
		private ModulatorType sourceModulationData;

		// Token: 0x040004E9 RID: 1257
		private GeneratorEnum destinationGenerator;

		// Token: 0x040004EA RID: 1258
		private short amount;

		// Token: 0x040004EB RID: 1259
		private ModulatorType sourceModulationAmount;

		// Token: 0x040004EC RID: 1260
		private TransformEnum sourceTransform;
	}
}
