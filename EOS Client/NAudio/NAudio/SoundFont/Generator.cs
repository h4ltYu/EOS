using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Soundfont generator
	/// </summary>
	// Token: 0x020000B1 RID: 177
	public class Generator
	{
		/// <summary>
		/// Gets the generator type
		/// </summary>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000D58C File Offset: 0x0000B78C
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000D594 File Offset: 0x0000B794
		public GeneratorEnum GeneratorType
		{
			get
			{
				return this.generatorType;
			}
			set
			{
				this.generatorType = value;
			}
		}

		/// <summary>
		/// Generator amount as an unsigned short
		/// </summary>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000D59D File Offset: 0x0000B79D
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000D5A5 File Offset: 0x0000B7A5
		public ushort UInt16Amount
		{
			get
			{
				return this.rawAmount;
			}
			set
			{
				this.rawAmount = value;
			}
		}

		/// <summary>
		/// Generator amount as a signed short
		/// </summary>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000D5B7 File Offset: 0x0000B7B7
		public short Int16Amount
		{
			get
			{
				return (short)this.rawAmount;
			}
			set
			{
				this.rawAmount = (ushort)value;
			}
		}

		/// <summary>
		/// Low byte amount
		/// </summary>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public byte LowByteAmount
		{
			get
			{
				return (byte)(this.rawAmount & 255);
			}
			set
			{
				this.rawAmount &= 65280;
				this.rawAmount += (ushort)value;
			}
		}

		/// <summary>
		/// High byte amount
		/// </summary>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000D605 File Offset: 0x0000B805
		public byte HighByteAmount
		{
			get
			{
				return (byte)((this.rawAmount & 65280) >> 8);
			}
			set
			{
				this.rawAmount &= 255;
				this.rawAmount += (ushort)(value << 8);
			}
		}

		/// <summary>
		/// Instrument
		/// </summary>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000D62C File Offset: 0x0000B82C
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000D634 File Offset: 0x0000B834
		public Instrument Instrument
		{
			get
			{
				return this.instrument;
			}
			set
			{
				this.instrument = value;
			}
		}

		/// <summary>
		/// Sample Header
		/// </summary>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000D63D File Offset: 0x0000B83D
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000D645 File Offset: 0x0000B845
		public SampleHeader SampleHeader
		{
			get
			{
				return this.sampleHeader;
			}
			set
			{
				this.sampleHeader = value;
			}
		}

		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000D650 File Offset: 0x0000B850
		public override string ToString()
		{
			if (this.generatorType == GeneratorEnum.Instrument)
			{
				return string.Format("Generator Instrument {0}", this.instrument.Name);
			}
			if (this.generatorType == GeneratorEnum.SampleID)
			{
				return string.Format("Generator SampleID {0}", this.sampleHeader);
			}
			return string.Format("Generator {0} {1}", this.generatorType, this.rawAmount);
		}

		// Token: 0x04000493 RID: 1171
		private GeneratorEnum generatorType;

		// Token: 0x04000494 RID: 1172
		private ushort rawAmount;

		// Token: 0x04000495 RID: 1173
		private Instrument instrument;

		// Token: 0x04000496 RID: 1174
		private SampleHeader sampleHeader;
	}
}
