using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Pan slider control
	/// </summary>
	// Token: 0x020000CD RID: 205
	public class PanSlider : UserControl
	{
		/// <summary>
		/// True when pan value changed
		/// </summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000482 RID: 1154 RVA: 0x0000EF24 File Offset: 0x0000D124
		// (remove) Token: 0x06000483 RID: 1155 RVA: 0x0000EF5C File Offset: 0x0000D15C
		public event EventHandler PanChanged;

		/// <summary>
		/// Creates a new PanSlider control
		/// </summary>
		// Token: 0x06000484 RID: 1156 RVA: 0x0000EF91 File Offset: 0x0000D191
		public PanSlider()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		// Token: 0x06000485 RID: 1157 RVA: 0x0000EF9F File Offset: 0x0000D19F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		// Token: 0x06000486 RID: 1158 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		private void InitializeComponent()
		{
			base.Name = "PanSlider";
			base.Size = new Size(104, 16);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" />
		/// </summary>
		// Token: 0x06000487 RID: 1159 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		protected override void OnPaint(PaintEventArgs pe)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Alignment = StringAlignment.Center;
			string s;
			if ((double)this.pan == 0.0)
			{
				pe.Graphics.FillRectangle(Brushes.Orange, base.Width / 2 - 1, 1, 3, base.Height - 2);
				s = "C";
			}
			else if (this.pan > 0f)
			{
				pe.Graphics.FillRectangle(Brushes.Orange, base.Width / 2, 1, (int)((float)(base.Width / 2) * this.pan), base.Height - 2);
				s = string.Format("{0:F0}%R", this.pan * 100f);
			}
			else
			{
				pe.Graphics.FillRectangle(Brushes.Orange, (int)((float)(base.Width / 2) * (this.pan + 1f)), 1, (int)((float)(base.Width / 2) * (0f - this.pan)), base.Height - 2);
				s = string.Format("{0:F0}%L", this.pan * -100f);
			}
			pe.Graphics.DrawRectangle(Pens.Black, 0, 0, base.Width - 1, base.Height - 1);
			pe.Graphics.DrawString(s, this.Font, Brushes.Black, base.ClientRectangle, stringFormat);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		// Token: 0x06000488 RID: 1160 RVA: 0x0000F143 File Offset: 0x0000D343
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.SetPanFromMouse(e.X);
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		/// <param name="e"></param>
		// Token: 0x06000489 RID: 1161 RVA: 0x0000F165 File Offset: 0x0000D365
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.SetPanFromMouse(e.X);
			base.OnMouseDown(e);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000F17A File Offset: 0x0000D37A
		private void SetPanFromMouse(int x)
		{
			this.Pan = (float)x / (float)base.Width * 2f - 1f;
		}

		/// <summary>
		/// The current Pan setting
		/// </summary>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000F198 File Offset: 0x0000D398
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public float Pan
		{
			get
			{
				return this.pan;
			}
			set
			{
				if (value < -1f)
				{
					value = -1f;
				}
				if (value > 1f)
				{
					value = 1f;
				}
				if (value != this.pan)
				{
					this.pan = value;
					if (this.PanChanged != null)
					{
						this.PanChanged(this, EventArgs.Empty);
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000546 RID: 1350
		private Container components;

		// Token: 0x04000547 RID: 1351
		private float pan;
	}
}
