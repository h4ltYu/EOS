using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// Mixer Line Component type enumeration
	/// </summary>
	// Token: 0x02000111 RID: 273
	public enum MixerLineComponentType
	{
		/// <summary>
		/// Audio line is a destination that cannot be defined by one of the standard component types. A mixer device is required to use this component type for line component types that have not been defined by Microsoft Corporation.
		/// MIXERLINE_COMPONENTTYPE_DST_UNDEFINED
		/// </summary>
		// Token: 0x040006AF RID: 1711
		DestinationUndefined,
		/// <summary>
		/// Audio line is a digital destination (for example, digital input to a DAT or CD audio device).
		/// MIXERLINE_COMPONENTTYPE_DST_DIGITAL 
		/// </summary>
		// Token: 0x040006B0 RID: 1712
		DestinationDigital,
		/// <summary>
		/// Audio line is a line level destination (for example, line level input from a CD audio device) that will be the final recording source for the analog-to-digital converter (ADC). Because most audio cards for personal computers provide some sort of gain for the recording audio source line, the mixer device will use the MIXERLINE_COMPONENTTYPE_DST_WAVEIN type.
		/// MIXERLINE_COMPONENTTYPE_DST_LINE
		/// </summary>
		// Token: 0x040006B1 RID: 1713
		DestinationLine,
		/// <summary>
		/// Audio line is a destination used for a monitor.
		/// MIXERLINE_COMPONENTTYPE_DST_MONITOR
		/// </summary>
		// Token: 0x040006B2 RID: 1714
		DestinationMonitor,
		/// <summary>
		/// Audio line is an adjustable (gain and/or attenuation) destination intended to drive speakers. This is the typical component type for the audio output of audio cards for personal computers.
		/// MIXERLINE_COMPONENTTYPE_DST_SPEAKERS
		/// </summary>
		// Token: 0x040006B3 RID: 1715
		DestinationSpeakers,
		/// <summary>
		/// Audio line is an adjustable (gain and/or attenuation) destination intended to drive headphones. Most audio cards use the same audio destination line for speakers and headphones, in which case the mixer device simply uses the MIXERLINE_COMPONENTTYPE_DST_SPEAKERS type.
		/// MIXERLINE_COMPONENTTYPE_DST_HEADPHONES
		/// </summary>
		// Token: 0x040006B4 RID: 1716
		DestinationHeadphones,
		/// <summary>
		/// Audio line is a destination that will be routed to a telephone line.
		/// MIXERLINE_COMPONENTTYPE_DST_TELEPHONE
		/// </summary>
		// Token: 0x040006B5 RID: 1717
		DestinationTelephone,
		/// <summary>
		/// Audio line is a destination that will be the final recording source for the waveform-audio input (ADC). This line typically provides some sort of gain or attenuation. This is the typical component type for the recording line of most audio cards for personal computers.
		/// MIXERLINE_COMPONENTTYPE_DST_WAVEIN
		/// </summary>
		// Token: 0x040006B6 RID: 1718
		DestinationWaveIn,
		/// <summary>
		/// Audio line is a destination that will be the final recording source for voice input. This component type is exactly like MIXERLINE_COMPONENTTYPE_DST_WAVEIN but is intended specifically for settings used during voice recording/recognition. Support for this line is optional for a mixer device. Many mixer devices provide only MIXERLINE_COMPONENTTYPE_DST_WAVEIN.
		/// MIXERLINE_COMPONENTTYPE_DST_VOICEIN
		/// </summary>
		// Token: 0x040006B7 RID: 1719
		DestinationVoiceIn,
		/// <summary>
		/// Audio line is a source that cannot be defined by one of the standard component types. A mixer device is required to use this component type for line component types that have not been defined by Microsoft Corporation.
		/// MIXERLINE_COMPONENTTYPE_SRC_UNDEFINED
		/// </summary>
		// Token: 0x040006B8 RID: 1720
		SourceUndefined = 4096,
		/// <summary>
		/// Audio line is a digital source (for example, digital output from a DAT or audio CD).
		/// MIXERLINE_COMPONENTTYPE_SRC_DIGITAL
		/// </summary>
		// Token: 0x040006B9 RID: 1721
		SourceDigital,
		/// <summary>
		/// Audio line is a line-level source (for example, line-level input from an external stereo) that can be used as an optional recording source. Because most audio cards for personal computers provide some sort of gain for the recording source line, the mixer device will use the MIXERLINE_COMPONENTTYPE_SRC_AUXILIARY type.
		/// MIXERLINE_COMPONENTTYPE_SRC_LINE
		/// </summary>
		// Token: 0x040006BA RID: 1722
		SourceLine,
		/// <summary>
		/// Audio line is a microphone recording source. Most audio cards for personal computers provide at least two types of recording sources: an auxiliary audio line and microphone input. A microphone audio line typically provides some sort of gain. Audio cards that use a single input for use with a microphone or auxiliary audio line should use the MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE component type.
		/// MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE
		/// </summary>
		// Token: 0x040006BB RID: 1723
		SourceMicrophone,
		/// <summary>
		/// Audio line is a source originating from the output of an internal synthesizer. Most audio cards for personal computers provide some sort of MIDI synthesizer (for example, an Adlib®-compatible or OPL/3 FM synthesizer).
		/// MIXERLINE_COMPONENTTYPE_SRC_SYNTHESIZER
		/// </summary>
		// Token: 0x040006BC RID: 1724
		SourceSynthesizer,
		/// <summary>
		/// Audio line is a source originating from the output of an internal audio CD. This component type is provided for audio cards that provide an audio source line intended to be connected to an audio CD (or CD-ROM playing an audio CD).
		/// MIXERLINE_COMPONENTTYPE_SRC_COMPACTDISC
		/// </summary>
		// Token: 0x040006BD RID: 1725
		SourceCompactDisc,
		/// <summary>
		/// Audio line is a source originating from an incoming telephone line.
		/// MIXERLINE_COMPONENTTYPE_SRC_TELEPHONE
		/// </summary>
		// Token: 0x040006BE RID: 1726
		SourceTelephone,
		/// <summary>
		/// Audio line is a source originating from personal computer speaker. Several audio cards for personal computers provide the ability to mix what would typically be played on the internal speaker with the output of an audio card. Some audio cards support the ability to use this output as a recording source.
		/// MIXERLINE_COMPONENTTYPE_SRC_PCSPEAKER
		/// </summary>
		// Token: 0x040006BF RID: 1727
		SourcePcSpeaker,
		/// <summary>
		/// Audio line is a source originating from the waveform-audio output digital-to-analog converter (DAC). Most audio cards for personal computers provide this component type as a source to the MIXERLINE_COMPONENTTYPE_DST_SPEAKERS destination. Some cards also allow this source to be routed to the MIXERLINE_COMPONENTTYPE_DST_WAVEIN destination.
		/// MIXERLINE_COMPONENTTYPE_SRC_WAVEOUT
		/// </summary>
		// Token: 0x040006C0 RID: 1728
		SourceWaveOut,
		/// <summary>
		/// Audio line is a source originating from the auxiliary audio line. This line type is intended as a source with gain or attenuation that can be routed to the MIXERLINE_COMPONENTTYPE_DST_SPEAKERS destination and/or recorded from the MIXERLINE_COMPONENTTYPE_DST_WAVEIN destination.
		/// MIXERLINE_COMPONENTTYPE_SRC_AUXILIARY
		/// </summary>
		// Token: 0x040006C1 RID: 1729
		SourceAuxiliary,
		/// <summary>
		/// Audio line is an analog source (for example, analog output from a video-cassette tape).
		/// MIXERLINE_COMPONENTTYPE_SRC_ANALOG
		/// </summary>
		// Token: 0x040006C2 RID: 1730
		SourceAnalog
	}
}
