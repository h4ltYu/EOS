using System;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
	/// <summary>
	/// Raised when ASIO data has been recorded.
	/// It is important to handle this as quickly as possible as it is in the buffer callback
	/// </summary>
	// Token: 0x020001B0 RID: 432
	public class AsioAudioAvailableEventArgs : EventArgs
	{
		/// <summary>
		/// Initialises a new instance of AsioAudioAvailableEventArgs
		/// </summary>
		/// <param name="inputBuffers">Pointers to the ASIO buffers for each channel</param>
		/// <param name="outputBuffers">Pointers to the ASIO buffers for each channel</param>
		/// <param name="samplesPerBuffer">Number of samples in each buffer</param>
		/// <param name="asioSampleType">Audio format within each buffer</param>
		// Token: 0x060008DC RID: 2268 RVA: 0x00019A84 File Offset: 0x00017C84
		public AsioAudioAvailableEventArgs(IntPtr[] inputBuffers, IntPtr[] outputBuffers, int samplesPerBuffer, AsioSampleType asioSampleType)
		{
			this.InputBuffers = inputBuffers;
			this.OutputBuffers = outputBuffers;
			this.SamplesPerBuffer = samplesPerBuffer;
			this.AsioSampleType = asioSampleType;
		}

		/// <summary>
		/// Pointer to a buffer per input channel
		/// </summary>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00019AA9 File Offset: 0x00017CA9
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00019AB1 File Offset: 0x00017CB1
		public IntPtr[] InputBuffers { get; private set; }

		/// <summary>
		/// Pointer to a buffer per output channel
		/// Allows you to write directly to the output buffers
		/// If you do so, set SamplesPerBuffer = true,
		/// and make sure all buffers are written to with valid data
		/// </summary>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00019ABA File Offset: 0x00017CBA
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00019AC2 File Offset: 0x00017CC2
		public IntPtr[] OutputBuffers { get; private set; }

		/// <summary>
		/// Set to true if you have written to the output buffers
		/// If so, AsioOut will not read from its source
		/// </summary>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00019ACB File Offset: 0x00017CCB
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00019AD3 File Offset: 0x00017CD3
		public bool WrittenToOutputBuffers { get; set; }

		/// <summary>
		/// Number of samples in each buffer
		/// </summary>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00019ADC File Offset: 0x00017CDC
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00019AE4 File Offset: 0x00017CE4
		public int SamplesPerBuffer { get; private set; }

		/// <summary>
		/// Converts all the recorded audio into a buffer of 32 bit floating point samples, interleaved by channel
		/// </summary>
		/// <samples>The samples as 32 bit floating point, interleaved</samples>
		// Token: 0x060008E5 RID: 2277 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public unsafe int GetAsInterleavedSamples(float[] samples)
		{
			int num = this.InputBuffers.Length;
			if (samples.Length < this.SamplesPerBuffer * num)
			{
				throw new ArgumentException("Buffer not big enough");
			}
			int num2 = 0;
			if (this.AsioSampleType == AsioSampleType.Int32LSB)
			{
				for (int i = 0; i < this.SamplesPerBuffer; i++)
				{
					for (int j = 0; j < num; j++)
					{
						samples[num2++] = (float)(*(int*)((byte*)((void*)this.InputBuffers[j]) + (IntPtr)i * 4)) / 2.14748365E+09f;
					}
				}
			}
			else if (this.AsioSampleType == AsioSampleType.Int16LSB)
			{
				for (int k = 0; k < this.SamplesPerBuffer; k++)
				{
					for (int l = 0; l < num; l++)
					{
						samples[num2++] = (float)(*(short*)((byte*)((void*)this.InputBuffers[l]) + (IntPtr)k * 2)) / 32767f;
					}
				}
			}
			else if (this.AsioSampleType == AsioSampleType.Int24LSB)
			{
				for (int m = 0; m < this.SamplesPerBuffer; m++)
				{
					for (int n = 0; n < num; n++)
					{
						byte* ptr = (byte*)((void*)this.InputBuffers[n]) + (IntPtr)m * 3;
						int num3 = (int)(*ptr) | (int)ptr[1] << 8 | (int)((sbyte)ptr[2]) << 16;
						samples[num2++] = (float)num3 / 8388608f;
					}
				}
			}
			else
			{
				if (this.AsioSampleType != AsioSampleType.Float32LSB)
				{
					throw new NotImplementedException(string.Format("ASIO Sample Type {0} not supported", this.AsioSampleType));
				}
				for (int num4 = 0; num4 < this.SamplesPerBuffer; num4++)
				{
					for (int num5 = 0; num5 < num; num5++)
					{
						samples[num2++] = *(float*)((byte*)((void*)this.InputBuffers[num5]) + (IntPtr)num4 * 4);
					}
				}
			}
			return this.SamplesPerBuffer * num;
		}

		/// <summary>
		/// Audio format within each buffer
		/// Most commonly this will be one of, Int32LSB, Int16LSB, Int24LSB or Float32LSB
		/// </summary>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00019CC8 File Offset: 0x00017EC8
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00019CD0 File Offset: 0x00017ED0
		public AsioSampleType AsioSampleType { get; private set; }

		/// <summary>
		/// Gets as interleaved samples, allocating a float array
		/// </summary>
		/// <returns>The samples as 32 bit floating point values</returns>
		// Token: 0x060008E8 RID: 2280 RVA: 0x00019CDC File Offset: 0x00017EDC
		[Obsolete("Better performance if you use the overload that takes an array, and reuse the same one")]
		public float[] GetAsInterleavedSamples()
		{
			int num = this.InputBuffers.Length;
			float[] array = new float[this.SamplesPerBuffer * num];
			this.GetAsInterleavedSamples(array);
			return array;
		}
	}
}
