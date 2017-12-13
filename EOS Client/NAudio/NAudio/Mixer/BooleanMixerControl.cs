using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>
	/// Boolean mixer control
	/// </summary>
	// Token: 0x020000FA RID: 250
	public class BooleanMixerControl : MixerControl
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0001335B File Offset: 0x0001155B
		internal BooleanMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		/// <summary>
		/// Gets the details for this control
		/// </summary>
		/// <param name="pDetails">memory pointer</param>
		// Token: 0x060005DE RID: 1502 RVA: 0x00013392 File Offset: 0x00011592
		protected override void GetDetails(IntPtr pDetails)
		{
			this.boolDetails = (MixerInterop.MIXERCONTROLDETAILS_BOOLEAN)Marshal.PtrToStructure(pDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
		}

		/// <summary>
		/// The current value of the control
		/// </summary>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000133AF File Offset: 0x000115AF
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x000133C8 File Offset: 0x000115C8
		public bool Value
		{
			get
			{
				base.GetControlDetails();
				return this.boolDetails.fValue == 1;
			}
			set
			{
				this.boolDetails.fValue = (value ? 1 : 0);
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf(this.boolDetails));
				Marshal.StructureToPtr(this.boolDetails, this.mixerControlDetails.paDetails, false);
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		// Token: 0x0400060B RID: 1547
		private MixerInterop.MIXERCONTROLDETAILS_BOOLEAN boolDetails;
	}
}
