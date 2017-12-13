using System;
using System.IO;
using System.Text;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	/// <summary>
	/// This class writes WAV data to a .wav file on disk
	/// </summary>
	// Token: 0x020001B3 RID: 435
	public class WaveFileWriter : Stream
	{
		/// <summary>
		/// Creates a 16 bit Wave File from an ISampleProvider
		/// BEWARE: the source provider must not return data indefinitely
		/// </summary>
		/// <param name="filename">The filename to write to</param>
		/// <param name="sourceProvider">The source sample provider</param>
		// Token: 0x06000917 RID: 2327 RVA: 0x0001A320 File Offset: 0x00018520
		public static void CreateWaveFile16(string filename, ISampleProvider sourceProvider)
		{
			WaveFileWriter.CreateWaveFile(filename, new SampleToWaveProvider16(sourceProvider));
		}

		/// <summary>
		/// Creates a Wave file by reading all the data from a WaveProvider
		/// BEWARE: the WaveProvider MUST return 0 from its Read method when it is finished,
		/// or the Wave File will grow indefinitely.
		/// </summary>
		/// <param name="filename">The filename to use</param>
		/// <param name="sourceProvider">The source WaveProvider</param>
		// Token: 0x06000918 RID: 2328 RVA: 0x0001A330 File Offset: 0x00018530
		public static void CreateWaveFile(string filename, IWaveProvider sourceProvider)
		{
			using (WaveFileWriter waveFileWriter = new WaveFileWriter(filename, sourceProvider.WaveFormat))
			{
				long num = 0L;
				byte[] array = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond * 4];
				for (;;)
				{
					int num2 = sourceProvider.Read(array, 0, array.Length);
					if (num2 == 0)
					{
						break;
					}
					num += (long)num2;
					waveFileWriter.Write(array, 0, num2);
				}
			}
		}

		/// <summary>
		/// WaveFileWriter that actually writes to a stream
		/// </summary>
		/// <param name="outStream">Stream to be written to</param>
		/// <param name="format">Wave format to use</param>
		// Token: 0x06000919 RID: 2329 RVA: 0x0001A39C File Offset: 0x0001859C
		public WaveFileWriter(Stream outStream, WaveFormat format)
		{
			this.outStream = outStream;
			this.format = format;
			this.writer = new BinaryWriter(outStream, Encoding.UTF8);
			this.writer.Write(Encoding.UTF8.GetBytes("RIFF"));
			this.writer.Write(0);
			this.writer.Write(Encoding.UTF8.GetBytes("WAVE"));
			this.writer.Write(Encoding.UTF8.GetBytes("fmt "));
			format.Serialize(this.writer);
			this.CreateFactChunk();
			this.WriteDataChunkHeader();
		}

		/// <summary>
		/// Creates a new WaveFileWriter
		/// </summary>
		/// <param name="filename">The filename to write to</param>
		/// <param name="format">The Wave Format of the output data</param>
		// Token: 0x0600091A RID: 2330 RVA: 0x0001A44C File Offset: 0x0001864C
		public WaveFileWriter(string filename, WaveFormat format) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), format)
		{
			this.filename = filename;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001A465 File Offset: 0x00018665
		private void WriteDataChunkHeader()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("data"));
			this.dataSizePos = this.outStream.Position;
			this.writer.Write(0);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001A4A0 File Offset: 0x000186A0
		private void CreateFactChunk()
		{
			if (this.HasFactChunk())
			{
				this.writer.Write(Encoding.UTF8.GetBytes("fact"));
				this.writer.Write(4);
				this.factSampleCountPos = this.outStream.Position;
				this.writer.Write(0);
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001A4F8 File Offset: 0x000186F8
		private bool HasFactChunk()
		{
			return this.format.Encoding != WaveFormatEncoding.Pcm && this.format.BitsPerSample != 0;
		}

		/// <summary>
		/// The wave file name or null if not applicable
		/// </summary>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001A51B File Offset: 0x0001871B
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>
		/// Number of bytes of audio in the data chunk
		/// </summary>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001A523 File Offset: 0x00018723
		public override long Length
		{
			get
			{
				return this.dataChunkSize;
			}
		}

		/// <summary>
		/// WaveFormat of this wave file
		/// </summary>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001A52B File Offset: 0x0001872B
		public WaveFormat WaveFormat
		{
			get
			{
				return this.format;
			}
		}

		/// <summary>
		/// Returns false: Cannot read from a WaveFileWriter
		/// </summary>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001A533 File Offset: 0x00018733
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true: Can write to a WaveFileWriter
		/// </summary>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001A536 File Offset: 0x00018736
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Returns false: Cannot seek within a WaveFileWriter
		/// </summary>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001A539 File Offset: 0x00018739
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Read is not supported for a WaveFileWriter
		/// </summary>
		// Token: 0x06000924 RID: 2340 RVA: 0x0001A53C File Offset: 0x0001873C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Cannot read from a WaveFileWriter");
		}

		/// <summary>
		/// Seek is not supported for a WaveFileWriter
		/// </summary>
		// Token: 0x06000925 RID: 2341 RVA: 0x0001A548 File Offset: 0x00018748
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Cannot seek within a WaveFileWriter");
		}

		/// <summary>
		/// SetLength is not supported for WaveFileWriter
		/// </summary>
		/// <param name="value"></param>
		// Token: 0x06000926 RID: 2342 RVA: 0x0001A554 File Offset: 0x00018754
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("Cannot set length of a WaveFileWriter");
		}

		/// <summary>
		/// Gets the Position in the WaveFile (i.e. number of bytes written so far)
		/// </summary>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001A560 File Offset: 0x00018760
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001A568 File Offset: 0x00018768
		public override long Position
		{
			get
			{
				return this.dataChunkSize;
			}
			set
			{
				throw new InvalidOperationException("Repositioning a WaveFileWriter is not supported");
			}
		}

		/// <summary>
		/// Appends bytes to the WaveFile (assumes they are already in the correct format)
		/// </summary>
		/// <param name="data">the buffer containing the wave data</param>
		/// <param name="offset">the offset from which to start writing</param>
		/// <param name="count">the number of bytes to write</param>
		// Token: 0x06000929 RID: 2345 RVA: 0x0001A574 File Offset: 0x00018774
		[Obsolete("Use Write instead")]
		public void WriteData(byte[] data, int offset, int count)
		{
			this.Write(data, offset, count);
		}

		/// <summary>
		/// Appends bytes to the WaveFile (assumes they are already in the correct format)
		/// </summary>
		/// <param name="data">the buffer containing the wave data</param>
		/// <param name="offset">the offset from which to start writing</param>
		/// <param name="count">the number of bytes to write</param>
		// Token: 0x0600092A RID: 2346 RVA: 0x0001A580 File Offset: 0x00018780
		public override void Write(byte[] data, int offset, int count)
		{
			if (this.outStream.Length + (long)count > (long)((ulong)-1))
			{
				throw new ArgumentException("WAV file too large", "count");
			}
			this.outStream.Write(data, offset, count);
			this.dataChunkSize += (long)count;
		}

		/// <summary>
		/// Writes a single sample to the Wave file
		/// </summary>
		/// <param name="sample">the sample to write (assumed floating point with 1.0f as max value)</param>
		// Token: 0x0600092B RID: 2347 RVA: 0x0001A5CC File Offset: 0x000187CC
		public void WriteSample(float sample)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				this.writer.Write((short)(32767f * sample));
				this.dataChunkSize += 2L;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				byte[] bytes = BitConverter.GetBytes((int)(2.14748365E+09f * sample));
				this.value24[0] = bytes[1];
				this.value24[1] = bytes[2];
				this.value24[2] = bytes[3];
				this.writer.Write(this.value24);
				this.dataChunkSize += 3L;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				this.writer.Write(65535 * (int)sample);
				this.dataChunkSize += 4L;
				return;
			}
			if (this.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
			{
				this.writer.Write(sample);
				this.dataChunkSize += 4L;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		/// <summary>
		/// Writes 32 bit floating point samples to the Wave file
		/// They will be converted to the appropriate bit depth depending on the WaveFormat of the WAV file
		/// </summary>
		/// <param name="samples">The buffer containing the floating point samples</param>
		/// <param name="offset">The offset from which to start writing</param>
		/// <param name="count">The number of floating point samples to write</param>
		// Token: 0x0600092C RID: 2348 RVA: 0x0001A6E4 File Offset: 0x000188E4
		public void WriteSamples(float[] samples, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteSample(samples[offset + i]);
			}
		}

		/// <summary>
		/// Writes 16 bit samples to the Wave file
		/// </summary>
		/// <param name="samples">The buffer containing the 16 bit samples</param>
		/// <param name="offset">The offset from which to start writing</param>
		/// <param name="count">The number of 16 bit samples to write</param>
		// Token: 0x0600092D RID: 2349 RVA: 0x0001A708 File Offset: 0x00018908
		[Obsolete("Use WriteSamples instead")]
		public void WriteData(short[] samples, int offset, int count)
		{
			this.WriteSamples(samples, offset, count);
		}

		/// <summary>
		/// Writes 16 bit samples to the Wave file
		/// </summary>
		/// <param name="samples">The buffer containing the 16 bit samples</param>
		/// <param name="offset">The offset from which to start writing</param>
		/// <param name="count">The number of 16 bit samples to write</param>
		// Token: 0x0600092E RID: 2350 RVA: 0x0001A714 File Offset: 0x00018914
		public void WriteSamples(short[] samples, int offset, int count)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				for (int i = 0; i < count; i++)
				{
					this.writer.Write(samples[i + offset]);
				}
				this.dataChunkSize += (long)(count * 2);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				for (int j = 0; j < count; j++)
				{
					byte[] bytes = BitConverter.GetBytes(65535 * (int)samples[j + offset]);
					this.value24[0] = bytes[1];
					this.value24[1] = bytes[2];
					this.value24[2] = bytes[3];
					this.writer.Write(this.value24);
				}
				this.dataChunkSize += (long)(count * 3);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				for (int k = 0; k < count; k++)
				{
					this.writer.Write(65535 * (int)samples[k + offset]);
				}
				this.dataChunkSize += (long)(count * 4);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
			{
				for (int l = 0; l < count; l++)
				{
					this.writer.Write((float)samples[l + offset] / 32768f);
				}
				this.dataChunkSize += (long)(count * 4);
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		/// <summary>
		/// Ensures data is written to disk
		/// </summary>
		// Token: 0x0600092F RID: 2351 RVA: 0x0001A884 File Offset: 0x00018A84
		public override void Flush()
		{
			long position = this.writer.BaseStream.Position;
			this.UpdateHeader(this.writer);
			this.writer.BaseStream.Position = position;
		}

		/// <summary>
		/// Actually performs the close,making sure the header contains the correct data
		/// </summary>
		/// <param name="disposing">True if called from <see>Dispose</see></param>
		// Token: 0x06000930 RID: 2352 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.outStream != null)
			{
				try
				{
					this.UpdateHeader(this.writer);
				}
				finally
				{
					this.outStream.Close();
					this.outStream = null;
				}
			}
		}

		/// <summary>
		/// Updates the header with file size information
		/// </summary>
		// Token: 0x06000931 RID: 2353 RVA: 0x0001A90C File Offset: 0x00018B0C
		protected virtual void UpdateHeader(BinaryWriter writer)
		{
			writer.Flush();
			this.UpdateRiffChunk(writer);
			this.UpdateFactChunk(writer);
			this.UpdateDataChunk(writer);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001A929 File Offset: 0x00018B29
		private void UpdateDataChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.dataSizePos, SeekOrigin.Begin);
			writer.Write((uint)this.dataChunkSize);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001A947 File Offset: 0x00018B47
		private void UpdateRiffChunk(BinaryWriter writer)
		{
			writer.Seek(4, SeekOrigin.Begin);
			writer.Write((uint)(this.outStream.Length - 8L));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001A968 File Offset: 0x00018B68
		private void UpdateFactChunk(BinaryWriter writer)
		{
			if (this.HasFactChunk())
			{
				int num = this.format.BitsPerSample * this.format.Channels;
				if (num != 0)
				{
					writer.Seek((int)this.factSampleCountPos, SeekOrigin.Begin);
					writer.Write((int)(this.dataChunkSize * 8L / (long)num));
				}
			}
		}

		/// <summary>
		/// Finaliser - should only be called if the user forgot to close this WaveFileWriter
		/// </summary>
		// Token: 0x06000935 RID: 2357 RVA: 0x0001A9BC File Offset: 0x00018BBC
		~WaveFileWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000A99 RID: 2713
		private Stream outStream;

		// Token: 0x04000A9A RID: 2714
		private readonly BinaryWriter writer;

		// Token: 0x04000A9B RID: 2715
		private long dataSizePos;

		// Token: 0x04000A9C RID: 2716
		private long factSampleCountPos;

		// Token: 0x04000A9D RID: 2717
		private long dataChunkSize;

		// Token: 0x04000A9E RID: 2718
		private readonly WaveFormat format;

		// Token: 0x04000A9F RID: 2719
		private readonly string filename;

		// Token: 0x04000AA0 RID: 2720
		private readonly byte[] value24 = new byte[3];
	}
}
