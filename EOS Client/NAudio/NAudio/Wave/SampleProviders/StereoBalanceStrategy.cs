using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Simplistic "balance" control - treating the mono input as if it was stereo
	/// In the centre, both channels full volume. Opposite channel decays linearly 
	/// as balance is turned to to one side
	/// </summary>
	// Token: 0x0200019D RID: 413
	public class StereoBalanceStrategy : IPanStrategy
	{
		/// <summary>
		/// Gets the left and right channel multipliers for this pan value
		/// </summary>
		/// <param name="pan">Pan value, between -1 and 1</param>
		/// <returns>Left and right multipliers</returns>
		// Token: 0x0600087C RID: 2172 RVA: 0x00018714 File Offset: 0x00016914
		public StereoSamplePair GetMultipliers(float pan)
		{
			float left = (pan <= 0f) ? 1f : ((1f - pan) / 2f);
			float right = (pan >= 0f) ? 1f : ((pan + 1f) / 2f);
			return new StereoSamplePair
			{
				Left = left,
				Right = right
			};
		}
	}
}
