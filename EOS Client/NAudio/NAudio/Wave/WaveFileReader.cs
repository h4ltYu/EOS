using System;
using System.Collections.Generic;
using System.IO;
using NAudio.FileFormats.Wav;

namespace NAudio.Wave
{
	/// <summary>This class supports the reading of WAV files,
	/// providing a repositionable WaveStream that returns the raw data
	/// contained in the WAV file
	/// </summary>
	// Token: 0x020001EC RID: 492
	public class WaveFileReader : WaveStream
	{
		/// <summary>Supports opening a WAV file</summary>
		/// <remarks>The WAV file format is a real mess, but we will only
		/// support the basic WAV file format which actually covers the vast
		/// majority of WAV files out there. For more WAV file format information
		/// visit www.wotsit.org. If you have a WAV file that can't be read by
		/// this class, email it to the NAudio project and we will probably
		/// fix this reader to support it
		/// </remarks>
		// Token: 0x06000AEC RID: 2796 RVA: 0x00020550 File Offset: 0x0001E750
		public WaveFileReader(string waveFile) : this(File.OpenRead(waveFile))
		{
			this.ownInput = true;
		}

		/// <summary>
		/// Creates a Wave File Reader based on an input stream
		/// </summary>
		/// <param name="inputStream">The input stream containing a WAV file including header</param>
		// Token: 0x06000AED RID: 2797 RVA: 0x00020568 File Offset: 0x0001E768
		public WaveFileReader(Stream inputStream)
		{
			this.waveStream = inputStream;
			WaveFileChunkReader waveFileChunkReader = new WaveFileChunkReader();
			waveFileChunkReader.ReadWaveHeader(inputStream);
			this.waveFormat = waveFileChunkReader.WaveFormat;
			this.dataPosition = waveFileChunkReader.DataChunkPosition;
			this.dataChunkLength = waveFileChunkReader.DataChunkLength;
			this.chunks = waveFileChunkReader.RiffChunks;
			this.Position = 0L;
		}

		/// <summary>
		/// Gets a list of the additional chunks found in this file
		/// </summary>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000205DD File Offset: 0x0001E7DD
		public List<RiffChunk> ExtraChunks
		{
			get
			{
				return this.chunks;
			}
		}

		/// <summary>
		/// Gets the data for the specified chunk
		/// </summary>
		// Token: 0x06000AEF RID: 2799 RVA: 0x000205E8 File Offset: 0x0001E7E8
		public byte[] GetChunkData(RiffChunk chunk)
		{
			long position = this.waveStream.Position;
			this.waveStream.Position = chunk.StreamPosition;
			byte[] array = new byte[chunk.Length];
			this.waveStream.Read(array, 0, array.Length);
			this.waveStream.Position = position;
			return array;
		}

		/// <summary>
		/// Cleans up the resources associated with this WaveFileReader
		/// </summary>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002063C File Offset: 0x0001E83C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.waveStream != null)
			{
				if (this.ownInput)
				{
					this.waveStream.Close();
				}
				this.waveStream = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002066A File Offset: 0x0001E86A
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// This is the length of audio data contained in this WAV file, in bytes
		/// (i.e. the byte length of the data chunk, not the length of the WAV file itself)
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00020672 File Offset: 0x0001E872
		public override long Length
		{
			get
			{
				return this.dataChunkLength;
			}
		}

		/// <summary>
		/// Number of Samples (if possible to calculate)
		/// This currently does not take into account number of channels, so
		/// divide again by number of channels if you want the number of 
		/// audio 'frames'
		/// </summary>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002067C File Offset: 0x0001E87C
		public long SampleCount
		{
			get
			{
				if (this.waveFormat.Encoding == WaveFormatEncoding.Pcm || this.waveFormat.Encoding == WaveFormatEncoding.Extensible || this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					return this.dataChunkLength / (long)this.BlockAlign;
				}
				throw new InvalidOperationException("Sample count is calculated only for the standard encodings");
			}
		}

		/// <summary>
		/// Position in the WAV data chunk.
		/// <see cref="P:System.IO.Stream.Position" />
		/// </summary>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000206D0 File Offset: 0x0001E8D0
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public override long Position
		{
			get
			{
				return this.waveStream.Position - this.dataPosition;
			}
			set
			{
				lock (this.lockObject)
				{
					value = Math.Min(value, this.Length);
					value -= value % (long)this.waveFormat.BlockAlign;
					this.waveStream.Position = value + this.dataPosition;
				}
			}
		}

		/// <summary>
		/// Reads bytes from the Wave File
		/// <see cref="M:System.IO.Stream.Read(System.Byte[],System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002074C File Offset: 0x0001E94C
		public override int Read(byte[] array, int offset, int count)
		{
			if (count % this.waveFormat.BlockAlign != 0)
			{
				throw new ArgumentException(string.Format("Must read complete blocks: requested {0}, block align is {1}", count, this.WaveFormat.BlockAlign));
			}
			int result;
			lock (this.lockObject)
			{
				if (this.Position + (long)count > this.dataChunkLength)
				{
					count = (int)(this.dataChunkLength - this.Position);
				}
				result = this.waveStream.Read(array, offset, count);
			}
			return result;
		}

		/// <summary>
		/// Attempts to read the next sample or group of samples as floating point normalised into the range -1.0f to 1.0f
		/// </summary>
		/// <returns>An array of samples, 1 for mono, 2 for stereo etc. Null indicates end of file reached
		/// </returns>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public float[] ReadNextSampleFrame()
		{
			WaveFormatEncoding encoding = this.waveFormat.Encoding;
			switch (encoding)
			{
			case WaveFormatEncoding.Pcm:
			case WaveFormatEncoding.IeeeFloat:
				goto IL_36;
			case WaveFormatEncoding.Adpcm:
				break;
			default:
				if (encoding == WaveFormatEncoding.Extensible)
				{
					goto IL_36;
				}
				break;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
			IL_36:
			float[] array = new float[this.waveFormat.Channels];
			int num = this.waveFormat.Channels * (this.waveFormat.BitsPerSample / 8);
			byte[] array2 = new byte[num];
			int num2 = this.Read(array2, 0, num);
			if (num2 == 0)
			{
				return null;
			}
			if (num2 < num)
			{
				throw new InvalidDataException("Unexpected end of file");
			}
			int num3 = 0;
			for (int i = 0; i < this.waveFormat.Channels; i++)
			{
				if (this.waveFormat.BitsPerSample == 16)
				{
					array[i] = (float)BitConverter.ToInt16(array2, num3) / 32768f;
					num3 += 2;
				}
				else if (this.waveFormat.BitsPerSample == 24)
				{
					array[i] = (float)((int)((sbyte)array2[num3 + 2]) << 16 | (int)array2[num3 + 1] << 8 | (int)array2[num3]) / 8388608f;
					num3 += 3;
				}
				else if (this.waveFormat.BitsPerSample == 32 && this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					array[i] = BitConverter.ToSingle(array2, num3);
					num3 += 4;
				}
				else
				{
					if (this.waveFormat.BitsPerSample != 32)
					{
						throw new InvalidOperationException("Unsupported bit depth");
					}
					array[i] = (float)BitConverter.ToInt32(array2, num3) / 2.14748365E+09f;
					num3 += 4;
				}
			}
			return array;
		}

		/// <summary>
		/// Attempts to read a sample into a float. n.b. only applicable for uncompressed formats
		/// Will normalise the value read into the range -1.0f to 1.0f if it comes from a PCM encoding
		/// </summary>
		/// <returns>False if the end of the WAV data chunk was reached</returns>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002096C File Offset: 0x0001EB6C
		[Obsolete("Use ReadNextSampleFrame instead (this version does not support stereo properly)")]
		public bool TryReadFloat(out float sampleValue)
		{
			float[] array = this.ReadNextSampleFrame();
			sampleValue = ((array != null) ? array[0] : 0f);
			return array != null;
		}

		// Token: 0x04000B9A RID: 2970
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B9B RID: 2971
		private readonly bool ownInput;

		// Token: 0x04000B9C RID: 2972
		private readonly long dataPosition;

		// Token: 0x04000B9D RID: 2973
		private readonly long dataChunkLength;

		// Token: 0x04000B9E RID: 2974
		private readonly List<RiffChunk> chunks = new List<RiffChunk>();

		// Token: 0x04000B9F RID: 2975
		private readonly object lockObject = new object();

		// Token: 0x04000BA0 RID: 2976
		private Stream waveStream;
	}
}
