using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Helper class turning an already 32 bit floating point IWaveProvider
	/// into an ISampleProvider - hopefully not needed for most applications
	/// </summary>
	// Token: 0x020001E3 RID: 483
	public class WaveToSampleProvider : SampleProviderConverterBase
	{
		/// <summary>
		/// Initializes a new instance of the WaveToSampleProvider class
		/// </summary>
		/// <param name="source">Source wave provider, must be IEEE float</param>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001F374 File Offset: 0x0001D574
		public WaveToSampleProvider(IWaveProvider source) : base(source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
		}

		/// <summary>
		/// Reads from this provider
		/// </summary>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001F398 File Offset: 0x0001D598
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 4;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int result = num2 / 4;
			int num3 = offset;
			for (int i = 0; i < num2; i += 4)
			{
				buffer[num3++] = BitConverter.ToSingle(this.sourceBuffer, i);
			}
			return result;
		}
	}
}
