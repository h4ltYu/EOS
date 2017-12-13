using System;

namespace NAudio.Dmo
{
	// Token: 0x02000083 RID: 131
	internal class AudioMediaSubtypes
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00009C58 File Offset: 0x00007E58
		public static string GetAudioSubtypeName(Guid subType)
		{
			for (int i = 0; i < AudioMediaSubtypes.AudioSubTypes.Length; i++)
			{
				if (subType == AudioMediaSubtypes.AudioSubTypes[i])
				{
					return AudioMediaSubtypes.AudioSubTypeNames[i];
				}
			}
			return subType.ToString();
		}

		// Token: 0x040003CB RID: 971
		public static readonly Guid MEDIASUBTYPE_PCM = new Guid("00000001-0000-0010-8000-00AA00389B71");

		// Token: 0x040003CC RID: 972
		public static readonly Guid MEDIASUBTYPE_PCMAudioObsolete = new Guid("e436eb8a-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003CD RID: 973
		public static readonly Guid MEDIASUBTYPE_MPEG1Packet = new Guid("e436eb80-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003CE RID: 974
		public static readonly Guid MEDIASUBTYPE_MPEG1Payload = new Guid("e436eb81-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003CF RID: 975
		public static readonly Guid MEDIASUBTYPE_MPEG2_AUDIO = new Guid("e06d802b-db46-11cf-b4d1-00805f6cbbea");

		// Token: 0x040003D0 RID: 976
		public static readonly Guid MEDIASUBTYPE_DVD_LPCM_AUDIO = new Guid("e06d8032-db46-11cf-b4d1-00805f6cbbea");

		// Token: 0x040003D1 RID: 977
		public static readonly Guid MEDIASUBTYPE_DRM_Audio = new Guid("00000009-0000-0010-8000-00aa00389b71");

		// Token: 0x040003D2 RID: 978
		public static readonly Guid MEDIASUBTYPE_IEEE_FLOAT = new Guid("00000003-0000-0010-8000-00aa00389b71");

		// Token: 0x040003D3 RID: 979
		public static readonly Guid MEDIASUBTYPE_DOLBY_AC3 = new Guid("e06d802c-db46-11cf-b4d1-00805f6cbbea");

		// Token: 0x040003D4 RID: 980
		public static readonly Guid MEDIASUBTYPE_DOLBY_AC3_SPDIF = new Guid("00000092-0000-0010-8000-00aa00389b71");

		// Token: 0x040003D5 RID: 981
		public static readonly Guid MEDIASUBTYPE_RAW_SPORT = new Guid("00000240-0000-0010-8000-00aa00389b71");

		// Token: 0x040003D6 RID: 982
		public static readonly Guid MEDIASUBTYPE_SPDIF_TAG_241h = new Guid("00000241-0000-0010-8000-00aa00389b71");

		// Token: 0x040003D7 RID: 983
		public static readonly Guid WMMEDIASUBTYPE_MP3 = new Guid("00000055-0000-0010-8000-00AA00389B71");

		// Token: 0x040003D8 RID: 984
		public static readonly Guid MEDIASUBTYPE_WAVE = new Guid("e436eb8b-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003D9 RID: 985
		public static readonly Guid MEDIASUBTYPE_AU = new Guid("e436eb8c-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003DA RID: 986
		public static readonly Guid MEDIASUBTYPE_AIFF = new Guid("e436eb8d-524f-11ce-9f53-0020af0ba770");

		// Token: 0x040003DB RID: 987
		public static readonly Guid[] AudioSubTypes = new Guid[]
		{
			AudioMediaSubtypes.MEDIASUBTYPE_PCM,
			AudioMediaSubtypes.MEDIASUBTYPE_PCMAudioObsolete,
			AudioMediaSubtypes.MEDIASUBTYPE_MPEG1Packet,
			AudioMediaSubtypes.MEDIASUBTYPE_MPEG1Payload,
			AudioMediaSubtypes.MEDIASUBTYPE_MPEG2_AUDIO,
			AudioMediaSubtypes.MEDIASUBTYPE_DVD_LPCM_AUDIO,
			AudioMediaSubtypes.MEDIASUBTYPE_DRM_Audio,
			AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT,
			AudioMediaSubtypes.MEDIASUBTYPE_DOLBY_AC3,
			AudioMediaSubtypes.MEDIASUBTYPE_DOLBY_AC3_SPDIF,
			AudioMediaSubtypes.MEDIASUBTYPE_RAW_SPORT,
			AudioMediaSubtypes.MEDIASUBTYPE_SPDIF_TAG_241h,
			AudioMediaSubtypes.WMMEDIASUBTYPE_MP3
		};

		// Token: 0x040003DC RID: 988
		public static readonly string[] AudioSubTypeNames = new string[]
		{
			"PCM",
			"PCM Obsolete",
			"MPEG1Packet",
			"MPEG1Payload",
			"MPEG2_AUDIO",
			"DVD_LPCM_AUDIO",
			"DRM_Audio",
			"IEEE_FLOAT",
			"DOLBY_AC3",
			"DOLBY_AC3_SPDIF",
			"RAW_SPORT",
			"SPDIF_TAG_241h",
			"MP3"
		};
	}
}
