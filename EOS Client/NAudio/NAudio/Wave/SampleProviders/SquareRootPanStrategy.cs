using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Square Root Pan, thanks to Yuval Naveh
	/// </summary>
	// Token: 0x0200019E RID: 414
	public class SquareRootPanStrategy : IPanStrategy
	{
		/// <summary>
		/// Gets the left and right channel multipliers for this pan value
		/// </summary>
		/// <param name="pan">Pan value, between -1 and 1</param>
		/// <returns>Left and right multipliers</returns>
		// Token: 0x0600087E RID: 2174 RVA: 0x0001877C File Offset: 0x0001697C
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float left = (float)Math.Sqrt((double)num);
			float right = (float)Math.Sqrt((double)(1f - num));
			return new StereoSamplePair
			{
				Left = left,
				Right = right
			};
		}
	}
}
