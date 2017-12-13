using System;
using System.Collections.Generic;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveProvider that can mix together multiple 32 bit floating point input provider
	/// All channels must have the same number of inputs and same sample rate
	/// n.b. Work in Progress - not tested yet
	/// </summary>
	// Token: 0x020001D9 RID: 473
	public class MixingWaveProvider32 : IWaveProvider
	{
		/// <summary>
		/// Creates a new MixingWaveProvider32
		/// </summary>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0001E56A File Offset: 0x0001C76A
		public MixingWaveProvider32()
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
			this.bytesPerSample = 4;
			this.inputs = new List<IWaveProvider>();
		}

		/// <summary>
		/// Creates a new 32 bit MixingWaveProvider32
		/// </summary>
		/// <param name="inputs">inputs - must all have the same format.</param>
		/// <exception cref="T:System.ArgumentException">Thrown if the input streams are not 32 bit floating point,
		/// or if they have different formats to each other</exception>
		// Token: 0x06000A69 RID: 2665 RVA: 0x0001E598 File Offset: 0x0001C798
		public MixingWaveProvider32(IEnumerable<IWaveProvider> inputs) : this()
		{
			foreach (IWaveProvider waveProvider in inputs)
			{
				this.AddInputStream(waveProvider);
			}
		}

		/// <summary>
		/// Add a new input to the mixer
		/// </summary>
		/// <param name="waveProvider">The wave input to add</param>
		// Token: 0x06000A6A RID: 2666 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		public void AddInputStream(IWaveProvider waveProvider)
		{
			if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be IEEE floating point", "waveProvider.WaveFormat");
			}
			if (waveProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Only 32 bit audio currently supported", "waveProvider.WaveFormat");
			}
			if (this.inputs.Count == 0)
			{
				int sampleRate = waveProvider.WaveFormat.SampleRate;
				int channels = waveProvider.WaveFormat.Channels;
				this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			}
			else if (!waveProvider.WaveFormat.Equals(this.waveFormat))
			{
				throw new ArgumentException("All incoming channels must have the same format", "waveProvider.WaveFormat");
			}
			lock (this.inputs)
			{
				this.inputs.Add(waveProvider);
			}
		}

		/// <summary>
		/// Remove an input from the mixer
		/// </summary>
		/// <param name="waveProvider">waveProvider to remove</param>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E6BC File Offset: 0x0001C8BC
		public void RemoveInputStream(IWaveProvider waveProvider)
		{
			lock (this.inputs)
			{
				this.inputs.Remove(waveProvider);
			}
		}

		/// <summary>
		/// The number of inputs to this mixer
		/// </summary>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public int InputCount
		{
			get
			{
				return this.inputs.Count;
			}
		}

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="buffer">buffer to read into</param>
		/// <param name="offset">offset into buffer</param>
		/// <param name="count">number of bytes required</param>
		/// <returns>Number of bytes read.</returns>
		/// <exception cref="T:System.ArgumentException">Thrown if an invalid number of bytes requested</exception>
		// Token: 0x06000A6D RID: 2669 RVA: 0x0001E70C File Offset: 0x0001C90C
		public int Read(byte[] buffer, int offset, int count)
		{
			if (count % this.bytesPerSample != 0)
			{
				throw new ArgumentException("Must read an whole number of samples", "count");
			}
			Array.Clear(buffer, offset, count);
			int num = 0;
			byte[] array = new byte[count];
			lock (this.inputs)
			{
				foreach (IWaveProvider waveProvider in this.inputs)
				{
					int num2 = waveProvider.Read(array, 0, count);
					num = Math.Max(num, num2);
					if (num2 > 0)
					{
						MixingWaveProvider32.Sum32BitAudio(buffer, offset, array, num2);
					}
				}
			}
			return num;
		}

		/// <summary>
		/// Actually performs the mixing
		/// </summary>
		// Token: 0x06000A6E RID: 2670 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		private unsafe static void Sum32BitAudio(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
			{
				float* ptr3 = (float*)ptr;
				float* ptr4 = (float*)ptr2;
				int num = bytesRead / 4;
				for (int i = 0; i < num; i++)
				{
					ptr3[i] += ptr4[i];
				}
			}
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001E81A File Offset: 0x0001CA1A
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B5C RID: 2908
		private List<IWaveProvider> inputs;

		// Token: 0x04000B5D RID: 2909
		private WaveFormat waveFormat;

		// Token: 0x04000B5E RID: 2910
		private int bytesPerSample;
	}
}
