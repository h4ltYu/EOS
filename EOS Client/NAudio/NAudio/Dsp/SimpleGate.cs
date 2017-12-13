using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
	// Token: 0x0200009F RID: 159
	internal class SimpleGate : AttRelEnvelope
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public SimpleGate() : base(10.0, 10.0, 44100.0)
		{
			this.threshdB = 0.0;
			this.thresh = 1.0;
			this.env = 1E-25;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000BD98 File Offset: 0x00009F98
		public void Process(ref double in1, ref double in2)
		{
			double val = Math.Abs(in1);
			double val2 = Math.Abs(in2);
			double num = Math.Max(val, val2);
			double num2 = (num > this.thresh) ? 1.0 : 0.0;
			num2 += 1E-25;
			base.Run(num2, ref this.env);
			num2 = this.env - 1E-25;
			in1 *= num2;
			in2 *= num2;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000BE11 File Offset: 0x0000A011
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000BE19 File Offset: 0x0000A019
		public double Threshold
		{
			get
			{
				return this.threshdB;
			}
			set
			{
				this.threshdB = value;
				this.thresh = Decibels.DecibelsToLinear(value);
			}
		}

		// Token: 0x0400043C RID: 1084
		private double threshdB;

		// Token: 0x0400043D RID: 1085
		private double thresh;

		// Token: 0x0400043E RID: 1086
		private double env;
	}
}
