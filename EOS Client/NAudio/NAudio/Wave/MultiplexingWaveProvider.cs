using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Allows any number of inputs to be patched to outputs
	/// Uses could include swapping left and right channels, turning mono into stereo,
	/// feeding different input sources to different soundcard outputs etc
	/// </summary>
	// Token: 0x020001DA RID: 474
	public class MultiplexingWaveProvider : IWaveProvider
	{
		/// <summary>
		/// Creates a multiplexing wave provider, allowing re-patching of input channels to different
		/// output channels
		/// </summary>
		/// <param name="inputs">Input wave providers. Must all be of the same format, but can have any number of channels</param>
		/// <param name="numberOfOutputChannels">Desired number of output channels.</param>
		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E824 File Offset: 0x0001CA24
		public MultiplexingWaveProvider(IEnumerable<IWaveProvider> inputs, int numberOfOutputChannels)
		{
			this.inputs = new List<IWaveProvider>(inputs);
			this.outputChannelCount = numberOfOutputChannels;
			if (this.inputs.Count == 0)
			{
				throw new ArgumentException("You must provide at least one input");
			}
			if (numberOfOutputChannels < 1)
			{
				throw new ArgumentException("You must provide at least one output");
			}
			foreach (IWaveProvider waveProvider in this.inputs)
			{
				if (this.waveFormat == null)
				{
					if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
					{
						this.waveFormat = new WaveFormat(waveProvider.WaveFormat.SampleRate, waveProvider.WaveFormat.BitsPerSample, numberOfOutputChannels);
					}
					else
					{
						if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
						{
							throw new ArgumentException("Only PCM and 32 bit float are supported");
						}
						this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(waveProvider.WaveFormat.SampleRate, numberOfOutputChannels);
					}
				}
				else
				{
					if (waveProvider.WaveFormat.BitsPerSample != this.waveFormat.BitsPerSample)
					{
						throw new ArgumentException("All inputs must have the same bit depth");
					}
					if (waveProvider.WaveFormat.SampleRate != this.waveFormat.SampleRate)
					{
						throw new ArgumentException("All inputs must have the same sample rate");
					}
				}
				this.inputChannelCount += waveProvider.WaveFormat.Channels;
			}
			this.bytesPerSample = this.waveFormat.BitsPerSample / 8;
			this.mappings = new List<int>();
			for (int i = 0; i < this.outputChannelCount; i++)
			{
				this.mappings.Add(i % this.inputChannelCount);
			}
		}

		/// <summary>
		/// Reads data from this WaveProvider
		/// </summary>
		/// <param name="buffer">Buffer to be filled with sample data</param>
		/// <param name="offset">Offset to write to within buffer, usually 0</param>
		/// <param name="count">Number of bytes required</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.bytesPerSample * this.outputChannelCount;
			int num2 = count / num;
			int num3 = 0;
			int num4 = 0;
			foreach (IWaveProvider waveProvider in this.inputs)
			{
				int num5 = this.bytesPerSample * waveProvider.WaveFormat.Channels;
				int num6 = num2 * num5;
				this.inputBuffer = BufferHelpers.Ensure(this.inputBuffer, num6);
				int num7 = waveProvider.Read(this.inputBuffer, 0, num6);
				num4 = Math.Max(num4, num7 / num5);
				for (int i = 0; i < waveProvider.WaveFormat.Channels; i++)
				{
					int num8 = num3 + i;
					for (int j = 0; j < this.outputChannelCount; j++)
					{
						if (this.mappings[j] == num8)
						{
							int num9 = i * this.bytesPerSample;
							int num10 = offset + j * this.bytesPerSample;
							int k;
							for (k = 0; k < num2; k++)
							{
								if (num9 >= num7)
								{
									break;
								}
								Array.Copy(this.inputBuffer, num9, buffer, num10, this.bytesPerSample);
								num10 += num;
								num9 += num5;
							}
							while (k < num2)
							{
								Array.Clear(buffer, num10, this.bytesPerSample);
								num10 += num;
								k++;
							}
						}
					}
				}
				num3 += waveProvider.WaveFormat.Channels;
			}
			return num4 * num;
		}

		/// <summary>
		/// The WaveFormat of this WaveProvider
		/// </summary>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001EB54 File Offset: 0x0001CD54
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Connects a specified input channel to an output channel
		/// </summary>
		/// <param name="inputChannel">Input Channel index (zero based). Must be less than InputChannelCount</param>
		/// <param name="outputChannel">Output Channel index (zero based). Must be less than OutputChannelCount</param>
		// Token: 0x06000A73 RID: 2675 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		public void ConnectInputToOutput(int inputChannel, int outputChannel)
		{
			if (inputChannel < 0 || inputChannel >= this.InputChannelCount)
			{
				throw new ArgumentException("Invalid input channel");
			}
			if (outputChannel < 0 || outputChannel >= this.OutputChannelCount)
			{
				throw new ArgumentException("Invalid output channel");
			}
			this.mappings[outputChannel] = inputChannel;
		}

		/// <summary>
		/// The number of input channels. Note that this is not the same as the number of input wave providers. If you pass in
		/// one stereo and one mono input provider, the number of input channels is three.
		/// </summary>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001EB9B File Offset: 0x0001CD9B
		public int InputChannelCount
		{
			get
			{
				return this.inputChannelCount;
			}
		}

		/// <summary>
		/// The number of output channels, as specified in the constructor.
		/// </summary>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001EBA3 File Offset: 0x0001CDA3
		public int OutputChannelCount
		{
			get
			{
				return this.outputChannelCount;
			}
		}

		// Token: 0x04000B5F RID: 2911
		private readonly IList<IWaveProvider> inputs;

		// Token: 0x04000B60 RID: 2912
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B61 RID: 2913
		private readonly int outputChannelCount;

		// Token: 0x04000B62 RID: 2914
		private readonly int inputChannelCount;

		// Token: 0x04000B63 RID: 2915
		private readonly List<int> mappings;

		// Token: 0x04000B64 RID: 2916
		private readonly int bytesPerSample;

		/// <summary>
		/// persistent temporary buffer to prevent creating work for garbage collector
		/// </summary>
		// Token: 0x04000B65 RID: 2917
		private byte[] inputBuffer;
	}
}
