using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Control that represents a potentiometer
	/// TODO list:
	/// Optional Log scale
	/// Optional reverse scale
	/// Keyboard control
	/// Optional bitmap mode
	/// Optional complete draw mode
	/// Tooltip support
	/// </summary>
	// Token: 0x020000CE RID: 206
	public class Pot : UserControl
	{
		/// <summary>
		/// Value changed event
		/// </summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600048D RID: 1165 RVA: 0x0000F1FC File Offset: 0x0000D3FC
		// (remove) Token: 0x0600048E RID: 1166 RVA: 0x0000F234 File Offset: 0x0000D434
		public event EventHandler ValueChanged;

		/// <summary>
		/// Creates a new pot control
		/// </summary>
		// Token: 0x0600048F RID: 1167 RVA: 0x0000F269 File Offset: 0x0000D469
		public Pot()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.InitializeComponent();
		}

		/// <summary>
		/// Minimum Value of the Pot
		/// </summary>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000F2A1 File Offset: 0x0000D4A1
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
		public double Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (value >= this.maximum)
				{
					throw new ArgumentOutOfRangeException("Minimum must be less than maximum");
				}
				this.minimum = value;
				if (this.Value < this.minimum)
				{
					this.Value = this.minimum;
				}
			}
		}

		/// <summary>
		/// Maximum Value of the Pot
		/// </summary>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public double Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (value <= this.minimum)
				{
					throw new ArgumentOutOfRangeException("Maximum must be greater than minimum");
				}
				this.maximum = value;
				if (this.Value > this.maximum)
				{
					this.Value = this.maximum;
				}
			}
		}

		/// <summary>
		/// The current value of the pot
		/// </summary>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000F31F File Offset: 0x0000D51F
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000F327 File Offset: 0x0000D527
		public double Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.SetValue(value, false);
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000F331 File Offset: 0x0000D531
		private void SetValue(double newValue, bool raiseEvents)
		{
			if (this.value != newValue)
			{
				this.value = newValue;
				if (raiseEvents && this.ValueChanged != null)
				{
					this.ValueChanged(this, EventArgs.Empty);
				}
				base.Invalidate();
			}
		}

		/// <summary>
		/// Draws the control
		/// </summary>
		// Token: 0x06000497 RID: 1175 RVA: 0x0000F368 File Offset: 0x0000D568
		protected override void OnPaint(PaintEventArgs e)
		{
			int num = Math.Min(base.Width - 4, base.Height - 4);
			Pen pen = new Pen(this.ForeColor, 3f);
			pen.LineJoin = LineJoin.Round;
			GraphicsState gstate = e.Graphics.Save();
			e.Graphics.TranslateTransform((float)(base.Width / 2), (float)(base.Height / 2));
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.DrawArc(pen, new Rectangle(num / -2, num / -2, num, num), 135f, 270f);
			double num2 = (this.value - this.minimum) / (this.maximum - this.minimum);
			double num3 = 135.0 + num2 * 270.0;
			double num4 = (double)num / 2.0 * Math.Cos(3.1415926535897931 * num3 / 180.0);
			double num5 = (double)num / 2.0 * Math.Sin(3.1415926535897931 * num3 / 180.0);
			e.Graphics.DrawLine(pen, 0f, 0f, (float)num4, (float)num5);
			e.Graphics.Restore(gstate);
			base.OnPaint(e);
		}

		/// <summary>
		/// Handles the mouse down event to allow changing value by dragging
		/// </summary>
		// Token: 0x06000498 RID: 1176 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.dragging = true;
			this.beginDragY = e.Y;
			this.beginDragValue = this.value;
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Handles the mouse up event to allow changing value by dragging
		/// </summary>
		// Token: 0x06000499 RID: 1177 RVA: 0x0000F4DB File Offset: 0x0000D6DB
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.dragging = false;
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Handles the mouse down event to allow changing value by dragging
		/// </summary>
		// Token: 0x0600049A RID: 1178 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.dragging)
			{
				int num = this.beginDragY - e.Y;
				double num2 = (this.maximum - this.minimum) * ((double)num / 150.0);
				double num3 = this.beginDragValue + num2;
				if (num3 < this.minimum)
				{
					num3 = this.minimum;
				}
				if (num3 > this.maximum)
				{
					num3 = this.maximum;
				}
				this.SetValue(num3, true);
			}
			base.OnMouseMove(e);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		// Token: 0x0600049B RID: 1179 RVA: 0x0000F562 File Offset: 0x0000D762
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
		// Token: 0x0600049C RID: 1180 RVA: 0x0000F584 File Offset: 0x0000D784
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "Pot";
			base.Size = new Size(32, 32);
			base.ResumeLayout(false);
		}

		// Token: 0x04000549 RID: 1353
		private double minimum;

		// Token: 0x0400054A RID: 1354
		private double maximum = 1.0;

		// Token: 0x0400054B RID: 1355
		private double value = 0.5;

		// Token: 0x0400054C RID: 1356
		private int beginDragY;

		// Token: 0x0400054D RID: 1357
		private double beginDragValue;

		// Token: 0x0400054E RID: 1358
		private bool dragging;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000550 RID: 1360
		private IContainer components;
	}
}
