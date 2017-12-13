using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// ASIODriverCapability holds all the information from the ASIODriver.
	/// Use ASIODriverExt to get the Capabilities
	/// </summary>
	// Token: 0x0200006C RID: 108
	internal class AsioDriverCapability
	{
		// Token: 0x0400034C RID: 844
		public string DriverName;

		// Token: 0x0400034D RID: 845
		public int NbInputChannels;

		// Token: 0x0400034E RID: 846
		public int NbOutputChannels;

		// Token: 0x0400034F RID: 847
		public int InputLatency;

		// Token: 0x04000350 RID: 848
		public int OutputLatency;

		// Token: 0x04000351 RID: 849
		public int BufferMinSize;

		// Token: 0x04000352 RID: 850
		public int BufferMaxSize;

		// Token: 0x04000353 RID: 851
		public int BufferPreferredSize;

		// Token: 0x04000354 RID: 852
		public int BufferGranularity;

		// Token: 0x04000355 RID: 853
		public double SampleRate;

		// Token: 0x04000356 RID: 854
		public ASIOChannelInfo[] InputChannelInfos;

		// Token: 0x04000357 RID: 855
		public ASIOChannelInfo[] OutputChannelInfos;
	}
}
