using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Utility class to intercept audio from an IWaveProvider and
	/// save it to disk
	/// </summary>
	// Token: 0x020001E4 RID: 484
	public class WaveRecorder : IWaveProvider, IDisposable
	{
		/// <summary>
		/// Constructs a new WaveRecorder
		/// </summary>
		/// <param name="destination">The location to write the WAV file to</param>
		/// <param name="source">The Source Wave Provider</param>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001F3EF File Offset: 0x0001D5EF
		public WaveRecorder(IWaveProvider source, string destination)
		{
			this.source = source;
			this.writer = new WaveFileWriter(destination, source.WaveFormat);
		}

		/// <summary>
		/// Read simply returns what the source returns, but writes to disk along the way
		/// </summary>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001F410 File Offset: 0x0001D610
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this.source.Read(buffer, offset, count);
			this.writer.Write(buffer, offset, num);
			return num;
		}

		/// <summary>
		/// The WaveFormat
		/// </summary>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001F43B File Offset: 0x0001D63B
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		/// <summary>
		/// Closes the WAV file
		/// </summary>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001F448 File Offset: 0x0001D648
		public void Dispose()
		{
			if (this.writer != null)
			{
				this.writer.Dispose();
				this.writer = null;
			}
		}

		// Token: 0x04000B7E RID: 2942
		private WaveFileWriter writer;

		// Token: 0x04000B7F RID: 2943
		private IWaveProvider source;
	}
}
