using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Summary description for WaveFormatEncoding.
	/// </summary>
	// Token: 0x020001AB RID: 427
	public enum WaveFormatEncoding : ushort
	{
		/// <summary>WAVE_FORMAT_UNKNOWN,	Microsoft Corporation</summary>
		// Token: 0x040009D4 RID: 2516
		Unknown,
		/// <summary>WAVE_FORMAT_PCM		Microsoft Corporation</summary>
		// Token: 0x040009D5 RID: 2517
		Pcm,
		/// <summary>WAVE_FORMAT_ADPCM		Microsoft Corporation</summary>
		// Token: 0x040009D6 RID: 2518
		Adpcm,
		/// <summary>WAVE_FORMAT_IEEE_FLOAT Microsoft Corporation</summary>
		// Token: 0x040009D7 RID: 2519
		IeeeFloat,
		/// <summary>WAVE_FORMAT_VSELP		Compaq Computer Corp.</summary>
		// Token: 0x040009D8 RID: 2520
		Vselp,
		/// <summary>WAVE_FORMAT_IBM_CVSD	IBM Corporation</summary>
		// Token: 0x040009D9 RID: 2521
		IbmCvsd,
		/// <summary>WAVE_FORMAT_ALAW		Microsoft Corporation</summary>
		// Token: 0x040009DA RID: 2522
		ALaw,
		/// <summary>WAVE_FORMAT_MULAW		Microsoft Corporation</summary>
		// Token: 0x040009DB RID: 2523
		MuLaw,
		/// <summary>WAVE_FORMAT_DTS		Microsoft Corporation</summary>
		// Token: 0x040009DC RID: 2524
		Dts,
		/// <summary>WAVE_FORMAT_DRM		Microsoft Corporation</summary>
		// Token: 0x040009DD RID: 2525
		Drm,
		/// <summary>WAVE_FORMAT_WMAVOICE9 </summary>
		// Token: 0x040009DE RID: 2526
		WmaVoice9,
		/// <summary>WAVE_FORMAT_OKI_ADPCM	OKI</summary>
		// Token: 0x040009DF RID: 2527
		OkiAdpcm = 16,
		/// <summary>WAVE_FORMAT_DVI_ADPCM	Intel Corporation</summary>
		// Token: 0x040009E0 RID: 2528
		DviAdpcm,
		/// <summary>WAVE_FORMAT_IMA_ADPCM  Intel Corporation</summary>
		// Token: 0x040009E1 RID: 2529
		ImaAdpcm = 17,
		/// <summary>WAVE_FORMAT_MEDIASPACE_ADPCM Videologic</summary>
		// Token: 0x040009E2 RID: 2530
		MediaspaceAdpcm,
		/// <summary>WAVE_FORMAT_SIERRA_ADPCM Sierra Semiconductor Corp </summary>
		// Token: 0x040009E3 RID: 2531
		SierraAdpcm,
		/// <summary>WAVE_FORMAT_G723_ADPCM Antex Electronics Corporation </summary>
		// Token: 0x040009E4 RID: 2532
		G723Adpcm,
		/// <summary>WAVE_FORMAT_DIGISTD DSP Solutions, Inc.</summary>
		// Token: 0x040009E5 RID: 2533
		DigiStd,
		/// <summary>WAVE_FORMAT_DIGIFIX DSP Solutions, Inc.</summary>
		// Token: 0x040009E6 RID: 2534
		DigiFix,
		/// <summary>WAVE_FORMAT_DIALOGIC_OKI_ADPCM Dialogic Corporation</summary>
		// Token: 0x040009E7 RID: 2535
		DialogicOkiAdpcm,
		/// <summary>WAVE_FORMAT_MEDIAVISION_ADPCM Media Vision, Inc.</summary>
		// Token: 0x040009E8 RID: 2536
		MediaVisionAdpcm,
		/// <summary>WAVE_FORMAT_CU_CODEC Hewlett-Packard Company </summary>
		// Token: 0x040009E9 RID: 2537
		CUCodec,
		/// <summary>WAVE_FORMAT_YAMAHA_ADPCM Yamaha Corporation of America</summary>
		// Token: 0x040009EA RID: 2538
		YamahaAdpcm = 32,
		/// <summary>WAVE_FORMAT_SONARC Speech Compression</summary>
		// Token: 0x040009EB RID: 2539
		SonarC,
		/// <summary>WAVE_FORMAT_DSPGROUP_TRUESPEECH DSP Group, Inc </summary>
		// Token: 0x040009EC RID: 2540
		DspGroupTrueSpeech,
		/// <summary>WAVE_FORMAT_ECHOSC1 Echo Speech Corporation</summary>
		// Token: 0x040009ED RID: 2541
		EchoSpeechCorporation1,
		/// <summary>WAVE_FORMAT_AUDIOFILE_AF36, Virtual Music, Inc.</summary>
		// Token: 0x040009EE RID: 2542
		AudioFileAf36,
		/// <summary>WAVE_FORMAT_APTX Audio Processing Technology</summary>
		// Token: 0x040009EF RID: 2543
		Aptx,
		/// <summary>WAVE_FORMAT_AUDIOFILE_AF10, Virtual Music, Inc.</summary>
		// Token: 0x040009F0 RID: 2544
		AudioFileAf10,
		/// <summary>WAVE_FORMAT_PROSODY_1612, Aculab plc</summary>
		// Token: 0x040009F1 RID: 2545
		Prosody1612,
		/// <summary>WAVE_FORMAT_LRC, Merging Technologies S.A. </summary>
		// Token: 0x040009F2 RID: 2546
		Lrc,
		/// <summary>WAVE_FORMAT_DOLBY_AC2, Dolby Laboratories</summary>
		// Token: 0x040009F3 RID: 2547
		DolbyAc2 = 48,
		/// <summary>WAVE_FORMAT_GSM610, Microsoft Corporation</summary>
		// Token: 0x040009F4 RID: 2548
		Gsm610,
		/// <summary>WAVE_FORMAT_MSNAUDIO, Microsoft Corporation</summary>
		// Token: 0x040009F5 RID: 2549
		MsnAudio,
		/// <summary>WAVE_FORMAT_ANTEX_ADPCME, Antex Electronics Corporation</summary>
		// Token: 0x040009F6 RID: 2550
		AntexAdpcme,
		/// <summary>WAVE_FORMAT_CONTROL_RES_VQLPC, Control Resources Limited </summary>
		// Token: 0x040009F7 RID: 2551
		ControlResVqlpc,
		/// <summary>WAVE_FORMAT_DIGIREAL, DSP Solutions, Inc. </summary>
		// Token: 0x040009F8 RID: 2552
		DigiReal,
		/// <summary>WAVE_FORMAT_DIGIADPCM, DSP Solutions, Inc.</summary>
		// Token: 0x040009F9 RID: 2553
		DigiAdpcm,
		/// <summary>WAVE_FORMAT_CONTROL_RES_CR10, Control Resources Limited</summary>
		// Token: 0x040009FA RID: 2554
		ControlResCr10,
		/// <summary></summary>
		// Token: 0x040009FB RID: 2555
		WAVE_FORMAT_NMS_VBXADPCM,
		/// <summary></summary>
		// Token: 0x040009FC RID: 2556
		WAVE_FORMAT_CS_IMAADPCM,
		/// <summary></summary>
		// Token: 0x040009FD RID: 2557
		WAVE_FORMAT_ECHOSC3,
		/// <summary></summary>
		// Token: 0x040009FE RID: 2558
		WAVE_FORMAT_ROCKWELL_ADPCM,
		/// <summary></summary>
		// Token: 0x040009FF RID: 2559
		WAVE_FORMAT_ROCKWELL_DIGITALK,
		/// <summary></summary>
		// Token: 0x04000A00 RID: 2560
		WAVE_FORMAT_XEBEC,
		/// <summary></summary>
		// Token: 0x04000A01 RID: 2561
		WAVE_FORMAT_G721_ADPCM = 64,
		/// <summary></summary>
		// Token: 0x04000A02 RID: 2562
		WAVE_FORMAT_G728_CELP,
		/// <summary></summary>
		// Token: 0x04000A03 RID: 2563
		WAVE_FORMAT_MSG723,
		/// <summary>WAVE_FORMAT_MPEG, Microsoft Corporation </summary>
		// Token: 0x04000A04 RID: 2564
		Mpeg = 80,
		/// <summary></summary>
		// Token: 0x04000A05 RID: 2565
		WAVE_FORMAT_RT24 = 82,
		/// <summary></summary>
		// Token: 0x04000A06 RID: 2566
		WAVE_FORMAT_PAC,
		/// <summary>WAVE_FORMAT_MPEGLAYER3, ISO/MPEG Layer3 Format Tag </summary>
		// Token: 0x04000A07 RID: 2567
		MpegLayer3 = 85,
		/// <summary></summary>
		// Token: 0x04000A08 RID: 2568
		WAVE_FORMAT_LUCENT_G723 = 89,
		/// <summary></summary>
		// Token: 0x04000A09 RID: 2569
		WAVE_FORMAT_CIRRUS = 96,
		/// <summary></summary>
		// Token: 0x04000A0A RID: 2570
		WAVE_FORMAT_ESPCM,
		/// <summary></summary>
		// Token: 0x04000A0B RID: 2571
		WAVE_FORMAT_VOXWARE,
		/// <summary></summary>
		// Token: 0x04000A0C RID: 2572
		WAVE_FORMAT_CANOPUS_ATRAC,
		/// <summary></summary>
		// Token: 0x04000A0D RID: 2573
		WAVE_FORMAT_G726_ADPCM,
		/// <summary></summary>
		// Token: 0x04000A0E RID: 2574
		WAVE_FORMAT_G722_ADPCM,
		/// <summary></summary>
		// Token: 0x04000A0F RID: 2575
		WAVE_FORMAT_DSAT_DISPLAY = 103,
		/// <summary></summary>
		// Token: 0x04000A10 RID: 2576
		WAVE_FORMAT_VOXWARE_BYTE_ALIGNED = 105,
		/// <summary></summary>
		// Token: 0x04000A11 RID: 2577
		WAVE_FORMAT_VOXWARE_AC8 = 112,
		/// <summary></summary>
		// Token: 0x04000A12 RID: 2578
		WAVE_FORMAT_VOXWARE_AC10,
		/// <summary></summary>
		// Token: 0x04000A13 RID: 2579
		WAVE_FORMAT_VOXWARE_AC16,
		/// <summary></summary>
		// Token: 0x04000A14 RID: 2580
		WAVE_FORMAT_VOXWARE_AC20,
		/// <summary></summary>
		// Token: 0x04000A15 RID: 2581
		WAVE_FORMAT_VOXWARE_RT24,
		/// <summary></summary>
		// Token: 0x04000A16 RID: 2582
		WAVE_FORMAT_VOXWARE_RT29,
		/// <summary></summary>
		// Token: 0x04000A17 RID: 2583
		WAVE_FORMAT_VOXWARE_RT29HW,
		/// <summary></summary>
		// Token: 0x04000A18 RID: 2584
		WAVE_FORMAT_VOXWARE_VR12,
		/// <summary></summary>
		// Token: 0x04000A19 RID: 2585
		WAVE_FORMAT_VOXWARE_VR18,
		/// <summary></summary>
		// Token: 0x04000A1A RID: 2586
		WAVE_FORMAT_VOXWARE_TQ40,
		/// <summary></summary>
		// Token: 0x04000A1B RID: 2587
		WAVE_FORMAT_SOFTSOUND = 128,
		/// <summary></summary>
		// Token: 0x04000A1C RID: 2588
		WAVE_FORMAT_VOXWARE_TQ60,
		/// <summary></summary>
		// Token: 0x04000A1D RID: 2589
		WAVE_FORMAT_MSRT24,
		/// <summary></summary>
		// Token: 0x04000A1E RID: 2590
		WAVE_FORMAT_G729A,
		/// <summary></summary>
		// Token: 0x04000A1F RID: 2591
		WAVE_FORMAT_MVI_MVI2,
		/// <summary></summary>
		// Token: 0x04000A20 RID: 2592
		WAVE_FORMAT_DF_G726,
		/// <summary></summary>
		// Token: 0x04000A21 RID: 2593
		WAVE_FORMAT_DF_GSM610,
		/// <summary></summary>
		// Token: 0x04000A22 RID: 2594
		WAVE_FORMAT_ISIAUDIO = 136,
		/// <summary></summary>
		// Token: 0x04000A23 RID: 2595
		WAVE_FORMAT_ONLIVE,
		/// <summary></summary>
		// Token: 0x04000A24 RID: 2596
		WAVE_FORMAT_SBC24 = 145,
		/// <summary></summary>
		// Token: 0x04000A25 RID: 2597
		WAVE_FORMAT_DOLBY_AC3_SPDIF,
		/// <summary></summary>
		// Token: 0x04000A26 RID: 2598
		WAVE_FORMAT_MEDIASONIC_G723,
		/// <summary></summary>
		// Token: 0x04000A27 RID: 2599
		WAVE_FORMAT_PROSODY_8KBPS,
		/// <summary></summary>
		// Token: 0x04000A28 RID: 2600
		WAVE_FORMAT_ZYXEL_ADPCM = 151,
		/// <summary></summary>
		// Token: 0x04000A29 RID: 2601
		WAVE_FORMAT_PHILIPS_LPCBB,
		/// <summary></summary>
		// Token: 0x04000A2A RID: 2602
		WAVE_FORMAT_PACKED,
		/// <summary></summary>
		// Token: 0x04000A2B RID: 2603
		WAVE_FORMAT_MALDEN_PHONYTALK = 160,
		/// <summary>WAVE_FORMAT_GSM</summary>
		// Token: 0x04000A2C RID: 2604
		Gsm,
		/// <summary>WAVE_FORMAT_G729</summary>
		// Token: 0x04000A2D RID: 2605
		G729,
		/// <summary>WAVE_FORMAT_G723</summary>
		// Token: 0x04000A2E RID: 2606
		G723,
		/// <summary>WAVE_FORMAT_ACELP</summary>
		// Token: 0x04000A2F RID: 2607
		Acelp,
		/// <summary>
		/// WAVE_FORMAT_RAW_AAC1
		/// </summary>
		// Token: 0x04000A30 RID: 2608
		RawAac = 255,
		/// <summary></summary>
		// Token: 0x04000A31 RID: 2609
		WAVE_FORMAT_RHETOREX_ADPCM,
		/// <summary></summary>
		// Token: 0x04000A32 RID: 2610
		WAVE_FORMAT_IRAT,
		/// <summary></summary>
		// Token: 0x04000A33 RID: 2611
		WAVE_FORMAT_VIVO_G723 = 273,
		/// <summary></summary>
		// Token: 0x04000A34 RID: 2612
		WAVE_FORMAT_VIVO_SIREN,
		/// <summary></summary>
		// Token: 0x04000A35 RID: 2613
		WAVE_FORMAT_DIGITAL_G723 = 291,
		/// <summary></summary>
		// Token: 0x04000A36 RID: 2614
		WAVE_FORMAT_SANYO_LD_ADPCM = 293,
		/// <summary></summary>
		// Token: 0x04000A37 RID: 2615
		WAVE_FORMAT_SIPROLAB_ACEPLNET = 304,
		/// <summary></summary>
		// Token: 0x04000A38 RID: 2616
		WAVE_FORMAT_SIPROLAB_ACELP4800,
		/// <summary></summary>
		// Token: 0x04000A39 RID: 2617
		WAVE_FORMAT_SIPROLAB_ACELP8V3,
		/// <summary></summary>
		// Token: 0x04000A3A RID: 2618
		WAVE_FORMAT_SIPROLAB_G729,
		/// <summary></summary>
		// Token: 0x04000A3B RID: 2619
		WAVE_FORMAT_SIPROLAB_G729A,
		/// <summary></summary>
		// Token: 0x04000A3C RID: 2620
		WAVE_FORMAT_SIPROLAB_KELVIN,
		/// <summary></summary>
		// Token: 0x04000A3D RID: 2621
		WAVE_FORMAT_G726ADPCM = 320,
		/// <summary></summary>
		// Token: 0x04000A3E RID: 2622
		WAVE_FORMAT_QUALCOMM_PUREVOICE = 336,
		/// <summary></summary>
		// Token: 0x04000A3F RID: 2623
		WAVE_FORMAT_QUALCOMM_HALFRATE,
		/// <summary></summary>
		// Token: 0x04000A40 RID: 2624
		WAVE_FORMAT_TUBGSM = 341,
		/// <summary></summary>
		// Token: 0x04000A41 RID: 2625
		WAVE_FORMAT_MSAUDIO1 = 352,
		/// <summary>
		/// Windows Media Audio, WAVE_FORMAT_WMAUDIO2, Microsoft Corporation
		/// </summary>
		// Token: 0x04000A42 RID: 2626
		WindowsMediaAudio,
		/// <summary>
		/// Windows Media Audio Professional WAVE_FORMAT_WMAUDIO3, Microsoft Corporation
		/// </summary>
		// Token: 0x04000A43 RID: 2627
		WindowsMediaAudioProfessional,
		/// <summary>
		/// Windows Media Audio Lossless, WAVE_FORMAT_WMAUDIO_LOSSLESS
		/// </summary>
		// Token: 0x04000A44 RID: 2628
		WindowsMediaAudioLosseless,
		/// <summary>
		/// Windows Media Audio Professional over SPDIF WAVE_FORMAT_WMASPDIF (0x0164)
		/// </summary>
		// Token: 0x04000A45 RID: 2629
		WindowsMediaAudioSpdif,
		/// <summary></summary>
		// Token: 0x04000A46 RID: 2630
		WAVE_FORMAT_UNISYS_NAP_ADPCM = 368,
		/// <summary></summary>
		// Token: 0x04000A47 RID: 2631
		WAVE_FORMAT_UNISYS_NAP_ULAW,
		/// <summary></summary>
		// Token: 0x04000A48 RID: 2632
		WAVE_FORMAT_UNISYS_NAP_ALAW,
		/// <summary></summary>
		// Token: 0x04000A49 RID: 2633
		WAVE_FORMAT_UNISYS_NAP_16K,
		/// <summary></summary>
		// Token: 0x04000A4A RID: 2634
		WAVE_FORMAT_CREATIVE_ADPCM = 512,
		/// <summary></summary>
		// Token: 0x04000A4B RID: 2635
		WAVE_FORMAT_CREATIVE_FASTSPEECH8 = 514,
		/// <summary></summary>
		// Token: 0x04000A4C RID: 2636
		WAVE_FORMAT_CREATIVE_FASTSPEECH10,
		/// <summary></summary>
		// Token: 0x04000A4D RID: 2637
		WAVE_FORMAT_UHER_ADPCM = 528,
		/// <summary></summary>
		// Token: 0x04000A4E RID: 2638
		WAVE_FORMAT_QUARTERDECK = 544,
		/// <summary></summary>
		// Token: 0x04000A4F RID: 2639
		WAVE_FORMAT_ILINK_VC = 560,
		/// <summary></summary>
		// Token: 0x04000A50 RID: 2640
		WAVE_FORMAT_RAW_SPORT = 576,
		/// <summary></summary>
		// Token: 0x04000A51 RID: 2641
		WAVE_FORMAT_ESST_AC3,
		/// <summary></summary>
		// Token: 0x04000A52 RID: 2642
		WAVE_FORMAT_IPI_HSX = 592,
		/// <summary></summary>
		// Token: 0x04000A53 RID: 2643
		WAVE_FORMAT_IPI_RPELP,
		/// <summary></summary>
		// Token: 0x04000A54 RID: 2644
		WAVE_FORMAT_CS2 = 608,
		/// <summary></summary>
		// Token: 0x04000A55 RID: 2645
		WAVE_FORMAT_SONY_SCX = 624,
		/// <summary></summary>
		// Token: 0x04000A56 RID: 2646
		WAVE_FORMAT_FM_TOWNS_SND = 768,
		/// <summary></summary>
		// Token: 0x04000A57 RID: 2647
		WAVE_FORMAT_BTV_DIGITAL = 1024,
		/// <summary></summary>
		// Token: 0x04000A58 RID: 2648
		WAVE_FORMAT_QDESIGN_MUSIC = 1104,
		/// <summary></summary>
		// Token: 0x04000A59 RID: 2649
		WAVE_FORMAT_VME_VMPCM = 1664,
		/// <summary></summary>
		// Token: 0x04000A5A RID: 2650
		WAVE_FORMAT_TPC,
		/// <summary></summary>
		// Token: 0x04000A5B RID: 2651
		WAVE_FORMAT_OLIGSM = 4096,
		/// <summary></summary>
		// Token: 0x04000A5C RID: 2652
		WAVE_FORMAT_OLIADPCM,
		/// <summary></summary>
		// Token: 0x04000A5D RID: 2653
		WAVE_FORMAT_OLICELP,
		/// <summary></summary>
		// Token: 0x04000A5E RID: 2654
		WAVE_FORMAT_OLISBC,
		/// <summary></summary>
		// Token: 0x04000A5F RID: 2655
		WAVE_FORMAT_OLIOPR,
		/// <summary></summary>
		// Token: 0x04000A60 RID: 2656
		WAVE_FORMAT_LH_CODEC = 4352,
		/// <summary></summary>
		// Token: 0x04000A61 RID: 2657
		WAVE_FORMAT_NORRIS = 5120,
		/// <summary></summary>
		// Token: 0x04000A62 RID: 2658
		WAVE_FORMAT_SOUNDSPACE_MUSICOMPRESS = 5376,
		/// <summary>
		/// Advanced Audio Coding (AAC) audio in Audio Data Transport Stream (ADTS) format.
		/// The format block is a WAVEFORMATEX structure with wFormatTag equal to WAVE_FORMAT_MPEG_ADTS_AAC.
		/// </summary>
		/// <remarks>
		/// The WAVEFORMATEX structure specifies the core AAC-LC sample rate and number of channels, 
		/// prior to applying spectral band replication (SBR) or parametric stereo (PS) tools, if present.
		/// No additional data is required after the WAVEFORMATEX structure.
		/// </remarks>
		/// <see>http://msdn.microsoft.com/en-us/library/dd317599%28VS.85%29.aspx</see>
		// Token: 0x04000A63 RID: 2659
		MPEG_ADTS_AAC = 5632,
		/// <summary></summary>
		/// <remarks>Source wmCodec.h</remarks>
		// Token: 0x04000A64 RID: 2660
		MPEG_RAW_AAC,
		/// <summary>
		/// MPEG-4 audio transport stream with a synchronization layer (LOAS) and a multiplex layer (LATM).
		/// The format block is a WAVEFORMATEX structure with wFormatTag equal to WAVE_FORMAT_MPEG_LOAS.
		/// </summary>
		/// <remarks>
		/// The WAVEFORMATEX structure specifies the core AAC-LC sample rate and number of channels, 
		/// prior to applying spectral SBR or PS tools, if present.
		/// No additional data is required after the WAVEFORMATEX structure.
		/// </remarks>
		/// <see>http://msdn.microsoft.com/en-us/library/dd317599%28VS.85%29.aspx</see>
		// Token: 0x04000A65 RID: 2661
		MPEG_LOAS,
		/// <summary>NOKIA_MPEG_ADTS_AAC</summary>
		/// <remarks>Source wmCodec.h</remarks>
		// Token: 0x04000A66 RID: 2662
		NOKIA_MPEG_ADTS_AAC = 5640,
		/// <summary>NOKIA_MPEG_RAW_AAC</summary>
		/// <remarks>Source wmCodec.h</remarks>
		// Token: 0x04000A67 RID: 2663
		NOKIA_MPEG_RAW_AAC,
		/// <summary>VODAFONE_MPEG_ADTS_AAC</summary>
		/// <remarks>Source wmCodec.h</remarks>
		// Token: 0x04000A68 RID: 2664
		VODAFONE_MPEG_ADTS_AAC,
		/// <summary>VODAFONE_MPEG_RAW_AAC</summary>
		/// <remarks>Source wmCodec.h</remarks>
		// Token: 0x04000A69 RID: 2665
		VODAFONE_MPEG_RAW_AAC,
		/// <summary>
		/// High-Efficiency Advanced Audio Coding (HE-AAC) stream.
		/// The format block is an HEAACWAVEFORMAT structure.
		/// </summary>
		/// <see>http://msdn.microsoft.com/en-us/library/dd317599%28VS.85%29.aspx</see>
		// Token: 0x04000A6A RID: 2666
		MPEG_HEAAC = 5648,
		/// <summary>WAVE_FORMAT_DVM</summary>
		// Token: 0x04000A6B RID: 2667
		WAVE_FORMAT_DVM = 8192,
		/// <summary>WAVE_FORMAT_VORBIS1 "Og" Original stream compatible</summary>
		// Token: 0x04000A6C RID: 2668
		Vorbis1 = 26447,
		/// <summary>WAVE_FORMAT_VORBIS2 "Pg" Have independent header</summary>
		// Token: 0x04000A6D RID: 2669
		Vorbis2,
		/// <summary>WAVE_FORMAT_VORBIS3 "Qg" Have no codebook header</summary>
		// Token: 0x04000A6E RID: 2670
		Vorbis3,
		/// <summary>WAVE_FORMAT_VORBIS1P "og" Original stream compatible</summary>
		// Token: 0x04000A6F RID: 2671
		Vorbis1P = 26479,
		/// <summary>WAVE_FORMAT_VORBIS2P "pg" Have independent headere</summary>
		// Token: 0x04000A70 RID: 2672
		Vorbis2P,
		/// <summary>WAVE_FORMAT_VORBIS3P "qg" Have no codebook header</summary>
		// Token: 0x04000A71 RID: 2673
		Vorbis3P,
		/// <summary>WAVE_FORMAT_EXTENSIBLE</summary>
		// Token: 0x04000A72 RID: 2674
		Extensible = 65534,
		/// <summary></summary>
		// Token: 0x04000A73 RID: 2675
		WAVE_FORMAT_DEVELOPMENT
	}
}
