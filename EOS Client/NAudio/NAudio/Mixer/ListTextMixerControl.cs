using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// List text mixer control
	/// </summary>
	// Token: 0x020000FC RID: 252
	public class ListTextMixerControl : MixerControl
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001348D File Offset: 0x0001168D
		internal ListTextMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		/// <summary>
		/// Get the details for this control
		/// </summary>
		/// <param name="pDetails">Memory location to read to</param>
		// Token: 0x060005E4 RID: 1508 RVA: 0x000134C4 File Offset: 0x000116C4
		protected override void GetDetails(IntPtr pDetails)
		{
		}
	}
}
