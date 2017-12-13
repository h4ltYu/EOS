using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// Mixer Interop Flags
	/// </summary>
	// Token: 0x02000103 RID: 259
	[Flags]
	public enum MixerFlags
	{
		/// <summary>
		/// MIXER_OBJECTF_HANDLE 	= 0x80000000;
		/// </summary>
		// Token: 0x0400064D RID: 1613
		Handle = -2147483648,
		/// <summary>
		/// MIXER_OBJECTF_MIXER 	= 0x00000000;
		/// </summary>
		// Token: 0x0400064E RID: 1614
		Mixer = 0,
		/// <summary>
		/// MIXER_OBJECTF_HMIXER
		/// </summary>
		// Token: 0x0400064F RID: 1615
		MixerHandle = -2147483648,
		/// <summary>
		/// MIXER_OBJECTF_WAVEOUT
		/// </summary>
		// Token: 0x04000650 RID: 1616
		WaveOut = 268435456,
		/// <summary>
		/// MIXER_OBJECTF_HWAVEOUT
		/// </summary>
		// Token: 0x04000651 RID: 1617
		WaveOutHandle = -1879048192,
		/// <summary>
		/// MIXER_OBJECTF_WAVEIN
		/// </summary>
		// Token: 0x04000652 RID: 1618
		WaveIn = 536870912,
		/// <summary>
		/// MIXER_OBJECTF_HWAVEIN
		/// </summary>
		// Token: 0x04000653 RID: 1619
		WaveInHandle = -1610612736,
		/// <summary>
		/// MIXER_OBJECTF_MIDIOUT
		/// </summary>
		// Token: 0x04000654 RID: 1620
		MidiOut = 805306368,
		/// <summary>
		/// MIXER_OBJECTF_HMIDIOUT
		/// </summary>
		// Token: 0x04000655 RID: 1621
		MidiOutHandle = -1342177280,
		/// <summary>
		/// MIXER_OBJECTF_MIDIIN
		/// </summary>
		// Token: 0x04000656 RID: 1622
		MidiIn = 1073741824,
		/// <summary>
		/// MIXER_OBJECTF_HMIDIIN
		/// </summary>
		// Token: 0x04000657 RID: 1623
		MidiInHandle = -1073741824,
		/// <summary>
		/// MIXER_OBJECTF_AUX
		/// </summary>
		// Token: 0x04000658 RID: 1624
		Aux = 1342177280,
		/// <summary>
		/// MIXER_GETCONTROLDETAILSF_VALUE      	= 0x00000000;
		/// MIXER_SETCONTROLDETAILSF_VALUE      	= 0x00000000;
		/// </summary>
		// Token: 0x04000659 RID: 1625
		Value = 0,
		/// <summary>
		/// MIXER_GETCONTROLDETAILSF_LISTTEXT   	= 0x00000001;
		/// MIXER_SETCONTROLDETAILSF_LISTTEXT   	= 0x00000001;
		/// </summary>
		// Token: 0x0400065A RID: 1626
		ListText = 1,
		/// <summary>
		/// MIXER_GETCONTROLDETAILSF_QUERYMASK  	= 0x0000000F;
		/// MIXER_SETCONTROLDETAILSF_QUERYMASK  	= 0x0000000F;
		/// MIXER_GETLINECONTROLSF_QUERYMASK    	= 0x0000000F;
		/// </summary>
		// Token: 0x0400065B RID: 1627
		QueryMask = 15,
		/// <summary>
		/// MIXER_GETLINECONTROLSF_ALL          	= 0x00000000;
		/// </summary>
		// Token: 0x0400065C RID: 1628
		All = 0,
		/// <summary>
		/// MIXER_GETLINECONTROLSF_ONEBYID      	= 0x00000001;
		/// </summary>
		// Token: 0x0400065D RID: 1629
		OneById = 1,
		/// <summary>
		/// MIXER_GETLINECONTROLSF_ONEBYTYPE    	= 0x00000002;
		/// </summary>
		// Token: 0x0400065E RID: 1630
		OneByType = 2,
		/// <summary>
		/// MIXER_GETLINEINFOF_DESTINATION      	= 0x00000000;
		/// </summary>
		// Token: 0x0400065F RID: 1631
		GetLineInfoOfDestination = 0,
		/// <summary>
		/// MIXER_GETLINEINFOF_SOURCE           	= 0x00000001;
		/// </summary>
		// Token: 0x04000660 RID: 1632
		GetLineInfoOfSource = 1,
		/// <summary>
		/// MIXER_GETLINEINFOF_LINEID           	= 0x00000002;
		/// </summary>
		// Token: 0x04000661 RID: 1633
		GetLineInfoOfLineId = 2,
		/// <summary>
		/// MIXER_GETLINEINFOF_COMPONENTTYPE    	= 0x00000003;
		/// </summary>
		// Token: 0x04000662 RID: 1634
		GetLineInfoOfComponentType = 3,
		/// <summary>
		/// MIXER_GETLINEINFOF_TARGETTYPE       	= 0x00000004;
		/// </summary>
		// Token: 0x04000663 RID: 1635
		GetLineInfoOfTargetType = 4,
		/// <summary>
		/// MIXER_GETLINEINFOF_QUERYMASK        	= 0x0000000F;
		/// </summary>
		// Token: 0x04000664 RID: 1636
		GetLineInfoOfQueryMask = 15
	}
}
