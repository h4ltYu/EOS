using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Linear Pan
	/// </summary>
	// Token: 0x020001A0 RID: 416
	public class LinearPanStrategy : IPanStrategy
	{
		/// <summary>
		/// Gets the left and right channel multipliers for this pan value
		/// </summary>
		/// <param name="pan">Pan value, between -1 and 1</param>
		/// <returns>Left and right multipliers</returns>
		// Token: 0x06000882 RID: 2178 RVA: 0x00018830 File Offset: 0x00016A30
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float left = num;
			float right = 1f - num;
			return new StereoSamplePair
			{
				Left = left,
				Right = right
			};
		}
	}
}
