using System;
using NAudio.Wave.Compression;

namespace NAudio.Wave
{
	/// <summary>
	/// MP3 Frame Decompressor using ACM
	/// </summary>
	// Token: 0x020000A8 RID: 168
	public class AcmMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
	{
		/// <summary>
		/// Creates a new ACM frame decompressor
		/// </summary>
		/// <param name="sourceFormat">The MP3 source format</param>
		// Token: 0x060003C1 RID: 961 RVA: 0x0000D01C File Offset: 0x0000B21C
		public AcmMp3FrameDecompressor(WaveFormat sourceFormat)
		{
			this.pcmFormat = AcmStream.SuggestPcmFormat(sourceFormat);
			try
			{
				this.conversionStream = new AcmStream(sourceFormat, this.pcmFormat);
			}
			catch (Exception)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
				throw;
			}
		}

		/// <summary>
		/// Output format (PCM)
		/// </summary>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000D070 File Offset: 0x0000B270
		public WaveFormat OutputFormat
		{
			get
			{
				return this.pcmFormat;
			}
		}

		/// <summary>
		/// Decompresses a frame
		/// </summary>
		/// <param name="frame">The MP3 frame</param>
		/// <param name="dest">destination buffer</param>
		/// <param name="destOffset">Offset within destination buffer</param>
		/// <returns>Bytes written into destination buffer</returns>
		// Token: 0x060003C3 RID: 963 RVA: 0x0000D078 File Offset: 0x0000B278
		public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame", "You must provide a non-null Mp3Frame to decompress");
			}
			Array.Copy(frame.RawData, this.conversionStream.SourceBuffer, frame.FrameLength);
			int num = 0;
			int num2 = this.conversionStream.Convert(frame.FrameLength, out num);
			if (num != frame.FrameLength)
			{
				throw new InvalidOperationException(string.Format("Couldn't convert the whole MP3 frame (converted {0}/{1})", num, frame.FrameLength));
			}
			Array.Copy(this.conversionStream.DestBuffer, 0, dest, destOffset, num2);
			return num2;
		}

		/// <summary>
		/// Resets the MP3 Frame Decompressor after a reposition operation
		/// </summary>
		// Token: 0x060003C4 RID: 964 RVA: 0x0000D109 File Offset: 0x0000B309
		public void Reset()
		{
			this.conversionStream.Reposition();
		}

		/// <summary>
		/// Disposes of this MP3 frame decompressor
		/// </summary>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000D116 File Offset: 0x0000B316
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.conversionStream != null)
				{
					this.conversionStream.Dispose();
				}
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Finalizer ensuring that resources get released properly
		/// </summary>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000D140 File Offset: 0x0000B340
		~AcmMp3FrameDecompressor()
		{
			this.Dispose();
		}

		// Token: 0x04000476 RID: 1142
		private readonly AcmStream conversionStream;

		// Token: 0x04000477 RID: 1143
		private readonly WaveFormat pcmFormat;

		// Token: 0x04000478 RID: 1144
		private bool disposed;
	}
}
