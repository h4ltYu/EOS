using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Required Interface for a Panning Strategy
	/// </summary>
	// Token: 0x0200019C RID: 412
	public interface IPanStrategy
	{
		/// <summary>
		/// Gets the left and right multipliers for a given pan value
		/// </summary>
		/// <param name="pan">Pan value from -1 to 1</param>
		/// <returns>Left and right multipliers in a stereo sample pair</returns>
		// Token: 0x0600087B RID: 2171
		StereoSamplePair GetMultipliers(float pan);
	}
}
