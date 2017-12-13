using System;
using NAudio.Dsp;

namespace NAudio.Wave
{
	/// <summary>
	/// A simple compressor
	/// </summary>
	// Token: 0x020001FA RID: 506
	public class SimpleCompressorStream : WaveStream
	{
		/// <summary>
		/// Create a new simple compressor stream
		/// </summary>
		/// <param name="sourceStream">Source stream</param>
		// Token: 0x06000B57 RID: 2903 RVA: 0x00021C24 File Offset: 0x0001FE24
		public SimpleCompressorStream(WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.channels = sourceStream.WaveFormat.Channels;
			this.bytesPerSample = sourceStream.WaveFormat.BitsPerSample / 8;
			this.simpleCompressor = new SimpleCompressor(5.0, 10.0, (double)sourceStream.WaveFormat.SampleRate);
			this.simpleCompressor.Threshold = 16.0;
			this.simpleCompressor.Ratio = 6.0;
			this.simpleCompressor.MakeUpGain = 16.0;
		}

		/// <summary>
		/// Make-up Gain
		/// </summary>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00021CD2 File Offset: 0x0001FED2
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00021CE0 File Offset: 0x0001FEE0
		public double MakeUpGain
		{
			get
			{
				return this.simpleCompressor.MakeUpGain;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.MakeUpGain = value;
				}
			}
		}

		/// <summary>
		/// Threshold
		/// </summary>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00021D20 File Offset: 0x0001FF20
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00021D30 File Offset: 0x0001FF30
		public double Threshold
		{
			get
			{
				return this.simpleCompressor.Threshold;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Threshold = value;
				}
			}
		}

		/// <summary>
		/// Ratio
		/// </summary>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00021D70 File Offset: 0x0001FF70
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00021D80 File Offset: 0x0001FF80
		public double Ratio
		{
			get
			{
				return this.simpleCompressor.Ratio;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Ratio = value;
				}
			}
		}

		/// <summary>
		/// Attack time
		/// </summary>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00021DC0 File Offset: 0x0001FFC0
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		public double Attack
		{
			get
			{
				return this.simpleCompressor.Attack;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Attack = value;
				}
			}
		}

		/// <summary>
		/// Release time
		/// </summary>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00021E10 File Offset: 0x00020010
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00021E20 File Offset: 0x00020020
		public double Release
		{
			get
			{
				return this.simpleCompressor.Release;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Release = value;
				}
			}
		}

		/// <summary>
		/// Determine whether the stream has the required amount of data.
		/// </summary>
		/// <param name="count">Number of bytes of data required from the stream.</param>
		/// <returns>Flag indicating whether the required amount of data is avialable.</returns>
		// Token: 0x06000B62 RID: 2914 RVA: 0x00021E60 File Offset: 0x00020060
		public override bool HasData(int count)
		{
			return this.sourceStream.HasData(count);
		}

		/// <summary>
		/// Turns gain on or off
		/// </summary>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00021E6E File Offset: 0x0002006E
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00021E76 File Offset: 0x00020076
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>
		/// Returns the stream length
		/// </summary>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00021E7F File Offset: 0x0002007F
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		/// <summary>
		/// Gets or sets the current position in the stream
		/// </summary>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00021E8C File Offset: 0x0002008C
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x00021E9C File Offset: 0x0002009C
		public override long Position
		{
			get
			{
				return this.sourceStream.Position;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceStream.Position = value;
				}
			}
		}

		/// <summary>
		/// Gets the WaveFormat of this stream
		/// </summary>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00021EDC File Offset: 0x000200DC
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00021EEC File Offset: 0x000200EC
		private void ReadSamples(byte[] buffer, int start, out double left, out double right)
		{
			if (this.bytesPerSample == 4)
			{
				left = (double)BitConverter.ToSingle(buffer, start);
				if (this.channels > 1)
				{
					right = (double)BitConverter.ToSingle(buffer, start + this.bytesPerSample);
					return;
				}
				right = left;
				return;
			}
			else
			{
				if (this.bytesPerSample != 2)
				{
					throw new InvalidOperationException(string.Format("Unsupported bytes per sample: {0}", this.bytesPerSample));
				}
				left = (double)BitConverter.ToInt16(buffer, start) / 32768.0;
				if (this.channels > 1)
				{
					right = (double)BitConverter.ToInt16(buffer, start + this.bytesPerSample) / 32768.0;
					return;
				}
				right = left;
				return;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00021F94 File Offset: 0x00020194
		private void WriteSamples(byte[] buffer, int start, double left, double right)
		{
			if (this.bytesPerSample == 4)
			{
				Array.Copy(BitConverter.GetBytes((float)left), 0, buffer, start, this.bytesPerSample);
				if (this.channels > 1)
				{
					Array.Copy(BitConverter.GetBytes((float)right), 0, buffer, start + this.bytesPerSample, this.bytesPerSample);
					return;
				}
			}
			else if (this.bytesPerSample == 2)
			{
				Array.Copy(BitConverter.GetBytes((short)(left * 32768.0)), 0, buffer, start, this.bytesPerSample);
				if (this.channels > 1)
				{
					Array.Copy(BitConverter.GetBytes((short)(right * 32768.0)), 0, buffer, start + this.bytesPerSample, this.bytesPerSample);
				}
			}
		}

		/// <summary>
		/// Reads bytes from this stream
		/// </summary>
		/// <param name="array">Buffer to read into</param>
		/// <param name="offset">Offset in array to read into</param>
		/// <param name="count">Number of bytes to read</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000B6B RID: 2923 RVA: 0x00022040 File Offset: 0x00020240
		public override int Read(byte[] array, int offset, int count)
		{
			int result;
			lock (this.lockObject)
			{
				if (this.Enabled)
				{
					if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
					{
						this.sourceBuffer = new byte[count];
					}
					int num = this.sourceStream.Read(this.sourceBuffer, 0, count);
					int num2 = num / (this.bytesPerSample * this.channels);
					for (int i = 0; i < num2; i++)
					{
						int num3 = i * this.bytesPerSample * this.channels;
						double left;
						double right;
						this.ReadSamples(this.sourceBuffer, num3, out left, out right);
						this.simpleCompressor.Process(ref left, ref right);
						this.WriteSamples(array, offset + num3, left, right);
					}
					result = count;
				}
				else
				{
					result = this.sourceStream.Read(array, offset, count);
				}
			}
			return result;
		}

		/// <summary>
		/// Disposes this stream
		/// </summary>
		/// <param name="disposing">true if the user called this</param>
		// Token: 0x06000B6C RID: 2924 RVA: 0x00022124 File Offset: 0x00020324
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Gets the block alignment for this stream
		/// </summary>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002214A File Offset: 0x0002034A
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign;
			}
		}

		// Token: 0x04000BD5 RID: 3029
		private WaveStream sourceStream;

		// Token: 0x04000BD6 RID: 3030
		private readonly SimpleCompressor simpleCompressor;

		// Token: 0x04000BD7 RID: 3031
		private byte[] sourceBuffer;

		// Token: 0x04000BD8 RID: 3032
		private bool enabled;

		// Token: 0x04000BD9 RID: 3033
		private readonly int channels;

		// Token: 0x04000BDA RID: 3034
		private readonly int bytesPerSample;

		// Token: 0x04000BDB RID: 3035
		private readonly object lockObject = new object();
	}
}
