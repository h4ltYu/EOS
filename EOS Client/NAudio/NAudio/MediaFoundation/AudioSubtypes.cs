using System;
using NAudio.Utils;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Audio Subtype GUIDs
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa372553%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x0200003F RID: 63
	public static class AudioSubtypes
	{
		/// <summary>
		/// Advanced Audio Coding (AAC).
		/// </summary>
		// Token: 0x040000F1 RID: 241
		[FieldDescription("AAC")]
		public static readonly Guid MFAudioFormat_AAC = new Guid("00001610-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Not used
		/// </summary>
		// Token: 0x040000F2 RID: 242
		[FieldDescription("ADTS")]
		public static readonly Guid MFAudioFormat_ADTS = new Guid("00001600-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Dolby AC-3 audio over Sony/Philips Digital Interface (S/PDIF).
		/// </summary>
		// Token: 0x040000F3 RID: 243
		[FieldDescription("Dolby AC3 SPDIF")]
		public static readonly Guid MFAudioFormat_Dolby_AC3_SPDIF = new Guid("00000092-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Encrypted audio data used with secure audio path.
		/// </summary>
		// Token: 0x040000F4 RID: 244
		[FieldDescription("DRM")]
		public static readonly Guid MFAudioFormat_DRM = new Guid("00000009-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Digital Theater Systems (DTS) audio.
		/// </summary>
		// Token: 0x040000F5 RID: 245
		[FieldDescription("DTS")]
		public static readonly Guid MFAudioFormat_DTS = new Guid("00000008-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Uncompressed IEEE floating-point audio.
		/// </summary>
		// Token: 0x040000F6 RID: 246
		[FieldDescription("IEEE floating-point")]
		public static readonly Guid MFAudioFormat_Float = new Guid("00000003-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// MPEG Audio Layer-3 (MP3).
		/// </summary>
		// Token: 0x040000F7 RID: 247
		[FieldDescription("MP3")]
		public static readonly Guid MFAudioFormat_MP3 = new Guid("00000055-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// MPEG-1 audio payload.
		/// </summary>
		// Token: 0x040000F8 RID: 248
		[FieldDescription("MPEG")]
		public static readonly Guid MFAudioFormat_MPEG = new Guid("00000050-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Windows Media Audio 9 Voice codec.
		/// </summary>
		// Token: 0x040000F9 RID: 249
		[FieldDescription("WMA 9 Voice codec")]
		public static readonly Guid MFAudioFormat_MSP1 = new Guid("0000000a-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Uncompressed PCM audio.
		/// </summary>
		// Token: 0x040000FA RID: 250
		[FieldDescription("PCM")]
		public static readonly Guid MFAudioFormat_PCM = new Guid("00000001-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Windows Media Audio 9 Professional codec over S/PDIF.
		/// </summary>
		// Token: 0x040000FB RID: 251
		[FieldDescription("WMA SPDIF")]
		public static readonly Guid MFAudioFormat_WMASPDIF = new Guid("00000164-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Windows Media Audio 9 Lossless codec or Windows Media Audio 9.1 codec.
		/// </summary>
		// Token: 0x040000FC RID: 252
		[FieldDescription("WMAudio Lossless")]
		public static readonly Guid MFAudioFormat_WMAudio_Lossless = new Guid("00000163-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Windows Media Audio 8 codec, Windows Media Audio 9 codec, or Windows Media Audio 9.1 codec.
		/// </summary>
		// Token: 0x040000FD RID: 253
		[FieldDescription("Windows Media Audio")]
		public static readonly Guid MFAudioFormat_WMAudioV8 = new Guid("00000161-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Windows Media Audio 9 Professional codec or Windows Media Audio 9.1 Professional codec.
		/// </summary>
		// Token: 0x040000FE RID: 254
		[FieldDescription("Windows Media Audio Professional")]
		public static readonly Guid MFAudioFormat_WMAudioV9 = new Guid("00000162-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Dolby Digital (AC-3).
		/// </summary>
		// Token: 0x040000FF RID: 255
		[FieldDescription("Dolby AC3")]
		public static readonly Guid MFAudioFormat_Dolby_AC3 = new Guid("e06d802c-db46-11cf-b4d1-00805f6cbbea");

		/// <summary>
		/// MPEG-4 and AAC Audio Types
		/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd317599(v=vs.85).aspx
		/// Reference : wmcodecdsp.h
		/// </summary>
		// Token: 0x04000100 RID: 256
		[FieldDescription("MPEG-4 and AAC Audio Types")]
		public static readonly Guid MEDIASUBTYPE_RAW_AAC1 = new Guid("000000ff-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Dolby Audio Types
		/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd317599(v=vs.85).aspx
		/// Reference : wmcodecdsp.h
		/// </summary>
		// Token: 0x04000101 RID: 257
		[FieldDescription("Dolby Audio Types")]
		public static readonly Guid MEDIASUBTYPE_DVM = new Guid("00002000-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Dolby Audio Types
		/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd317599(v=vs.85).aspx
		/// Reference : wmcodecdsp.h
		/// </summary>
		// Token: 0x04000102 RID: 258
		[FieldDescription("Dolby Audio Types")]
		public static readonly Guid MEDIASUBTYPE_DOLBY_DDPLUS = new Guid("a7fb87af-2d02-42fb-a4d4-05cd93843bdd");

		/// <summary>
		/// μ-law coding
		/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd390971(v=vs.85).aspx
		/// Reference : Ksmedia.h
		/// </summary>
		// Token: 0x04000103 RID: 259
		[FieldDescription("μ-law")]
		public static readonly Guid KSDATAFORMAT_SUBTYPE_MULAW = new Guid("00000007-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Adaptive delta pulse code modulation (ADPCM)
		/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd390971(v=vs.85).aspx
		/// Reference : Ksmedia.h
		/// </summary>
		// Token: 0x04000104 RID: 260
		[FieldDescription("ADPCM")]
		public static readonly Guid KSDATAFORMAT_SUBTYPE_ADPCM = new Guid("00000002-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Dolby Digital Plus formatted for HDMI output.
		/// http://msdn.microsoft.com/en-us/library/windows/hardware/ff538392(v=vs.85).aspx
		/// Reference : internet
		/// </summary>
		// Token: 0x04000105 RID: 261
		[FieldDescription("Dolby Digital Plus for HDMI")]
		public static readonly Guid KSDATAFORMAT_SUBTYPE_IEC61937_DOLBY_DIGITAL_PLUS = new Guid("0000000a-0cea-0010-8000-00aa00389b71");

		/// <summary>
		/// MSAudio1 - unknown meaning
		/// Reference : wmcodecdsp.h
		/// </summary>
		// Token: 0x04000106 RID: 262
		[FieldDescription("MSAudio1")]
		public static readonly Guid MEDIASUBTYPE_MSAUDIO1 = new Guid("00000160-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// IMA ADPCM ACM Wrapper
		/// </summary>
		// Token: 0x04000107 RID: 263
		[FieldDescription("IMA ADPCM")]
		public static readonly Guid ImaAdpcm = new Guid("00000011-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// WMSP2 - unknown meaning
		/// Reference: wmsdkidl.h
		/// </summary>
		// Token: 0x04000108 RID: 264
		[FieldDescription("WMSP2")]
		public static readonly Guid WMMEDIASUBTYPE_WMSP2 = new Guid("0000000b-0000-0010-8000-00aa00389b71");
	}
}
