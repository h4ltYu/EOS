using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>
	/// Represents a mixer control
	/// </summary>
	// Token: 0x020000F9 RID: 249
	public abstract class MixerControl
	{
		/// <summary>
		/// Gets all the mixer controls
		/// </summary>
		/// <param name="mixerHandle">Mixer Handle</param>
		/// <param name="mixerLine">Mixer Line</param>
		/// <param name="mixerHandleType">Mixer Handle Type</param>
		/// <returns></returns>
		// Token: 0x060005CB RID: 1483 RVA: 0x00012C9C File Offset: 0x00010E9C
		public static IList<MixerControl> GetMixerControls(IntPtr mixerHandle, MixerLine mixerLine, MixerFlags mixerHandleType)
		{
			List<MixerControl> list = new List<MixerControl>();
			if (mixerLine.ControlsCount > 0)
			{
				int num = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
				MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
				IntPtr intPtr = Marshal.AllocHGlobal(num * mixerLine.ControlsCount);
				mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
				mixerlinecontrols.dwLineID = mixerLine.LineId;
				mixerlinecontrols.cControls = mixerLine.ControlsCount;
				mixerlinecontrols.pamxctrl = intPtr;
				mixerlinecontrols.cbmxctrl = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
				try
				{
					MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, mixerHandleType);
					if (mmResult != MmResult.NoError)
					{
						throw new MmException(mmResult, "mixerGetLineControls");
					}
					for (int i = 0; i < mixerlinecontrols.cControls; i++)
					{
						long value = intPtr.ToInt64() + (long)(num * i);
						MixerInterop.MIXERCONTROL mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure((IntPtr)value, typeof(MixerInterop.MIXERCONTROL));
						MixerControl item = MixerControl.GetMixerControl(mixerHandle, mixerLine.LineId, mixercontrol.dwControlID, mixerLine.Channels, mixerHandleType);
						list.Add(item);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return list;
		}

		/// <summary>
		/// Gets a specified Mixer Control
		/// </summary>
		/// <param name="mixerHandle">Mixer Handle</param>
		/// <param name="nLineID">Line ID</param>
		/// <param name="controlId">Control ID</param>
		/// <param name="nChannels">Number of Channels</param>
		/// <param name="mixerFlags">Flags to use (indicates the meaning of mixerHandle)</param>
		/// <returns></returns>
		// Token: 0x060005CC RID: 1484 RVA: 0x00012DC8 File Offset: 0x00010FC8
		public static MixerControl GetMixerControl(IntPtr mixerHandle, int nLineID, int controlId, int nChannels, MixerFlags mixerFlags)
		{
			MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
			MixerInterop.MIXERCONTROL mixercontrol = default(MixerInterop.MIXERCONTROL);
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mixercontrol));
			mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
			mixerlinecontrols.cControls = 1;
			mixerlinecontrols.dwControlID = controlId;
			mixerlinecontrols.cbmxctrl = Marshal.SizeOf(mixercontrol);
			mixerlinecontrols.pamxctrl = intPtr;
			mixerlinecontrols.dwLineID = nLineID;
			MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, MixerFlags.ListText | mixerFlags);
			if (mmResult != MmResult.NoError)
			{
				Marshal.FreeCoTaskMem(intPtr);
				throw new MmException(mmResult, "mixerGetLineControls");
			}
			mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure(mixerlinecontrols.pamxctrl, typeof(MixerInterop.MIXERCONTROL));
			Marshal.FreeCoTaskMem(intPtr);
			if (MixerControl.IsControlBoolean(mixercontrol.dwControlType))
			{
				return new BooleanMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlSigned(mixercontrol.dwControlType))
			{
				return new SignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlUnsigned(mixercontrol.dwControlType))
			{
				return new UnsignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlListText(mixercontrol.dwControlType))
			{
				return new ListTextMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlCustom(mixercontrol.dwControlType))
			{
				return new CustomMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			throw new InvalidOperationException(string.Format("Unknown mixer control type {0}", mixercontrol.dwControlType));
		}

		/// <summary>
		/// Gets the control details
		/// </summary>
		// Token: 0x060005CD RID: 1485 RVA: 0x00012F18 File Offset: 0x00011118
		protected void GetControlDetails()
		{
			this.mixerControlDetails.cbStruct = Marshal.SizeOf(this.mixerControlDetails);
			this.mixerControlDetails.dwControlID = this.mixerControl.dwControlID;
			if (this.IsCustom)
			{
				this.mixerControlDetails.cChannels = 0;
			}
			else if ((this.mixerControl.fdwControl & 1u) != 0u)
			{
				this.mixerControlDetails.cChannels = 1;
			}
			else
			{
				this.mixerControlDetails.cChannels = this.nChannels;
			}
			if ((this.mixerControl.fdwControl & 2u) != 0u)
			{
				this.mixerControlDetails.hwndOwner = (IntPtr)((long)((ulong)this.mixerControl.cMultipleItems));
			}
			else if (this.IsCustom)
			{
				this.mixerControlDetails.hwndOwner = IntPtr.Zero;
			}
			else
			{
				this.mixerControlDetails.hwndOwner = IntPtr.Zero;
			}
			if (this.IsBoolean)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
			}
			else if (this.IsListText)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_LISTTEXT));
			}
			else if (this.IsSigned)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_SIGNED));
			}
			else if (this.IsUnsigned)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
			}
			else
			{
				this.mixerControlDetails.cbDetails = this.mixerControl.Metrics.customData;
			}
			int num = this.mixerControlDetails.cbDetails * this.mixerControlDetails.cChannels;
			if ((this.mixerControl.fdwControl & 2u) != 0u)
			{
				num *= (int)this.mixerControl.cMultipleItems;
			}
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			this.mixerControlDetails.paDetails = intPtr;
			MmResult mmResult = MixerInterop.mixerGetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType);
			if (mmResult == MmResult.NoError)
			{
				this.GetDetails(this.mixerControlDetails.paDetails);
			}
			Marshal.FreeCoTaskMem(intPtr);
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "mixerGetControlDetails");
			}
		}

		/// <summary>
		/// Gets the control details
		/// </summary>
		/// <param name="pDetails"></param>
		// Token: 0x060005CE RID: 1486
		protected abstract void GetDetails(IntPtr pDetails);

		/// <summary>
		/// Mixer control name
		/// </summary>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001313B File Offset: 0x0001133B
		public string Name
		{
			get
			{
				return this.mixerControl.szName;
			}
		}

		/// <summary>
		/// Mixer control type
		/// </summary>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00013148 File Offset: 0x00011348
		public MixerControlType ControlType
		{
			get
			{
				return this.mixerControl.dwControlType;
			}
		}

		/// <summary>
		/// Returns true if this is a boolean control
		/// </summary>
		/// <param name="controlType">Control type</param>
		// Token: 0x060005D1 RID: 1489 RVA: 0x00013158 File Offset: 0x00011358
		private static bool IsControlBoolean(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.StereoEnhance)
			{
				if (controlType != MixerControlType.BooleanMeter)
				{
					switch (controlType)
					{
					case MixerControlType.Boolean:
					case MixerControlType.OnOff:
					case MixerControlType.Mute:
					case MixerControlType.Mono:
					case MixerControlType.Loudness:
					case MixerControlType.StereoEnhance:
						break;
					default:
						return false;
					}
				}
			}
			else if (controlType != MixerControlType.Button)
			{
				switch (controlType)
				{
				case MixerControlType.SingleSelect:
				case MixerControlType.Mux:
					break;
				default:
					switch (controlType)
					{
					case MixerControlType.MultipleSelect:
					case MixerControlType.Mixer:
						break;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}

		/// <summary>
		/// Is this a boolean control
		/// </summary>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000131D2 File Offset: 0x000113D2
		public bool IsBoolean
		{
			get
			{
				return MixerControl.IsControlBoolean(this.mixerControl.dwControlType);
			}
		}

		/// <summary>
		/// Determines whether a specified mixer control type is a list text control
		/// </summary>
		// Token: 0x060005D3 RID: 1491 RVA: 0x000131E4 File Offset: 0x000113E4
		private static bool IsControlListText(MixerControlType controlType)
		{
			if (controlType != MixerControlType.Equalizer)
			{
				switch (controlType)
				{
				case MixerControlType.SingleSelect:
				case MixerControlType.Mux:
					break;
				default:
					switch (controlType)
					{
					case MixerControlType.MultipleSelect:
					case MixerControlType.Mixer:
						break;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}

		/// <summary>
		/// True if this is a list text control
		/// </summary>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00013228 File Offset: 0x00011428
		public bool IsListText
		{
			get
			{
				return MixerControl.IsControlListText(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001323C File Offset: 0x0001143C
		private static bool IsControlSigned(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.Signed)
			{
				switch (controlType)
				{
				case MixerControlType.SignedMeter:
				case MixerControlType.PeakMeter:
					break;
				default:
					if (controlType != MixerControlType.Signed)
					{
						return false;
					}
					break;
				}
			}
			else if (controlType != MixerControlType.Decibels)
			{
				switch (controlType)
				{
				case MixerControlType.Slider:
				case MixerControlType.Pan:
				case MixerControlType.QSoundPan:
					break;
				default:
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// True if this is a signed control
		/// </summary>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00013296 File Offset: 0x00011496
		public bool IsSigned
		{
			get
			{
				return MixerControl.IsControlSigned(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000132A8 File Offset: 0x000114A8
		private static bool IsControlUnsigned(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.Percent)
			{
				if (controlType != MixerControlType.UnsignedMeter && controlType != MixerControlType.Unsigned && controlType != MixerControlType.Percent)
				{
					return false;
				}
			}
			else
			{
				switch (controlType)
				{
				case MixerControlType.Fader:
				case MixerControlType.Volume:
				case MixerControlType.Bass:
				case MixerControlType.Treble:
				case MixerControlType.Equalizer:
					break;
				default:
					if (controlType != MixerControlType.MicroTime && controlType != MixerControlType.MilliTime)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		/// <summary>
		/// True if this is an unsigned control
		/// </summary>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001330C File Offset: 0x0001150C
		public bool IsUnsigned
		{
			get
			{
				return MixerControl.IsControlUnsigned(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001331E File Offset: 0x0001151E
		private static bool IsControlCustom(MixerControlType controlType)
		{
			return controlType == MixerControlType.Custom;
		}

		/// <summary>
		/// True if this is a custom control
		/// </summary>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00013324 File Offset: 0x00011524
		public bool IsCustom
		{
			get
			{
				return MixerControl.IsControlCustom(this.mixerControl.dwControlType);
			}
		}

		/// <summary>
		/// String representation for debug purposes
		/// </summary>
		// Token: 0x060005DB RID: 1499 RVA: 0x00013336 File Offset: 0x00011536
		public override string ToString()
		{
			return string.Format("{0} {1}", this.Name, this.ControlType);
		}

		// Token: 0x04000606 RID: 1542
		internal MixerInterop.MIXERCONTROL mixerControl;

		// Token: 0x04000607 RID: 1543
		internal MixerInterop.MIXERCONTROLDETAILS mixerControlDetails;

		/// <summary>
		/// Mixer Handle
		/// </summary>
		// Token: 0x04000608 RID: 1544
		protected IntPtr mixerHandle;

		/// <summary>
		/// Number of Channels
		/// </summary>
		// Token: 0x04000609 RID: 1545
		protected int nChannels;

		/// <summary>
		/// Mixer Handle Type
		/// </summary>
		// Token: 0x0400060A RID: 1546
		protected MixerFlags mixerHandleType;
	}
}
