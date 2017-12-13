using System;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// ACM Format
	/// </summary>
	// Token: 0x02000166 RID: 358
	public class AcmFormat
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00016B67 File Offset: 0x00014D67
		internal AcmFormat(AcmFormatDetails formatDetails)
		{
			this.formatDetails = formatDetails;
			this.waveFormat = WaveFormat.MarshalFromPtr(formatDetails.waveFormatPointer);
		}

		/// <summary>
		/// Format Index
		/// </summary>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00016B88 File Offset: 0x00014D88
		public int FormatIndex
		{
			get
			{
				return this.formatDetails.formatIndex;
			}
		}

		/// <summary>
		/// Format Tag
		/// </summary>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00016B95 File Offset: 0x00014D95
		public WaveFormatEncoding FormatTag
		{
			get
			{
				return (WaveFormatEncoding)this.formatDetails.formatTag;
			}
		}

		/// <summary>
		/// Support Flags
		/// </summary>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00016BA3 File Offset: 0x00014DA3
		public AcmDriverDetailsSupportFlags SupportFlags
		{
			get
			{
				return this.formatDetails.supportFlags;
			}
		}

		/// <summary>
		/// WaveFormat
		/// </summary>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00016BB0 File Offset: 0x00014DB0
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// WaveFormat Size
		/// </summary>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public int WaveFormatByteSize
		{
			get
			{
				return this.formatDetails.waveFormatByteSize;
			}
		}

		/// <summary>
		/// Format Description
		/// </summary>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00016BC5 File Offset: 0x00014DC5
		public string FormatDescription
		{
			get
			{
				return this.formatDetails.formatDescription;
			}
		}

		// Token: 0x040007E0 RID: 2016
		private readonly AcmFormatDetails formatDetails;

		// Token: 0x040007E1 RID: 2017
		private readonly WaveFormat waveFormat;
	}
}
