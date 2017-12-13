using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace NAudio.Gui
{
	/// <summary>
	/// Control for viewing waveforms
	/// </summary>
	// Token: 0x020000D2 RID: 210
	public class WaveViewer : UserControl
	{
		/// <summary>
		/// Creates a new WaveViewer control
		/// </summary>
		// Token: 0x060004BD RID: 1213 RVA: 0x0000FBDA File Offset: 0x0000DDDA
		public WaveViewer()
		{
			this.InitializeComponent();
			this.DoubleBuffered = true;
		}

		/// <summary>
		/// sets the associated wavestream
		/// </summary>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000FBFA File Offset: 0x0000DDFA
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000FC02 File Offset: 0x0000DE02
		public WaveStream WaveStream
		{
			get
			{
				return this.waveStream;
			}
			set
			{
				this.waveStream = value;
				if (this.waveStream != null)
				{
					this.bytesPerSample = this.waveStream.WaveFormat.BitsPerSample / 8 * this.waveStream.WaveFormat.Channels;
				}
				base.Invalidate();
			}
		}

		/// <summary>
		/// The zoom level, in samples per pixel
		/// </summary>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000FC42 File Offset: 0x0000DE42
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000FC4A File Offset: 0x0000DE4A
		public int SamplesPerPixel
		{
			get
			{
				return this.samplesPerPixel;
			}
			set
			{
				this.samplesPerPixel = value;
				base.Invalidate();
			}
		}

		/// <summary>
		/// Start position (currently in bytes)
		/// </summary>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000FC59 File Offset: 0x0000DE59
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000FC61 File Offset: 0x0000DE61
		public long StartPosition
		{
			get
			{
				return this.startPosition;
			}
			set
			{
				this.startPosition = value;
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000FC6A File Offset: 0x0000DE6A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" />
		/// </summary>
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.waveStream != null)
			{
				this.waveStream.Position = 0L;
				byte[] array = new byte[this.samplesPerPixel * this.bytesPerSample];
				this.waveStream.Position = this.startPosition + (long)(e.ClipRectangle.Left * this.bytesPerSample * this.samplesPerPixel);
				for (float num = (float)e.ClipRectangle.X; num < (float)e.ClipRectangle.Right; num += 1f)
				{
					short num2 = 0;
					short num3 = 0;
					int num4 = this.waveStream.Read(array, 0, this.samplesPerPixel * this.bytesPerSample);
					if (num4 == 0)
					{
						break;
					}
					for (int i = 0; i < num4; i += 2)
					{
						short num5 = BitConverter.ToInt16(array, i);
						if (num5 < num2)
						{
							num2 = num5;
						}
						if (num5 > num3)
						{
							num3 = num5;
						}
					}
					float num6 = ((float)num2 - -32768f) / 65535f;
					float num7 = ((float)num3 - -32768f) / 65535f;
					e.Graphics.DrawLine(Pens.Black, num, (float)base.Height * num6, num, (float)base.Height * num7);
				}
			}
			base.OnPaint(e);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		// Token: 0x04000560 RID: 1376
		private Container components;

		// Token: 0x04000561 RID: 1377
		private WaveStream waveStream;

		// Token: 0x04000562 RID: 1378
		private int samplesPerPixel = 128;

		// Token: 0x04000563 RID: 1379
		private long startPosition;

		// Token: 0x04000564 RID: 1380
		private int bytesPerSample;
	}
}
