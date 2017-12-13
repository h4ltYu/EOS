using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Channel
	/// </summary>
	// Token: 0x02000014 RID: 20
	public class AudioEndpointVolumeChannel
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004328 File Offset: 0x00002528
		internal AudioEndpointVolumeChannel(IAudioEndpointVolume parent, int channel)
		{
			this.channel = (uint)channel;
			this.audioEndpointVolume = parent;
		}

		/// <summary>
		/// Volume Level
		/// </summary>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004340 File Offset: 0x00002540
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00004366 File Offset: 0x00002566
		public float VolumeLevel
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevel(this.channel, out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevel(this.channel, value, Guid.Empty));
			}
		}

		/// <summary>
		/// Volume Level Scalar
		/// </summary>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004384 File Offset: 0x00002584
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000043AA File Offset: 0x000025AA
		public float VolumeLevelScalar
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevelScalar(this.channel, out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevelScalar(this.channel, value, Guid.Empty));
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly uint channel;

		// Token: 0x04000053 RID: 83
		private readonly IAudioEndpointVolume audioEndpointVolume;
	}
}
