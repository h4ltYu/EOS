using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Base class for creating a 32 bit floating point wave provider
	/// Can also be used as a base class for an ISampleProvider that can 
	/// be plugged straight into anything requiring an IWaveProvider
	/// </summary>
	// Token: 0x020001E2 RID: 482
	public abstract class WaveProvider32 : IWaveProvider, ISampleProvider
	{
		/// <summary>
		/// Initializes a new instance of the WaveProvider32 class 
		/// defaulting to 44.1kHz mono
		/// </summary>
		// Token: 0x06000A9D RID: 2717 RVA: 0x0001F310 File Offset: 0x0001D510
		public WaveProvider32() : this(44100, 1)
		{
		}

		/// <summary>
		/// Initializes a new instance of the WaveProvider32 class with the specified
		/// sample rate and number of channels
		/// </summary>
		// Token: 0x06000A9E RID: 2718 RVA: 0x0001F31E File Offset: 0x0001D51E
		public WaveProvider32(int sampleRate, int channels)
		{
			this.SetWaveFormat(sampleRate, channels);
		}

		/// <summary>
		/// Allows you to specify the sample rate and channels for this WaveProvider
		/// (should be initialised before you pass it to a wave player)
		/// </summary>
		// Token: 0x06000A9F RID: 2719 RVA: 0x0001F32E File Offset: 0x0001D52E
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}

		/// <summary>
		/// Implements the Read method of IWaveProvider by delegating to the abstract
		/// Read method taking a float array
		/// </summary>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001F340 File Offset: 0x0001D540
		public int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int sampleCount = count / 4;
			int num = this.Read(waveBuffer.FloatBuffer, offset / 4, sampleCount);
			return num * 4;
		}

		/// <summary>
		/// Method to override in derived classes
		/// Supply the requested number of samples into the buffer
		/// </summary>
		// Token: 0x06000AA1 RID: 2721
		public abstract int Read(float[] buffer, int offset, int sampleCount);

		/// <summary>
		/// The Wave Format
		/// </summary>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001F36C File Offset: 0x0001D56C
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B7D RID: 2941
		private WaveFormat waveFormat;
	}
}
