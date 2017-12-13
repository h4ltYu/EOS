using System;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// ACM Format Tag
	/// </summary>
	// Token: 0x0200016C RID: 364
	public class AcmFormatTag
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x00016BD2 File Offset: 0x00014DD2
		internal AcmFormatTag(AcmFormatTagDetails formatTagDetails)
		{
			this.formatTagDetails = formatTagDetails;
		}

		/// <summary>
		/// Format Tag Index
		/// </summary>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00016BE1 File Offset: 0x00014DE1
		public int FormatTagIndex
		{
			get
			{
				return this.formatTagDetails.formatTagIndex;
			}
		}

		/// <summary>
		/// Format Tag
		/// </summary>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00016BEE File Offset: 0x00014DEE
		public WaveFormatEncoding FormatTag
		{
			get
			{
				return (WaveFormatEncoding)this.formatTagDetails.formatTag;
			}
		}

		/// <summary>
		/// Format Size
		/// </summary>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00016BFC File Offset: 0x00014DFC
		public int FormatSize
		{
			get
			{
				return this.formatTagDetails.formatSize;
			}
		}

		/// <summary>
		/// Support Flags
		/// </summary>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00016C09 File Offset: 0x00014E09
		public AcmDriverDetailsSupportFlags SupportFlags
		{
			get
			{
				return this.formatTagDetails.supportFlags;
			}
		}

		/// <summary>
		/// Standard Formats Count
		/// </summary>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00016C16 File Offset: 0x00014E16
		public int StandardFormatsCount
		{
			get
			{
				return this.formatTagDetails.standardFormatsCount;
			}
		}

		/// <summary>
		/// Format Description
		/// </summary>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00016C23 File Offset: 0x00014E23
		public string FormatDescription
		{
			get
			{
				return this.formatTagDetails.formatDescription;
			}
		}

		// Token: 0x04000813 RID: 2067
		private AcmFormatTagDetails formatTagDetails;
	}
}
