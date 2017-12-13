using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Helper class turning an already 64 bit floating point IWaveProvider
	/// into an ISampleProvider - hopefully not needed for most applications
	/// </summary>
	// Token: 0x0200007D RID: 125
	public class WaveToSampleProvider64 : SampleProviderConverterBase
	{
		/// <summary>
		/// Initializes a new instance of the WaveToSampleProvider class
		/// </summary>
		/// <param name="source">Source wave provider, must be IEEE float</param>
		// Token: 0x060002A7 RID: 679 RVA: 0x00008F0E File Offset: 0x0000710E
		public WaveToSampleProvider64(IWaveProvider source) : base(source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
		}

		/// <summary>
		/// Reads from this provider
		/// </summary>
		// Token: 0x060002A8 RID: 680 RVA: 0x00008F30 File Offset: 0x00007130
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 8;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int result = num2 / 8;
			int num3 = offset;
			for (int i = 0; i < num2; i += 8)
			{
				long value = BitConverter.ToInt64(this.sourceBuffer, i);
				buffer[num3++] = (float)BitConverter.Int64BitsToDouble(value);
			}
			return result;
		}
	}
}
