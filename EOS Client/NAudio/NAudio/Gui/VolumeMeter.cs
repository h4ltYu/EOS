using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Implements a rudimentary volume meter
	/// </summary>
	// Token: 0x020000CF RID: 207
	public class VolumeMeter : Control
	{
		/// <summary>
		/// Basic volume meter
		/// </summary>
		// Token: 0x0600049D RID: 1181 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		public VolumeMeter()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.MinDb = -60f;
			this.MaxDb = 18f;
			this.Amplitude = 0f;
			this.Orientation = Orientation.Vertical;
			this.InitializeComponent();
			this.OnForeColorChanged(EventArgs.Empty);
		}

		/// <summary>
		/// On Fore Color Changed
		/// </summary>
		// Token: 0x0600049E RID: 1182 RVA: 0x0000F62C File Offset: 0x0000D82C
		protected override void OnForeColorChanged(EventArgs e)
		{
			this.foregroundBrush = new SolidBrush(this.ForeColor);
			base.OnForeColorChanged(e);
		}

		/// <summary>
		/// Current Value
		/// </summary>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000F646 File Offset: 0x0000D846
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000F64E File Offset: 0x0000D84E
		[DefaultValue(-3.0)]
		public float Amplitude
		{
			get
			{
				return this.amplitude;
			}
			set
			{
				this.amplitude = value;
				base.Invalidate();
			}
		}

		/// <summary>
		/// Minimum decibels
		/// </summary>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000F65D File Offset: 0x0000D85D
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000F665 File Offset: 0x0000D865
		[DefaultValue(-60.0)]
		public float MinDb { get; set; }

		/// <summary>
		/// Maximum decibels
		/// </summary>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000F66E File Offset: 0x0000D86E
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000F676 File Offset: 0x0000D876
		[DefaultValue(18.0)]
		public float MaxDb { get; set; }

		/// <summary>
		/// Meter orientation
		/// </summary>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000F67F File Offset: 0x0000D87F
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000F687 File Offset: 0x0000D887
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation { get; set; }

		/// <summary>
		/// Paints the volume meter
		/// </summary>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000F690 File Offset: 0x0000D890
		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.DrawRectangle(Pens.Black, 0, 0, base.Width - 1, base.Height - 1);
			double num = 20.0 * Math.Log10((double)this.Amplitude);
			if (num < (double)this.MinDb)
			{
				num = (double)this.MinDb;
			}
			if (num > (double)this.MaxDb)
			{
				num = (double)this.MaxDb;
			}
			double num2 = (num - (double)this.MinDb) / (double)(this.MaxDb - this.MinDb);
			int num3 = base.Width - 2;
			int num4 = base.Height - 2;
			if (this.Orientation == Orientation.Horizontal)
			{
				num3 = (int)((double)num3 * num2);
				pe.Graphics.FillRectangle(this.foregroundBrush, 1, 1, num3, num4);
				return;
			}
			num4 = (int)((double)num4 * num2);
			pe.Graphics.FillRectangle(this.foregroundBrush, 1, base.Height - 1 - num4, num3, num4);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F76D File Offset: 0x0000D96D
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
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F78C File Offset: 0x0000D98C
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000551 RID: 1361
		private Brush foregroundBrush;

		// Token: 0x04000552 RID: 1362
		private float amplitude;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000553 RID: 1363
		private IContainer components;
	}
}
