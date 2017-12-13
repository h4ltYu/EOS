using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Meter Information
	/// </summary>
	// Token: 0x0200011F RID: 287
	public class AudioMeterInformation
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00014978 File Offset: 0x00012B78
		internal AudioMeterInformation(IAudioMeterInformation realInterface)
		{
			this.audioMeterInformation = realInterface;
			int num;
			Marshal.ThrowExceptionForHR(this.audioMeterInformation.QueryHardwareSupport(out num));
			this.hardwareSupport = (EEndpointHardwareSupport)num;
			this.channels = new AudioMeterInformationChannels(this.audioMeterInformation);
		}

		/// <summary>
		/// Peak Values
		/// </summary>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x000149BC File Offset: 0x00012BBC
		public AudioMeterInformationChannels PeakValues
		{
			get
			{
				return this.channels;
			}
		}

		/// <summary>
		/// Hardware Support
		/// </summary>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000149C4 File Offset: 0x00012BC4
		public EEndpointHardwareSupport HardwareSupport
		{
			get
			{
				return this.hardwareSupport;
			}
		}

		/// <summary>
		/// Master Peak Value
		/// </summary>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000149CC File Offset: 0x00012BCC
		public float MasterPeakValue
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetPeakValue(out result));
				return result;
			}
		}

		// Token: 0x040006F6 RID: 1782
		private readonly IAudioMeterInformation audioMeterInformation;

		// Token: 0x040006F7 RID: 1783
		private readonly EEndpointHardwareSupport hardwareSupport;

		// Token: 0x040006F8 RID: 1784
		private readonly AudioMeterInformationChannels channels;
	}
}
