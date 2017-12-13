using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// uuids.h, ksuuids.h
	/// </summary>
	// Token: 0x02000095 RID: 149
	internal static class MediaTypes
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public static string GetMediaTypeName(Guid majorType)
		{
			for (int i = 0; i < MediaTypes.MajorTypes.Length; i++)
			{
				if (majorType == MediaTypes.MajorTypes[i])
				{
					return MediaTypes.MajorTypeNames[i];
				}
			}
			throw new ArgumentException("Major Type not found");
		}

		// Token: 0x04000416 RID: 1046
		public static readonly Guid MEDIATYPE_AnalogAudio = new Guid("0482DEE1-7817-11cf-8a03-00aa006ecb65");

		// Token: 0x04000417 RID: 1047
		public static readonly Guid MEDIATYPE_AnalogVideo = new Guid("0482DDE1-7817-11cf-8A03-00AA006ECB65");

		// Token: 0x04000418 RID: 1048
		public static readonly Guid MEDIATYPE_Audio = new Guid("73647561-0000-0010-8000-00AA00389B71");

		// Token: 0x04000419 RID: 1049
		public static readonly Guid MEDIATYPE_AUXLine21Data = new Guid("670AEA80-3A82-11d0-B79B-00AA003767A7");

		// Token: 0x0400041A RID: 1050
		public static readonly Guid MEDIATYPE_File = new Guid("656c6966-0000-0010-8000-00AA00389B71");

		// Token: 0x0400041B RID: 1051
		public static readonly Guid MEDIATYPE_Interleaved = new Guid("73766169-0000-0010-8000-00AA00389B71");

		// Token: 0x0400041C RID: 1052
		public static readonly Guid MEDIATYPE_Midi = new Guid("7364696D-0000-0010-8000-00AA00389B71");

		// Token: 0x0400041D RID: 1053
		public static readonly Guid MEDIATYPE_ScriptCommand = new Guid("73636d64-0000-0010-8000-00AA00389B71");

		// Token: 0x0400041E RID: 1054
		public static readonly Guid MEDIATYPE_Stream = new Guid("e436eb83-524f-11ce-9f53-0020af0ba770");

		// Token: 0x0400041F RID: 1055
		public static readonly Guid MEDIATYPE_Text = new Guid("73747874-0000-0010-8000-00AA00389B71");

		// Token: 0x04000420 RID: 1056
		public static readonly Guid MEDIATYPE_Timecode = new Guid("0482DEE3-7817-11cf-8a03-00aa006ecb65");

		// Token: 0x04000421 RID: 1057
		public static readonly Guid MEDIATYPE_Video = new Guid("73646976-0000-0010-8000-00AA00389B71");

		// Token: 0x04000422 RID: 1058
		public static readonly Guid[] MajorTypes = new Guid[]
		{
			MediaTypes.MEDIATYPE_AnalogAudio,
			MediaTypes.MEDIATYPE_AnalogVideo,
			MediaTypes.MEDIATYPE_Audio,
			MediaTypes.MEDIATYPE_AUXLine21Data,
			MediaTypes.MEDIATYPE_File,
			MediaTypes.MEDIATYPE_Interleaved,
			MediaTypes.MEDIATYPE_Midi,
			MediaTypes.MEDIATYPE_ScriptCommand,
			MediaTypes.MEDIATYPE_Stream,
			MediaTypes.MEDIATYPE_Text,
			MediaTypes.MEDIATYPE_Timecode,
			MediaTypes.MEDIATYPE_Video
		};

		// Token: 0x04000423 RID: 1059
		public static readonly string[] MajorTypeNames = new string[]
		{
			"Analog Audio",
			"Analog Video",
			"Audio",
			"AUXLine21Data",
			"File",
			"Interleaved",
			"Midi",
			"ScriptCommand",
			"Stream",
			"Text",
			"Timecode",
			"Video"
		};
	}
}
