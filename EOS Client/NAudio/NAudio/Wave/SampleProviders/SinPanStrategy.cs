using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Sinus Pan, thanks to Yuval Naveh
	/// </summary>
	// Token: 0x0200019F RID: 415
	public class SinPanStrategy : IPanStrategy
	{
		/// <summary>
		/// Gets the left and right channel multipliers for this pan value
		/// </summary>
		/// <param name="pan">Pan value, between -1 and 1</param>
		/// <returns>Left and right multipliers</returns>
		// Token: 0x06000880 RID: 2176 RVA: 0x000187D4 File Offset: 0x000169D4
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float left = (float)Math.Sin((double)(num * 1.57079637f));
			float right = (float)Math.Cos((double)(num * 1.57079637f));
			return new StereoSamplePair
			{
				Left = left,
				Right = right
			};
		}

		// Token: 0x040009B4 RID: 2484
		private const float HalfPi = 1.57079637f;
	}
}
