using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Interface for MP3 frame by frame decoder
	/// </summary>
	// Token: 0x020000A4 RID: 164
	public interface IMp3FrameDecompressor : IDisposable
	{
		/// <summary>
		/// Decompress a single MP3 frame
		/// </summary>
		/// <param name="frame">Frame to decompress</param>
		/// <param name="dest">Output buffer</param>
		/// <param name="destOffset">Offset within output buffer</param>
		/// <returns>Bytes written to output buffer</returns>
		// Token: 0x06000391 RID: 913
		int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset);

		/// <summary>
		/// Tell the decoder that we have repositioned
		/// </summary>
		// Token: 0x06000392 RID: 914
		void Reset();

		/// <summary>
		/// PCM format that we are converting into
		/// </summary>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000393 RID: 915
		WaveFormat OutputFormat { get; }
	}
}
