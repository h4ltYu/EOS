using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Summary description for Fader.
	/// </summary>
	// Token: 0x020000CC RID: 204
	public class Fader : Control
	{
		/// <summary>
		/// Creates a new Fader control
		/// </summary>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000EC32 File Offset: 0x0000CE32
		public Fader()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000EC68 File Offset: 0x0000CE68
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000EC88 File Offset: 0x0000CE88
		private void DrawSlider(Graphics g)
		{
			Brush brush = new SolidBrush(Color.White);
			Pen pen = new Pen(Color.Black);
			this.sliderRectangle.X = (base.Width - this.SliderWidth) / 2;
			this.sliderRectangle.Width = this.SliderWidth;
			this.sliderRectangle.Y = (int)((float)(base.Height - this.SliderHeight) * this.percent);
			this.sliderRectangle.Height = this.SliderHeight;
			g.FillRectangle(brush, this.sliderRectangle);
			g.DrawLine(pen, this.sliderRectangle.Left, this.sliderRectangle.Top + this.sliderRectangle.Height / 2, this.sliderRectangle.Right, this.sliderRectangle.Top + this.sliderRectangle.Height / 2);
			brush.Dispose();
			pen.Dispose();
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" />
		/// </summary>
		// Token: 0x06000475 RID: 1141 RVA: 0x0000ED70 File Offset: 0x0000CF70
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (this.Orientation == Orientation.Vertical)
			{
				Brush brush = new SolidBrush(Color.Black);
				graphics.FillRectangle(brush, base.Width / 2, this.SliderHeight / 2, 2, base.Height - this.SliderHeight);
				brush.Dispose();
				this.DrawSlider(graphics);
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		// Token: 0x06000476 RID: 1142 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.sliderRectangle.Contains(e.X, e.Y))
			{
				this.dragging = true;
				this.dragY = e.Y - this.sliderRectangle.Y;
			}
			base.OnMouseDown(e);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>
		// Token: 0x06000477 RID: 1143 RVA: 0x0000EE20 File Offset: 0x0000D020
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.dragging)
			{
				int num = e.Y - this.dragY;
				if (num < 0)
				{
					this.percent = 0f;
				}
				else if (num > base.Height - this.SliderHeight)
				{
					this.percent = 1f;
				}
				else
				{
					this.percent = (float)num / (float)(base.Height - this.SliderHeight);
				}
				base.Invalidate();
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" />
		/// </summary>        
		// Token: 0x06000478 RID: 1144 RVA: 0x0000EE95 File Offset: 0x0000D095
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.dragging = false;
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Minimum value of this fader
		/// </summary>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				this.minimum = value;
			}
		}

		/// <summary>
		/// Maximum value of this fader
		/// </summary>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000EEBE File Offset: 0x0000D0BE
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				this.maximum = value;
			}
		}

		/// <summary>
		/// Current value of this fader
		/// </summary>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000EEE6 File Offset: 0x0000D0E6
		public int Value
		{
			get
			{
				return (int)(this.percent * (float)(this.maximum - this.minimum)) + this.minimum;
			}
			set
			{
				this.percent = (float)(value - this.minimum) / (float)(this.maximum - this.minimum);
			}
		}

		/// <summary>
		/// Fader orientation
		/// </summary>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000EF06 File Offset: 0x0000D106
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000EF0E File Offset: 0x0000D10E
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				this.orientation = value;
			}
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		// Token: 0x06000481 RID: 1153 RVA: 0x0000EF17 File Offset: 0x0000D117
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x0400053C RID: 1340
		private int minimum;

		// Token: 0x0400053D RID: 1341
		private int maximum;

		// Token: 0x0400053E RID: 1342
		private float percent;

		// Token: 0x0400053F RID: 1343
		private Orientation orientation;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000540 RID: 1344
		private Container components;

		// Token: 0x04000541 RID: 1345
		private readonly int SliderHeight = 30;

		// Token: 0x04000542 RID: 1346
		private readonly int SliderWidth = 15;

		// Token: 0x04000543 RID: 1347
		private Rectangle sliderRectangle = default(Rectangle);

		// Token: 0x04000544 RID: 1348
		private bool dragging;

		// Token: 0x04000545 RID: 1349
		private int dragY;
	}
}
