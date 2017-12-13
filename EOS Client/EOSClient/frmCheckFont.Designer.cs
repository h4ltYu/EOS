namespace EOSClient
{
	// Token: 0x02000003 RID: 3
	public partial class frmCheckFont : global::System.Windows.Forms.Form
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		private void InitializeComponent()
		{
			this.btnClose = new global::System.Windows.Forms.Button();
			this.txtFontGuide = new global::System.Windows.Forms.TextBox();
			this.lblTestDisplay = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.btnClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new global::System.Drawing.Point(628, 480);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			this.txtFontGuide.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtFontGuide.BackColor = global::System.Drawing.Color.White;
			this.txtFontGuide.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtFontGuide.Location = new global::System.Drawing.Point(12, 12);
			this.txtFontGuide.Multiline = true;
			this.txtFontGuide.Name = "txtFontGuide";
			this.txtFontGuide.ReadOnly = true;
			this.txtFontGuide.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtFontGuide.Size = new global::System.Drawing.Size(691, 448);
			this.txtFontGuide.TabIndex = 1;
			this.lblTestDisplay.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.lblTestDisplay.AutoSize = true;
			this.lblTestDisplay.Font = new global::System.Drawing.Font("MS Mincho", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblTestDisplay.Location = new global::System.Drawing.Point(9, 480);
			this.lblTestDisplay.Name = "lblTestDisplay";
			this.lblTestDisplay.Size = new global::System.Drawing.Size(173, 39);
			this.lblTestDisplay.TabIndex = 2;
			this.lblTestDisplay.Text = "ベトナム (in Japanese)\r\n越南 (in Chinese)\r\nViệt Nam (in Vietnamese)\r\n";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(715, 532);
			base.Controls.Add(this.lblTestDisplay);
			base.Controls.Add(this.txtFontGuide);
			base.Controls.Add(this.btnClose);
			base.Name = "frmCheckFont";
			this.Text = "Check fonts for Japanese/Chinese exam.";
			base.Load += new global::System.EventHandler(this.frmCheckFont_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000001 RID: 1
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000002 RID: 2
		private global::System.Windows.Forms.Button btnClose;

		// Token: 0x04000003 RID: 3
		private global::System.Windows.Forms.TextBox txtFontGuide;

		// Token: 0x04000004 RID: 4
		private global::System.Windows.Forms.Label lblTestDisplay;
	}
}
