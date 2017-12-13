using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Windows Forms control for painting audio waveforms
	/// </summary>
	// Token: 0x020000D1 RID: 209
	public class WaveformPainter : Control
	{
		/// <summary>
		/// Constructs a new instance of the WaveFormPainter class
		/// </summary>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000FA16 File Offset: 0x0000DC16
		public WaveformPainter()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.InitializeComponent();
			this.OnForeColorChanged(EventArgs.Empty);
			this.OnResize(EventArgs.Empty);
		}

		/// <summary>
		/// On Resize
		/// </summary>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000FA56 File Offset: 0x0000DC56
		protected override void OnResize(EventArgs e)
		{
			this.maxSamples = base.Width;
			base.OnResize(e);
		}

		/// <summary>
		/// On ForeColor Changed
		/// </summary>
		/// <param name="e"></param>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		protected override void OnForeColorChanged(EventArgs e)
		{
			this.foregroundPen = new Pen(this.ForeColor);
			base.OnForeColorChanged(e);
		}

		/// <summary>
		/// Add Max Value
		/// </summary>
		/// <param name="maxSample"></param>
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public void AddMax(float maxSample)
		{
			if (this.maxSamples == 0)
			{
				return;
			}
			if (this.samples.Count <= this.maxSamples)
			{
				this.samples.Add(maxSample);
			}
			else if (this.insertPos < this.maxSamples)
			{
				this.samples[this.insertPos] = maxSample;
			}
			this.insertPos++;
			this.insertPos %= this.maxSamples;
			base.Invalidate();
		}

		/// <summary>
		/// On Paint
		/// </summary>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000FB08 File Offset: 0x0000DD08
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			for (int i = 0; i < base.Width; i++)
			{
				float num = (float)base.Height * this.GetSample(i - base.Width + this.insertPos);
				float num2 = ((float)base.Height - num) / 2f;
				pe.Graphics.DrawLine(this.foregroundPen, (float)i, num2, (float)i, num2 + num);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private float GetSample(int index)
		{
			if (index < 0)
			{
				index += this.maxSamples;
			}
			if (index >= 0 & index < this.samples.Count)
			{
				return this.samples[index];
			}
			return 0f;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		// Token: 0x060004BB RID: 1211 RVA: 0x0000FBAE File Offset: 0x0000DDAE
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
		// Token: 0x060004BC RID: 1212 RVA: 0x0000FBCD File Offset: 0x0000DDCD
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x0400055B RID: 1371
		private Pen foregroundPen;

		// Token: 0x0400055C RID: 1372
		private List<float> samples = new List<float>(1000);

		// Token: 0x0400055D RID: 1373
		private int maxSamples;

		// Token: 0x0400055E RID: 1374
		private int insertPos;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		// Token: 0x0400055F RID: 1375
		private IContainer components;
	}
}
