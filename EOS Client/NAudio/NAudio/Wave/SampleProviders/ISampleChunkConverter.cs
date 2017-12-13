using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Sample provider interface to make WaveChannel32 extensible
	/// Still a bit ugly, hence internal at the moment - and might even make these into
	/// bit depth converting WaveProviders
	/// </summary>
	// Token: 0x02000190 RID: 400
	internal interface ISampleChunkConverter
	{
		// Token: 0x06000842 RID: 2114
		bool Supports(WaveFormat format);

		// Token: 0x06000843 RID: 2115
		void LoadNextChunk(IWaveProvider sourceProvider, int samplePairsRequired);

		// Token: 0x06000844 RID: 2116
		bool GetNextSample(out float sampleLeft, out float sampleRight);
	}
}
