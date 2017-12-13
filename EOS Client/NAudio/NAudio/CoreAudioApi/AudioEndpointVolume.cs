using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume
	/// </summary>
	// Token: 0x0200011E RID: 286
	public class AudioEndpointVolume : IDisposable
	{
		/// <summary>
		/// On Volume Notification
		/// </summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000657 RID: 1623 RVA: 0x00014710 File Offset: 0x00012910
		// (remove) Token: 0x06000658 RID: 1624 RVA: 0x00014748 File Offset: 0x00012948
		public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

		/// <summary>
		/// Volume Range
		/// </summary>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001477D File Offset: 0x0001297D
		public AudioEndpointVolumeVolumeRange VolumeRange
		{
			get
			{
				return this.volumeRange;
			}
		}

		/// <summary>
		/// Hardware Support
		/// </summary>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00014785 File Offset: 0x00012985
		public EEndpointHardwareSupport HardwareSupport
		{
			get
			{
				return this.hardwareSupport;
			}
		}

		/// <summary>
		/// Step Information
		/// </summary>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001478D File Offset: 0x0001298D
		public AudioEndpointVolumeStepInformation StepInformation
		{
			get
			{
				return this.stepInformation;
			}
		}

		/// <summary>
		/// Channels
		/// </summary>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00014795 File Offset: 0x00012995
		public AudioEndpointVolumeChannels Channels
		{
			get
			{
				return this.channels;
			}
		}

		/// <summary>
		/// Master Volume Level
		/// </summary>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000147A0 File Offset: 0x000129A0
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x000147C0 File Offset: 0x000129C0
		public float MasterVolumeLevel
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevel(out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
			}
		}

		/// <summary>
		/// Master Volume Level Scalar
		/// </summary>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000147D8 File Offset: 0x000129D8
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x000147F8 File Offset: 0x000129F8
		public float MasterVolumeLevelScalar
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevelScalar(out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
			}
		}

		/// <summary>
		/// Mute
		/// </summary>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00014810 File Offset: 0x00012A10
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00014830 File Offset: 0x00012A30
		public bool Mute
		{
			get
			{
				bool result;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMute(out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMute(value, Guid.Empty));
			}
		}

		/// <summary>
		/// Volume Step Up
		/// </summary>
		// Token: 0x06000663 RID: 1635 RVA: 0x00014848 File Offset: 0x00012A48
		public void VolumeStepUp()
		{
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepUp(Guid.Empty));
		}

		/// <summary>
		/// Volume Step Down
		/// </summary>
		// Token: 0x06000664 RID: 1636 RVA: 0x0001485F File Offset: 0x00012A5F
		public void VolumeStepDown()
		{
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepDown(Guid.Empty));
		}

		/// <summary>
		/// Creates a new Audio endpoint volume
		/// </summary>
		/// <param name="realEndpointVolume">IAudioEndpointVolume COM interface</param>
		// Token: 0x06000665 RID: 1637 RVA: 0x00014878 File Offset: 0x00012A78
		internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
		{
			this.audioEndPointVolume = realEndpointVolume;
			this.channels = new AudioEndpointVolumeChannels(this.audioEndPointVolume);
			this.stepInformation = new AudioEndpointVolumeStepInformation(this.audioEndPointVolume);
			uint num;
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.QueryHardwareSupport(out num));
			this.hardwareSupport = (EEndpointHardwareSupport)num;
			this.volumeRange = new AudioEndpointVolumeVolumeRange(this.audioEndPointVolume);
			this.callBack = new AudioEndpointVolumeCallback(this);
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.RegisterControlChangeNotify(this.callBack));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00014900 File Offset: 0x00012B00
		internal void FireNotification(AudioVolumeNotificationData notificationData)
		{
			AudioEndpointVolumeNotificationDelegate onVolumeNotification = this.OnVolumeNotification;
			if (onVolumeNotification != null)
			{
				onVolumeNotification(notificationData);
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x06000667 RID: 1639 RVA: 0x0001491E File Offset: 0x00012B1E
		public void Dispose()
		{
			if (this.callBack != null)
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.UnregisterControlChangeNotify(this.callBack));
				this.callBack = null;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		// Token: 0x06000668 RID: 1640 RVA: 0x0001494C File Offset: 0x00012B4C
		~AudioEndpointVolume()
		{
			this.Dispose();
		}

		// Token: 0x040006EF RID: 1775
		private readonly IAudioEndpointVolume audioEndPointVolume;

		// Token: 0x040006F0 RID: 1776
		private readonly AudioEndpointVolumeChannels channels;

		// Token: 0x040006F1 RID: 1777
		private readonly AudioEndpointVolumeStepInformation stepInformation;

		// Token: 0x040006F2 RID: 1778
		private readonly AudioEndpointVolumeVolumeRange volumeRange;

		// Token: 0x040006F3 RID: 1779
		private readonly EEndpointHardwareSupport hardwareSupport;

		// Token: 0x040006F4 RID: 1780
		private AudioEndpointVolumeCallback callBack;
	}
}
