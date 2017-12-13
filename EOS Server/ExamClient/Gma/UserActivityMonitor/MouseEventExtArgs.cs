using System;
using System.Windows.Forms;

namespace Gma.UserActivityMonitor
{
    public class MouseEventExtArgs : MouseEventArgs
    {
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta) : base(buttons, clicks, x, y, delta)
        {
        }

        internal MouseEventExtArgs(MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
        }

        public bool Handled
        {
            get
            {
                return this.m_Handled;
            }
            set
            {
                this.m_Handled = value;
            }
        }

        private bool m_Handled;
    }
}
