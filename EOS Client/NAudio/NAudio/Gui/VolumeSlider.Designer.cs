using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// VolumeSlider control
	/// </summary>
	// Token: 0x020000D0 RID: 208
	public class VolumeSlider : UserControl
	{
		/// <summary>
		/// Volume changed event
		/// </summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004AA RID: 1194 RVA: 0x0000F79C File Offset: 0x0000D99C
		// (remove) Token: 0x060004AB RID: 1195 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public event EventHandler VolumeChanged;

		/// <summary>
		/// Creates a new VolumeSlider control
		/// </summary>
		// Token: 0x060004AC RID: 1196 RVA: 0x0000F809 File Offset: 0x0000DA09
		public VolumeSlider()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000F82D File Offset: 0x0000DA2D
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
		// Token: 0x060004AE RID: 1198 RVA: 0x0000F84C File Offset: 0x0000DA4C
		private void InitializeComponent()
		{
			base.Name = "VolumeSlider";
			base.Size = new Size(96, 16);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" />
		/// </summary>
		// Token: 0x060004AF RID: 1199 RVA: 0x0000F868 File Offset: 0x0000DA68
		protected override void OnPaint(PaintEventArgs pe)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Alignment = StringAlignment.Center;
			pe.Graphics.DrawRectangle(Pens.Black, 0, 0, base.Width - 1, base.Height - 1);
			float num = 20f * (float)Math.Log10((double)this.Volume);
			float num2 = 1f - num / this.MinDb;
			pe.Graphics.FillRectangle(Brushes.LightGreen, 1, 1, (int)((float)(base.Width - 2) * num2), base.Height - 2);
			string s = string.Format("{0:F2} dB", num);
			pe.Graphics.DrawString(s, this.Font, Brushes.Black, base.ClientRectangle, stringFormat);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F928 File Offset: 0x0000DB28
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.SetVolumeFromMouse(e.X);
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F94A File Offset: 0x0000DB4A
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.SetVolumeFromMouse(e.X);
			base.OnMouseDown(e);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F960 File Offset: 0x0000DB60
		private void SetVolumeFromMouse(int x)
		{
			float num = (1f - (float)x / (float)base.Width) * this.MinDb;
			if (x <= 0)
			{
				this.Volume = 0f;
				return;
			}
			this.Volume = (float)Math.Pow(10.0, (double)(num / 20f));
		}

		/// <summary>
		/// The volume for this control
		/// </summary>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000F9B2 File Offset: 0x0000DBB2
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		[DefaultValue(1f)]
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				if (value < 0f)
				{
					value = 0f;
				}
				if (value > 1f)
				{
					value = 1f;
				}
				if (this.volume != value)
				{
					this.volume = value;
					if (this.VolumeChanged != null)
					{
						this.VolumeChanged(this, EventArgs.Empty);
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000557 RID: 1367
		private Container components;

		// Token: 0x04000558 RID: 1368
		private float volume = 1f;

		// Token: 0x04000559 RID: 1369
		private float MinDb = -48f;
	}
}
