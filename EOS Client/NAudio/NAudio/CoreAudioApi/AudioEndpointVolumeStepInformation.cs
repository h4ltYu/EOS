using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Step Information
	/// </summary>
	// Token: 0x02000017 RID: 23
	public class AudioEndpointVolumeStepInformation
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00004441 File Offset: 0x00002641
		internal AudioEndpointVolumeStepInformation(IAudioEndpointVolume parent)
		{
			Marshal.ThrowExceptionForHR(parent.GetVolumeStepInfo(out this.step, out this.stepCount));
		}

		/// <summary>
		/// Step
		/// </summary>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004460 File Offset: 0x00002660
		public uint Step
		{
			get
			{
				return this.step;
			}
		}

		/// <summary>
		/// StepCount
		/// </summary>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004468 File Offset: 0x00002668
		public uint StepCount
		{
			get
			{
				return this.stepCount;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly uint step;

		// Token: 0x04000057 RID: 87
		private readonly uint stepCount;
	}
}
