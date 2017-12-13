namespace EOSClient
{
	// Token: 0x02000006 RID: 6
	public partial class AuthenticateForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026B8 File Offset: 0x000008B8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026F4 File Offset: 0x000008F4
		private void InitializeComponent()
		{
			global::System.Configuration.AppSettingsReader appSettingsReader = new global::System.Configuration.AppSettingsReader();
			this.btnLogin = new global::System.Windows.Forms.Button();
			this.txtUser = new global::System.Windows.Forms.TextBox();
			this.txtPassword = new global::System.Windows.Forms.TextBox();
			this.txtDomain = new global::System.Windows.Forms.TextBox();
			this.lblUser = new global::System.Windows.Forms.Label();
			this.lblPass = new global::System.Windows.Forms.Label();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.lblDomain = new global::System.Windows.Forms.Label();
			this.lblExamCode = new global::System.Windows.Forms.Label();
			this.txtExamCode = new global::System.Windows.Forms.TextBox();
			this.lblMessage = new global::System.Windows.Forms.Label();
			this.lblVersion = new global::System.Windows.Forms.Label();
			this.linkLBLCheckFont = new global::System.Windows.Forms.LinkLabel();
			this.lblLinkCheckSound = new global::System.Windows.Forms.LinkLabel();
			base.SuspendLayout();
			this.btnLogin.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnLogin.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnLogin.Location = new global::System.Drawing.Point(95, 192);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new global::System.Drawing.Size(86, 23);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Login";
			this.btnLogin.Click += new global::System.EventHandler(this.btnLogin_Click);
			this.txtUser.Location = new global::System.Drawing.Point(96, 80);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new global::System.Drawing.Size(272, 20);
			this.txtUser.TabIndex = 1;
			this.txtPassword.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txtPassword.Location = new global::System.Drawing.Point(96, 120);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new global::System.Drawing.Size(272, 22);
			this.txtPassword.TabIndex = 2;
			this.txtDomain.Enabled = false;
			this.txtDomain.Location = new global::System.Drawing.Point(96, 160);
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new global::System.Drawing.Size(272, 20);
			this.txtDomain.TabIndex = 9;
			this.txtDomain.Text = (string)appSettingsReader.GetValue("domain", typeof(string));
			this.lblUser.AutoSize = true;
			this.lblUser.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblUser.Location = new global::System.Drawing.Point(15, 80);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new global::System.Drawing.Size(80, 16);
			this.lblUser.TabIndex = 6;
			this.lblUser.Text = "User Name:";
			this.lblPass.AutoSize = true;
			this.lblPass.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblPass.Location = new global::System.Drawing.Point(24, 120);
			this.lblPass.Name = "lblPass";
			this.lblPass.Size = new global::System.Drawing.Size(71, 16);
			this.lblPass.TabIndex = 7;
			this.lblPass.Text = "Password:";
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnCancel.Location = new global::System.Drawing.Point(230, 192);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(86, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Exit";
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.lblDomain.AutoSize = true;
			this.lblDomain.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblDomain.Location = new global::System.Drawing.Point(37, 160);
			this.lblDomain.Name = "lblDomain";
			this.lblDomain.Size = new global::System.Drawing.Size(58, 16);
			this.lblDomain.TabIndex = 5;
			this.lblDomain.Text = "Domain:";
			this.lblExamCode.AutoSize = true;
			this.lblExamCode.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblExamCode.Location = new global::System.Drawing.Point(14, 38);
			this.lblExamCode.Name = "lblExamCode";
			this.lblExamCode.Size = new global::System.Drawing.Size(81, 16);
			this.lblExamCode.TabIndex = 10;
			this.lblExamCode.Text = "Exam Code:";
			this.txtExamCode.Location = new global::System.Drawing.Point(96, 38);
			this.txtExamCode.Name = "txtExamCode";
			this.txtExamCode.Size = new global::System.Drawing.Size(272, 20);
			this.txtExamCode.TabIndex = 0;
			this.lblMessage.AutoSize = true;
			this.lblMessage.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblMessage.ForeColor = global::System.Drawing.Color.Red;
			this.lblMessage.Location = new global::System.Drawing.Point(23, 250);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new global::System.Drawing.Size(355, 17);
			this.lblMessage.TabIndex = 11;
			this.lblMessage.Text = "Register the exam may take some minutes, please wait!";
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new global::System.Drawing.Point(266, 9);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new global::System.Drawing.Size(102, 13);
			this.lblVersion.TabIndex = 12;
			this.lblVersion.Text = "Version 21.07.20.17";
			this.linkLBLCheckFont.AutoSize = true;
			this.linkLBLCheckFont.Location = new global::System.Drawing.Point(227, 228);
			this.linkLBLCheckFont.Name = "linkLBLCheckFont";
			this.linkLBLCheckFont.Size = new global::System.Drawing.Size(59, 13);
			this.linkLBLCheckFont.TabIndex = 13;
			this.linkLBLCheckFont.TabStop = true;
			this.linkLBLCheckFont.Text = "Check font";
			this.linkLBLCheckFont.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLBLCheckFont_LinkClicked);
			this.lblLinkCheckSound.AutoSize = true;
			this.lblLinkCheckSound.Location = new global::System.Drawing.Point(93, 228);
			this.lblLinkCheckSound.Name = "lblLinkCheckSound";
			this.lblLinkCheckSound.Size = new global::System.Drawing.Size(110, 13);
			this.lblLinkCheckSound.TabIndex = 14;
			this.lblLinkCheckSound.TabStop = true;
			this.lblLinkCheckSound.Text = "Check sound (7 secs)";
			this.lblLinkCheckSound.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkCheckSound_LinkClicked);
			base.AcceptButton = this.btnLogin;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.ClientSize = new global::System.Drawing.Size(398, 278);
			base.Controls.Add(this.lblLinkCheckSound);
			base.Controls.Add(this.linkLBLCheckFont);
			base.Controls.Add(this.lblVersion);
			base.Controls.Add(this.lblMessage);
			base.Controls.Add(this.lblExamCode);
			base.Controls.Add(this.txtExamCode);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.lblPass);
			base.Controls.Add(this.lblUser);
			base.Controls.Add(this.txtDomain);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.txtUser);
			base.Controls.Add(this.lblDomain);
			base.Controls.Add(this.btnLogin);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AuthenticateForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EOS Login Form";
			base.Load += new global::System.EventHandler(this.AuthenticateForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Button btnLogin;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.TextBox txtUser;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.TextBox txtPassword;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.TextBox txtDomain;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.Label lblUser;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.Label lblPass;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.Label lblDomain;

		// Token: 0x04000010 RID: 16
		private global::System.ComponentModel.Container components = null;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.Label lblExamCode;

		// Token: 0x04000012 RID: 18
		private global::System.Windows.Forms.Label lblMessage;

		// Token: 0x04000013 RID: 19
		private global::System.Windows.Forms.Label lblVersion;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.LinkLabel linkLBLCheckFont;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.LinkLabel lblLinkCheckSound;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.TextBox txtExamCode;
	}
}
