using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// Custom Mixer control
	/// </summary>
	// Token: 0x020000FB RID: 251
	public class CustomMixerControl : MixerControl
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00013454 File Offset: 0x00011654
		internal CustomMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		/// <summary>
		/// Get the data for this custom control
		/// </summary>
		/// <param name="pDetails">pointer to memory to receive data</param>
		// Token: 0x060005E2 RID: 1506 RVA: 0x0001348B File Offset: 0x0001168B
		protected override void GetDetails(IntPtr pDetails)
		{
		}
	}
}
