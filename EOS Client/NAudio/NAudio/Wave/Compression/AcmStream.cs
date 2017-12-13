using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// AcmStream encapsulates an Audio Compression Manager Stream
	/// used to convert audio from one format to another
	/// </summary>
	// Token: 0x02000174 RID: 372
	public class AcmStream : IDisposable
	{
		/// <summary>
		/// Creates a new ACM stream to convert one format to another. Note that
		/// not all conversions can be done in one step
		/// </summary>
		/// <param name="sourceFormat">The source audio format</param>
		/// <param name="destFormat">The destination audio format</param>
		// Token: 0x060007CC RID: 1996 RVA: 0x00016C38 File Offset: 0x00014E38
		public AcmStream(WaveFormat sourceFormat, WaveFormat destFormat)
		{
			try
			{
				this.streamHandle = IntPtr.Zero;
				this.sourceFormat = sourceFormat;
				int num = Math.Max(65536, sourceFormat.AverageBytesPerSecond);
				num -= num % sourceFormat.BlockAlign;
				MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, IntPtr.Zero, sourceFormat, destFormat, null, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
				int destBufferLength = this.SourceToDest(num);
				this.streamHeader = new AcmStreamHeader(this.streamHandle, num, destBufferLength);
				this.driverHandle = IntPtr.Zero;
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Creates a new ACM stream to convert one format to another, using a 
		/// specified driver identified and wave filter
		/// </summary>
		/// <param name="driverId">the driver identifier</param>
		/// <param name="sourceFormat">the source format</param>
		/// <param name="waveFilter">the wave filter</param>
		// Token: 0x060007CD RID: 1997 RVA: 0x00016CE4 File Offset: 0x00014EE4
		public AcmStream(IntPtr driverId, WaveFormat sourceFormat, WaveFilter waveFilter)
		{
			int num = Math.Max(16384, sourceFormat.AverageBytesPerSecond);
			this.sourceFormat = sourceFormat;
			num -= num % sourceFormat.BlockAlign;
			MmException.Try(AcmInterop.acmDriverOpen(out this.driverHandle, driverId, 0), "acmDriverOpen");
			MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, this.driverHandle, sourceFormat, sourceFormat, waveFilter, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
			this.streamHeader = new AcmStreamHeader(this.streamHandle, num, this.SourceToDest(num));
		}

		/// <summary>
		/// Returns the number of output bytes for a given number of input bytes
		/// </summary>
		/// <param name="source">Number of input bytes</param>
		/// <returns>Number of output bytes</returns>
		// Token: 0x060007CE RID: 1998 RVA: 0x00016D74 File Offset: 0x00014F74
		public int SourceToDest(int source)
		{
			if (source == 0)
			{
				return 0;
			}
			int result2;
			MmResult result = AcmInterop.acmStreamSize(this.streamHandle, source, out result2, AcmStreamSizeFlags.Source);
			MmException.Try(result, "acmStreamSize");
			return result2;
		}

		/// <summary>
		/// Returns the number of source bytes for a given number of destination bytes
		/// </summary>
		/// <param name="dest">Number of destination bytes</param>
		/// <returns>Number of source bytes</returns>
		// Token: 0x060007CF RID: 1999 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public int DestToSource(int dest)
		{
			if (dest == 0)
			{
				return 0;
			}
			int result;
			MmException.Try(AcmInterop.acmStreamSize(this.streamHandle, dest, out result, AcmStreamSizeFlags.Destination), "acmStreamSize");
			return result;
		}

		/// <summary>
		/// Suggests an appropriate PCM format that the compressed format can be converted
		/// to in one step
		/// </summary>
		/// <param name="compressedFormat">The compressed format</param>
		/// <returns>The PCM format</returns>
		// Token: 0x060007D0 RID: 2000 RVA: 0x00016DD0 File Offset: 0x00014FD0
		public static WaveFormat SuggestPcmFormat(WaveFormat compressedFormat)
		{
			WaveFormat waveFormat = new WaveFormat(compressedFormat.SampleRate, 16, compressedFormat.Channels);
			MmException.Try(AcmInterop.acmFormatSuggest(IntPtr.Zero, compressedFormat, waveFormat, Marshal.SizeOf(waveFormat), AcmFormatSuggestFlags.FormatTag), "acmFormatSuggest");
			return waveFormat;
		}

		/// <summary>
		/// Returns the Source Buffer. Fill this with data prior to calling convert
		/// </summary>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00016E13 File Offset: 0x00015013
		public byte[] SourceBuffer
		{
			get
			{
				return this.streamHeader.SourceBuffer;
			}
		}

		/// <summary>
		/// Returns the Destination buffer. This will contain the converted data
		/// after a successful call to Convert
		/// </summary>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00016E20 File Offset: 0x00015020
		public byte[] DestBuffer
		{
			get
			{
				return this.streamHeader.DestBuffer;
			}
		}

		/// <summary>
		/// Report that we have repositioned in the source stream
		/// </summary>
		// Token: 0x060007D3 RID: 2003 RVA: 0x00016E2D File Offset: 0x0001502D
		public void Reposition()
		{
			this.streamHeader.Reposition();
		}

		/// <summary>
		/// Converts the contents of the SourceBuffer into the DestinationBuffer
		/// </summary>
		/// <param name="bytesToConvert">The number of bytes in the SourceBuffer
		/// that need to be converted</param>
		/// <param name="sourceBytesConverted">The number of source bytes actually converted</param>
		/// <returns>The number of converted bytes in the DestinationBuffer</returns>
		// Token: 0x060007D4 RID: 2004 RVA: 0x00016E3A File Offset: 0x0001503A
		public int Convert(int bytesToConvert, out int sourceBytesConverted)
		{
			if (bytesToConvert % this.sourceFormat.BlockAlign != 0)
			{
				bytesToConvert -= bytesToConvert % this.sourceFormat.BlockAlign;
			}
			return this.streamHeader.Convert(bytesToConvert, out sourceBytesConverted);
		}

		/// <summary>
		/// Converts the contents of the SourceBuffer into the DestinationBuffer
		/// </summary>
		/// <param name="bytesToConvert">The number of bytes in the SourceBuffer
		/// that need to be converted</param>
		/// <returns>The number of converted bytes in the DestinationBuffer</returns>
		// Token: 0x060007D5 RID: 2005 RVA: 0x00016E6C File Offset: 0x0001506C
		[Obsolete("Call the version returning sourceBytesConverted instead")]
		public int Convert(int bytesToConvert)
		{
			int num;
			int result = this.Convert(bytesToConvert, out num);
			if (num != bytesToConvert)
			{
				throw new MmException(MmResult.NotSupported, "AcmStreamHeader.Convert didn't convert everything");
			}
			return result;
		}

		/// <summary>
		/// Frees resources associated with this ACM Stream
		/// </summary>
		// Token: 0x060007D6 RID: 2006 RVA: 0x00016E94 File Offset: 0x00015094
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Frees resources associated with this ACM Stream
		/// </summary>
		// Token: 0x060007D7 RID: 2007 RVA: 0x00016EA4 File Offset: 0x000150A4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.streamHeader != null)
			{
				this.streamHeader.Dispose();
				this.streamHeader = null;
			}
			if (this.streamHandle != IntPtr.Zero)
			{
				MmResult mmResult = AcmInterop.acmStreamClose(this.streamHandle, 0);
				this.streamHandle = IntPtr.Zero;
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "acmStreamClose");
				}
			}
			if (this.driverHandle != IntPtr.Zero)
			{
				AcmInterop.acmDriverClose(this.driverHandle, 0);
				this.driverHandle = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Frees resources associated with this ACM Stream
		/// </summary>
		// Token: 0x060007D8 RID: 2008 RVA: 0x00016F34 File Offset: 0x00015134
		~AcmStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0400082E RID: 2094
		private IntPtr streamHandle;

		// Token: 0x0400082F RID: 2095
		private IntPtr driverHandle;

		// Token: 0x04000830 RID: 2096
		private AcmStreamHeader streamHeader;

		// Token: 0x04000831 RID: 2097
		private WaveFormat sourceFormat;
	}
}
