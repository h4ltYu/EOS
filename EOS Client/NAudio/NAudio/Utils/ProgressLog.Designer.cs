using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Utils
{
	/// <summary>
	/// A thread-safe Progress Log Control
	/// </summary>
	// Token: 0x0200011B RID: 283
	public class ProgressLog : UserControl
	{
		/// <summary>
		/// Creates a new progress log control
		/// </summary>
		// Token: 0x06000649 RID: 1609 RVA: 0x00014517 File Offset: 0x00012717
		public ProgressLog()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// The contents of the log as text
		/// </summary>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00014525 File Offset: 0x00012725
		public new string Text
		{
			get
			{
				return this.richTextBoxLog.Text;
			}
		}

		/// <summary>
		/// Log a message
		/// </summary>
		// Token: 0x0600064B RID: 1611 RVA: 0x00014534 File Offset: 0x00012734
		public void LogMessage(Color color, string message)
		{
			if (this.richTextBoxLog.InvokeRequired)
			{
				base.Invoke(new ProgressLog.LogMessageDelegate(this.LogMessage), new object[]
				{
					color,
					message
				});
				return;
			}
			this.richTextBoxLog.SelectionStart = this.richTextBoxLog.TextLength;
			this.richTextBoxLog.SelectionColor = color;
			this.richTextBoxLog.AppendText(message);
			this.richTextBoxLog.AppendText(Environment.NewLine);
		}

		/// <summary>
		/// Clear the log
		/// </summary>
		// Token: 0x0600064C RID: 1612 RVA: 0x000145B5 File Offset: 0x000127B5
		public void ClearLog()
		{
			if (this.richTextBoxLog.InvokeRequired)
			{
				base.Invoke(new ProgressLog.ClearLogDelegate(this.ClearLog), new object[0]);
				return;
			}
			this.richTextBoxLog.Clear();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		// Token: 0x0600064D RID: 1613 RVA: 0x000145E9 File Offset: 0x000127E9
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
		// Token: 0x0600064E RID: 1614 RVA: 0x00014608 File Offset: 0x00012808
		private void InitializeComponent()
		{
			this.richTextBoxLog = new RichTextBox();
			base.SuspendLayout();
			this.richTextBoxLog.BorderStyle = BorderStyle.None;
			this.richTextBoxLog.Dock = DockStyle.Fill;
			this.richTextBoxLog.Location = new Point(1, 1);
			this.richTextBoxLog.Name = "richTextBoxLog";
			this.richTextBoxLog.ReadOnly = true;
			this.richTextBoxLog.Size = new Size(311, 129);
			this.richTextBoxLog.TabIndex = 0;
			this.richTextBoxLog.Text = "";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ControlDarkDark;
			base.Controls.Add(this.richTextBoxLog);
			base.Name = "ProgressLog";
			base.Padding = new Padding(1);
			base.Size = new Size(313, 131);
			base.ResumeLayout(false);
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		// Token: 0x040006ED RID: 1773
		private IContainer components;

		// Token: 0x040006EE RID: 1774
		private RichTextBox richTextBoxLog;

		// Token: 0x0200011C RID: 284
		// (Invoke) Token: 0x06000650 RID: 1616
		private delegate void LogMessageDelegate(Color color, string message);

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000654 RID: 1620
		private delegate void ClearLogDelegate();
	}
}
