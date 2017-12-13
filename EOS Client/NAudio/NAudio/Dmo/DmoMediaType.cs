using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.Dmo
{
	/// <summary>
	/// http://msdn.microsoft.com/en-us/library/aa929922.aspx
	/// DMO_MEDIA_TYPE 
	/// </summary>
	// Token: 0x02000205 RID: 517
	public struct DmoMediaType
	{
		/// <summary>
		/// Major type
		/// </summary>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00023B5C File Offset: 0x00021D5C
		public Guid MajorType
		{
			get
			{
				return this.majortype;
			}
		}

		/// <summary>
		/// Major type name
		/// </summary>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00023B64 File Offset: 0x00021D64
		public string MajorTypeName
		{
			get
			{
				return MediaTypes.GetMediaTypeName(this.majortype);
			}
		}

		/// <summary>
		/// Subtype
		/// </summary>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00023B71 File Offset: 0x00021D71
		public Guid SubType
		{
			get
			{
				return this.subtype;
			}
		}

		/// <summary>
		/// Subtype name
		/// </summary>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00023B79 File Offset: 0x00021D79
		public string SubTypeName
		{
			get
			{
				if (this.majortype == MediaTypes.MEDIATYPE_Audio)
				{
					return AudioMediaSubtypes.GetAudioSubtypeName(this.subtype);
				}
				return this.subtype.ToString();
			}
		}

		/// <summary>
		/// Fixed size samples
		/// </summary>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00023BAA File Offset: 0x00021DAA
		public bool FixedSizeSamples
		{
			get
			{
				return this.bFixedSizeSamples;
			}
		}

		/// <summary>
		/// Sample size
		/// </summary>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00023BB2 File Offset: 0x00021DB2
		public int SampleSize
		{
			get
			{
				return this.lSampleSize;
			}
		}

		/// <summary>
		/// Format type
		/// </summary>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00023BBA File Offset: 0x00021DBA
		public Guid FormatType
		{
			get
			{
				return this.formattype;
			}
		}

		/// <summary>
		/// Format type name
		/// </summary>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00023BC4 File Offset: 0x00021DC4
		public string FormatTypeName
		{
			get
			{
				if (this.formattype == DmoMediaTypeGuids.FORMAT_None)
				{
					return "None";
				}
				if (this.formattype == Guid.Empty)
				{
					return "Null";
				}
				if (this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
				{
					return "WaveFormatEx";
				}
				return this.FormatType.ToString();
			}
		}

		/// <summary>
		/// Gets the structure as a Wave format (if it is one)
		/// </summary>        
		// Token: 0x06000BDD RID: 3037 RVA: 0x00023C2D File Offset: 0x00021E2D
		public WaveFormat GetWaveFormat()
		{
			if (this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
			{
				return WaveFormat.MarshalFromPtr(this.pbFormat);
			}
			throw new InvalidOperationException("Not a WaveFormat type");
		}

		/// <summary>
		/// Sets this object up to point to a wave format
		/// </summary>
		/// <param name="waveFormat">Wave format structure</param>
		// Token: 0x06000BDE RID: 3038 RVA: 0x00023C58 File Offset: 0x00021E58
		public void SetWaveFormat(WaveFormat waveFormat)
		{
			this.majortype = MediaTypes.MEDIATYPE_Audio;
			WaveFormatExtensible waveFormatExtensible = waveFormat as WaveFormatExtensible;
			if (waveFormatExtensible == null)
			{
				WaveFormatEncoding encoding = waveFormat.Encoding;
				switch (encoding)
				{
				case WaveFormatEncoding.Pcm:
					this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
					goto IL_87;
				case WaveFormatEncoding.Adpcm:
					break;
				case WaveFormatEncoding.IeeeFloat:
					this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
					goto IL_87;
				default:
					if (encoding == WaveFormatEncoding.MpegLayer3)
					{
						this.subtype = AudioMediaSubtypes.WMMEDIASUBTYPE_MP3;
						goto IL_87;
					}
					break;
				}
				throw new ArgumentException(string.Format("Not a supported encoding {0}", waveFormat.Encoding));
			}
			this.subtype = waveFormatExtensible.SubFormat;
			IL_87:
			this.bFixedSizeSamples = (this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_PCM || this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT);
			this.formattype = DmoMediaTypeGuids.FORMAT_WaveFormatEx;
			if (this.cbFormat < Marshal.SizeOf(waveFormat))
			{
				throw new InvalidOperationException("Not enough memory assigned for a WaveFormat structure");
			}
			Marshal.StructureToPtr(waveFormat, this.pbFormat, false);
		}

		// Token: 0x04000C26 RID: 3110
		private Guid majortype;

		// Token: 0x04000C27 RID: 3111
		private Guid subtype;

		// Token: 0x04000C28 RID: 3112
		private bool bFixedSizeSamples;

		// Token: 0x04000C29 RID: 3113
		private bool bTemporalCompression;

		// Token: 0x04000C2A RID: 3114
		private int lSampleSize;

		// Token: 0x04000C2B RID: 3115
		private Guid formattype;

		// Token: 0x04000C2C RID: 3116
		private IntPtr pUnk;

		// Token: 0x04000C2D RID: 3117
		private int cbFormat;

		// Token: 0x04000C2E RID: 3118
		private IntPtr pbFormat;
	}
}
