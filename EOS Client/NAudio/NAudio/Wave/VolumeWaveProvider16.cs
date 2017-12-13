using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Helper class allowing us to modify the volume of a 16 bit stream without converting to IEEE float
	/// </summary>
	// Token: 0x020001DD RID: 477
	public class VolumeWaveProvider16 : IWaveProvider
	{
		/// <summary>
		/// Constructs a new VolumeWaveProvider16
		/// </summary>
		/// <param name="sourceProvider">Source provider, must be 16 bit PCM</param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001EE84 File Offset: 0x0001D084
		public VolumeWaveProvider16(IWaveProvider sourceProvider)
		{
			this.Volume = 1f;
			this.sourceProvider = sourceProvider;
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Expecting PCM input");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Expecting 16 bit");
			}
		}

		/// <summary>
		/// Gets or sets volume. 
		/// 1.0 is full scale, 0.0 is silence, anything over 1.0 will amplify but potentially clip
		/// </summary>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0001EEDC File Offset: 0x0001D0DC
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		/// <summary>
		/// WaveFormat of this WaveProvider
		/// </summary>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001EEED File Offset: 0x0001D0ED
		public WaveFormat WaveFormat
		{
			get
			{
				return this.sourceProvider.WaveFormat;
			}
		}

		/// <summary>
		/// Read bytes from this WaveProvider
		/// </summary>
		/// <param name="buffer">Buffer to read into</param>
		/// <param name="offset">Offset within buffer to read to</param>
		/// <param name="count">Bytes desired</param>
		/// <returns>Bytes read</returns>
		// Token: 0x06000A88 RID: 2696 RVA: 0x0001EEFC File Offset: 0x0001D0FC
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.sourceProvider.Read(buffer, offset, count);
			if (this.volume == 0f)
			{
				for (int i = 0; i < num; i++)
				{
					buffer[offset++] = 0;
				}
			}
			else if (this.volume != 1f)
			{
				for (int j = 0; j < num; j += 2)
				{
					short num2 = (short)((int)buffer[offset + 1] << 8 | (int)buffer[offset]);
					float num3 = (float)num2 * this.volume;
					num2 = (short)num3;
					if (this.Volume > 1f)
					{
						if (num3 > 32767f)
						{
							num2 = short.MaxValue;
						}
						else if (num3 < -32768f)
						{
							num2 = short.MinValue;
						}
					}
					buffer[offset++] = (byte)(num2 & 255);
					buffer[offset++] = (byte)(num2 >> 8);
				}
			}
			return num;
		}

		// Token: 0x04000B70 RID: 2928
		private readonly IWaveProvider sourceProvider;

		// Token: 0x04000B71 RID: 2929
		private float volume;
	}
}
