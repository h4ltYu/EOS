using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI in device
	/// </summary>
	// Token: 0x020000DE RID: 222
	public class MidiIn : IDisposable
	{
		/// <summary>
		/// Called when a MIDI message is received
		/// </summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000514 RID: 1300 RVA: 0x00011388 File Offset: 0x0000F588
		// (remove) Token: 0x06000515 RID: 1301 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public event EventHandler<MidiInMessageEventArgs> MessageReceived;

		/// <summary>
		/// An invalid MIDI message
		/// </summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000516 RID: 1302 RVA: 0x000113F8 File Offset: 0x0000F5F8
		// (remove) Token: 0x06000517 RID: 1303 RVA: 0x00011430 File Offset: 0x0000F630
		public event EventHandler<MidiInMessageEventArgs> ErrorReceived;

		/// <summary>
		/// Gets the number of MIDI input devices available in the system
		/// </summary>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011465 File Offset: 0x0000F665
		public static int NumberOfDevices
		{
			get
			{
				return MidiInterop.midiInGetNumDevs();
			}
		}

		/// <summary>
		/// Opens a specified MIDI in device
		/// </summary>
		/// <param name="deviceNo">The device number</param>
		// Token: 0x06000519 RID: 1305 RVA: 0x0001146C File Offset: 0x0000F66C
		public MidiIn(int deviceNo)
		{
			this.callback = new MidiInterop.MidiInCallback(this.Callback);
			MmException.Try(MidiInterop.midiInOpen(out this.hMidiIn, (IntPtr)deviceNo, this.callback, IntPtr.Zero, 196608), "midiInOpen");
		}

		/// <summary>
		/// Closes this MIDI in device
		/// </summary>
		// Token: 0x0600051A RID: 1306 RVA: 0x000114C7 File Offset: 0x0000F6C7
		public void Close()
		{
			this.Dispose();
		}

		/// <summary>
		/// Closes this MIDI in device
		/// </summary>
		// Token: 0x0600051B RID: 1307 RVA: 0x000114CF File Offset: 0x0000F6CF
		public void Dispose()
		{
			GC.KeepAlive(this.callback);
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Start the MIDI in device
		/// </summary>
		// Token: 0x0600051C RID: 1308 RVA: 0x000114E9 File Offset: 0x0000F6E9
		public void Start()
		{
			MmException.Try(MidiInterop.midiInStart(this.hMidiIn), "midiInStart");
		}

		/// <summary>
		/// Stop the MIDI in device
		/// </summary>
		// Token: 0x0600051D RID: 1309 RVA: 0x00011500 File Offset: 0x0000F700
		public void Stop()
		{
			MmException.Try(MidiInterop.midiInStop(this.hMidiIn), "midiInStop");
		}

		/// <summary>
		/// Reset the MIDI in device
		/// </summary>
		// Token: 0x0600051E RID: 1310 RVA: 0x00011517 File Offset: 0x0000F717
		public void Reset()
		{
			MmException.Try(MidiInterop.midiInReset(this.hMidiIn), "midiInReset");
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011530 File Offset: 0x0000F730
		private void Callback(IntPtr midiInHandle, MidiInterop.MidiInMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2)
		{
			switch (message)
			{
			case MidiInterop.MidiInMessage.Open:
			case MidiInterop.MidiInMessage.Close:
			case MidiInterop.MidiInMessage.LongData:
			case MidiInterop.MidiInMessage.LongError:
			case (MidiInterop.MidiInMessage)967:
			case (MidiInterop.MidiInMessage)968:
			case (MidiInterop.MidiInMessage)969:
			case (MidiInterop.MidiInMessage)970:
			case (MidiInterop.MidiInMessage)971:
			case MidiInterop.MidiInMessage.MoreData:
				break;
			case MidiInterop.MidiInMessage.Data:
				if (this.MessageReceived != null)
				{
					this.MessageReceived(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
					return;
				}
				break;
			case MidiInterop.MidiInMessage.Error:
				if (this.ErrorReceived != null)
				{
					this.ErrorReceived(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
				}
				break;
			default:
				return;
			}
		}

		/// <summary>
		/// Gets the MIDI in device info
		/// </summary>
		// Token: 0x06000520 RID: 1312 RVA: 0x000115CC File Offset: 0x0000F7CC
		public static MidiInCapabilities DeviceInfo(int midiInDeviceNumber)
		{
			MidiInCapabilities midiInCapabilities = default(MidiInCapabilities);
			int size = Marshal.SizeOf(midiInCapabilities);
			MmException.Try(MidiInterop.midiInGetDevCaps((IntPtr)midiInDeviceNumber, out midiInCapabilities, size), "midiInGetDevCaps");
			return midiInCapabilities;
		}

		/// <summary>
		/// Closes the MIDI out device
		/// </summary>
		/// <param name="disposing">True if called from Dispose</param>
		// Token: 0x06000521 RID: 1313 RVA: 0x00011606 File Offset: 0x0000F806
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				MidiInterop.midiInClose(this.hMidiIn);
			}
			this.disposed = true;
		}

		/// <summary>
		/// Cleanup
		/// </summary>
		// Token: 0x06000522 RID: 1314 RVA: 0x00011624 File Offset: 0x0000F824
		~MidiIn()
		{
			this.Dispose(false);
		}

		// Token: 0x040005AC RID: 1452
		private IntPtr hMidiIn = IntPtr.Zero;

		// Token: 0x040005AD RID: 1453
		private bool disposed;

		// Token: 0x040005AE RID: 1454
		private MidiInterop.MidiInCallback callback;
	}
}
