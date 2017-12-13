using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Base class for creating a 16 bit wave provider
	/// </summary>
	// Token: 0x020001E1 RID: 481
	public abstract class WaveProvider16 : IWaveProvider
	{
		/// <summary>
		/// Initializes a new instance of the WaveProvider16 class 
		/// defaulting to 44.1kHz mono
		/// </summary>
		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F2AA File Offset: 0x0001D4AA
		public WaveProvider16() : this(44100, 1)
		{
		}

		/// <summary>
		/// Initializes a new instance of the WaveProvider16 class with the specified
		/// sample rate and number of channels
		/// </summary>
		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public WaveProvider16(int sampleRate, int channels)
		{
			this.SetWaveFormat(sampleRate, channels);
		}

		/// <summary>
		/// Allows you to specify the sample rate and channels for this WaveProvider
		/// (should be initialised before you pass it to a wave player)
		/// </summary>
		// Token: 0x06000A99 RID: 2713 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = new WaveFormat(sampleRate, 16, channels);
		}

		/// <summary>
		/// Implements the Read method of IWaveProvider by delegating to the abstract
		/// Read method taking a short array
		/// </summary>
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001F2DC File Offset: 0x0001D4DC
		public int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int sampleCount = count / 2;
			int num = this.Read(waveBuffer.ShortBuffer, offset / 2, sampleCount);
			return num * 2;
		}

		/// <summary>
		/// Method to override in derived classes
		/// Supply the requested number of samples into the buffer
		/// </summary>
		// Token: 0x06000A9B RID: 2715
		public abstract int Read(short[] buffer, int offset, int sampleCount);

		/// <summary>
		/// The Wave Format
		/// </summary>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001F308 File Offset: 0x0001D508
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B7C RID: 2940
		private WaveFormat waveFormat;
	}
}
