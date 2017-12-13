using System;
using System.IO;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveStream that simply passes on data from its source stream
	/// (e.g. a MemoryStream)
	/// </summary>
	// Token: 0x020001F7 RID: 503
	public class RawSourceWaveStream : WaveStream
	{
		/// <summary>
		/// Initialises a new instance of RawSourceWaveStream
		/// </summary>
		/// <param name="sourceStream">The source stream containing raw audio</param>
		/// <param name="waveFormat">The waveformat of the audio in the source stream</param>
		// Token: 0x06000B40 RID: 2880 RVA: 0x000217DD File Offset: 0x0001F9DD
		public RawSourceWaveStream(Stream sourceStream, WaveFormat waveFormat)
		{
			this.sourceStream = sourceStream;
			this.waveFormat = waveFormat;
		}

		/// <summary>
		/// The WaveFormat of this stream
		/// </summary>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000217F3 File Offset: 0x0001F9F3
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// The length in bytes of this stream (if supported)
		/// </summary>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000217FB File Offset: 0x0001F9FB
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		/// <summary>
		/// The current position in this stream
		/// </summary>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00021808 File Offset: 0x0001FA08
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00021815 File Offset: 0x0001FA15
		public override long Position
		{
			get
			{
				return this.sourceStream.Position;
			}
			set
			{
				this.sourceStream.Position = value;
			}
		}

		/// <summary>
		/// Reads data from the stream
		/// </summary>
		// Token: 0x06000B45 RID: 2885 RVA: 0x00021823 File Offset: 0x0001FA23
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.sourceStream.Read(buffer, offset, count);
		}

		// Token: 0x04000BC9 RID: 3017
		private Stream sourceStream;

		// Token: 0x04000BCA RID: 3018
		private WaveFormat waveFormat;
	}
}
