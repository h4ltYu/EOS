using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NAudio.Wave
{
    internal partial class WaveWindow : Form
    {
        public WaveWindow(WaveInterop.WaveCallback waveCallback)
        {
            this.waveCallback = waveCallback;
        }

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

        private WaveInterop.WaveCallback waveCallback;
    }
}
