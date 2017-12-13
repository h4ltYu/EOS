using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts a mono sample provider to stereo, with a customisable pan strategy
	/// </summary>
	// Token: 0x0200019A RID: 410
	public class PanningSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Initialises a new instance of the PanningSampleProvider
		/// </summary>
		/// <param name="source">Source sample provider, must be mono</param>
		// Token: 0x0600086F RID: 2159 RVA: 0x00018594 File Offset: 0x00016794
		public PanningSampleProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source sample provider must be mono");
			}
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
			this.panStrategy = new SinPanStrategy();
		}

		/// <summary>
		/// Pan value, must be between -1 (left) and 1 (right)
		/// </summary>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x000185E9 File Offset: 0x000167E9
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x000185F1 File Offset: 0x000167F1
		public float Pan
		{
			get
			{
				return this.pan;
			}
			set
			{
				if (value < -1f || value > 1f)
				{
					throw new ArgumentOutOfRangeException("value", "Pan must be in the range -1 to 1");
				}
				this.pan = value;
				this.UpdateMultipliers();
			}
		}

		/// <summary>
		/// The pan strategy currently in use
		/// </summary>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00018620 File Offset: 0x00016820
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00018628 File Offset: 0x00016828
		public IPanStrategy PanStrategy
		{
			get
			{
				return this.panStrategy;
			}
			set
			{
				this.panStrategy = value;
				this.UpdateMultipliers();
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00018638 File Offset: 0x00016838
		private void UpdateMultipliers()
		{
			StereoSamplePair multipliers = this.panStrategy.GetMultipliers(this.Pan);
			this.leftMultiplier = multipliers.Left;
			this.rightMultiplier = multipliers.Right;
		}

		/// <summary>
		/// The WaveFormat of this sample provider
		/// </summary>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00018671 File Offset: 0x00016871
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
		/// <param name="count">Number of samples desired</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000876 RID: 2166 RVA: 0x0001867C File Offset: 0x0001687C
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i++)
			{
				buffer[num3++] = this.leftMultiplier * this.sourceBuffer[i];
				buffer[num3++] = this.rightMultiplier * this.sourceBuffer[i];
			}
			return num2 * 2;
		}

		// Token: 0x040009AB RID: 2475
		private readonly ISampleProvider source;

		// Token: 0x040009AC RID: 2476
		private float pan;

		// Token: 0x040009AD RID: 2477
		private float leftMultiplier;

		// Token: 0x040009AE RID: 2478
		private float rightMultiplier;

		// Token: 0x040009AF RID: 2479
		private readonly WaveFormat waveFormat;

		// Token: 0x040009B0 RID: 2480
		private float[] sourceBuffer;

		// Token: 0x040009B1 RID: 2481
		private IPanStrategy panStrategy;
	}
}
