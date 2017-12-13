using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI out device
	/// </summary>
	// Token: 0x020000EA RID: 234
	public class MidiOut : IDisposable
	{
		/// <summary>
		/// Gets the number of MIDI devices available in the system
		/// </summary>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000116EB File Offset: 0x0000F8EB
		public static int NumberOfDevices
		{
			get
			{
				return MidiInterop.midiOutGetNumDevs();
			}
		}

		/// <summary>
		/// Gets the MIDI Out device info
		/// </summary>
		// Token: 0x0600055F RID: 1375 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public static MidiOutCapabilities DeviceInfo(int midiOutDeviceNumber)
		{
			MidiOutCapabilities midiOutCapabilities = default(MidiOutCapabilities);
			int uSize = Marshal.SizeOf(midiOutCapabilities);
			MmException.Try(MidiInterop.midiOutGetDevCaps((IntPtr)midiOutDeviceNumber, out midiOutCapabilities, uSize), "midiOutGetDevCaps");
			return midiOutCapabilities;
		}

		/// <summary>
		/// Opens a specified MIDI out device
		/// </summary>
		/// <param name="deviceNo">The device number</param>
		// Token: 0x06000560 RID: 1376 RVA: 0x00011730 File Offset: 0x0000F930
		public MidiOut(int deviceNo)
		{
			this.callback = new MidiInterop.MidiOutCallback(this.Callback);
			MmException.Try(MidiInterop.midiOutOpen(out this.hMidiOut, (IntPtr)deviceNo, this.callback, IntPtr.Zero, 196608), "midiOutOpen");
		}

		/// <summary>
		/// Closes this MIDI out device
		/// </summary>
		// Token: 0x06000561 RID: 1377 RVA: 0x0001178B File Offset: 0x0000F98B
		public void Close()
		{
			this.Dispose();
		}

		/// <summary>
		/// Closes this MIDI out device
		/// </summary>
		// Token: 0x06000562 RID: 1378 RVA: 0x00011793 File Offset: 0x0000F993
		public void Dispose()
		{
			GC.KeepAlive(this.callback);
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets or sets the volume for this MIDI out device
		/// </summary>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000117B0 File Offset: 0x0000F9B0
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000117D7 File Offset: 0x0000F9D7
		public int Volume
		{
			get
			{
				int result = 0;
				MmException.Try(MidiInterop.midiOutGetVolume(this.hMidiOut, ref result), "midiOutGetVolume");
				return result;
			}
			set
			{
				MmException.Try(MidiInterop.midiOutSetVolume(this.hMidiOut, value), "midiOutSetVolume");
			}
		}

		/// <summary>
		/// Resets the MIDI out device
		/// </summary>
		// Token: 0x06000565 RID: 1381 RVA: 0x000117EF File Offset: 0x0000F9EF
		public void Reset()
		{
			MmException.Try(MidiInterop.midiOutReset(this.hMidiOut), "midiOutReset");
		}

		/// <summary>
		/// Sends a MIDI out message
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="param1">Parameter 1</param>
		/// <param name="param2">Parameter 2</param>
		// Token: 0x06000566 RID: 1382 RVA: 0x00011806 File Offset: 0x0000FA06
		public void SendDriverMessage(int message, int param1, int param2)
		{
			MmException.Try(MidiInterop.midiOutMessage(this.hMidiOut, message, (IntPtr)param1, (IntPtr)param2), "midiOutMessage");
		}

		/// <summary>
		/// Sends a MIDI message to the MIDI out device
		/// </summary>
		/// <param name="message">The message to send</param>
		// Token: 0x06000567 RID: 1383 RVA: 0x0001182A File Offset: 0x0000FA2A
		public void Send(int message)
		{
			MmException.Try(MidiInterop.midiOutShortMsg(this.hMidiOut, message), "midiOutShortMsg");
		}

		/// <summary>
		/// Closes the MIDI out device
		/// </summary>
		/// <param name="disposing">True if called from Dispose</param>
		// Token: 0x06000568 RID: 1384 RVA: 0x00011842 File Offset: 0x0000FA42
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				MidiInterop.midiOutClose(this.hMidiOut);
			}
			this.disposed = true;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001185F File Offset: 0x0000FA5F
		private void Callback(IntPtr midiInHandle, MidiInterop.MidiOutMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2)
		{
		}

		/// <summary>
		/// Send a long message, for example sysex.
		/// </summary>
		/// <param name="byteBuffer">The bytes to send.</param>
		// Token: 0x0600056A RID: 1386 RVA: 0x00011864 File Offset: 0x0000FA64
		public void SendBuffer(byte[] byteBuffer)
		{
			MidiInterop.MIDIHDR midihdr = default(MidiInterop.MIDIHDR);
			midihdr.lpData = Marshal.AllocHGlobal(byteBuffer.Length);
			Marshal.Copy(byteBuffer, 0, midihdr.lpData, byteBuffer.Length);
			midihdr.dwBufferLength = byteBuffer.Length;
			midihdr.dwBytesRecorded = byteBuffer.Length;
			int uSize = Marshal.SizeOf(midihdr);
			MidiInterop.midiOutPrepareHeader(this.hMidiOut, ref midihdr, uSize);
			MmResult mmResult = MidiInterop.midiOutLongMsg(this.hMidiOut, ref midihdr, uSize);
			if (mmResult != MmResult.NoError)
			{
				MidiInterop.midiOutUnprepareHeader(this.hMidiOut, ref midihdr, uSize);
			}
			Marshal.FreeHGlobal(midihdr.lpData);
		}

		/// <summary>
		/// Cleanup
		/// </summary>
		// Token: 0x0600056B RID: 1387 RVA: 0x000118F8 File Offset: 0x0000FAF8
		~MidiOut()
		{
			this.Dispose(false);
		}

		// Token: 0x040005D7 RID: 1495
		private IntPtr hMidiOut = IntPtr.Zero;

		// Token: 0x040005D8 RID: 1496
		private bool disposed;

		// Token: 0x040005D9 RID: 1497
		private MidiInterop.MidiOutCallback callback;
	}
}
