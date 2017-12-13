using System;
using NAudio.Dmo;
using NAudio.Wave;

namespace NAudio.FileFormats.Mp3
{
	/// <summary>
	/// MP3 Frame decompressor using the Windows Media MP3 Decoder DMO object
	/// </summary>
	// Token: 0x020000A5 RID: 165
	public class DmoMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the DMO MP3 Frame decompressor
		/// </summary>
		/// <param name="sourceFormat"></param>
		// Token: 0x06000394 RID: 916 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public DmoMp3FrameDecompressor(WaveFormat sourceFormat)
		{
			this.mp3Decoder = new WindowsMediaMp3Decoder();
			if (!this.mp3Decoder.MediaObject.SupportsInputWaveFormat(0, sourceFormat))
			{
				throw new ArgumentException("Unsupported input format");
			}
			this.mp3Decoder.MediaObject.SetInputWaveFormat(0, sourceFormat);
			this.pcmFormat = new WaveFormat(sourceFormat.SampleRate, sourceFormat.Channels);
			if (!this.mp3Decoder.MediaObject.SupportsOutputWaveFormat(0, this.pcmFormat))
			{
				throw new ArgumentException(string.Format("Unsupported output format {0}", this.pcmFormat));
			}
			this.mp3Decoder.MediaObject.SetOutputWaveFormat(0, this.pcmFormat);
			this.inputMediaBuffer = new MediaBuffer(sourceFormat.AverageBytesPerSecond);
			this.outputBuffer = new DmoOutputDataBuffer(this.pcmFormat.AverageBytesPerSecond);
		}

		/// <summary>
		/// Converted PCM WaveFormat
		/// </summary>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000C49B File Offset: 0x0000A69B
		public WaveFormat OutputFormat
		{
			get
			{
				return this.pcmFormat;
			}
		}

		/// <summary>
		/// Decompress a single frame of MP3
		/// </summary>
		// Token: 0x06000396 RID: 918 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
		{
			this.inputMediaBuffer.LoadData(frame.RawData, frame.FrameLength);
			if (this.reposition)
			{
				this.mp3Decoder.MediaObject.Flush();
				this.reposition = false;
			}
			this.mp3Decoder.MediaObject.ProcessInput(0, this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
			this.outputBuffer.MediaBuffer.SetLength(0);
			this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
			this.mp3Decoder.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[]
			{
				this.outputBuffer
			});
			if (this.outputBuffer.Length == 0)
			{
				return 0;
			}
			this.outputBuffer.RetrieveData(dest, destOffset);
			return this.outputBuffer.Length;
		}

		/// <summary>
		/// Alerts us that a reposition has occured so the MP3 decoder needs to reset its state
		/// </summary>
		// Token: 0x06000397 RID: 919 RVA: 0x0000C574 File Offset: 0x0000A774
		public void Reset()
		{
			this.reposition = true;
		}

		/// <summary>
		/// Dispose of this obejct and clean up resources
		/// </summary>
		// Token: 0x06000398 RID: 920 RVA: 0x0000C580 File Offset: 0x0000A780
		public void Dispose()
		{
			if (this.inputMediaBuffer != null)
			{
				this.inputMediaBuffer.Dispose();
				this.inputMediaBuffer = null;
			}
			this.outputBuffer.Dispose();
			if (this.mp3Decoder != null)
			{
				this.mp3Decoder.Dispose();
				this.mp3Decoder = null;
			}
		}

		// Token: 0x0400045B RID: 1115
		private WindowsMediaMp3Decoder mp3Decoder;

		// Token: 0x0400045C RID: 1116
		private WaveFormat pcmFormat;

		// Token: 0x0400045D RID: 1117
		private MediaBuffer inputMediaBuffer;

		// Token: 0x0400045E RID: 1118
		private DmoOutputDataBuffer outputBuffer;

		// Token: 0x0400045F RID: 1119
		private bool reposition;
	}
}
