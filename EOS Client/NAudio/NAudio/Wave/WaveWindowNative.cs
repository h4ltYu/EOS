using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NAudio.Wave
{
	// Token: 0x0200018E RID: 398
	internal class WaveWindowNative : NativeWindow
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00017A3B File Offset: 0x00015C3B
		public WaveWindowNative(WaveInterop.WaveCallback waveCallback)
		{
			this.waveCallback = waveCallback;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00017A4C File Offset: 0x00015C4C
		protected override void WndProc(ref Message m)
		{
			WaveInterop.WaveMessage msg = (WaveInterop.WaveMessage)m.Msg;
			switch (msg)
			{
			case WaveInterop.WaveMessage.WaveOutOpen:
			case WaveInterop.WaveMessage.WaveOutClose:
			case WaveInterop.WaveMessage.WaveInOpen:
			case WaveInterop.WaveMessage.WaveInClose:
				this.waveCallback(m.WParam, msg, IntPtr.Zero, null, IntPtr.Zero);
				return;
			case WaveInterop.WaveMessage.WaveOutDone:
			case WaveInterop.WaveMessage.WaveInData:
			{
				IntPtr wparam = m.WParam;
				WaveHeader waveHeader = new WaveHeader();
				Marshal.PtrToStructure(m.LParam, waveHeader);
				this.waveCallback(wparam, msg, IntPtr.Zero, waveHeader, IntPtr.Zero);
				return;
			}
			default:
				base.WndProc(ref m);
				return;
			}
		}

		// Token: 0x04000988 RID: 2440
		private WaveInterop.WaveCallback waveCallback;
	}
}
