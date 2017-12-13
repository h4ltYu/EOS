using System;

namespace NAudio.Dsp
{
	/// <summary>
	/// BiQuad filter
	/// </summary>
	// Token: 0x02000098 RID: 152
	public class BiQuadFilter
	{
		/// <summary>
		/// Passes a single sample through the filter
		/// </summary>
		/// <param name="inSample">Input sample</param>
		/// <returns>Output sample</returns>
		// Token: 0x0600033F RID: 831 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public float Transform(float inSample)
		{
			double num = this.a0 * (double)inSample + this.a1 * (double)this.x1 + this.a2 * (double)this.x2 - this.a3 * (double)this.y1 - this.a4 * (double)this.y2;
			this.x2 = this.x1;
			this.x1 = inSample;
			this.y2 = this.y1;
			this.y1 = (float)num;
			return this.y1;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000ADFC File Offset: 0x00008FFC
		private void SetCoefficients(double aa0, double aa1, double aa2, double b0, double b1, double b2)
		{
			this.a0 = b0 / aa0;
			this.a1 = b1 / aa0;
			this.a2 = b2 / aa0;
			this.a3 = aa1 / aa0;
			this.a4 = aa2 / aa0;
		}

		/// <summary>
		/// Set this up as a low pass filter
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="cutoffFrequency">Cut-off Frequency</param>
		/// <param name="q">Bandwidth</param>
		// Token: 0x06000341 RID: 833 RVA: 0x0000AE30 File Offset: 0x00009030
		public void SetLowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num) / (double)(2f * q);
			double b = (1.0 - num2) / 2.0;
			double b2 = 1.0 - num2;
			double b3 = (1.0 - num2) / 2.0;
			double aa = 1.0 + num3;
			double aa2 = -2.0 * num2;
			double aa3 = 1.0 - num3;
			this.SetCoefficients(aa, aa2, aa3, b, b2, b3);
		}

		/// <summary>
		/// Set this up as a peaking EQ
		/// </summary>
		/// <param name="sampleRate">Sample Rate</param>
		/// <param name="centreFrequency">Centre Frequency</param>
		/// <param name="q">Bandwidth (Q)</param>
		/// <param name="dbGain">Gain in decibels</param>
		// Token: 0x06000342 RID: 834 RVA: 0x0000AED8 File Offset: 0x000090D8
		public void SetPeakingEq(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			double num = 6.2831853071795862 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = Math.Pow(10.0, (double)(dbGain / 40f));
			double b = 1.0 + num4 * num5;
			double b2 = -2.0 * num2;
			double b3 = 1.0 - num4 * num5;
			double aa = 1.0 + num4 / num5;
			double aa2 = -2.0 * num2;
			double aa3 = 1.0 - num4 / num5;
			this.SetCoefficients(aa, aa2, aa3, b, b2, b3);
		}

		/// <summary>
		/// Set this as a high pass filter
		/// </summary>
		// Token: 0x06000343 RID: 835 RVA: 0x0000AF94 File Offset: 0x00009194
		public void SetHighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num) / (double)(2f * q);
			double b = (1.0 + num2) / 2.0;
			double b2 = -(1.0 + num2);
			double b3 = (1.0 + num2) / 2.0;
			double aa = 1.0 + num3;
			double aa2 = -2.0 * num2;
			double aa3 = 1.0 - num3;
			this.SetCoefficients(aa, aa2, aa3, b, b2, b3);
		}

		/// <summary>
		/// Create a low pass filter
		/// </summary>
		// Token: 0x06000344 RID: 836 RVA: 0x0000B03C File Offset: 0x0000923C
		public static BiQuadFilter LowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetLowPassFilter(sampleRate, cutoffFrequency, q);
			return biQuadFilter;
		}

		/// <summary>
		/// Create a High pass filter
		/// </summary>
		// Token: 0x06000345 RID: 837 RVA: 0x0000B05C File Offset: 0x0000925C
		public static BiQuadFilter HighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetHighPassFilter(sampleRate, cutoffFrequency, q);
			return biQuadFilter;
		}

		/// <summary>
		/// Create a bandpass filter with constant skirt gain
		/// </summary>
		// Token: 0x06000346 RID: 838 RVA: 0x0000B07C File Offset: 0x0000927C
		public static BiQuadFilter BandPassFilterConstantSkirtGain(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double b = num3 / 2.0;
			int num5 = 0;
			double b2 = -num3 / 2.0;
			double num6 = 1.0 + num4;
			double num7 = -2.0 * num2;
			double num8 = 1.0 - num4;
			return new BiQuadFilter(num6, num7, num8, b, (double)num5, b2);
		}

		/// <summary>
		/// Create a bandpass filter with constant peak gain
		/// </summary>
		// Token: 0x06000347 RID: 839 RVA: 0x0000B10C File Offset: 0x0000930C
		public static BiQuadFilter BandPassFilterConstantPeakGain(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double b = num4;
			int num5 = 0;
			double b2 = -num4;
			double num6 = 1.0 + num4;
			double num7 = -2.0 * num2;
			double num8 = 1.0 - num4;
			return new BiQuadFilter(num6, num7, num8, b, (double)num5, b2);
		}

		/// <summary>
		/// Creates a notch filter
		/// </summary>
		// Token: 0x06000348 RID: 840 RVA: 0x0000B188 File Offset: 0x00009388
		public static BiQuadFilter NotchFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			int num5 = 1;
			double b = -2.0 * num2;
			int num6 = 1;
			double num7 = 1.0 + num4;
			double num8 = -2.0 * num2;
			double num9 = 1.0 - num4;
			return new BiQuadFilter(num7, num8, num9, (double)num5, b, (double)num6);
		}

		/// <summary>
		/// Creaes an all pass filter
		/// </summary>
		// Token: 0x06000349 RID: 841 RVA: 0x0000B20C File Offset: 0x0000940C
		public static BiQuadFilter AllPassFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.2831853071795862 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double b = 1.0 - num4;
			double b2 = -2.0 * num2;
			double b3 = 1.0 + num4;
			double num5 = 1.0 + num4;
			double num6 = -2.0 * num2;
			double num7 = 1.0 - num4;
			return new BiQuadFilter(num5, num6, num7, b, b2, b3);
		}

		/// <summary>
		/// Create a Peaking EQ
		/// </summary>
		// Token: 0x0600034A RID: 842 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public static BiQuadFilter PeakingEQ(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetPeakingEq(sampleRate, centreFrequency, q, dbGain);
			return biQuadFilter;
		}

		/// <summary>
		/// H(s) = A * (s^2 + (sqrt(A)/Q)*s + A)/(A*s^2 + (sqrt(A)/Q)*s + 1)
		/// </summary>
		/// <param name="sampleRate"></param>
		/// <param name="cutoffFrequency"></param>
		/// <param name="shelfSlope">a "shelf slope" parameter (for shelving EQ only).  
		/// When S = 1, the shelf slope is as steep as it can be and remain monotonically
		/// increasing or decreasing gain with frequency.  The shelf slope, in dB/octave, 
		/// remains proportional to S for all other values for a fixed f0/Fs and dBgain.</param>
		/// <param name="dbGain">Gain in decibels</param>
		// Token: 0x0600034B RID: 843 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public static BiQuadFilter LowShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = 6.2831853071795862 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, (double)(dbGain / 40f));
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			double b = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 + num6);
			double b2 = 2.0 * num4 * (num4 - 1.0 - (num4 + 1.0) * num2);
			double b3 = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 - num6);
			double num7 = num4 + 1.0 + (num4 - 1.0) * num2 + num6;
			double num8 = -2.0 * (num4 - 1.0 + (num4 + 1.0) * num2);
			double num9 = num4 + 1.0 + (num4 - 1.0) * num2 - num6;
			return new BiQuadFilter(num7, num8, num9, b, b2, b3);
		}

		/// <summary>
		/// H(s) = A * (A*s^2 + (sqrt(A)/Q)*s + 1)/(s^2 + (sqrt(A)/Q)*s + A)
		/// </summary>
		/// <param name="sampleRate"></param>
		/// <param name="cutoffFrequency"></param>
		/// <param name="shelfSlope"></param>
		/// <param name="dbGain"></param>
		/// <returns></returns>
		// Token: 0x0600034C RID: 844 RVA: 0x0000B430 File Offset: 0x00009630
		public static BiQuadFilter HighShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = 6.2831853071795862 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, (double)(dbGain / 40f));
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			double b = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 + num6);
			double b2 = -2.0 * num4 * (num4 - 1.0 + (num4 + 1.0) * num2);
			double b3 = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 - num6);
			double num7 = num4 + 1.0 - (num4 - 1.0) * num2 + num6;
			double num8 = 2.0 * (num4 - 1.0 - (num4 + 1.0) * num2);
			double num9 = num4 + 1.0 - (num4 - 1.0) * num2 - num6;
			return new BiQuadFilter(num7, num8, num9, b, b2, b3);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000B59C File Offset: 0x0000979C
		private BiQuadFilter()
		{
			this.x1 = (this.x2 = 0f);
			this.y1 = (this.y2 = 0f);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000B5D8 File Offset: 0x000097D8
		private BiQuadFilter(double a0, double a1, double a2, double b0, double b1, double b2)
		{
			this.SetCoefficients(a0, a1, a2, b0, b1, b2);
			this.x1 = (this.x2 = 0f);
			this.y1 = (this.y2 = 0f);
		}

		// Token: 0x04000427 RID: 1063
		private double a0;

		// Token: 0x04000428 RID: 1064
		private double a1;

		// Token: 0x04000429 RID: 1065
		private double a2;

		// Token: 0x0400042A RID: 1066
		private double a3;

		// Token: 0x0400042B RID: 1067
		private double a4;

		// Token: 0x0400042C RID: 1068
		private float x1;

		// Token: 0x0400042D RID: 1069
		private float x2;

		// Token: 0x0400042E RID: 1070
		private float y1;

		// Token: 0x0400042F RID: 1071
		private float y2;
	}
}
