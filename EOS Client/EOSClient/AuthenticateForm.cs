using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using EncryptData;
using IRemote;
using NAudio.Wave;
using QuestionLib;

namespace EOSClient
{
	// Token: 0x02000006 RID: 6
	public partial class AuthenticateForm : Form
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002684 File Offset: 0x00000884
		public AuthenticateForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000303C File Offset: 0x0000123C
		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (this.txtExamCode.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please provide an Exam code");
			}
			else if (this.txtUser.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please provide an username");
			}
			else if (this.txtPassword.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please provide a password");
			}
			else if (this.txtDomain.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please provide a domain address");
			}
			else
			{
				try
				{
					string url = string.Concat(new object[]
					{
						"tcp://",
						this.si.IP,
						":",
						this.si.Port,
						"/Server"
					});
					IRemoteServer remoteServer = (IRemoteServer)Activator.GetObject(typeof(IRemoteServer), url);
					RegisterData registerData = new RegisterData();
					registerData.Login = this.txtUser.Text;
					registerData.Password = this.txtPassword.Text;
					registerData.ExamCode = this.txtExamCode.Text;
					registerData.Machine = Environment.MachineName.ToUpper();
					EOSData eosdata = remoteServer.ConductExam(registerData);
					if (eosdata.Status == RegisterStatus.EXAM_CODE_NOT_EXISTS)
					{
						MessageBox.Show("Exam code is not available!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (eosdata.Status == RegisterStatus.FINISHED)
					{
						MessageBox.Show("The exam is finished!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (eosdata.Status == RegisterStatus.REGISTERED)
					{
						MessageBox.Show("This user [" + this.txtUser.Text + "] is already registered. Need re-assign to continue the exam.", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (eosdata.Status == RegisterStatus.REGISTER_ERROR)
					{
						MessageBox.Show("Register ERROR, try again", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (eosdata.Status == RegisterStatus.NOT_ALLOW_MACHINE)
					{
						MessageBox.Show("Your machine is not allow to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (eosdata.Status == RegisterStatus.NOT_ALLOW_STUDENT)
					{
						MessageBox.Show("The account is NOT allow to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (eosdata.Status == RegisterStatus.LOGIN_FAILED)
					{
						MessageBox.Show("Sorry, unable to verify your information. Check [User Name] and [Password]!", "Login failed");
					}
					if (eosdata.Status == RegisterStatus.NEW || eosdata.Status == RegisterStatus.RE_ASSIGN)
					{
						base.Hide();
						eosdata.GUI = GZipHelper.DeCompress(eosdata.GUI, eosdata.OriginSize);
						Assembly assembly = Assembly.Load(eosdata.GUI);
						Type type = assembly.GetType("ExamClient.frmEnglishExamClient");
						Form form = (Form)Activator.CreateInstance(type);
						IExamclient examclient = (IExamclient)form;
						eosdata.GUI = null;
						eosdata.ServerInfomation = this.si;
						eosdata.RegData = registerData;
						examclient.SetExamData(eosdata);
						form.Show();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Start Exam Error:\nCannot connect to the EOS server!\n", "Connecting...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000033D4 File Offset: 0x000015D4
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000033E0 File Offset: 0x000015E0
		private void AuthenticateForm_Load(object sender, EventArgs e)
		{
			if (!this.IsAdministrator())
			{
				MessageBox.Show("You must login Windows as System Administrator or Run [EOS Client] as Administrator.", "Run as Administrator!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Application.Exit();
			}
			string text = "EOS_Server_Info.dat";
			if (File.Exists(Application.StartupPath + "\\" + text))
			{
				try
				{
					string key = "04021976";
					byte[] arrBytes = EncryptSupport.DecryptQuestions_FromFile(text, key);
					this.si = (ServerInfo)EncryptSupport.ByteArrayToObject(arrBytes);
					if (!this.version.Equals(this.si.Version))
					{
						MessageBox.Show("Wrong EOS client version! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Application.Exit();
					}
				}
				catch
				{
					MessageBox.Show("Wrong [" + text + "] file format! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Application.Exit();
				}
			}
			else
			{
				MessageBox.Show("File [" + text + "] not found! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Application.Exit();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000034F4 File Offset: 0x000016F4
		private bool IsAdministrator()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
			return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000352C File Offset: 0x0000172C
		private void linkLBLCheckFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this.fcf == null || this.fcf.IsDisposed)
			{
				this.fcf = new frmCheckFont();
			}
			this.fcf.Show();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003570 File Offset: 0x00001770
		private void lblLinkCheckSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!File.Exists("ghosts.mp3"))
			{
				MessageBox.Show("Audio file ghosts.mp3 cannot be found!", "Check sound");
			}
			else
			{
				this.PlayFromUrl("ghosts.mp3");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000035AC File Offset: 0x000017AC
		public void PlayFromUrl(string url)
		{
			FileStream fileStream = File.OpenRead(url);
			byte[] buffer = new byte[fileStream.Length];
			fileStream.Read(buffer, 0, (int)fileStream.Length);
			fileStream.Close();
			Stream inputStream = new MemoryStream(buffer);
			Mp3FileReader waveProvider = new Mp3FileReader(inputStream);
			WaveOut waveOut = new WaveOut();
			waveOut.Init(waveProvider);
			waveOut.Volume = 1f;
			waveOut.Play();
		}

		// Token: 0x04000017 RID: 23
		private string version = "032E41B3-916D-41e6-AC45-1BBBF9C43194";

		// Token: 0x04000018 RID: 24
		private ServerInfo si = null;

		// Token: 0x04000019 RID: 25
		private frmCheckFont fcf = null;
	}
}
