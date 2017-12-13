using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// AudioSessionControl object for information
	/// regarding an audio session
	/// </summary>
	// Token: 0x0200001B RID: 27
	public class AudioSessionControl : IDisposable
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="audioSessionControl"></param>
		// Token: 0x06000063 RID: 99 RVA: 0x0000458C File Offset: 0x0000278C
		public AudioSessionControl(IAudioSessionControl audioSessionControl)
		{
			this.audioSessionControlInterface = audioSessionControl;
			this.audioSessionControlInterface2 = (audioSessionControl as IAudioSessionControl2);
			IAudioMeterInformation audioMeterInformation = this.audioSessionControlInterface as IAudioMeterInformation;
			ISimpleAudioVolume simpleAudioVolume = this.audioSessionControlInterface as ISimpleAudioVolume;
			if (audioMeterInformation != null)
			{
				this.audioMeterInformation = new AudioMeterInformation(audioMeterInformation);
			}
			if (simpleAudioVolume != null)
			{
				this.simpleAudioVolume = new SimpleAudioVolume(simpleAudioVolume);
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x06000064 RID: 100 RVA: 0x000045E8 File Offset: 0x000027E8
		public void Dispose()
		{
			if (this.audioSessionEventCallback != null)
			{
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification(this.audioSessionEventCallback));
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		// Token: 0x06000065 RID: 101 RVA: 0x00004610 File Offset: 0x00002810
		~AudioSessionControl()
		{
			this.Dispose();
		}

		/// <summary>
		/// Audio meter information of the audio session.
		/// </summary>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000463C File Offset: 0x0000283C
		public AudioMeterInformation AudioMeterInformation
		{
			get
			{
				return this.audioMeterInformation;
			}
		}

		/// <summary>
		/// Simple audio volume of the audio session (for volume and mute status).
		/// </summary>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004644 File Offset: 0x00002844
		public SimpleAudioVolume SimpleAudioVolume
		{
			get
			{
				return this.simpleAudioVolume;
			}
		}

		/// <summary>
		/// The current state of the audio session.
		/// </summary>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000464C File Offset: 0x0000284C
		public AudioSessionState State
		{
			get
			{
				AudioSessionState result;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetState(out result));
				return result;
			}
		}

		/// <summary>
		/// The name of the audio session.
		/// </summary>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000466C File Offset: 0x0000286C
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004692 File Offset: 0x00002892
		public string DisplayName
		{
			get
			{
				string empty = string.Empty;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetDisplayName(out empty));
				return empty;
			}
			set
			{
				if (value != string.Empty)
				{
					Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetDisplayName(value, Guid.Empty));
				}
			}
		}

		/// <summary>
		/// the path to the icon shown in the mixer.
		/// </summary>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000046DE File Offset: 0x000028DE
		public string IconPath
		{
			get
			{
				string empty = string.Empty;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetIconPath(out empty));
				return empty;
			}
			set
			{
				if (value != string.Empty)
				{
					Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetIconPath(value, Guid.Empty));
				}
			}
		}

		/// <summary>
		/// The session identifier of the audio session.
		/// </summary>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004704 File Offset: 0x00002904
		public string GetSessionIdentifier
		{
			get
			{
				if (this.audioSessionControlInterface2 == null)
				{
					throw new InvalidOperationException("Not supported on this version of Windows");
				}
				string result;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionIdentifier(out result));
				return result;
			}
		}

		/// <summary>
		/// The session instance identifier of the audio session.
		/// </summary>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004738 File Offset: 0x00002938
		public string GetSessionInstanceIdentifier
		{
			get
			{
				if (this.audioSessionControlInterface2 == null)
				{
					throw new InvalidOperationException("Not supported on this version of Windows");
				}
				string result;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionInstanceIdentifier(out result));
				return result;
			}
		}

		/// <summary>
		/// The process identifier of the audio session.
		/// </summary>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000476C File Offset: 0x0000296C
		public uint GetProcessID
		{
			get
			{
				if (this.audioSessionControlInterface2 == null)
				{
					throw new InvalidOperationException("Not supported on this version of Windows");
				}
				uint result;
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetProcessId(out result));
				return result;
			}
		}

		/// <summary>
		/// Is the session a system sounds session.
		/// </summary>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000479F File Offset: 0x0000299F
		public bool IsSystemSoundsSession
		{
			get
			{
				if (this.audioSessionControlInterface2 == null)
				{
					throw new InvalidOperationException("Not supported on this version of Windows");
				}
				return this.audioSessionControlInterface2.IsSystemSoundsSession() == 0;
			}
		}

		/// <summary>
		/// the grouping param for an audio session grouping
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000071 RID: 113 RVA: 0x000047C4 File Offset: 0x000029C4
		public Guid GetGroupingParam()
		{
			Guid empty = Guid.Empty;
			Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetGroupingParam(out empty));
			return empty;
		}

		/// <summary>
		/// For chanigng the grouping param and supplying the context of said change
		/// </summary>
		/// <param name="groupingId"></param>
		/// <param name="context"></param>
		// Token: 0x06000072 RID: 114 RVA: 0x000047EA File Offset: 0x000029EA
		public void SetGroupingParam(Guid groupingId, Guid context)
		{
			Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetGroupingParam(groupingId, context));
		}

		/// <summary>
		/// Registers an even client for callbacks
		/// </summary>
		/// <param name="eventClient"></param>
		// Token: 0x06000073 RID: 115 RVA: 0x000047FE File Offset: 0x000029FE
		public void RegisterEventClient(IAudioSessionEventsHandler eventClient)
		{
			this.audioSessionEventCallback = new AudioSessionEventsCallback(eventClient);
			Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.RegisterAudioSessionNotification(this.audioSessionEventCallback));
		}

		/// <summary>
		/// Unregisters an event client from receiving callbacks
		/// </summary>
		/// <param name="eventClient"></param>
		// Token: 0x06000074 RID: 116 RVA: 0x00004822 File Offset: 0x00002A22
		public void UnRegisterEventClient(IAudioSessionEventsHandler eventClient)
		{
			if (this.audioSessionEventCallback != null)
			{
				Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification(this.audioSessionEventCallback));
			}
		}

		// Token: 0x0400005D RID: 93
		private readonly IAudioSessionControl audioSessionControlInterface;

		// Token: 0x0400005E RID: 94
		private readonly IAudioSessionControl2 audioSessionControlInterface2;

		// Token: 0x0400005F RID: 95
		private AudioSessionEventsCallback audioSessionEventCallback;

		// Token: 0x04000060 RID: 96
		internal AudioMeterInformation audioMeterInformation;

		// Token: 0x04000061 RID: 97
		internal SimpleAudioVolume simpleAudioVolume;
	}
}
