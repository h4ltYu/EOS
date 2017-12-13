using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace EOSClient
{
	// Token: 0x02000003 RID: 3
	public partial class frmCheckFont : Form
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002359 File Offset: 0x00000559
		public frmCheckFont()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002374 File Offset: 0x00000574
		private bool checkFont(string fontName)
		{
			bool result;
			using (Font font = new Font(fontName, 12f, FontStyle.Regular))
			{
				if (font.Name.Equals(fontName))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023D0 File Offset: 0x000005D0
		private void frmCheckFont_Load(object sender, EventArgs e)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			InstalledFontCollection installedFontCollection = new InstalledFontCollection();
			FontFamily[] families = installedFontCollection.Families;
			foreach (FontFamily fontFamily in families)
			{
				string text = fontFamily.GetName(0).Trim().ToUpper();
				if (text.StartsWith("KaiTi".ToUpper()))
				{
					flag = true;
				}
				if (text.StartsWith("Ms Mincho".ToUpper()))
				{
					flag2 = true;
				}
				if (text.StartsWith("HGSeikai".ToUpper()))
				{
					flag3 = true;
				}
				if (text.StartsWith("NtMotoya".ToUpper()))
				{
					flag4 = true;
				}
			}
			string text2 = "CHECK FONT RESULT:\r\n\r\n";
			string str = "KaiTi";
			if (flag)
			{
				text2 = text2 + "Chinese font ('" + str + "') : OK.\r\n\r\n";
			}
			else
			{
				text2 = text2 + "Chinese font ('" + str + "') : NOT FOUND.\r\n\r\n";
			}
			string str2 = "MS Mincho";
			if (flag2)
			{
				text2 = text2 + "Japanese font 1 ('" + str2 + "') : OK.\r\n\r\n";
			}
			else
			{
				text2 = text2 + "Japanese font 1 ('" + str2 + "') :  NOT FOUND.\r\n\r\n";
			}
			str2 = "HGSeikaishotaiPRO";
			if (flag3)
			{
				text2 = text2 + "Japanese font 2 ('" + str2 + "') : OK.\r\n\r\n";
			}
			else
			{
				text2 = text2 + "Japanese font 2 ('" + str2 + "') :  NOT FOUND.\r\n\r\n";
			}
			str2 = "NtMotoya Kyotai";
			if (flag4)
			{
				text2 = text2 + "Japanese font 3 ('" + str2 + "') : OK.\r\n\r\n";
			}
			else
			{
				text2 = text2 + "Japanese font 3 ('" + str2 + "') :  NOT FOUND.\r\n\r\n";
			}
			if (!flag2 || !flag || !flag3 || !flag4)
			{
				text2 += "\r\n\r\nINSTALLING FONTS ON Windows:\r\n\r\nThere are several ways to install fonts on Windows.\r\nKeep in mind that you must be an Administrator on the target machine to install fonts.\r\n\r\n - Download the font.\r\n - Double-click on a font file to open the font preview and select 'Install'.\r\n\r\nOR\r\n\r\n - Right-click on a font file, and then select 'Install'.";
			}
			this.txtFontGuide.Text = text2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000025D1 File Offset: 0x000007D1
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
