using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
	// Token: 0x0200009E RID: 158
	internal class SimpleCompressor : AttRelEnvelope
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000BB78 File Offset: 0x00009D78
		public SimpleCompressor(double attackTime, double releaseTime, double sampleRate) : base(attackTime, releaseTime, sampleRate)
		{
			this.Threshold = 0.0;
			this.Ratio = 1.0;
			this.MakeUpGain = 0.0;
			this.envdB = 1E-25;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public SimpleCompressor() : base(10.0, 10.0, 44100.0)
		{
			this.Threshold = 0.0;
			this.Ratio = 1.0;
			this.MakeUpGain = 0.0;
			this.envdB = 1E-25;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BC36 File Offset: 0x00009E36
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000BC3E File Offset: 0x00009E3E
		public double MakeUpGain { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000BC47 File Offset: 0x00009E47
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000BC4F File Offset: 0x00009E4F
		public double Threshold { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000BC58 File Offset: 0x00009E58
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000BC60 File Offset: 0x00009E60
		public double Ratio { get; set; }

		// Token: 0x0600036E RID: 878 RVA: 0x0000BC69 File Offset: 0x00009E69
		public void InitRuntime()
		{
			this.envdB = 1E-25;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000BC7C File Offset: 0x00009E7C
		public void Process(ref double in1, ref double in2)
		{
			double val = Math.Abs(in1);
			double val2 = Math.Abs(in2);
			double num = Math.Max(val, val2);
			num += 1E-25;
			double num2 = Decibels.LinearToDecibels(num);
			double num3 = num2 - this.Threshold;
			if (num3 < 0.0)
			{
				num3 = 0.0;
			}
			num3 += 1E-25;
			base.Run(num3, ref this.envdB);
			num3 = this.envdB - 1E-25;
			double num4 = num3 * (this.Ratio - 1.0);
			num4 = Decibels.DecibelsToLinear(num4) * Decibels.DecibelsToLinear(this.MakeUpGain);
			in1 *= num4;
			in2 *= num4;
		}

		// Token: 0x04000438 RID: 1080
		private double envdB;
	}
}
