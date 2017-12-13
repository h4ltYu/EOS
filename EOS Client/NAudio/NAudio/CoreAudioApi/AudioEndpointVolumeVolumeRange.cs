using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Volume Range
	/// </summary>
	// Token: 0x02000018 RID: 24
	public class AudioEndpointVolumeVolumeRange
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004470 File Offset: 0x00002670
		internal AudioEndpointVolumeVolumeRange(IAudioEndpointVolume parent)
		{
			Marshal.ThrowExceptionForHR(parent.GetVolumeRange(out this.volumeMinDecibels, out this.volumeMaxDecibels, out this.volumeIncrementDecibels));
		}

		/// <summary>
		/// Minimum Decibels
		/// </summary>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004495 File Offset: 0x00002695
		public float MinDecibels
		{
			get
			{
				return this.volumeMinDecibels;
			}
		}

		/// <summary>
		/// Maximum Decibels
		/// </summary>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000449D File Offset: 0x0000269D
		public float MaxDecibels
		{
			get
			{
				return this.volumeMaxDecibels;
			}
		}

		/// <summary>
		/// Increment Decibels
		/// </summary>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000044A5 File Offset: 0x000026A5
		public float IncrementDecibels
		{
			get
			{
				return this.volumeIncrementDecibels;
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly float volumeMinDecibels;

		// Token: 0x04000059 RID: 89
		private readonly float volumeMaxDecibels;

		// Token: 0x0400005A RID: 90
		private readonly float volumeIncrementDecibels;
	}
}
