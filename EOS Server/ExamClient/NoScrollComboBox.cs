using System;
using System.Windows.Forms;

namespace ExamClient
{
    public class NoScrollComboBox : ComboBox
    {
        protected override void WndProc(ref Message m)
        {
            if (!(m.HWnd != base.Handle))
            {
                if (m.Msg != 522)
                {
                    base.WndProc(ref m);
                }
            }
        }
    }
}
