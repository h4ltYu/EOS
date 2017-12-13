using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>
	/// Represents an unsigned mixer control
	/// </summary>
	// Token: 0x02000113 RID: 275
	public class UnsignedMixerControl : MixerControl
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00013DEB File Offset: 0x00011FEB
		internal UnsignedMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
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
		// Token: 0x06000616 RID: 1558 RVA: 0x00013E24 File Offset: 0x00012024
		protected override void GetDetails(IntPtr pDetails)
		{
			this.unsignedDetails = new MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[this.nChannels];
			for (int i = 0; i < this.nChannels; i++)
			{
				this.unsignedDetails[i] = (MixerInterop.MIXERCONTROLDETAILS_UNSIGNED)Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
			}
		}

		/// <summary>
		/// The control value
		/// </summary>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00013E83 File Offset: 0x00012083
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00013E9C File Offset: 0x0001209C
		public uint Value
		{
			get
			{
				base.GetControlDetails();
				return this.unsignedDetails[0].dwValue;
			}
			set
			{
				int num = Marshal.SizeOf(this.unsignedDetails[0]);
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(num * this.nChannels);
				for (int i = 0; i < this.nChannels; i++)
				{
					this.unsignedDetails[i].dwValue = value;
					long value2 = this.mixerControlDetails.paDetails.ToInt64() + (long)(num * i);
					Marshal.StructureToPtr(this.unsignedDetails[i], (IntPtr)value2, false);
				}
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		/// <summary>
		/// The control's minimum value
		/// </summary>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00013F69 File Offset: 0x00012169
		public uint MinValue
		{
			get
			{
				return (uint)this.mixerControl.Bounds.minimum;
			}
		}

		/// <summary>
		/// The control's maximum value
		/// </summary>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00013F7B File Offset: 0x0001217B
		public uint MaxValue
		{
			get
			{
				return (uint)this.mixerControl.Bounds.maximum;
			}
		}

		/// <summary>
		/// Value of the control represented as a percentage
		/// </summary>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00013F8D File Offset: 0x0001218D
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00013FB8 File Offset: 0x000121B8
		public double Percent
		{
			get
			{
				return 100.0 * (this.Value - this.MinValue) / (this.MaxValue - this.MinValue);
			}
			set
			{
				this.Value = (uint)(this.MinValue + value / 100.0 * (this.MaxValue - this.MinValue));
			}
		}

		/// <summary>
		/// String Representation for debugging purposes
		/// </summary>
		// Token: 0x0600061D RID: 1565 RVA: 0x00013FE5 File Offset: 0x000121E5
		public override string ToString()
		{
			return string.Format("{0} {1}%", base.ToString(), this.Percent);
		}

		// Token: 0x040006C4 RID: 1732
		private MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[] unsignedDetails;
	}
}
