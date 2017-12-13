using System;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// A sample provider mixer, allowing inputs to be added and removed
	/// </summary>
	// Token: 0x02000199 RID: 409
	public class MixingSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Creates a new MixingSampleProvider, with no inputs, but a specified WaveFormat
		/// </summary>
		/// <param name="waveFormat">The WaveFormat of this mixer. All inputs must be in this format</param>
		// Token: 0x06000865 RID: 2149 RVA: 0x0001828A File Offset: 0x0001648A
		public MixingSampleProvider(WaveFormat waveFormat)
		{
			if (waveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Mixer wave format must be IEEE float");
			}
			this.sources = new List<ISampleProvider>();
			this.waveFormat = waveFormat;
		}

		/// <summary>
		/// Creates a new MixingSampleProvider, based on the given inputs
		/// </summary>
		/// <param name="sources">Mixer inputs - must all have the same waveformat, and must
		/// all be of the same WaveFormat. There must be at least one input</param>
		// Token: 0x06000866 RID: 2150 RVA: 0x000182B8 File Offset: 0x000164B8
		public MixingSampleProvider(IEnumerable<ISampleProvider> sources)
		{
			this.sources = new List<ISampleProvider>();
			foreach (ISampleProvider mixerInput in sources)
			{
				this.AddMixerInput(mixerInput);
			}
			if (this.sources.Count == 0)
			{
				throw new ArgumentException("Must provide at least one input in this constructor");
			}
		}

		/// <summary>
		/// When set to true, the Read method always returns the number
		/// of samples requested, even if there are no inputs, or if the
		/// current inputs reach their end. Setting this to true effectively
		/// makes this a never-ending sample provider, so take care if you plan
		/// to write it out to a file.
		/// </summary>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001832C File Offset: 0x0001652C
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00018334 File Offset: 0x00016534
		public bool ReadFully { get; set; }

		/// <summary>
		/// Adds a WaveProvider as a Mixer input.
		/// Must be PCM or IEEE float already
		/// </summary>
		/// <param name="mixerInput">IWaveProvider mixer input</param>
		// Token: 0x06000869 RID: 2153 RVA: 0x0001833D File Offset: 0x0001653D
		public void AddMixerInput(IWaveProvider mixerInput)
		{
			this.AddMixerInput(SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(mixerInput));
		}

		/// <summary>
		/// Adds a new mixer input
		/// </summary>
		/// <param name="mixerInput">Mixer input</param>
		// Token: 0x0600086A RID: 2154 RVA: 0x0001834C File Offset: 0x0001654C
		public void AddMixerInput(ISampleProvider mixerInput)
		{
			lock (this.sources)
			{
				if (this.sources.Count >= 1024)
				{
					throw new InvalidOperationException("Too many mixer inputs");
				}
				this.sources.Add(mixerInput);
			}
			if (this.waveFormat == null)
			{
				this.waveFormat = mixerInput.WaveFormat;
				return;
			}
			if (this.WaveFormat.SampleRate != mixerInput.WaveFormat.SampleRate || this.WaveFormat.Channels != mixerInput.WaveFormat.Channels)
			{
				throw new ArgumentException("All mixer inputs must have the same WaveFormat");
			}
		}

		/// <summary>
		/// Removes a mixer input
		/// </summary>
		/// <param name="mixerInput">Mixer input to remove</param>
		// Token: 0x0600086B RID: 2155 RVA: 0x000183F8 File Offset: 0x000165F8
		public void RemoveMixerInput(ISampleProvider mixerInput)
		{
			lock (this.sources)
			{
				this.sources.Remove(mixerInput);
			}
		}

		/// <summary>
		/// Removes all mixer inputs
		/// </summary>
		// Token: 0x0600086C RID: 2156 RVA: 0x00018438 File Offset: 0x00016638
		public void RemoveAllMixerInputs()
		{
			lock (this.sources)
			{
				this.sources.Clear();
			}
		}

		/// <summary>
		/// The output WaveFormat of this sample provider
		/// </summary>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00018478 File Offset: 0x00016678
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x0600086E RID: 2158 RVA: 0x00018480 File Offset: 0x00016680
		public int Read(float[] buffer, int offset, int count)
		{
			int num = 0;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, count);
			lock (this.sources)
			{
				for (int i = this.sources.Count - 1; i >= 0; i--)
				{
					ISampleProvider sampleProvider = this.sources[i];
					int num2 = sampleProvider.Read(this.sourceBuffer, 0, count);
					int num3 = offset;
					for (int j = 0; j < num2; j++)
					{
						if (j >= num)
						{
							buffer[num3++] = this.sourceBuffer[j];
						}
						else
						{
							buffer[num3++] += this.sourceBuffer[j];
						}
					}
					num = Math.Max(num2, num);
					if (num2 == 0)
					{
						this.sources.RemoveAt(i);
					}
				}
			}
			if (this.ReadFully && num < count)
			{
				int k = offset + num;
				while (k < offset + count)
				{
					buffer[k++] = 0f;
				}
				num = count;
			}
			return num;
		}

		// Token: 0x040009A6 RID: 2470
		private const int maxInputs = 1024;

		// Token: 0x040009A7 RID: 2471
		private List<ISampleProvider> sources;

		// Token: 0x040009A8 RID: 2472
		private WaveFormat waveFormat;

		// Token: 0x040009A9 RID: 2473
		private float[] sourceBuffer;
	}
}
