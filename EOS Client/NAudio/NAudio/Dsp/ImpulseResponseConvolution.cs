using System;

namespace NAudio.Dsp
{
	/// <summary>
	/// Summary description for ImpulseResponseConvolution.
	/// </summary>
	// Token: 0x0200009D RID: 157
	public class ImpulseResponseConvolution
	{
		/// <summary>
		/// A very simple mono convolution algorithm
		/// </summary>
		/// <remarks>
		/// This will be very slow
		/// </remarks>
		// Token: 0x06000363 RID: 867 RVA: 0x0000BAA8 File Offset: 0x00009CA8
		public float[] Convolve(float[] input, float[] impulseResponse)
		{
			float[] array = new float[input.Length + impulseResponse.Length];
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < impulseResponse.Length; j++)
				{
					if (i >= j && i - j < input.Length)
					{
						array[i] += impulseResponse[j] * input[i - j];
					}
				}
			}
			this.Normalize(array);
			return array;
		}

		/// <summary>
		/// This is actually a downwards normalize for data that will clip
		/// </summary>
		// Token: 0x06000364 RID: 868 RVA: 0x0000BB10 File Offset: 0x00009D10
		public void Normalize(float[] data)
		{
			float num = 0f;
			for (int i = 0; i < data.Length; i++)
			{
				num = Math.Max(num, Math.Abs(data[i]));
			}
			if ((double)num > 1.0)
			{
				for (int j = 0; j < data.Length; j++)
				{
					data[j] /= num;
				}
			}
		}
	}
}
