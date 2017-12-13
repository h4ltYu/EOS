using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>
	/// Represents a signed mixer control
	/// </summary>
	// Token: 0x02000112 RID: 274
	public class SignedMixerControl : MixerControl
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00013C5E File Offset: 0x00011E5E
		internal SignedMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		/// <summary>
		/// Gets details for this contrl
		/// </summary>
		// Token: 0x0600060D RID: 1549 RVA: 0x00013C95 File Offset: 0x00011E95
		protected override void GetDetails(IntPtr pDetails)
		{
			this.signedDetails = (MixerInterop.MIXERCONTROLDETAILS_SIGNED)Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_SIGNED));
		}

		/// <summary>
		/// The value of the control
		/// </summary>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00013CBC File Offset: 0x00011EBC
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00013CD0 File Offset: 0x00011ED0
		public int Value
		{
			get
			{
				base.GetControlDetails();
				return this.signedDetails.lValue;
			}
			set
			{
				this.signedDetails.lValue = value;
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf(this.signedDetails));
				Marshal.StructureToPtr(this.signedDetails, this.mixerControlDetails.paDetails, false);
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		/// <summary>
		/// Minimum value for this control
		/// </summary>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00013D56 File Offset: 0x00011F56
		public int MinValue
		{
			get
			{
				return this.mixerControl.Bounds.minimum;
			}
		}

		/// <summary>
		/// Maximum value for this control
		/// </summary>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00013D68 File Offset: 0x00011F68
		public int MaxValue
		{
			get
			{
				return this.mixerControl.Bounds.maximum;
			}
		}

		/// <summary>
		/// Value of the control represented as a percentage
		/// </summary>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00013D7A File Offset: 0x00011F7A
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public double Percent
		{
			get
			{
				return 100.0 * (double)(this.Value - this.MinValue) / (double)(this.MaxValue - this.MinValue);
			}
			set
			{
				this.Value = (int)((double)this.MinValue + value / 100.0 * (double)(this.MaxValue - this.MinValue));
			}
		}

		/// <summary>
		/// String Representation for debugging purposes
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000614 RID: 1556 RVA: 0x00013DCE File Offset: 0x00011FCE
		public override string ToString()
		{
			return string.Format("{0} {1}%", base.ToString(), this.Percent);
		}

		// Token: 0x040006C3 RID: 1731
		private MixerInterop.MIXERCONTROLDETAILS_SIGNED signedDetails;
	}
}
