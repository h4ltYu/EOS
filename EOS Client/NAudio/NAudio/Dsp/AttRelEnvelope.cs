using System;

namespace NAudio.Dsp
{
	// Token: 0x0200009B RID: 155
	internal class AttRelEnvelope
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000B6C5 File Offset: 0x000098C5
		public AttRelEnvelope(double attackMilliseconds, double releaseMilliseconds, double sampleRate)
		{
			this.attack = new EnvelopeDetector(attackMilliseconds, sampleRate);
			this.release = new EnvelopeDetector(releaseMilliseconds, sampleRate);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000B6E7 File Offset: 0x000098E7
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public double Attack
		{
			get
			{
				return this.attack.TimeConstant;
			}
			set
			{
				this.attack.TimeConstant = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000B702 File Offset: 0x00009902
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000B70F File Offset: 0x0000990F
		public double Release
		{
			get
			{
				return this.release.TimeConstant;
			}
			set
			{
				this.release.TimeConstant = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000B71D File Offset: 0x0000991D
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000B72C File Offset: 0x0000992C
		public double SampleRate
		{
			get
			{
				return this.attack.SampleRate;
			}
			set
			{
				EnvelopeDetector envelopeDetector = this.attack;
				this.release.SampleRate = value;
				envelopeDetector.SampleRate = value;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B753 File Offset: 0x00009953
		public void Run(double inValue, ref double state)
		{
			if (inValue > state)
			{
				this.attack.Run(inValue, ref state);
				return;
			}
			this.release.Run(inValue, ref state);
		}

		// Token: 0x04000435 RID: 1077
		protected const double DC_OFFSET = 1E-25;

		// Token: 0x04000436 RID: 1078
		private readonly EnvelopeDetector attack;

		// Token: 0x04000437 RID: 1079
		private readonly EnvelopeDetector release;
	}
}
