using System;

namespace NAudio.Codecs
{
	/// <summary>
	/// Stores state to be used between calls to Encode or Decode
	/// </summary>
	// Token: 0x02000005 RID: 5
	public class G722CodecState
	{
		/// <summary>
		/// ITU Test Mode
		/// TRUE if the operating in the special ITU test mode, with the band split filters disabled.
		/// </summary>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00003759 File Offset: 0x00001959
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00003761 File Offset: 0x00001961
		public bool ItuTestMode { get; set; }

		/// <summary>
		/// TRUE if the G.722 data is packed
		/// </summary>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000376A File Offset: 0x0000196A
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00003772 File Offset: 0x00001972
		public bool Packed { get; private set; }

		/// <summary>
		/// 8kHz Sampling
		/// TRUE if encode from 8k samples/second
		/// </summary>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000377B File Offset: 0x0000197B
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00003783 File Offset: 0x00001983
		public bool EncodeFrom8000Hz { get; private set; }

		/// <summary>
		/// Bits Per Sample
		/// 6 for 48000kbps, 7 for 56000kbps, or 8 for 64000kbps.
		/// </summary>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000378C File Offset: 0x0000198C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00003794 File Offset: 0x00001994
		public int BitsPerSample { get; private set; }

		/// <summary>
		/// Signal history for the QMF (x)
		/// </summary>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000379D File Offset: 0x0000199D
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000037A5 File Offset: 0x000019A5
		public int[] QmfSignalHistory { get; private set; }

		/// <summary>
		/// Band
		/// </summary>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000037AE File Offset: 0x000019AE
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000037B6 File Offset: 0x000019B6
		public Band[] Band { get; private set; }

		/// <summary>
		/// In bit buffer
		/// </summary>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000037BF File Offset: 0x000019BF
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000037C7 File Offset: 0x000019C7
		public uint InBuffer { get; internal set; }

		/// <summary>
		/// Number of bits in InBuffer
		/// </summary>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000037D0 File Offset: 0x000019D0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000037D8 File Offset: 0x000019D8
		public int InBits { get; internal set; }

		/// <summary>
		/// Out bit buffer
		/// </summary>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000037E1 File Offset: 0x000019E1
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000037E9 File Offset: 0x000019E9
		public uint OutBuffer { get; internal set; }

		/// <summary>
		/// Number of bits in OutBuffer
		/// </summary>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000037F2 File Offset: 0x000019F2
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000037FA File Offset: 0x000019FA
		public int OutBits { get; internal set; }

		/// <summary>
		/// Creates a new instance of G722 Codec State for a 
		/// new encode or decode session
		/// </summary>
		/// <param name="rate">Bitrate (typically 64000)</param>
		/// <param name="options">Special options</param>
		// Token: 0x06000020 RID: 32 RVA: 0x00003804 File Offset: 0x00001A04
		public G722CodecState(int rate, G722Flags options)
		{
			this.Band = new Band[]
			{
				new Band(),
				new Band()
			};
			this.QmfSignalHistory = new int[24];
			this.ItuTestMode = false;
			if (rate == 48000)
			{
				this.BitsPerSample = 6;
			}
			else if (rate == 56000)
			{
				this.BitsPerSample = 7;
			}
			else
			{
				if (rate != 64000)
				{
					throw new ArgumentException("Invalid rate, should be 48000, 56000 or 64000");
				}
				this.BitsPerSample = 8;
			}
			if ((options & G722Flags.SampleRate8000) == G722Flags.SampleRate8000)
			{
				this.EncodeFrom8000Hz = true;
			}
			if ((options & G722Flags.Packed) == G722Flags.Packed && this.BitsPerSample != 8)
			{
				this.Packed = true;
			}
			else
			{
				this.Packed = false;
			}
			this.Band[0].det = 32;
			this.Band[1].det = 8;
		}
	}
}
