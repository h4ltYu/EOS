using System;
using NAudio.Wave.Compression;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveStream that passes through an ACM Codec
	/// </summary>
	// Token: 0x020001FE RID: 510
	public class WaveFormatConversionStream : WaveStream
	{
		/// <summary>
		/// Creates a stream that can convert to PCM
		/// </summary>
		/// <param name="sourceStream">The source stream</param>
		/// <returns>A PCM stream</returns>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0002297C File Offset: 0x00020B7C
		public static WaveStream CreatePcmStream(WaveStream sourceStream)
		{
			if (sourceStream.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				return sourceStream;
			}
			WaveFormat waveFormat = AcmStream.SuggestPcmFormat(sourceStream.WaveFormat);
			if (waveFormat.SampleRate < 8000)
			{
				if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.G723)
				{
					throw new InvalidOperationException("Invalid suggested output format, please explicitly provide a target format");
				}
				waveFormat = new WaveFormat(8000, 16, 1);
			}
			return new WaveFormatConversionStream(waveFormat, sourceStream);
		}

		/// <summary>
		/// Create a new WaveFormat conversion stream
		/// </summary>
		/// <param name="targetFormat">Desired output format</param>
		/// <param name="sourceStream">Source stream</param>
		// Token: 0x06000B99 RID: 2969 RVA: 0x000229E8 File Offset: 0x00020BE8
		public WaveFormatConversionStream(WaveFormat targetFormat, WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.targetFormat = targetFormat;
			this.conversionStream = new AcmStream(sourceStream.WaveFormat, targetFormat);
			this.length = this.EstimateSourceToDest((long)((int)sourceStream.Length));
			this.position = 0L;
			this.preferredSourceReadSize = Math.Min(sourceStream.WaveFormat.AverageBytesPerSecond, this.conversionStream.SourceBuffer.Length);
			this.preferredSourceReadSize -= this.preferredSourceReadSize % sourceStream.WaveFormat.BlockAlign;
		}

		/// <summary>
		/// Converts source bytes to destination bytes
		/// </summary>
		// Token: 0x06000B9A RID: 2970 RVA: 0x00022A79 File Offset: 0x00020C79
		[Obsolete("can be unreliable, use of this method not encouraged")]
		public int SourceToDest(int source)
		{
			return (int)this.EstimateSourceToDest((long)source);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00022A84 File Offset: 0x00020C84
		private long EstimateSourceToDest(long source)
		{
			long num = source * (long)this.targetFormat.AverageBytesPerSecond / (long)this.sourceStream.WaveFormat.AverageBytesPerSecond;
			return num - num % (long)this.targetFormat.BlockAlign;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00022AC4 File Offset: 0x00020CC4
		private long EstimateDestToSource(long dest)
		{
			long num = dest * (long)this.sourceStream.WaveFormat.AverageBytesPerSecond / (long)this.targetFormat.AverageBytesPerSecond;
			num -= num % (long)this.sourceStream.WaveFormat.BlockAlign;
			return (long)((int)num);
		}

		/// <summary>
		/// Converts destination bytes to source bytes
		/// </summary>
		// Token: 0x06000B9D RID: 2973 RVA: 0x00022B0B File Offset: 0x00020D0B
		[Obsolete("can be unreliable, use of this method not encouraged")]
		public int DestToSource(int dest)
		{
			return (int)this.EstimateDestToSource((long)dest);
		}

		/// <summary>
		/// Returns the stream length
		/// </summary>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00022B16 File Offset: 0x00020D16
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>
		/// Gets or sets the current position in the stream
		/// </summary>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00022B1E File Offset: 0x00020D1E
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00022B28 File Offset: 0x00020D28
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				value -= value % (long)this.BlockAlign;
				long num = this.EstimateDestToSource(value);
				this.sourceStream.Position = num;
				this.position = this.EstimateSourceToDest(this.sourceStream.Position);
				this.leftoverDestBytes = 0;
				this.leftoverDestOffset = 0;
				this.conversionStream.Reposition();
			}
		}

		/// <summary>
		/// Gets the WaveFormat of this stream
		/// </summary>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00022B86 File Offset: 0x00020D86
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.targetFormat;
			}
		}

		/// <summary>
		/// Reads bytes from this stream
		/// </summary>
		/// <param name="buffer">Buffer to read into</param>
		/// <param name="offset">Offset in buffer to read into</param>
		/// <param name="count">Number of bytes to read</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00022B90 File Offset: 0x00020D90
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			if (count % this.BlockAlign != 0)
			{
				count -= count % this.BlockAlign;
			}
			while (i < count)
			{
				int num = Math.Min(count - i, this.leftoverDestBytes);
				if (num > 0)
				{
					Array.Copy(this.conversionStream.DestBuffer, this.leftoverDestOffset, buffer, offset + i, num);
					this.leftoverDestOffset += num;
					this.leftoverDestBytes -= num;
					i += num;
				}
				if (i >= count)
				{
					break;
				}
				int num2 = this.leftoverSourceBytes;
				int num3 = this.sourceStream.Read(this.conversionStream.SourceBuffer, 0, this.preferredSourceReadSize);
				if (num3 == 0)
				{
					break;
				}
				int num5;
				int num4 = this.conversionStream.Convert(num3, out num5);
				if (num5 == 0)
				{
					break;
				}
				if (num5 < num3)
				{
					this.sourceStream.Position -= (long)(num3 - num5);
				}
				if (num4 <= 0)
				{
					break;
				}
				int val = count - i;
				int num6 = Math.Min(num4, val);
				if (num6 < num4)
				{
					this.leftoverDestBytes = num4 - num6;
					this.leftoverDestOffset = num6;
				}
				Array.Copy(this.conversionStream.DestBuffer, 0, buffer, i + offset, num6);
				i += num6;
			}
			this.position += (long)i;
			return i;
		}

		/// <summary>
		/// Disposes this stream
		/// </summary>
		/// <param name="disposing">true if the user called this</param>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00022CCA File Offset: 0x00020ECA
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.conversionStream != null)
				{
					this.conversionStream.Dispose();
					this.conversionStream = null;
				}
				if (this.sourceStream != null)
				{
					this.sourceStream.Dispose();
					this.sourceStream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BF3 RID: 3059
		private AcmStream conversionStream;

		// Token: 0x04000BF4 RID: 3060
		private WaveStream sourceStream;

		// Token: 0x04000BF5 RID: 3061
		private WaveFormat targetFormat;

		// Token: 0x04000BF6 RID: 3062
		private long length;

		// Token: 0x04000BF7 RID: 3063
		private long position;

		// Token: 0x04000BF8 RID: 3064
		private int preferredSourceReadSize;

		// Token: 0x04000BF9 RID: 3065
		private int leftoverDestBytes;

		// Token: 0x04000BFA RID: 3066
		private int leftoverDestOffset;

		// Token: 0x04000BFB RID: 3067
		private int leftoverSourceBytes;
	}
}
