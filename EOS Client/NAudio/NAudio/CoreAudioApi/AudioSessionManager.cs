using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// AudioSessionManager
	///
	/// Designed to manage audio sessions and in particuar the
	/// SimpleAudioVolume interface to adjust a session volume
	/// </summary>
	// Token: 0x0200001E RID: 30
	public class AudioSessionManager
	{
		/// <summary>
		/// Occurs when audio session has been added (for example run another program that use audio playback).
		/// </summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000084 RID: 132 RVA: 0x000048C0 File Offset: 0x00002AC0
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x000048F8 File Offset: 0x00002AF8
		public event AudioSessionManager.SessionCreatedDelegate OnSessionCreated;

		// Token: 0x06000086 RID: 134 RVA: 0x0000492D File Offset: 0x00002B2D
		internal AudioSessionManager(IAudioSessionManager audioSessionManager)
		{
			this.audioSessionInterface = audioSessionManager;
			this.audioSessionInterface2 = (audioSessionManager as IAudioSessionManager2);
			this.RefreshSessions();
		}

		/// <summary>
		/// SimpleAudioVolume object
		/// for adjusting the volume for the user session
		/// </summary>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004950 File Offset: 0x00002B50
		public SimpleAudioVolume SimpleAudioVolume
		{
			get
			{
				if (this.simpleAudioVolume == null)
				{
					ISimpleAudioVolume realSimpleVolume;
					this.audioSessionInterface.GetSimpleAudioVolume(Guid.Empty, 0u, out realSimpleVolume);
					this.simpleAudioVolume = new SimpleAudioVolume(realSimpleVolume);
				}
				return this.simpleAudioVolume;
			}
		}

		/// <summary>
		/// AudioSessionControl object
		/// for registring for callbacks and other session information
		/// </summary>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000498C File Offset: 0x00002B8C
		public AudioSessionControl AudioSessionControl
		{
			get
			{
				if (this.audioSessionControl == null)
				{
					IAudioSessionControl audioSessionControl;
					this.audioSessionInterface.GetAudioSessionControl(Guid.Empty, 0u, out audioSessionControl);
					this.audioSessionControl = new AudioSessionControl(audioSessionControl);
				}
				return this.audioSessionControl;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000049C7 File Offset: 0x00002BC7
		internal void FireSessionCreated(IAudioSessionControl newSession)
		{
			if (this.OnSessionCreated != null)
			{
				this.OnSessionCreated(this, newSession);
			}
		}

		/// <summary>
		/// Refresh session of current device.
		/// </summary>
		// Token: 0x0600008A RID: 138 RVA: 0x000049E0 File Offset: 0x00002BE0
		public void RefreshSessions()
		{
			this.UnregisterNotifications();
			if (this.audioSessionInterface2 != null)
			{
				IAudioSessionEnumerator realEnumerator;
				Marshal.ThrowExceptionForHR(this.audioSessionInterface2.GetSessionEnumerator(out realEnumerator));
				this.sessions = new SessionCollection(realEnumerator);
				this.audioSessionNotification = new AudioSessionNotification(this);
				Marshal.ThrowExceptionForHR(this.audioSessionInterface2.RegisterSessionNotification(this.audioSessionNotification));
			}
		}

		/// <summary>
		/// Returns list of sessions of current device.
		/// </summary>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004A3B File Offset: 0x00002C3B
		public SessionCollection Sessions
		{
			get
			{
				return this.sessions;
			}
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		// Token: 0x0600008C RID: 140 RVA: 0x00004A43 File Offset: 0x00002C43
		public void Dispose()
		{
			this.UnregisterNotifications();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004A51 File Offset: 0x00002C51
		private void UnregisterNotifications()
		{
			if (this.sessions != null)
			{
				this.sessions = null;
			}
			if (this.audioSessionNotification != null)
			{
				Marshal.ThrowExceptionForHR(this.audioSessionInterface2.UnregisterSessionNotification(this.audioSessionNotification));
			}
		}

		/// <summary>
		/// Finalizer.
		/// </summary>
		// Token: 0x0600008E RID: 142 RVA: 0x00004A80 File Offset: 0x00002C80
		~AudioSessionManager()
		{
			this.Dispose();
		}

		// Token: 0x04000063 RID: 99
		private readonly IAudioSessionManager audioSessionInterface;

		// Token: 0x04000064 RID: 100
		private readonly IAudioSessionManager2 audioSessionInterface2;

		// Token: 0x04000065 RID: 101
		private AudioSessionNotification audioSessionNotification;

		// Token: 0x04000066 RID: 102
		private SessionCollection sessions;

		// Token: 0x04000067 RID: 103
		private SimpleAudioVolume simpleAudioVolume;

		// Token: 0x04000068 RID: 104
		private AudioSessionControl audioSessionControl;

		/// <summary>
		///
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="newSession"></param>
		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000090 RID: 144
		public delegate void SessionCreatedDelegate(object sender, IAudioSessionControl newSession);
	}
}
