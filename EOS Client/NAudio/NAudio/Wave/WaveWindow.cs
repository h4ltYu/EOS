using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NAudio.Wave
{
	// Token: 0x0200018F RID: 399
	internal partial class WaveWindow : Form
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x00017ADF File Offset: 0x00015CDF
		public WaveWindow(WaveInterop.WaveCallback waveCallback)
		{
			this.waveCallback = waveCallback;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00017AF0 File Offset: 0x00015CF0
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

		// Token: 0x04000989 RID: 2441
		private WaveInterop.WaveCallback waveCallback;
	}
}
