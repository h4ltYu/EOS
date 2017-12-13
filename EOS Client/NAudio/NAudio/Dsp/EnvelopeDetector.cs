using System;

namespace NAudio.Dsp
{
	// Token: 0x0200009A RID: 154
	internal class EnvelopeDetector
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000B622 File Offset: 0x00009822
		public EnvelopeDetector() : this(1.0, 44100.0)
		{
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B63C File Offset: 0x0000983C
		public EnvelopeDetector(double ms, double sampleRate)
		{
			this.sampleRate = sampleRate;
			this.ms = ms;
			this.SetCoef();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B658 File Offset: 0x00009858
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000B660 File Offset: 0x00009860
		public double TimeConstant
		{
			get
			{
				return this.ms;
			}
			set
			{
				this.ms = value;
				this.SetCoef();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B66F File Offset: 0x0000986F
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000B677 File Offset: 0x00009877
		public double SampleRate
		{
			get
			{
				return this.sampleRate;
			}
			set
			{
				this.sampleRate = value;
				this.SetCoef();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B686 File Offset: 0x00009886
		public void Run(double inValue, ref double state)
		{
			state = inValue + this.coeff * (state - inValue);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B697 File Offset: 0x00009897
		private void SetCoef()
		{
			this.coeff = Math.Exp(-1.0 / (0.001 * this.ms * this.sampleRate));
		}

		// Token: 0x04000432 RID: 1074
		private double sampleRate;

		// Token: 0x04000433 RID: 1075
		private double ms;

		// Token: 0x04000434 RID: 1076
		private double coeff;
	}
}
