using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CustomAutoScrollPanel
{
    public class ScrollablePanel : Panel
    {
        public event ScrollEventHandler ScrollHorizontal;

        public event ScrollEventHandler ScrollVertical;

        public event MouseEventHandler ScrollMouseWheel;

        public ScrollablePanel()
        {
            base.Click += this.ScrollablePanel_Click;
            this.AutoScroll = true;
        }

        public int AutoScrollHPos
        {
            get
            {
                return ScrollablePanel.GetScrollPos(base.Handle, 0);
            }
            set
            {
                ScrollablePanel.SetScrollPos(base.Handle, 0, value, true);
            }
        }

        public int AutoScrollVPos
        {
            get
            {
                return ScrollablePanel.GetScrollPos(base.Handle, 1);
            }
            set
            {
                ScrollablePanel.SetScrollPos(base.Handle, 1, value, true);
            }
        }

        public int AutoScrollHorizontalMinimum
        {
            get
            {
                return this.autoScrollHorizontalMinimum;
            }
            set
            {
                this.autoScrollHorizontalMinimum = value;
                ScrollablePanel.SetScrollRange(base.Handle, 0, this.autoScrollHorizontalMinimum, this.autoScrollHorizontalMaximum, true);
            }
        }

        public int AutoScrollHorizontalMaximum
        {
            get
            {
                return this.autoScrollHorizontalMaximum;
            }
            set
            {
                this.autoScrollHorizontalMaximum = value;
                ScrollablePanel.SetScrollRange(base.Handle, 0, this.autoScrollHorizontalMinimum, this.autoScrollHorizontalMaximum, true);
            }
        }

        public int AutoScrollVerticalMinimum
        {
            get
            {
                return this.autoScrollVerticalMinimum;
            }
            set
            {
                this.autoScrollVerticalMinimum = value;
                ScrollablePanel.SetScrollRange(base.Handle, 1, this.autoScrollHorizontalMinimum, this.autoScrollHorizontalMaximum, true);
            }
        }

        public int AutoScrollVerticalMaximum
        {
            get
            {
                return this.autoScrollVerticalMaximum;
            }
            set
            {
                this.autoScrollVerticalMaximum = value;
                ScrollablePanel.SetScrollRange(base.Handle, 1, this.autoScrollHorizontalMinimum, this.autoScrollHorizontalMaximum, true);
            }
        }

        public bool EnableAutoScrollHorizontal
        {
            get
            {
                return this.enableAutoHorizontal;
            }
            set
            {
                this.enableAutoHorizontal = value;
                if (value)
                {
                    ScrollablePanel.EnableScrollBar(base.Handle, 0u, 0u);
                }
                else
                {
                    ScrollablePanel.EnableScrollBar(base.Handle, 0u, 3u);
                }
            }
        }

        public bool EnableAutoScrollVertical
        {
            get
            {
                return this.enableAutoVertical;
            }
            set
            {
                this.enableAutoVertical = value;
                if (value)
                {
                    ScrollablePanel.EnableScrollBar(base.Handle, 1u, 0u);
                }
                else
                {
                    ScrollablePanel.EnableScrollBar(base.Handle, 1u, 3u);
                }
            }
        }

        public bool VisibleAutoScrollHorizontal
        {
            get
            {
                return this.visibleAutoHorizontal;
            }
            set
            {
                this.visibleAutoHorizontal = value;
                ScrollablePanel.ShowScrollBar(base.Handle, 0, value);
            }
        }

        public bool VisibleAutoScrollVertical
        {
            get
            {
                return this.visibleAutoVertical;
            }
            set
            {
                this.visibleAutoVertical = value;
                ScrollablePanel.ShowScrollBar(base.Handle, 1, value);
            }
        }

        private int getSBFromScrollEventType(ScrollEventType type)
        {
            int result = -1;
            switch (type)
            {
                case ScrollEventType.SmallDecrement:
                    result = 0;
                    break;
                case ScrollEventType.SmallIncrement:
                    result = 1;
                    break;
                case ScrollEventType.LargeDecrement:
                    result = 2;
                    break;
                case ScrollEventType.LargeIncrement:
                    result = 3;
                    break;
                case ScrollEventType.ThumbPosition:
                    result = 4;
                    break;
                case ScrollEventType.ThumbTrack:
                    result = 5;
                    break;
                case ScrollEventType.First:
                    result = 6;
                    break;
                case ScrollEventType.Last:
                    result = 7;
                    break;
                case ScrollEventType.EndScroll:
                    result = 8;
                    break;
            }
            return result;
        }

        private ScrollEventType getScrollEventType(IntPtr wParam)
        {
            ScrollEventType result;
            switch (ScrollablePanel.LoWord((int)wParam))
            {
                case 0:
                    result = ScrollEventType.SmallDecrement;
                    break;
                case 1:
                    result = ScrollEventType.SmallIncrement;
                    break;
                case 2:
                    result = ScrollEventType.LargeDecrement;
                    break;
                case 3:
                    result = ScrollEventType.LargeIncrement;
                    break;
                case 4:
                    result = ScrollEventType.ThumbPosition;
                    break;
                case 5:
                    result = ScrollEventType.ThumbTrack;
                    break;
                case 6:
                    result = ScrollEventType.First;
                    break;
                case 7:
                    result = ScrollEventType.Last;
                    break;
                case 8:
                    result = ScrollEventType.EndScroll;
                    break;
                default:
                    result = ScrollEventType.EndScroll;
                    break;
            }
            return result;
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            if (!(msg.HWnd != base.Handle))
            {
                int num = msg.Msg;
                switch (num)
                {
                    case 276:
                        try
                        {
                            ScrollEventType scrollEventType = this.getScrollEventType(msg.WParam);
                            ScrollEventArgs e = new ScrollEventArgs(scrollEventType, ScrollablePanel.GetScrollPos(base.Handle, 0));
                            this.ScrollHorizontal(this, e);
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    case 277:
                        try
                        {
                            ScrollEventType scrollEventType = this.getScrollEventType(msg.WParam);
                            ScrollEventArgs e = new ScrollEventArgs(scrollEventType, ScrollablePanel.GetScrollPos(base.Handle, 1));
                            this.ScrollVertical(this, e);
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    default:
                        if (num == 522)
                        {
                            if (this.VisibleAutoScrollVertical)
                            {
                                try
                                {
                                    int delta = ScrollablePanel.HiWord((int)msg.WParam);
                                    int y = ScrollablePanel.HiWord((int)msg.LParam);
                                    int x = ScrollablePanel.LoWord((int)msg.LParam);
                                    num = ScrollablePanel.LoWord((int)msg.WParam);
                                    MouseButtons button;
                                    if (num <= 16)
                                    {
                                        switch (num)
                                        {
                                            case 1:
                                                button = MouseButtons.Left;
                                                goto IL_103;
                                            case 2:
                                                button = MouseButtons.Right;
                                                goto IL_103;
                                            default:
                                                if (num == 16)
                                                {
                                                    button = MouseButtons.Middle;
                                                    goto IL_103;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (num == 32)
                                        {
                                            button = MouseButtons.XButton1;
                                            goto IL_103;
                                        }
                                        if (num == 64)
                                        {
                                            button = MouseButtons.XButton2;
                                            goto IL_103;
                                        }
                                    }
                                    button = MouseButtons.None;
                                    IL_103:
                                    MouseEventArgs e2 = new MouseEventArgs(button, 1, x, y, delta);
                                    this.ScrollMouseWheel(this, e2);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        break;
                }
            }
        }

        public void performScrollHorizontal(ScrollEventType type)
        {
            int sbfromScrollEventType = this.getSBFromScrollEventType(type);
            if (sbfromScrollEventType != -1)
            {
                ScrollablePanel.SendMessage(base.Handle, 276u, (UIntPtr)((ulong)((long)sbfromScrollEventType)), (IntPtr)0);
            }
        }

        public void performScrollVertical(ScrollEventType type)
        {
            int sbfromScrollEventType = this.getSBFromScrollEventType(type);
            if (sbfromScrollEventType != -1)
            {
                ScrollablePanel.SendMessage(base.Handle, 277u, (UIntPtr)((ulong)((long)sbfromScrollEventType)), (IntPtr)0);
            }
        }

        private void ScrollablePanel_Click(object sender, EventArgs e)
        {
            base.Focus();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int code);

        [DllImport("user32.dll")]
        public static extern bool EnableScrollBar(IntPtr hWnd, uint wSBflags, uint wArrows);

        [DllImport("user32.dll")]
        public static extern int SetScrollRange(IntPtr hWnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int HIWORD(IntPtr wParam);

        private static int MakeLong(int LoWord, int HiWord)
        {
            return HiWord << 16 | (LoWord & 65535);
        }

        private static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)(HiWord << 16 | (LoWord & 65535));
        }

        private static int HiWord(int number)
        {
            int result;
            if (((long)number & (long)-2147483648) == (long)-2147483648)
            {
                result = number >> 16;
            }
            else
            {
                result = (number >> 16 & 65535);
            }
            return result;
        }

        private static int LoWord(int number)
        {
            return number & 65535;
        }

        private const int SB_LINEUP = 0;

        private const int SB_LINEDOWN = 1;

        private const int SB_PAGEUP = 2;

        private const int SB_PAGEDOWN = 3;

        private const int SB_THUMBPOSITION = 4;

        private const int SB_THUMBTRACK = 5;

        private const int SB_TOP = 6;

        private const int SB_BOTTOM = 7;

        private const int SB_ENDSCROLL = 8;

        private const int WM_HSCROLL = 276;

        private const int WM_VSCROLL = 277;

        private const int WM_MOUSEWHEEL = 522;

        private const int WM_NCCALCSIZE = 131;

        private const int WM_PAINT = 15;

        private const int WM_SIZE = 5;

        private const uint SB_HORZ = 0u;

        private const uint SB_VERT = 1u;

        private const uint SB_CTL = 2u;

        private const uint SB_BOTH = 3u;

        private const uint ESB_DISABLE_BOTH = 3u;

        private const uint ESB_ENABLE_BOTH = 0u;

        private const int MK_LBUTTON = 1;

        private const int MK_RBUTTON = 2;

        private const int MK_SHIFT = 4;

        private const int MK_CONTROL = 8;

        private const int MK_MBUTTON = 16;

        private const int MK_XBUTTON1 = 32;

        private const int MK_XBUTTON2 = 64;

        private bool enableAutoHorizontal = true;

        private bool enableAutoVertical = true;

        private bool visibleAutoHorizontal = true;

        private bool visibleAutoVertical = true;

        private int autoScrollHorizontalMinimum = 0;

        private int autoScrollHorizontalMaximum = 100;

        private int autoScrollVerticalMinimum = 0;

        private int autoScrollVerticalMaximum = 100;
    }
}
