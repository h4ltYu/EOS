using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Signal Generator
	/// Sin, Square, Triangle, SawTooth, White Noise, Pink Noise, Sweep.
	/// </summary>
	/// <remarks>
	/// Posibility to change ISampleProvider
	/// Example :
	/// ---------
	/// WaveOut _waveOutGene = new WaveOut();
	/// WaveGenerator wg = new SignalGenerator();
	/// wg.Type = ...
	/// wg.Frequency = ...
	/// wg ...
	/// _waveOutGene.Init(wg);
	/// _waveOutGene.Play();
	/// </remarks>
	// Token: 0x0200007B RID: 123
	public class SignalGenerator : ISampleProvider
	{
		/// <summary>
		/// Initializes a new instance for the Generator (Default :: 44.1Khz, 2 channels, Sinus, Frequency = 440, Gain = 1)
		/// </summary>
		// Token: 0x06000294 RID: 660 RVA: 0x0000895F File Offset: 0x00006B5F
		public SignalGenerator() : this(44100, 2)
		{
		}

		/// <summary>
		/// Initializes a new instance for the Generator (UserDef SampleRate &amp; Channels)
		/// </summary>
		/// <param name="sampleRate">Desired sample rate</param>
		/// <param name="channel">Number of channels</param>
		// Token: 0x06000295 RID: 661 RVA: 0x00008970 File Offset: 0x00006B70
		public SignalGenerator(int sampleRate, int channel)
		{
			this.phi = 0.0;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channel);
			this.Type = SignalGeneratorType.Sin;
			this.Frequency = 440.0;
			this.Gain = 1.0;
			this.PhaseReverse = new bool[channel];
			this.SweepLengthSecs = 2.0;
		}

		/// <summary>
		/// The waveformat of this WaveProvider (same as the source)
		/// </summary>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000089F6 File Offset: 0x00006BF6
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Frequency for the Generator. (20.0 - 20000.0 Hz)
		/// Sin, Square, Triangle, SawTooth, Sweep (Start Frequency).
		/// </summary>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000089FE File Offset: 0x00006BFE
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00008A06 File Offset: 0x00006C06
		public double Frequency { get; set; }

		/// <summary>
		/// Return Log of Frequency Start (Read only)
		/// </summary>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008A0F File Offset: 0x00006C0F
		public double FrequencyLog
		{
			get
			{
				return Math.Log(this.Frequency);
			}
		}

		/// <summary>
		/// End Frequency for the Sweep Generator. (Start Frequency in Frequency)
		/// </summary>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008A1C File Offset: 0x00006C1C
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00008A24 File Offset: 0x00006C24
		public double FrequencyEnd { get; set; }

		/// <summary>
		/// Return Log of Frequency End (Read only)
		/// </summary>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008A2D File Offset: 0x00006C2D
		public double FrequencyEndLog
		{
			get
			{
				return Math.Log(this.FrequencyEnd);
			}
		}

		/// <summary>
		/// Gain for the Generator. (0.0 to 1.0)
		/// </summary>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00008A3A File Offset: 0x00006C3A
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00008A42 File Offset: 0x00006C42
		public double Gain { get; set; }

		/// <summary>
		/// Channel PhaseReverse
		/// </summary>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008A4B File Offset: 0x00006C4B
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00008A53 File Offset: 0x00006C53
		public bool[] PhaseReverse { get; private set; }

		/// <summary>
		/// Type of Generator.
		/// </summary>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008A5C File Offset: 0x00006C5C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00008A64 File Offset: 0x00006C64
		public SignalGeneratorType Type { get; set; }

		/// <summary>
		/// Length Seconds for the Sweep Generator.
		/// </summary>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008A6D File Offset: 0x00006C6D
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008A75 File Offset: 0x00006C75
		public double SweepLengthSecs { get; set; }

		/// <summary>
		/// Reads from this provider.
		/// </summary>
		// Token: 0x060002A5 RID: 677 RVA: 0x00008A80 File Offset: 0x00006C80
		public int Read(float[] buffer, int offset, int count)
		{
			int num = offset;
			for (int i = 0; i < count / this.waveFormat.Channels; i++)
			{
				double num4;
				switch (this.Type)
				{
				case SignalGeneratorType.Pink:
				{
					double num2 = this.NextRandomTwo();
					this.pinkNoiseBuffer[0] = 0.99886 * this.pinkNoiseBuffer[0] + num2 * 0.0555179;
					this.pinkNoiseBuffer[1] = 0.99332 * this.pinkNoiseBuffer[1] + num2 * 0.0750759;
					this.pinkNoiseBuffer[2] = 0.969 * this.pinkNoiseBuffer[2] + num2 * 0.153852;
					this.pinkNoiseBuffer[3] = 0.8665 * this.pinkNoiseBuffer[3] + num2 * 0.3104856;
					this.pinkNoiseBuffer[4] = 0.55 * this.pinkNoiseBuffer[4] + num2 * 0.5329522;
					this.pinkNoiseBuffer[5] = -0.7616 * this.pinkNoiseBuffer[5] - num2 * 0.016898;
					double num3 = this.pinkNoiseBuffer[0] + this.pinkNoiseBuffer[1] + this.pinkNoiseBuffer[2] + this.pinkNoiseBuffer[3] + this.pinkNoiseBuffer[4] + this.pinkNoiseBuffer[5] + this.pinkNoiseBuffer[6] + num2 * 0.5362;
					this.pinkNoiseBuffer[6] = num2 * 0.115926;
					num4 = this.Gain * (num3 / 5.0);
					break;
				}
				case SignalGeneratorType.White:
					num4 = this.Gain * this.NextRandomTwo();
					break;
				case SignalGeneratorType.Sweep:
				{
					double num5 = Math.Exp(this.FrequencyLog + (double)this.nSample * (this.FrequencyEndLog - this.FrequencyLog) / (this.SweepLengthSecs * (double)this.waveFormat.SampleRate));
					double num6 = 6.2831853071795862 * num5 / (double)this.waveFormat.SampleRate;
					this.phi += num6;
					num4 = this.Gain * Math.Sin(this.phi);
					this.nSample++;
					if ((double)this.nSample > this.SweepLengthSecs * (double)this.waveFormat.SampleRate)
					{
						this.nSample = 0;
						this.phi = 0.0;
					}
					break;
				}
				case SignalGeneratorType.Sin:
				{
					double num6 = 6.2831853071795862 * this.Frequency / (double)this.waveFormat.SampleRate;
					num4 = this.Gain * Math.Sin((double)this.nSample * num6);
					this.nSample++;
					break;
				}
				case SignalGeneratorType.Square:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0 - 1.0;
					num4 = ((num7 > 0.0) ? this.Gain : (-this.Gain));
					this.nSample++;
					break;
				}
				case SignalGeneratorType.Triangle:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0;
					num4 = 2.0 * num7;
					if (num4 > 1.0)
					{
						num4 = 2.0 - num4;
					}
					if (num4 < -1.0)
					{
						num4 = -2.0 - num4;
					}
					num4 *= this.Gain;
					this.nSample++;
					break;
				}
				case SignalGeneratorType.SawTooth:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0 - 1.0;
					num4 = this.Gain * num7;
					this.nSample++;
					break;
				}
				default:
					num4 = 0.0;
					break;
				}
				for (int j = 0; j < this.waveFormat.Channels; j++)
				{
					if (this.PhaseReverse[j])
					{
						buffer[num++] = (float)(-(float)num4);
					}
					else
					{
						buffer[num++] = (float)num4;
					}
				}
			}
			return count;
		}

		/// <summary>
		/// Private :: Random for WhiteNoise &amp; Pink Noise (Value form -1 to 1)
		/// </summary>
		/// <returns>Random value from -1 to +1</returns>
		// Token: 0x060002A6 RID: 678 RVA: 0x00008EED File Offset: 0x000070ED
		private double NextRandomTwo()
		{
			return 2.0 * this.random.NextDouble() - 1.0;
		}

		// Token: 0x04000399 RID: 921
		private const double TwoPi = 6.2831853071795862;

		// Token: 0x0400039A RID: 922
		private readonly WaveFormat waveFormat;

		// Token: 0x0400039B RID: 923
		private readonly Random random = new Random();

		// Token: 0x0400039C RID: 924
		private readonly double[] pinkNoiseBuffer = new double[7];

		// Token: 0x0400039D RID: 925
		private int nSample;

		// Token: 0x0400039E RID: 926
		private double phi;
	}
}
