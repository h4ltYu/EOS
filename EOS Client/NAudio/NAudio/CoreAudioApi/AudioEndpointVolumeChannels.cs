using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Channels
	/// </summary>
	// Token: 0x02000015 RID: 21
	public class AudioEndpointVolumeChannels
	{
		/// <summary>
		/// Channel Count
		/// </summary>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000043C8 File Offset: 0x000025C8
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetChannelCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Indexer - get a specific channel
		/// </summary>
		// Token: 0x1700001C RID: 28
		public AudioEndpointVolumeChannel this[int index]
		{
			get
			{
				return this.channels[index];
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000043F4 File Offset: 0x000025F4
		internal AudioEndpointVolumeChannels(IAudioEndpointVolume parent)
		{
			this.audioEndPointVolume = parent;
			int count = this.Count;
			this.channels = new AudioEndpointVolumeChannel[count];
			for (int i = 0; i < count; i++)
			{
				this.channels[i] = new AudioEndpointVolumeChannel(this.audioEndPointVolume, i);
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly IAudioEndpointVolume audioEndPointVolume;

		// Token: 0x04000055 RID: 85
		private readonly AudioEndpointVolumeChannel[] channels;
	}
}
