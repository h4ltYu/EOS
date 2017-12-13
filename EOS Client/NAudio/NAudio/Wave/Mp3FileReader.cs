using System;
using System.Collections.Generic;
using System.IO;

namespace NAudio.Wave
{
	/// <summary>
	/// Class for reading from MP3 files
	/// </summary>
	// Token: 0x020001F2 RID: 498
	public class Mp3FileReader : WaveStream
	{
		/// <summary>
		/// The MP3 wave format (n.b. NOT the output format of this stream - see the WaveFormat property)
		/// </summary>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00020F39 File Offset: 0x0001F139
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00020F41 File Offset: 0x0001F141
		public Mp3WaveFormat Mp3WaveFormat { get; private set; }

		/// <summary>Supports opening a MP3 file</summary>
		// Token: 0x06000B23 RID: 2851 RVA: 0x00020F4A File Offset: 0x0001F14A
		public Mp3FileReader(string mp3FileName) : this(File.OpenRead(mp3FileName))
		{
			this.ownInputStream = true;
		}

		/// <summary>Supports opening a MP3 file</summary>
		/// <param name="mp3FileName">MP3 File name</param>
		/// <param name="frameDecompressorBuilder">Factory method to build a frame decompressor</param>
		// Token: 0x06000B24 RID: 2852 RVA: 0x00020F5F File Offset: 0x0001F15F
		public Mp3FileReader(string mp3FileName, Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder) : this(File.OpenRead(mp3FileName), frameDecompressorBuilder)
		{
			this.ownInputStream = true;
		}

		/// <summary>
		/// Opens MP3 from a stream rather than a file
		/// Will not dispose of this stream itself
		/// </summary>
		/// <param name="inputStream">The incoming stream containing MP3 data</param>
		// Token: 0x06000B25 RID: 2853 RVA: 0x00020F75 File Offset: 0x0001F175
		public Mp3FileReader(Stream inputStream) : this(inputStream, new Mp3FileReader.FrameDecompressorBuilder(Mp3FileReader.CreateAcmFrameDecompressor))
		{
		}

		/// <summary>
		/// Opens MP3 from a stream rather than a file
		/// Will not dispose of this stream itself
		/// </summary>
		/// <param name="inputStream">The incoming stream containing MP3 data</param>
		/// <param name="frameDecompressorBuilder">Factory method to build a frame decompressor</param>
		// Token: 0x06000B26 RID: 2854 RVA: 0x00020F8C File Offset: 0x0001F18C
		public Mp3FileReader(Stream inputStream, Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			try
			{
				this.mp3Stream = inputStream;
				this.id3v2Tag = Id3v2Tag.ReadTag(this.mp3Stream);
				this.dataStartPosition = this.mp3Stream.Position;
				Mp3Frame mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream);
				if (mp3Frame == null)
				{
					throw new InvalidDataException("Invalid MP3 file - no MP3 Frames Detected");
				}
				double num = (double)mp3Frame.BitRate;
				this.xingHeader = XingHeader.LoadXingHeader(mp3Frame);
				if (this.xingHeader != null)
				{
					this.dataStartPosition = this.mp3Stream.Position;
				}
				Mp3Frame mp3Frame2 = Mp3Frame.LoadFromStream(this.mp3Stream);
				if (mp3Frame2 != null && (mp3Frame2.SampleRate != mp3Frame.SampleRate || mp3Frame2.ChannelMode != mp3Frame.ChannelMode))
				{
					this.dataStartPosition = mp3Frame2.FileOffset;
					mp3Frame = mp3Frame2;
				}
				this.mp3DataLength = this.mp3Stream.Length - this.dataStartPosition;
				this.mp3Stream.Position = this.mp3Stream.Length - 128L;
				byte[] array = new byte[128];
				this.mp3Stream.Read(array, 0, 128);
				if (array[0] == 84 && array[1] == 65 && array[2] == 71)
				{
					this.id3v1Tag = array;
					this.mp3DataLength -= 128L;
				}
				this.mp3Stream.Position = this.dataStartPosition;
				this.Mp3WaveFormat = new Mp3WaveFormat(mp3Frame.SampleRate, (mp3Frame.ChannelMode == ChannelMode.Mono) ? 1 : 2, mp3Frame.FrameLength, (int)num);
				this.CreateTableOfContents();
				this.tocIndex = 0;
				num = (double)this.mp3DataLength * 8.0 / this.TotalSeconds();
				this.mp3Stream.Position = this.dataStartPosition;
				this.Mp3WaveFormat = new Mp3WaveFormat(mp3Frame.SampleRate, (mp3Frame.ChannelMode == ChannelMode.Mono) ? 1 : 2, mp3Frame.FrameLength, (int)num);
				this.decompressor = frameDecompressorBuilder(this.Mp3WaveFormat);
				this.waveFormat = this.decompressor.OutputFormat;
				this.bytesPerSample = this.decompressor.OutputFormat.BitsPerSample / 8 * this.decompressor.OutputFormat.Channels;
				this.decompressBuffer = new byte[1152 * this.bytesPerSample * 2];
			}
			catch (Exception)
			{
				if (this.ownInputStream)
				{
					inputStream.Dispose();
				}
				throw;
			}
		}

		/// <summary>
		/// Creates an ACM MP3 Frame decompressor. This is the default with NAudio
		/// </summary>
		/// <param name="mp3Format">A WaveFormat object based </param>
		/// <returns></returns>
		// Token: 0x06000B27 RID: 2855 RVA: 0x0002120C File Offset: 0x0001F40C
		public static IMp3FrameDecompressor CreateAcmFrameDecompressor(WaveFormat mp3Format)
		{
			return new AcmMp3FrameDecompressor(mp3Format);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00021214 File Offset: 0x0001F414
		private void CreateTableOfContents()
		{
			try
			{
				this.tableOfContents = new List<Mp3Index>((int)(this.mp3DataLength / 400L));
				Mp3Frame mp3Frame;
				do
				{
					Mp3Index mp3Index = new Mp3Index();
					mp3Index.FilePosition = this.mp3Stream.Position;
					mp3Index.SamplePosition = this.totalSamples;
					mp3Frame = this.ReadNextFrame(false);
					if (mp3Frame != null)
					{
						this.ValidateFrameFormat(mp3Frame);
						this.totalSamples += (long)mp3Frame.SampleCount;
						mp3Index.SampleCount = mp3Frame.SampleCount;
						mp3Index.ByteCount = (int)(this.mp3Stream.Position - mp3Index.FilePosition);
						this.tableOfContents.Add(mp3Index);
					}
				}
				while (mp3Frame != null);
			}
			catch (EndOfStreamException)
			{
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000212D0 File Offset: 0x0001F4D0
		private void ValidateFrameFormat(Mp3Frame frame)
		{
			if (frame.SampleRate != this.Mp3WaveFormat.SampleRate)
			{
				string message = string.Format("Got a frame at sample rate {0}, in an MP3 with sample rate {1}. Mp3FileReader does not support sample rate changes.", frame.SampleRate, this.Mp3WaveFormat.SampleRate);
				throw new InvalidOperationException(message);
			}
			int num = (frame.ChannelMode == ChannelMode.Mono) ? 1 : 2;
			if (num != this.Mp3WaveFormat.Channels)
			{
				string message2 = string.Format("Got a frame with channel mode {0}, in an MP3 with {1} channels. Mp3FileReader does not support changes to channel count.", frame.ChannelMode, this.Mp3WaveFormat.Channels);
				throw new InvalidOperationException(message2);
			}
		}

		/// <summary>
		/// Gets the total length of this file in milliseconds.
		/// </summary>
		// Token: 0x06000B2A RID: 2858 RVA: 0x00021366 File Offset: 0x0001F566
		private double TotalSeconds()
		{
			return (double)this.totalSamples / (double)this.Mp3WaveFormat.SampleRate;
		}

		/// <summary>
		/// ID3v2 tag if present
		/// </summary>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002137C File Offset: 0x0001F57C
		public Id3v2Tag Id3v2Tag
		{
			get
			{
				return this.id3v2Tag;
			}
		}

		/// <summary>
		/// ID3v1 tag if present
		/// </summary>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00021384 File Offset: 0x0001F584
		public byte[] Id3v1Tag
		{
			get
			{
				return this.id3v1Tag;
			}
		}

		/// <summary>
		/// Reads the next mp3 frame
		/// </summary>
		/// <returns>Next mp3 frame, or null if EOF</returns>
		// Token: 0x06000B2D RID: 2861 RVA: 0x0002138C File Offset: 0x0001F58C
		public Mp3Frame ReadNextFrame()
		{
			return this.ReadNextFrame(true);
		}

		/// <summary>
		/// Reads the next mp3 frame
		/// </summary>
		/// <returns>Next mp3 frame, or null if EOF</returns>
		// Token: 0x06000B2E RID: 2862 RVA: 0x00021398 File Offset: 0x0001F598
		private Mp3Frame ReadNextFrame(bool readData)
		{
			Mp3Frame mp3Frame = null;
			try
			{
				mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream, readData);
				if (mp3Frame != null)
				{
					this.tocIndex++;
				}
			}
			catch (EndOfStreamException)
			{
			}
			return mp3Frame;
		}

		/// <summary>
		/// This is the length in bytes of data available to be read out from the Read method
		/// (i.e. the decompressed MP3 length)
		/// n.b. this may return 0 for files whose length is unknown
		/// </summary>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000213DC File Offset: 0x0001F5DC
		public override long Length
		{
			get
			{
				return this.totalSamples * (long)this.bytesPerSample;
			}
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000213EC File Offset: 0x0001F5EC
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// <see cref="P:System.IO.Stream.Position" />
		/// </summary>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x000213F4 File Offset: 0x0001F5F4
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00021444 File Offset: 0x0001F644
		public override long Position
		{
			get
			{
				if (this.tocIndex >= this.tableOfContents.Count)
				{
					return this.Length;
				}
				return this.tableOfContents[this.tocIndex].SamplePosition * (long)this.bytesPerSample + (long)this.decompressBufferOffset;
			}
			set
			{
				lock (this.repositionLock)
				{
					value = Math.Max(Math.Min(value, this.Length), 0L);
					long num = value / (long)this.bytesPerSample;
					Mp3Index mp3Index = null;
					for (int i = 0; i < this.tableOfContents.Count; i++)
					{
						if (this.tableOfContents[i].SamplePosition >= num)
						{
							mp3Index = this.tableOfContents[i];
							this.tocIndex = i;
							break;
						}
					}
					if (mp3Index != null)
					{
						this.mp3Stream.Position = mp3Index.FilePosition;
					}
					else
					{
						this.mp3Stream.Position = this.mp3DataLength + this.dataStartPosition;
					}
					this.decompressBufferOffset = 0;
					this.decompressLeftovers = 0;
					this.repositionedFlag = true;
				}
			}
		}

		/// <summary>
		/// Reads decompressed PCM data from our MP3 file.
		/// </summary>
		// Token: 0x06000B33 RID: 2867 RVA: 0x0002151C File Offset: 0x0001F71C
		public override int Read(byte[] sampleBuffer, int offset, int numBytes)
		{
			int i = 0;
			lock (this.repositionLock)
			{
				if (this.decompressLeftovers != 0)
				{
					int num = Math.Min(this.decompressLeftovers, numBytes);
					Array.Copy(this.decompressBuffer, this.decompressBufferOffset, sampleBuffer, offset, num);
					this.decompressLeftovers -= num;
					if (this.decompressLeftovers == 0)
					{
						this.decompressBufferOffset = 0;
					}
					else
					{
						this.decompressBufferOffset += num;
					}
					i += num;
					offset += num;
				}
				while (i < numBytes)
				{
					Mp3Frame mp3Frame = this.ReadNextFrame();
					if (mp3Frame == null)
					{
						break;
					}
					if (this.repositionedFlag)
					{
						this.decompressor.Reset();
						this.repositionedFlag = false;
					}
					int num2 = this.decompressor.DecompressFrame(mp3Frame, this.decompressBuffer, 0);
					int num3 = Math.Min(num2, numBytes - i);
					Array.Copy(this.decompressBuffer, 0, sampleBuffer, offset, num3);
					if (num3 < num2)
					{
						this.decompressBufferOffset = num3;
						this.decompressLeftovers = num2 - num3;
					}
					else
					{
						this.decompressBufferOffset = 0;
						this.decompressLeftovers = 0;
					}
					offset += num3;
					i += num3;
				}
			}
			return i;
		}

		/// <summary>
		/// Xing header if present
		/// </summary>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002164C File Offset: 0x0001F84C
		public XingHeader XingHeader
		{
			get
			{
				return this.xingHeader;
			}
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000B35 RID: 2869 RVA: 0x00021654 File Offset: 0x0001F854
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mp3Stream != null)
				{
					if (this.ownInputStream)
					{
						this.mp3Stream.Dispose();
					}
					this.mp3Stream = null;
				}
				if (this.decompressor != null)
				{
					this.decompressor.Dispose();
					this.decompressor = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BB6 RID: 2998
		private readonly WaveFormat waveFormat;

		// Token: 0x04000BB7 RID: 2999
		private Stream mp3Stream;

		// Token: 0x04000BB8 RID: 3000
		private readonly long mp3DataLength;

		// Token: 0x04000BB9 RID: 3001
		private readonly long dataStartPosition;

		// Token: 0x04000BBA RID: 3002
		private readonly Id3v2Tag id3v2Tag;

		// Token: 0x04000BBB RID: 3003
		private readonly XingHeader xingHeader;

		// Token: 0x04000BBC RID: 3004
		private readonly byte[] id3v1Tag;

		// Token: 0x04000BBD RID: 3005
		private readonly bool ownInputStream;

		// Token: 0x04000BBE RID: 3006
		private List<Mp3Index> tableOfContents;

		// Token: 0x04000BBF RID: 3007
		private int tocIndex;

		// Token: 0x04000BC0 RID: 3008
		private long totalSamples;

		// Token: 0x04000BC1 RID: 3009
		private readonly int bytesPerSample;

		// Token: 0x04000BC2 RID: 3010
		private IMp3FrameDecompressor decompressor;

		// Token: 0x04000BC3 RID: 3011
		private readonly byte[] decompressBuffer;

		// Token: 0x04000BC4 RID: 3012
		private int decompressBufferOffset;

		// Token: 0x04000BC5 RID: 3013
		private int decompressLeftovers;

		// Token: 0x04000BC6 RID: 3014
		private bool repositionedFlag;

		// Token: 0x04000BC7 RID: 3015
		private readonly object repositionLock = new object();

		/// <summary>
		/// Function that can create an MP3 Frame decompressor
		/// </summary>
		/// <param name="mp3Format">A WaveFormat object describing the MP3 file format</param>
		/// <returns>An MP3 Frame decompressor</returns>
		// Token: 0x020001F3 RID: 499
		// (Invoke) Token: 0x06000B37 RID: 2871
		public delegate IMp3FrameDecompressor FrameDecompressorBuilder(WaveFormat mp3Format);
	}
}
