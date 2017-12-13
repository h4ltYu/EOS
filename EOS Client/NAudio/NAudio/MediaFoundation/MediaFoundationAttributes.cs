using System;
using NAudio.Utils;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Media Foundation attribute guids
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/ms696989%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x0200004D RID: 77
	public static class MediaFoundationAttributes
	{
		/// <summary>
		/// Specifies whether an MFT performs asynchronous processing.
		/// </summary>
		// Token: 0x040002A1 RID: 673
		public static readonly Guid MF_TRANSFORM_ASYNC = new Guid("f81a699a-649a-497d-8c73-29f8fed6ad7a");

		/// <summary>
		/// Enables the use of an asynchronous MFT.
		/// </summary>
		// Token: 0x040002A2 RID: 674
		public static readonly Guid MF_TRANSFORM_ASYNC_UNLOCK = new Guid("e5666d6b-3422-4eb6-a421-da7db1f8e207");

		/// <summary>
		/// Contains flags for an MFT activation object.
		/// </summary>
		// Token: 0x040002A3 RID: 675
		[FieldDescription("Transform Flags")]
		public static readonly Guid MF_TRANSFORM_FLAGS_Attribute = new Guid("9359bb7e-6275-46c4-a025-1c01e45f1a86");

		/// <summary>
		/// Specifies the category for an MFT.
		/// </summary>
		// Token: 0x040002A4 RID: 676
		[FieldDescription("Transform Category")]
		public static readonly Guid MF_TRANSFORM_CATEGORY_Attribute = new Guid("ceabba49-506d-4757-a6ff-66c184987e4e");

		/// <summary>
		/// Contains the class identifier (CLSID) of an MFT.
		/// </summary>
		// Token: 0x040002A5 RID: 677
		[FieldDescription("Class identifier")]
		public static readonly Guid MFT_TRANSFORM_CLSID_Attribute = new Guid("6821c42b-65a4-4e82-99bc-9a88205ecd0c");

		/// <summary>
		/// Contains the registered input types for a Media Foundation transform (MFT).
		/// </summary>
		// Token: 0x040002A6 RID: 678
		[FieldDescription("Input Types")]
		public static readonly Guid MFT_INPUT_TYPES_Attributes = new Guid("4276c9b1-759d-4bf3-9cd0-0d723d138f96");

		/// <summary>
		/// Contains the registered output types for a Media Foundation transform (MFT).
		/// </summary>
		// Token: 0x040002A7 RID: 679
		[FieldDescription("Output Types")]
		public static readonly Guid MFT_OUTPUT_TYPES_Attributes = new Guid("8eae8cf3-a44f-4306-ba5c-bf5dda242818");

		/// <summary>
		/// Contains the symbolic link for a hardware-based MFT.
		/// </summary>
		// Token: 0x040002A8 RID: 680
		public static readonly Guid MFT_ENUM_HARDWARE_URL_Attribute = new Guid("2fb866ac-b078-4942-ab6c-003d05cda674");

		/// <summary>
		/// Contains the display name for a hardware-based MFT.
		/// </summary>
		// Token: 0x040002A9 RID: 681
		[FieldDescription("Name")]
		public static readonly Guid MFT_FRIENDLY_NAME_Attribute = new Guid("314ffbae-5b41-4c95-9c19-4e7d586face3");

		/// <summary>
		/// Contains a pointer to the stream attributes of the connected stream on a hardware-based MFT.
		/// </summary>
		// Token: 0x040002AA RID: 682
		public static readonly Guid MFT_CONNECTED_STREAM_ATTRIBUTE = new Guid("71eeb820-a59f-4de2-bcec-38db1dd611a4");

		/// <summary>
		/// Specifies whether a hardware-based MFT is connected to another hardware-based MFT.
		/// </summary>
		// Token: 0x040002AB RID: 683
		public static readonly Guid MFT_CONNECTED_TO_HW_STREAM = new Guid("34e6e728-06d6-4491-a553-4795650db912");

		/// <summary>
		/// Specifies the preferred output format for an encoder.
		/// </summary>
		// Token: 0x040002AC RID: 684
		[FieldDescription("Preferred Output Format")]
		public static readonly Guid MFT_PREFERRED_OUTPUTTYPE_Attribute = new Guid("7e700499-396a-49ee-b1b4-f628021e8c9d");

		/// <summary>
		/// Specifies whether an MFT is registered only in the application's process.
		/// </summary>
		// Token: 0x040002AD RID: 685
		public static readonly Guid MFT_PROCESS_LOCAL_Attribute = new Guid("543186e4-4649-4e65-b588-4aa352aff379");

		/// <summary>
		/// Contains configuration properties for an encoder.
		/// </summary>
		// Token: 0x040002AE RID: 686
		public static readonly Guid MFT_PREFERRED_ENCODER_PROFILE = new Guid("53004909-1ef5-46d7-a18e-5a75f8b5905f");

		/// <summary>
		/// Specifies whether a hardware device source uses the system time for time stamps.
		/// </summary>
		// Token: 0x040002AF RID: 687
		public static readonly Guid MFT_HW_TIMESTAMP_WITH_QPC_Attribute = new Guid("8d030fb8-cc43-4258-a22e-9210bef89be4");

		/// <summary>
		/// Contains an IMFFieldOfUseMFTUnlock pointer, which can be used to unlock the MFT.
		/// </summary>
		// Token: 0x040002B0 RID: 688
		public static readonly Guid MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid("8ec2e9fd-9148-410d-831e-702439461a8e");

		/// <summary>
		/// Contains the merit value of a hardware codec.
		/// </summary>
		// Token: 0x040002B1 RID: 689
		public static readonly Guid MFT_CODEC_MERIT_Attribute = new Guid("88a7cb15-7b07-4a34-9128-e64c6703c4d3");

		/// <summary>
		/// Specifies whether a decoder is optimized for transcoding rather than for playback.
		/// </summary>
		// Token: 0x040002B2 RID: 690
		public static readonly Guid MFT_ENUM_TRANSCODE_ONLY_ATTRIBUTE = new Guid("111ea8cd-b62a-4bdb-89f6-67ffcdc2458b");

		/// <summary>
		/// Contains a pointer to the proxy object for the application's presentation descriptor.
		/// </summary>
		// Token: 0x040002B3 RID: 691
		[FieldDescription("PMP Host Context")]
		public static readonly Guid MF_PD_PMPHOST_CONTEXT = new Guid("6c990d31-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Contains a pointer to the presentation descriptor from the protected media path (PMP).
		/// </summary>
		// Token: 0x040002B4 RID: 692
		[FieldDescription("App Context")]
		public static readonly Guid MF_PD_APP_CONTEXT = new Guid("6c990d32-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies the duration of a presentation, in 100-nanosecond units.
		/// </summary>
		// Token: 0x040002B5 RID: 693
		[FieldDescription("Duration")]
		public static readonly Guid MF_PD_DURATION = new Guid("6c990d33-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies the total size of the source file, in bytes. 
		/// </summary>
		// Token: 0x040002B6 RID: 694
		[FieldDescription("Total File Size")]
		public static readonly Guid MF_PD_TOTAL_FILE_SIZE = new Guid("6c990d34-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies the audio encoding bit rate for the presentation, in bits per second.
		/// </summary>
		// Token: 0x040002B7 RID: 695
		[FieldDescription("Audio encoding bitrate")]
		public static readonly Guid MF_PD_AUDIO_ENCODING_BITRATE = new Guid("6c990d35-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies the video encoding bit rate for the presentation, in bits per second.
		/// </summary>
		// Token: 0x040002B8 RID: 696
		[FieldDescription("Video Encoding Bitrate")]
		public static readonly Guid MF_PD_VIDEO_ENCODING_BITRATE = new Guid("6c990d36-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies the MIME type of the content.
		/// </summary>
		// Token: 0x040002B9 RID: 697
		[FieldDescription("MIME Type")]
		public static readonly Guid MF_PD_MIME_TYPE = new Guid("6c990d37-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies when a presentation was last modified.
		/// </summary>
		// Token: 0x040002BA RID: 698
		[FieldDescription("Last Modified Time")]
		public static readonly Guid MF_PD_LAST_MODIFIED_TIME = new Guid("6c990d38-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// The identifier of the playlist element in the presentation.
		/// </summary>
		// Token: 0x040002BB RID: 699
		[FieldDescription("Element ID")]
		public static readonly Guid MF_PD_PLAYBACK_ELEMENT_ID = new Guid("6c990d39-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Contains the preferred RFC 1766 language of the media source.
		/// </summary>
		// Token: 0x040002BC RID: 700
		[FieldDescription("Preferred Language")]
		public static readonly Guid MF_PD_PREFERRED_LANGUAGE = new Guid("6c990d3a-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// The time at which the presentation must begin, relative to the start of the media source.
		/// </summary>
		// Token: 0x040002BD RID: 701
		[FieldDescription("Playback boundary time")]
		public static readonly Guid MF_PD_PLAYBACK_BOUNDARY_TIME = new Guid("6c990d3b-bb8e-477a-8598-0d5d96fcd88a");

		/// <summary>
		/// Specifies whether the audio streams in the presentation have a variable bit rate.
		/// </summary>
		// Token: 0x040002BE RID: 702
		[FieldDescription("Audio is variable bitrate")]
		public static readonly Guid MF_PD_AUDIO_ISVARIABLEBITRATE = new Guid("33026ee0-e387-4582-ae0a-34a2ad3baa18");

		/// <summary>
		/// Media type Major Type
		/// </summary>
		// Token: 0x040002BF RID: 703
		[FieldDescription("Major Media Type")]
		public static readonly Guid MF_MT_MAJOR_TYPE = new Guid("48eba18e-f8c9-4687-bf11-0a74c9f96a8f");

		/// <summary>
		/// Media Type subtype
		/// </summary>
		// Token: 0x040002C0 RID: 704
		[FieldDescription("Media Subtype")]
		public static readonly Guid MF_MT_SUBTYPE = new Guid("f7e34c9a-42e8-4714-b74b-cb29d72c35e5");

		/// <summary>
		/// Audio block alignment
		/// </summary>
		// Token: 0x040002C1 RID: 705
		[FieldDescription("Audio block alignment")]
		public static readonly Guid MF_MT_AUDIO_BLOCK_ALIGNMENT = new Guid("322de230-9eeb-43bd-ab7a-ff412251541d");

		/// <summary>
		/// Audio average bytes per second
		/// </summary>
		// Token: 0x040002C2 RID: 706
		[FieldDescription("Audio average bytes per second")]
		public static readonly Guid MF_MT_AUDIO_AVG_BYTES_PER_SECOND = new Guid("1aab75c8-cfef-451c-ab95-ac034b8e1731");

		/// <summary>
		/// Audio number of channels
		/// </summary>
		// Token: 0x040002C3 RID: 707
		[FieldDescription("Audio number of channels")]
		public static readonly Guid MF_MT_AUDIO_NUM_CHANNELS = new Guid("37e48bf5-645e-4c5b-89de-ada9e29b696a");

		/// <summary>
		/// Audio samples per second
		/// </summary>
		// Token: 0x040002C4 RID: 708
		[FieldDescription("Audio samples per second")]
		public static readonly Guid MF_MT_AUDIO_SAMPLES_PER_SECOND = new Guid("5faeeae7-0290-4c31-9e8a-c534f68d9dba");

		/// <summary>
		/// Audio bits per sample
		/// </summary>
		// Token: 0x040002C5 RID: 709
		[FieldDescription("Audio bits per sample")]
		public static readonly Guid MF_MT_AUDIO_BITS_PER_SAMPLE = new Guid("f2deb57f-40fa-4764-aa33-ed4f2d1ff669");

		/// <summary>
		/// Enables the source reader or sink writer to use hardware-based Media Foundation transforms (MFTs).
		/// </summary>
		// Token: 0x040002C6 RID: 710
		[FieldDescription("Enable Hardware Transforms")]
		public static readonly Guid MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS = new Guid("a634a91c-822b-41b9-a494-4de4643612b0");

		/// <summary>
		/// Contains additional format data for a media type. 
		/// </summary>
		// Token: 0x040002C7 RID: 711
		[FieldDescription("User data")]
		public static readonly Guid MF_MT_USER_DATA = new Guid("b6bc765f-4c3b-40a4-bd51-2535b66fe09d");

		/// <summary>
		/// Specifies for a media type whether each sample is independent of the other samples in the stream. 
		/// </summary>
		// Token: 0x040002C8 RID: 712
		[FieldDescription("All samples independent")]
		public static readonly Guid MF_MT_ALL_SAMPLES_INDEPENDENT = new Guid("c9173739-5e56-461c-b713-46fb995cb95f");

		/// <summary>
		/// Specifies for a media type whether the samples have a fixed size. 
		/// </summary>
		// Token: 0x040002C9 RID: 713
		[FieldDescription("Fixed size samples")]
		public static readonly Guid MF_MT_FIXED_SIZE_SAMPLES = new Guid("b8ebefaf-b718-4e04-b0a9-116775e3321b");

		/// <summary>
		/// Contains a DirectShow format GUID for a media type. 
		/// </summary>
		// Token: 0x040002CA RID: 714
		[FieldDescription("DirectShow Format Guid")]
		public static readonly Guid MF_MT_AM_FORMAT_TYPE = new Guid("73d1072d-1870-4174-a063-29ff4ff6c11e");

		/// <summary>
		/// Specifies the preferred legacy format structure to use when converting an audio media type. 
		/// </summary>
		// Token: 0x040002CB RID: 715
		[FieldDescription("Preferred legacy format structure")]
		public static readonly Guid MF_MT_AUDIO_PREFER_WAVEFORMATEX = new Guid("a901aaba-e037-458a-bdf6-545be2074042");

		/// <summary>
		/// Specifies for a media type whether the media data is compressed. 
		/// </summary>
		// Token: 0x040002CC RID: 716
		[FieldDescription("Is Compressed")]
		public static readonly Guid MF_MT_COMPRESSED = new Guid("3afd0cee-18f2-4ba5-a110-8bea502e1f92");

		/// <summary>
		/// Approximate data rate of the video stream, in bits per second, for a video media type. 
		/// </summary>
		// Token: 0x040002CD RID: 717
		[FieldDescription("Average bitrate")]
		public static readonly Guid MF_MT_AVG_BITRATE = new Guid("20332624-fb0d-4d9e-bd0d-cbf6786c102e");

		/// <summary>
		/// Specifies the payload type of an Advanced Audio Coding (AAC) stream.
		/// 0 - The stream contains raw_data_block elements only
		/// 1 - Audio Data Transport Stream (ADTS). The stream contains an adts_sequence, as defined by MPEG-2.
		/// 2 - Audio Data Interchange Format (ADIF). The stream contains an adif_sequence, as defined by MPEG-2.
		/// 3 - The stream contains an MPEG-4 audio transport stream with a synchronization layer (LOAS) and a multiplex layer (LATM).
		/// </summary>
		// Token: 0x040002CE RID: 718
		[FieldDescription("AAC payload type")]
		public static readonly Guid MF_MT_AAC_PAYLOAD_TYPE = new Guid("bfbabe79-7434-4d1c-94f0-72a3b9e17188");

		/// <summary>
		/// Specifies the audio profile and level of an Advanced Audio Coding (AAC) stream, as defined by ISO/IEC 14496-3.
		/// </summary>
		// Token: 0x040002CF RID: 719
		[FieldDescription("AAC Audio Profile Level Indication")]
		public static readonly Guid MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION = new Guid("7632f0e6-9538-4d61-acda-ea29c8c14456");
	}
}
