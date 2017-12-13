using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CloseConnections2003;
using EncryptData;
using Gma.UserActivityMonitor;
using IRemote;
using NAudio.Wave;
using QuestionLib;
using QuestionLib.Entity;
using ScreenShot;

namespace ExamClient
{
    public partial class frmEnglishExamClient : Form, IExamclient
    {
        public frmEnglishExamClient()
        {
            this.InitializeComponent();
        }

        public void SetExamData(EOSData ed)
        {
            this.examData = ed;
        }

        private void ControlManager()
        {
            this.timerCountDown.Enabled = true;
            this.timerTopMost.Enabled = true;
            this.btnExit.Enabled = false;
            this.tabControlQuestion.Enabled = true;
            this.nudFontSize.Enabled = true;
            this.btnFinish.Enabled = false;
            base.KeyPreview = true;
        }

        private void TimerTopMost_FirstDisplay()
        {
            if (this.tabControlQuestion.SelectedTab == this.tabPageFillBlank)
            {
                Question question = (Question)this.paper.FillBlankQuestions[this.indexFill];
                if (question.QType == QuestionType.FILL_BLANK_EMPTY)
                {
                    this.timerTopMost.Enabled = true;
                }
                else
                {
                    this.timerTopMost.Enabled = false;
                }
            }
            else
            {
                this.timerTopMost.Enabled = true;
            }
        }

        private void DisplayStudentGuide()
        {
            if (this.paper.StudentGuide.Equals(""))
            {
                this.txtGuide.Visible = false;
            }
            else
            {
                this.txtGuide.Text = this.paper.StudentGuide;
                this.txtGuide.Visible = true;
            }
        }

        private void RemoveTabPages()
        {
            this.tabControlQuestion.SelectedIndexChanged -= this.tabControlQuestion_SelectedIndexChanged;
            if (this.paper.FillBlankQuestions == null || this.paper.FillBlankQuestions.Count == 0)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageFillBlank);
            }
            if (this.paper.GrammarQuestions == null || this.paper.GrammarQuestions.Count == 0)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageGrammar);
            }
            if (this.paper.IndicateMQuestions == null || this.paper.IndicateMQuestions.Count == 0)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageIndicateMistake);
            }
            if (this.paper.MatchQuestions == null || this.paper.MatchQuestions.Count == 0)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageMatching);
            }
            if (this.paper.ReadingQuestions == null || this.paper.ReadingQuestions.Count == 0)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageReadingM);
            }
            if (this.paper.EssayQuestion == null)
            {
                this.tabControlQuestion.TabPages.Remove(this.tabPageEssay);
            }
            this.tabControlQuestion.SelectedIndexChanged += this.tabControlQuestion_SelectedIndexChanged;
        }

        private bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private ArrayList ShuffleList(ArrayList list)
        {
            ArrayList arrayList = new ArrayList();
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                int index = this.ran.Next(list.Count);
                arrayList.Add(list[index]);
                list.RemoveAt(index);
                list.TrimToSize();
            }
            return arrayList;
        }

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

        public void LoadFont()
        {
            List<string> list = new List<string>();
            list.Add("Microsoft Sans Serif");
            list.Add("KaiTi");
            list.Add("Ms Mincho");
            list.Add("HGSeikai");
            list.Add("NtMotoya");
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            FontFamily[] families = installedFontCollection.Families;
            foreach (FontFamily fontFamily in families)
            {
                string text = fontFamily.GetName(0).Trim().ToUpper();
                foreach (string text2 in list)
                {
                    string value = text2.ToUpper();
                    if (text.StartsWith(value))
                    {
                        this.domainUpDown.Items.Add(fontFamily.GetName(0));
                        break;
                    }
                }
            }
            Font font = this.lblMC_Text.Font;
            string name = font.FontFamily.GetName(0);
            for (int j = 0; j < this.domainUpDown.Items.Count; j++)
            {
                if (name.StartsWith(this.domainUpDown.Items[j].ToString()))
                {
                    this.domainUpDown.SelectedIndex = j;
                    break;
                }
            }
        }

        private void frmEnglishExamClient_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsAdministrator())
                {
                    MessageBox.Show("You must login Windows as System Administrator or Run [EOS Client] as Administrator.", "Run as Administrator!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Application.Exit();
                }
            }
            catch
            {
            }
            try
            {
                if (this.examData == null)
                {
                    MessageBox.Show("Get exam data error. Re-assign and Try again!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.paper = this.examData.ExamPaper;
                    if (this.paper == null)
                    {
                        MessageBox.Show("Get exam paper error. Re-assign and Try again!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (this.paper.AudioData != null)
                        {
                            if (this.paper.AudioData.Length != this.paper.AudioSize)
                            {
                                MessageBox.Show("Get exam audio error. Re-assign and Try again!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        if (this.paper.TestType == TestTypeEnum.WRITING_JP || this.paper.TestType == TestTypeEnum.WRITING_CN)
                        {
                            if (!this.checkFont("MS Mincho"))
                            {
                                MessageBox.Show("Cannot find the Japanese/Chinese 'MS Mincho' font! Install the font and start the exam again.", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Application.Exit();
                            }
                            this.nudFontSize.Value = 12m;
                        }
                        if (this.paper.TestType == TestTypeEnum.WRITING_VN || this.paper.TestType == TestTypeEnum.WRITING_EN)
                        {
                            this.txtWrittingEssay.Font = this.lblMC_Text.Font;
                            this.nudFontSize.Value = 10m;
                        }
                        this.lblMachine.Text = Environment.MachineName;
                        this.loginId = this.examData.RegData.Login;
                        this.lblLogin.Text = this.loginId;
                        this.lblExamServer.Text = this.examData.ServerInfomation.ServerAlias;
                        if (this.examData.RegData.ExamCode.Length >= 6)
                        {
                            this.lblExamCode.Text = this.examData.RegData.ExamCode.Substring(0, 6);
                        }
                        else
                        {
                            this.lblExamCode.Text = this.examData.RegData.ExamCode;
                        }
                        this.remotingURL = string.Concat(new object[]
                        {
                            "tcp:							this.examData.ServerInfomation.IP,

                            ":",
                            this.examData.ServerInfomation.Port,
                            "/Server"
                        });
                        this.monitorURL = string.Concat(new object[]
                        {
                            "tcp:							this.examData.ServerInfomation.MonitorServer_IP,

                            ":",
                            this.examData.ServerInfomation.MonitorServer_Port,
                            "/RemoteMonitorServer"
                        });
                        this.nudFontSize.Enabled = false;
                        this.btnFinish.Enabled = false;
                        this.lblMark.Text = "";
                        this.lblSaveServer.Text = "";
                        this.lblTotalMarks.Text = "";
                        this.lblWordCount.Text = "0 word";
                        this.lblTime.Text = "";
                        this.lblDuration.Text = "";
                        this.lblOver.Text = "";
                        this.lblReading.Text = "";
                        this.lblGrammarNumber.Text = "";
                        this.lblIndicateNumber.Text = "";
                        this.lblMatchNumber.Text = "";
                        this.txtColumnA.Text = "";
                        this.txtColumnB.Text = "";
                        this.lblMC_Text.Text = "";
                        this.txtIndiMistake.Text = "";
                        this.txtReadingM.Text = "";
                        this.chkReadingM = new CheckBox[6];
                        this.chkReadingM[0] = this.chkReadingA_M;
                        this.chkReadingM[1] = this.chkReadingB_M;
                        this.chkReadingM[2] = this.chkReadingC_M;
                        this.chkReadingM[3] = this.chkReadingD_M;
                        this.chkReadingM[4] = this.chkReadingE_M;
                        this.chkReadingM[5] = this.chkReadingF_M;
                        this.chkGrammar = new CheckBox[6];
                        this.chkGrammar[0] = this.chkGrammarA;
                        this.chkGrammar[1] = this.chkGrammarB;
                        this.chkGrammar[2] = this.chkGrammarC;
                        this.chkGrammar[3] = this.chkGrammarD;
                        this.chkGrammar[4] = this.chkGrammarE;
                        this.chkGrammar[5] = this.chkGrammarF;
                        this.chkIndiMistake = new CheckBox[6];
                        this.chkIndiMistake[0] = this.chkIndiMistakeA;
                        this.chkIndiMistake[1] = this.chkIndiMistakeB;
                        this.chkIndiMistake[2] = this.chkIndiMistakeC;
                        this.chkIndiMistake[3] = this.chkIndiMistakeD;
                        this.chkIndiMistake[4] = this.chkIndiMistakeE;
                        this.chkIndiMistake[5] = this.chkIndiMistakeF;
                        this.ResetFillBlank();
                        this.LoadFont();
                        this.DisplayPaper();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Display Paper\n" + ex.ToString() + ex.StackTrace);
            }
        }

        private void DisplayPaper()
        {
            if (this.paper.AudioData != null)
            {
                this.lblVol.Enabled = true;
                this.nudVol.Enabled = true;
            }
            if (this.paper.TestType == TestTypeEnum.NOT_WRITING || this.paper.TestType == TestTypeEnum.WRITING_EN)
            {
                if (this.examData.Status == RegisterStatus.NEW)
                {
                    this.lblDuration.Text = this.paper.Duration.ToString() + " minutes";
                    this.timeLeft = this.paper.Duration * 60;
                    this.lastSave = this.timeLeft;
                    this.DisplayTimeLeft();
                    this.DisplayStudentGuide();
                    this.RemoveTabPages();
                    if (this.paper.ListenCode.Trim().Equals(""))
                    {
                        this.ControlManager();
                        this.Shuffle();
                        this.DisplayReading(this.indexReading);
                        this.DisplayMatching(this.indexMatching);
                        this.DisplayGrammar(this.indexGrammar);
                        this.DisplayIndiMistake(this.indexIndicateMistake);
                        this.DisplayFillBlankQuestion(this.indexFill);
                        this.DisplayEssay();
                        this.TimerTopMost_FirstDisplay();
                        this.DisableAllKeyBoard();
                        if (this.paper.EssayQuestion != null)
                        {
                            this.undoStack.Add("");
                        }
                    }
                    else
                    {
                        this.txtOpenCode.Enabled = true;
                        this.btnShowExam.Enabled = true;
                    }
                    this.StartCloseTCPConnectionsThread();
                    this.DisableMouse();
                    this.lblTotalMarks.Text = this.paper.Mark.ToString();
                }
                if (this.examData.Status == RegisterStatus.RE_ASSIGN)
                {
                    this.lblDuration.Text = this.paper.Duration.ToString() + " minutes";
                    this.DisplayStudentGuide();
                    this.ControlManager();
                    this.sPaper = this.examData.StudentSubmitPaper;
                    string text = this.examData.RegData.ExamCode + "\\" + this.examData.RegData.Login + ".dat";
                    SubmitPaper submitPaper = null;
                    if (File.Exists(text))
                    {
                        try
                        {
                            submitPaper = QuestionHelper.LoadSubmitPaper(text);
                        }
                        catch
                        {
                        }
                    }
                    if (submitPaper != null && submitPaper.TimeLeft < this.sPaper.TimeLeft)
                    {
                        if (this.sPaper.Equals(submitPaper))
                        {
                            this.sPaper = submitPaper;
                        }
                    }
                    this.timeLeft = this.sPaper.TimeLeft;
                    this.DisplayTimeLeft();
                    this.indexReading = this.sPaper.IndexReading;
                    this.indexMatching = this.sPaper.IndexMatch;
                    this.indexGrammar = this.sPaper.IndexG;
                    this.indexIndicateMistake = this.sPaper.IndexIndiM;
                    this.indexFill = this.sPaper.IndexFill;
                    this.paper = QuestionHelper.Re_ConstructPaper(this.paper, this.sPaper);
                    this.RemoveTabPages();
                    this.DisplayReading(this.indexReading);
                    this.DisplayMatching(this.indexMatching);
                    this.DisplayGrammar(this.indexGrammar);
                    this.DisplayIndiMistake(this.indexIndicateMistake);
                    this.DisplayFillBlankQuestion(this.indexFill);
                    this.DisplayEssay();
                    this.txtOpenCode.Enabled = false;
                    this.btnShowExam.Enabled = false;
                    this.DisableMouse();
                    this.DisableAllKeyBoard();
                    this.TimerTopMost_FirstDisplay();
                    this.StartCloseTCPConnectionsThread();
                    this.paper.Mark = this.sPaper.SPaper.Mark;
                    this.lblTotalMarks.Text = this.paper.Mark.ToString();
                    if (this.paper.EssayQuestion != null)
                    {
                        this.undoStack.Add(this.paper.EssayQuestion.Development);
                    }
                    if (this.paper.AudioData != null)
                    {
                        this.timeLeft += 10;
                        if (this.timeLeft > 60 * this.paper.Duration)
                        {
                            this.timeLeft -= 10;
                        }
                    }
                    this.lastSave = this.timeLeft;
                    if (this.paper.AudioData != null && this.paper.AudioData.Length > 0)
                    {
                        int num = 60 * this.paper.Duration - this.timeLeft;
                        num = ((num > 0) ? num : 0);
                        this.PlayFromBuf(this.paper.AudioData, num);
                    }
                }
                if (this.tabControlQuestion.SelectedTab == this.tabPageGrammar)
                {
                    if (this.paper.GrammarQuestions.Count > 0)
                    {
                        this.AddQuestionButon(this.paper.GrammarQuestions.Count);
                        this.panelQuestionList.Visible = true;
                    }
                    if (this.examData.Status == RegisterStatus.RE_ASSIGN)
                    {
                        for (int i = 0; i < this.paper.GrammarQuestions.Count; i++)
                        {
                            Question q = (Question)this.paper.GrammarQuestions[i];
                            if (this.IsQuestionAnswered(q))
                            {
                                this.SetButtonColor(i + 1, Color.GreenYellow);
                            }
                            else
                            {
                                this.SetButtonColor(i + 1, this.BackColor);
                            }
                        }
                    }
                }
                if (this.tabControlQuestion.SelectedTab == this.tabPageIndicateMistake)
                {
                    if (this.paper.IndicateMQuestions.Count > 0)
                    {
                        this.AddQuestionButon(this.paper.IndicateMQuestions.Count);
                        this.panelQuestionList.Visible = true;
                    }
                    if (this.examData.Status == RegisterStatus.RE_ASSIGN)
                    {
                        for (int i = 0; i < this.paper.IndicateMQuestions.Count; i++)
                        {
                            Question q = (Question)this.paper.IndicateMQuestions[i];
                            if (this.IsQuestionAnswered(q))
                            {
                                this.SetButtonColor(i + 1, Color.GreenYellow);
                            }
                            else
                            {
                                this.SetButtonColor(i + 1, this.BackColor);
                            }
                        }
                    }
                }
            }
            else
            {
                base.WindowState = FormWindowState.Normal;
                base.ControlBox = true;
                base.MinimizeBox = true;
                base.MaximizeBox = true;
                if (this.examData.Status == RegisterStatus.NEW)
                {
                    this.lblDuration.Text = this.paper.Duration.ToString() + " minutes";
                    this.lblTotalMarks.Text = "";
                    this.timeLeft = this.paper.Duration * 60;
                    this.DisplayTimeLeft();
                    this.DisplayStudentGuide();
                    this.RemoveTabPages();
                    this.ControlManager();
                    base.TopMost = false;
                    this.DisplayEssay();
                    if (this.paper.EssayQuestion != null)
                    {
                        this.undoStack.Add("");
                    }
                    this.StartCloseTCPConnectionsThread();
                }
                if (this.examData.Status == RegisterStatus.RE_ASSIGN)
                {
                    this.lblDuration.Text = this.paper.Duration.ToString() + " minutes";
                    this.lblTotalMarks.Text = "";
                    this.DisplayStudentGuide();
                    this.ControlManager();
                    this.sPaper = this.examData.StudentSubmitPaper;
                    string text = this.examData.RegData.ExamCode + "\\" + this.examData.RegData.Login + ".dat";
                    SubmitPaper submitPaper = null;
                    if (File.Exists(text))
                    {
                        try
                        {
                            submitPaper = QuestionHelper.LoadSubmitPaper(text);
                        }
                        catch
                        {
                        }
                    }
                    if (submitPaper != null && submitPaper.TimeLeft < this.sPaper.TimeLeft)
                    {
                        if (this.sPaper.Equals(submitPaper))
                        {
                            this.sPaper = submitPaper;
                        }
                    }
                    this.timeLeft = this.sPaper.TimeLeft;
                    this.DisplayTimeLeft();
                    this.paper = QuestionHelper.Re_ConstructPaper(this.paper, this.sPaper);
                    this.RemoveTabPages();
                    this.DisplayEssay();
                    this.StartCloseTCPConnectionsThread();
                    if (this.paper.EssayQuestion != null)
                    {
                        this.undoStack.Add(this.paper.EssayQuestion.Development);
                    }
                }
            }
            try
            {
                ThreadStart start = new ThreadStart(this.CloseApps);
                Thread thread = new Thread(start);
                thread.Start();
            }
            catch
            {
            }
        }

        private void DisplayTimeLeft()
        {
            int num = this.timeLeft / 60;
            int num2 = this.timeLeft % 60;
            string text = "0" + num.ToString();
            text = text.Substring(text.Length - 2, 2);
            string text2 = "0" + num2.ToString();
            text2 = text2.Substring(text2.Length - 2, 2);
            this.lblTime.Text = text + ":" + text2;
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            this.timeLeft--;
            this.DisplayTimeLeft();
            if (this.timeLeft == 0)
            {
                this.timerCountDown.Enabled = false;
                this.btnFinish.Enabled = true;
                this.chbWantFinish.Checked = true;
                this.btnFinish.PerformClick();
            }
            else if (this.finishClick)
            {
                this.timerCountDown.Enabled = false;
            }
            else
            {
                if (this.paper.TestType == TestTypeEnum.NOT_WRITING || this.paper.TestType == TestTypeEnum.WRITING_EN)
                {
                    int foregroundWindow = Win32.GetForegroundWindow();
                    if (base.Handle.ToInt32() != foregroundWindow)
                    {
                        Win32.SendMessage(foregroundWindow, 274u, 61472, 0);
                    }
                    Win32.SetActiveWindow(base.Handle.ToInt32());
                }
                if (this.paper.EssayQuestion != null)
                {
                    if (this.timeLeft % this.autoSaveInteval == 0)
                    {
                        this.btnSaveEssay.PerformClick();
                    }
                }
                if (this.paper.AudioData != null)
                {
                    if (this.lastSave - this.timeLeft >= 10)
                    {
                        if (this.sPaper == null)
                        {
                            this.sPaper = this.CreateSubmitPaper(false);
                            this.SaveAtServer();
                        }
                        this.sPaper = this.CreateSubmitPaper(false);
                        this.SaveAtClient();
                        this.lastSave = this.timeLeft;
                    }
                }
                else if (!this.submitFirstTime)
                {
                    this.submitFirstTime = true;
                    this.Submit2Server_SaveAtClient();
                }
            }
        }

        public void SendScreenImage()
        {
            this.closeConnection = false;
            this.keepConnection = true;
            ScreenCapture screenCapture = new ScreenCapture();
            Image image = screenCapture.CaptureScreen();
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            try
            {
                IRemoteMonitorServer remoteMonitorServer = (IRemoteMonitorServer)Activator.GetObject(typeof(IRemoteMonitorServer), this.monitorURL);
                this.captureScreenInterval = remoteMonitorServer.SaveScreenImage(memoryStream.GetBuffer(), this.imageIndex++, this.examData.RegData.ExamCode, this.loginId);
                if (this.listMissScreenImages.Count > 0)
                {
                    ThreadStart start = new ThreadStart(this.SendOneMissScreenImage);
                    Thread thread = new Thread(start);
                    thread.Start();
                }
            }
            catch
            {
                this.listMissScreenImages.Add(memoryStream.GetBuffer());
            }
            this.keepConnection = false;
            this.closeConnection = true;
        }

        private void DisplayReading(int index)
        {
            if (this.paper.ReadingQuestions != null && this.paper.ReadingQuestions.Count != 0)
            {
                this.btnNextReadingM.Enabled = true;
                this.ResetReading();
                Passage passage = (Passage)this.paper.ReadingQuestions[index];
                this.txtReadingM.Text = passage.Text + "\r\n\r\n";
                int num = 0;
                float num2 = 0f;
                this.lstReadingQuestionM.SelectedIndexChanged -= this.lstReadingQuestionM_SelectedIndexChanged;
                if (passage.PassageQuestions.Count != 0)
                {
                    foreach (object obj in passage.PassageQuestions)
                    {
                        Question question = (Question)obj;
                        num++;
                        TextBox textBox = this.txtReadingM;
                        object text = textBox.Text;
                        textBox.Text = string.Concat(new object[]
                        {
                            text,
                            num,
                            ") ",
                            question.Text,
                            "\r\n"
                        });
                        int num3 = -1;
                        foreach (object obj2 in question.QuestionAnswers)
                        {
                            QuestionAnswer questionAnswer = (QuestionAnswer)obj2;
                            num3++;
                            TextBox textBox2 = this.txtReadingM;
                            string text2 = textBox2.Text;
                            textBox2.Text = string.Concat(new string[]
                            {
                                text2,
                                this.qaNo[num3],
                                ".  ",
                                questionAnswer.Text,
                                "\r\n"
                            });
                        }
                        TextBox textBox3 = this.txtReadingM;
                        textBox3.Text += "\r\n";
                        this.lstReadingQuestionM.Items.Add(new QuestionInListBox(question, num));
                        num2 += question.Mark;
                    }
                    this.lstReadingQuestionM.SelectedIndexChanged += this.lstReadingQuestionM_SelectedIndexChanged;
                    this.lblMark.Text = num2.ToString();
                    this.lblReading.Text = string.Concat(new object[]
                    {
                        "Reading ",
                        index + 1,
                        "/",
                        this.paper.ReadingQuestions.Count,
                        ":"
                    });
                    this.lstReadingQuestionM.SelectedIndex = this.indexReadingQuestion;
                    this.DisableNonFunctionKeys();
                }
            }
        }

        private void ResetReading()
        {
            this.txtReadingM.Text = "";
            this.lstReadingQuestionM.Items.Clear();
            foreach (CheckBox checkBox in this.chkReadingM)
            {
                checkBox.Tag = null;
                checkBox.Checked = false;
                checkBox.Visible = false;
            }
        }

        private void DisplayMatching(int index)
        {
            if (this.paper.MatchQuestions != null && this.paper.MatchQuestions.Count != 0)
            {
                this.btnNextMaching.Enabled = true;
                this.ResetMatching();
                MatchQuestion matchQuestion = (MatchQuestion)this.paper.MatchQuestions[index];
                this.txtColumnA.Text = matchQuestion.ColumnA;
                this.txtColumnB.Text = matchQuestion.ColumnB;
                char[] separator = new char[]
                {
                    ';'
                };
                if (matchQuestion.SudentAnswer != null)
                {
                    string[] items = matchQuestion.SudentAnswer.Split(separator);
                    this.lstAnswerMatch.Items.AddRange(items);
                }
                this.lblMark.Text = matchQuestion.Mark.ToString();
                this.lblMatchNumber.Text = string.Concat(new object[]
                {
                    "Match question ",
                    index + 1,
                    "/",
                    this.paper.MatchQuestions.Count
                });
                string[] array = matchQuestion.Solution.Split(separator);
                this.matchingAnswerCount = array.Length;
                this.DisableNonFunctionKeys();
            }
        }

        private void ResetMatching()
        {
            this.txtColumnA.Text = "";
            this.txtColumnB.Text = "";
            this.lstAnswerMatch.Items.Clear();
        }

        private void DisplayFillBlankQuestion(int index)
        {
            if (this.paper.FillBlankQuestions != null && this.paper.FillBlankQuestions.Count != 0)
            {
                this.oldFillBlankAnswer = "";
                this.btnNextFillBlank.Enabled = true;
                this.ResetFillBlank();
                Question question = (Question)this.paper.FillBlankQuestions[index];
                this.lblMark.Text = question.Mark.ToString();
                this.lblFillBlankNumber.Text = string.Concat(new object[]
                {
                    "Fill Blank question ",
                    index + 1,
                    "/",
                    this.paper.FillBlankQuestions.Count,
                    ":"
                });
                int left = this.lblFillBlankNumber.Left;
                int num = left;
                int num2 = this.lblFillBlankNumber.Top + 2 * this.lblFillBlankNumber.Height;
                char[] separator = new char[]
                {
                    '\n'
                };
                string[] array = question.Text.Trim().Split(separator);
                this.lblList = new ArrayList();
                if (question.QType == QuestionType.FILL_BLANK_ALL || question.QType == QuestionType.FILL_BLANK_GROUP)
                {
                    this.cboList = new ArrayList();
                    this.txtList = null;
                    this.timerTopMost.Enabled = false;
                    this.DisableNonFunctionKeys();
                }
                else
                {
                    this.txtList = new ArrayList();
                    this.cboList = null;
                    this.timerTopMost.Enabled = true;
                    this.EnableNonFunctionKeys();
                }
                int num3 = 0;
                foreach (string input in array)
                {
                    string fillBlank_Pattern = QuestionHelper.fillBlank_Pattern;
                    Regex regex = new Regex(fillBlank_Pattern, RegexOptions.ExplicitCapture);
                    MatchCollection matchCollection = regex.Matches(input);
                    string[] array3 = regex.Split(input);
                    for (int j = 0; j < array3.Length; j++)
                    {
                        Label label = new Label();
                        label.Visible = true;
                        label.AutoSize = true;
                        label.Text = array3[j].Replace("&", "&&");
                        this.lblList.Add(label);
                        if (num + label.Width > base.Width)
                        {
                            num = left;
                            num2 += 2 * label.Height;
                        }
                        label.Left = num;
                        label.Top = num2;
                        this.panelFillBlank.Controls.Add(label);
                        num += label.Width;
                        if (question.QType == QuestionType.FILL_BLANK_EMPTY && j < array3.Length - 1)
                        {
                            TextBox textBox = new TextBox();
                            textBox.Visible = true;
                            textBox.Width = 100;
                            this.txtList.Add(textBox);
                            if (num + textBox.Width > this.panelFillBlank.Width)
                            {
                                num = left;
                                num2 += 2 * this.lblFillBlankNumber.Height;
                            }
                            textBox.Left = num;
                            textBox.Top = num2;
                            this.panelFillBlank.Controls.Add(textBox);
                            num += textBox.Width;
                            QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[this.txtList.Count - 1];
                            textBox.Text = questionAnswer.Text;
                            textBox.Tag = questionAnswer;
                            textBox.TextChanged += this.ans_TextChanged;
                            textBox.Leave += this.ans_Leave;
                            this.oldFillBlankAnswer += textBox.Text;
                        }
                        if (question.QType == QuestionType.FILL_BLANK_ALL && j < array3.Length - 1)
                        {
                            NoScrollComboBox noScrollComboBox = new NoScrollComboBox();
                            noScrollComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            noScrollComboBox.Visible = true;
                            noScrollComboBox.Width = 100;
                            foreach (object obj in question.QuestionAnswers)
                            {
                                QuestionAnswer questionAnswer2 = (QuestionAnswer)obj;
                                noScrollComboBox.Items.Add(questionAnswer2.Text);
                            }
                            this.cboList.Add(noScrollComboBox);
                            if (num + noScrollComboBox.Width > this.panelFillBlank.Width)
                            {
                                num = left;
                                num2 += 2 * this.lblFillBlankNumber.Height;
                            }
                            noScrollComboBox.Left = num;
                            noScrollComboBox.Top = num2;
                            this.panelFillBlank.Controls.Add(noScrollComboBox);
                            num += noScrollComboBox.Width;
                            QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[this.cboList.Count - 1];
                            noScrollComboBox.Text = questionAnswer.Text;
                            noScrollComboBox.Tag = questionAnswer;
                            noScrollComboBox.SelectedIndexChanged += this.ans_TextChanged;
                            noScrollComboBox.Leave += this.ans_Leave;
                            this.oldFillBlankAnswer += noScrollComboBox.Text;
                        }
                        if (question.QType == QuestionType.FILL_BLANK_GROUP && j < array3.Length - 1)
                        {
                            NoScrollComboBox noScrollComboBox = new NoScrollComboBox();
                            noScrollComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            noScrollComboBox.Visible = true;
                            noScrollComboBox.Width = 100;
                            noScrollComboBox.Items.AddRange(this.GetFilledWord_Group(question.Text, num3));
                            num3++;
                            this.cboList.Add(noScrollComboBox);
                            if (num + noScrollComboBox.Width > this.panelFillBlank.Width)
                            {
                                num = left;
                                num2 += 2 * this.lblFillBlankNumber.Height;
                            }
                            noScrollComboBox.Left = num;
                            noScrollComboBox.Top = num2;
                            this.panelFillBlank.Controls.Add(noScrollComboBox);
                            num += noScrollComboBox.Width;
                            QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[this.cboList.Count - 1];
                            noScrollComboBox.Text = questionAnswer.Text;
                            noScrollComboBox.Tag = questionAnswer;
                            noScrollComboBox.SelectedIndexChanged += this.ans_TextChanged;
                            noScrollComboBox.Leave += this.ans_Leave;
                            this.oldFillBlankAnswer += noScrollComboBox.Text;
                        }
                    }
                    num2 += 2 * this.lblFillBlankNumber.Height;
                    num = left;
                }
            }
        }

        private void ResetFillBlank()
        {
            if (this.lblList != null)
            {
                for (int i = 0; i < this.lblList.Count; i++)
                {
                    this.panelFillBlank.Controls.Remove((Label)this.lblList[i]);
                }
                this.lblList = null;
            }
            if (this.txtList != null)
            {
                for (int i = 0; i < this.txtList.Count; i++)
                {
                    this.panelFillBlank.Controls.Remove((TextBox)this.txtList[i]);
                }
                this.txtList = null;
            }
            if (this.cboList != null)
            {
                for (int i = 0; i < this.cboList.Count; i++)
                {
                    this.panelFillBlank.Controls.Remove((ComboBox)this.cboList[i]);
                }
                this.cboList = null;
            }
        }

        private void DisplayGrammar(int index)
        {
            if (this.paper.GrammarQuestions != null && this.paper.GrammarQuestions.Count != 0)
            {
                this.btnNextGrammar.Enabled = true;
                this.ResetGrammar();
                Question question = (Question)this.paper.GrammarQuestions[index];
                if (question.ImageSize != 0)
                {
                    this.panelPicGrammar.Visible = true;
                    MemoryStream stream = new MemoryStream(question.ImageData);
                    Image image = Image.FromStream(stream);
                    this.picBoxGrammar.Image = image;
                }
                else
                {
                    this.panelPicGrammar.Visible = false;
                    this.picBoxGrammar.Image = null;
                }
                char[] array = new char[1];
                char[] trimChars = array;
                Label label = this.lblMC_Text;
                label.Text = label.Text + question.Text.Trim(trimChars) + "\r\n\r\n";
                int num = -1;
                this.oldGrammarChoice = "";
                foreach (object obj in question.QuestionAnswers)
                {
                    QuestionAnswer questionAnswer = (QuestionAnswer)obj;
                    num++;
                    Label label2 = this.lblMC_Text;
                    string text = label2.Text;
                    label2.Text = string.Concat(new string[]
                    {
                        text,
                        this.qaNo[num],
                        ".  ",
                        questionAnswer.Text,
                        "\r\n\r\n"
                    });
                    this.chkGrammar[num].Visible = true;
                    this.chkGrammar[num].Tag = questionAnswer;
                    this.chkGrammar[num].Checked = questionAnswer.Selected;
                    this.oldGrammarChoice += this.chkGrammar[num].Checked.ToString();
                }
                this.lblMark.Text = question.Mark.ToString();
                this.lblGrammarNumber.Text = string.Concat(new object[]
                {
                    "Multiple choices ",
                    index + 1,
                    "/",
                    this.paper.GrammarQuestions.Count
                });
                this.DisableNonFunctionKeys();
            }
        }

        private void ResetGrammar()
        {
            this.lblMC_Text.Text = "";
            foreach (CheckBox checkBox in this.chkGrammar)
            {
                checkBox.Tag = null;
                checkBox.Checked = false;
                checkBox.Visible = false;
            }
        }

        private void DisplayIndiMistake(int index)
        {
            if (this.paper.IndicateMQuestions != null && this.paper.IndicateMQuestions.Count != 0)
            {
                this.btnNextIndiMistake.Enabled = true;
                this.ResetIndiMistake();
                Question question = (Question)this.paper.IndicateMQuestions[index];
                TextBox textBox = this.txtIndiMistake;
                textBox.Text = textBox.Text + question.Text + "\r\n\r\n";
                int num = -1;
                this.oldIndiMistakeChoice = "";
                foreach (object obj in question.QuestionAnswers)
                {
                    QuestionAnswer questionAnswer = (QuestionAnswer)obj;
                    num++;
                    TextBox textBox2 = this.txtIndiMistake;
                    string text = textBox2.Text;
                    textBox2.Text = string.Concat(new string[]
                    {
                        text,
                        this.qaNo[num],
                        ".  ",
                        questionAnswer.Text,
                        "\r\n\r\n"
                    });
                    this.chkIndiMistake[num].Visible = true;
                    this.chkIndiMistake[num].Tag = questionAnswer;
                    this.chkIndiMistake[num].Checked = questionAnswer.Selected;
                    this.oldIndiMistakeChoice += this.chkIndiMistake[num].Checked.ToString();
                }
                this.lblMark.Text = question.Mark.ToString();
                this.lblIndicateNumber.Text = string.Concat(new object[]
                {
                    "Indicate Mistake question ",
                    index + 1,
                    "/",
                    this.paper.IndicateMQuestions.Count
                });
                this.DisableNonFunctionKeys();
            }
        }

        private void ResetIndiMistake()
        {
            this.txtIndiMistake.Text = "";
            foreach (CheckBox checkBox in this.chkIndiMistake)
            {
                checkBox.Tag = null;
                checkBox.Checked = false;
                checkBox.Visible = false;
            }
        }

        private void DisplayEssay()
        {
            if (this.paper.EssayQuestion != null)
            {
                this.ResetEssay();
                EssayQuestion essayQuestion = this.paper.EssayQuestion;
                MemoryStream stream = new MemoryStream(essayQuestion.Question);
                Image image = Image.FromStream(stream);
                this.pictureBoxEssay.Image = image;
                this.btnEssayZoom.Enabled = true;
                this.btnEssayNormal.Enabled = true;
                if (essayQuestion.Development == null || essayQuestion.Development.Equals(""))
                {
                    this.txtWrittingEssay.Text = "<Typing here>";
                }
                else
                {
                    this.txtWrittingEssay.Text = essayQuestion.Development;
                }
                this.lblMark.Text = "N/A";
                if (this.paper.TestType == TestTypeEnum.WRITING_EN)
                {
                    this.EnableNonFunctionKeys();
                    this.EnableMouse();
                }
            }
        }

        private void ResetEssay()
        {
            this.txtWrittingEssay.Text = "<Typing here>";
            this.pictureBoxEssay.Image = null;
            this.lblWordCount.Text = "";
        }

        private void Submit2Server_SaveAtClient()
        {
            this.lastSave = this.timeLeft;
            if (this.timeLeft != 0 && !this.finishClick)
            {
                this.sPaper = this.CreateSubmitPaper(false);
                Thread thread = new Thread(new ThreadStart(this.SaveAtServer));
                thread.Start();
                this.SaveAtClient();
            }
        }

        private void Set_lblSaveServerText(string text)
        {
            if (this.lblSaveServer.InvokeRequired)
            {
                frmEnglishExamClient.SetTextCallback method = new frmEnglishExamClient.SetTextCallback(this.Set_lblSaveServerText);
                base.Invoke(method, new object[]
                {
                    text
                });
            }
            else
            {
                this.lblSaveServer.Text = text;
            }
        }

        private void Set_lblMesageText(string text)
        {
            if (this.lblSaveServer.InvokeRequired)
            {
                frmEnglishExamClient.SetTextCallback method = new frmEnglishExamClient.SetTextCallback(this.Set_lblMesageText);
                base.Invoke(method, new object[]
                {
                    text
                });
            }
            else
            {
                this.lblMesage.Text = text;
            }
        }

        private void SaveAtServer()
        {
            if (this.timeLeft != 0 && !this.finishClick)
            {
                try
                {
                    this.closeConnection = false;
                    IRemoteServer remoteServer = (IRemoteServer)Activator.GetObject(typeof(IRemoteServer), this.remotingURL);
                    string lblMesageText = "";
                    SubmitStatus submitStatus = remoteServer.Submit(this.sPaper, ref lblMesageText);
                    this.closeConnection = true;
                    this.Set_lblMesageText(lblMesageText);
                    if (submitStatus == SubmitStatus.OK)
                    {
                        this.lblSaveServer.Text = "";
                    }
                    if (submitStatus == SubmitStatus.SUBMIT_ERROR)
                    {
                        if (this.timeLeft == 0 || this.finishClick)
                        {
                            return;
                        }
                        this.Set_lblSaveServerText("Save at server failed(Server Ready)!. Please inform the supervisor and continue the exam.");
                    }
                    if (submitStatus == SubmitStatus.EXAM_NOT_AVAILABLE)
                    {
                        if (this.timeLeft != 0 && !this.finishClick)
                        {
                            this.Set_lblSaveServerText("Save at server failed!. The exam is not available.");
                        }
                    }
                }
                catch
                {
                    this.closeConnection = true;
                    if (this.timeLeft != 0 && !this.finishClick)
                    {
                        this.Set_lblSaveServerText("Save at server failed!. Please inform the supervisor and continue the exam.");
                    }
                }
            }
        }

        private void SaveAtClient()
        {
            try
            {
                string text = this.paper.ExamCode.Trim() + "\\";
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                QuestionHelper.SaveSubmitPaper(text, this.sPaper);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnNextReadingM_Click(object sender, EventArgs e)
        {
            if (this.paper.ReadingQuestions.Count > 0)
            {
                this.indexReadingQuestion = 0;
                this.indexReading++;
                this.indexReading %= this.paper.ReadingQuestions.Count;
                this.DisplayReading(this.indexReading);
            }
        }

        private void lstReadingQuestionM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstReadingQuestionM.SelectedIndex >= 0)
            {
                foreach (CheckBox checkBox in this.chkReadingM)
                {
                    checkBox.Tag = null;
                    checkBox.Visible = false;
                    checkBox.Checked = false;
                }
                QuestionInListBox questionInListBox = (QuestionInListBox)this.lstReadingQuestionM.SelectedItem;
                Question question = questionInListBox.GetQuestion();
                if (question.ImageSize != 0)
                {
                    this.panelPicReadingM.Visible = true;
                    this.btnReadingRealSize.Visible = true;
                    this.btnReadingZoomIn.Visible = true;
                    this.btnReadingZoomOut.Visible = true;
                    MemoryStream stream = new MemoryStream(question.ImageData);
                    Image image = Image.FromStream(stream);
                    this.picBoxReadingM.Image = image;
                }
                else
                {
                    this.panelPicReadingM.Visible = false;
                    this.btnReadingRealSize.Visible = false;
                    this.btnReadingZoomIn.Visible = false;
                    this.btnReadingZoomOut.Visible = false;
                    this.picBoxReadingM.Image = null;
                }
                this.oldMultipleChoice = "";
                for (int j = 0; j < question.QuestionAnswers.Count; j++)
                {
                    QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[j];
                    this.chkReadingM[j].Visible = true;
                    this.chkReadingM[j].Tag = questionAnswer;
                    this.chkReadingM[j].Checked = questionAnswer.Selected;
                    this.oldMultipleChoice += this.chkReadingM[j].Checked.ToString();
                }
                this.txtReadingM.Focus();
                string text = string.Concat(new object[]
                {
                    this.lstReadingQuestionM.SelectedIndex + 1,
                    ") ",
                    question.Text,
                    "\r\n"
                });
                int selectionStart = this.txtReadingM.Text.IndexOf(text);
                int length = text.Length;
                this.txtReadingM.SelectionStart = selectionStart;
                this.txtReadingM.SelectionLength = length;
                this.txtReadingM.Select();
                this.txtReadingM.ScrollToCaret();
            }
        }

        private void chkReading_M_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Tag != null)
            {
                QuestionAnswer questionAnswer = (QuestionAnswer)checkBox.Tag;
                questionAnswer.Selected = checkBox.Checked;
                this.txtReadingM.Focus();
            }
        }

        private bool IsNeedSaveMultiple()
        {
            bool result;
            if (this.lstReadingQuestionM.SelectedItem == null)
            {
                result = false;
            }
            else
            {
                QuestionInListBox questionInListBox = (QuestionInListBox)this.lstReadingQuestionM.SelectedItem;
                Question question = questionInListBox.GetQuestion();
                string text = "";
                for (int i = 0; i < question.QuestionAnswers.Count; i++)
                {
                    text += this.chkReadingM[i].Checked.ToString();
                }
                result = !this.oldMultipleChoice.Equals(text);
            }
            return result;
        }

        private void btnNextMaching_Click(object sender, EventArgs e)
        {
            if (this.paper.MatchQuestions.Count > 0)
            {
                this.indexMatching++;
                this.indexMatching %= this.paper.MatchQuestions.Count;
                this.DisplayMatching(this.indexMatching);
            }
        }

        private void btnAddMSolution_Click(object sender, EventArgs e)
        {
            if (!this.txtNumber.Text.Trim().Equals("") && !this.txtLetter.Text.Trim().Equals(""))
            {
                for (int i = 0; i < this.lstAnswerMatch.Items.Count; i++)
                {
                    if (this.lstAnswerMatch.Items[i].ToString().StartsWith(this.txtNumber.Text + " -"))
                    {
                        return;
                    }
                }
                string item = this.txtNumber.Text + " - " + this.txtLetter.Text.ToUpper();
                if (this.lstAnswerMatch.Items.Count < this.matchingAnswerCount)
                {
                    this.lstAnswerMatch.Items.Add(item);
                    MatchQuestion matchQuestion = (MatchQuestion)this.paper.MatchQuestions[this.indexMatching];
                    matchQuestion.SudentAnswer = this.GetMatchAnswer();
                    this.Submit2Server_SaveAtClient();
                    this.txtNumber.Text = "";
                    this.txtLetter.Text = "";
                }
            }
        }

        private string GetMatchAnswer()
        {
            string text = "";
            for (int i = 0; i < this.lstAnswerMatch.Items.Count; i++)
            {
                text = text + this.lstAnswerMatch.Items[i].ToString().Trim() + ";";
            }
            if (text.Length > 0)
            {
                text = text.Remove(text.Length - 1, 1);
            }
            return text;
        }

        private void btnRemoveMSolution_Click(object sender, EventArgs e)
        {
            if (this.lstAnswerMatch.SelectedItem != null)
            {
                this.lstAnswerMatch.Items.Remove(this.lstAnswerMatch.SelectedItem);
                MatchQuestion matchQuestion = (MatchQuestion)this.paper.MatchQuestions[this.indexMatching];
                matchQuestion.SudentAnswer = this.GetMatchAnswer();
                this.Submit2Server_SaveAtClient();
            }
        }

        private void btnNextGrammar_Click(object sender, EventArgs e)
        {
            if (this.paper.GrammarQuestions.Count > 0)
            {
                if (this.IsNeedSaveGrammar())
                {
                    this.Submit2Server_SaveAtClient();
                }
                this.indexGrammar++;
                this.indexGrammar %= this.paper.GrammarQuestions.Count;
                this.DisplayGrammar(this.indexGrammar);
            }
        }

        private bool IsNeedSaveGrammar()
        {
            string text = "";
            foreach (CheckBox checkBox in this.chkGrammar)
            {
                if (checkBox.Visible)
                {
                    text += checkBox.Checked.ToString();
                }
            }
            return !this.oldGrammarChoice.Equals(text);
        }

        private void chkGrammar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Tag != null)
            {
                QuestionAnswer questionAnswer = (QuestionAnswer)checkBox.Tag;
                questionAnswer.Selected = checkBox.Checked;
                Question q = (Question)this.paper.GrammarQuestions[this.indexGrammar];
                if (this.IsQuestionAnswered(q))
                {
                    this.SetButtonColor(this.indexGrammar + 1, Color.GreenYellow);
                }
                else
                {
                    this.SetButtonColor(this.indexGrammar + 1, this.BackColor);
                }
            }
        }

        private void btnNextIndiMistake_Click(object sender, EventArgs e)
        {
            if (this.paper.IndicateMQuestions.Count > 0)
            {
                if (this.IsNeedSaveIndiMistake())
                {
                    this.Submit2Server_SaveAtClient();
                }
                this.indexIndicateMistake++;
                this.indexIndicateMistake %= this.paper.IndicateMQuestions.Count;
                this.DisplayIndiMistake(this.indexIndicateMistake);
            }
        }

        private bool IsNeedSaveIndiMistake()
        {
            string text = "";
            foreach (CheckBox checkBox in this.chkIndiMistake)
            {
                if (checkBox.Visible)
                {
                    text += checkBox.Checked.ToString();
                }
            }
            return !this.oldIndiMistakeChoice.Equals(text);
        }

        private void chkIndiMistake_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Tag != null)
            {
                QuestionAnswer questionAnswer = (QuestionAnswer)checkBox.Tag;
                questionAnswer.Selected = checkBox.Checked;
                Question q = (Question)this.paper.IndicateMQuestions[this.indexIndicateMistake];
                if (this.IsQuestionAnswered(q))
                {
                    this.SetButtonColor(this.indexIndicateMistake + 1, Color.GreenYellow);
                }
                else
                {
                    this.SetButtonColor(this.indexIndicateMistake + 1, this.BackColor);
                }
            }
        }

        private bool IsQuestionAnswered(Question q)
        {
            foreach (object obj in q.QuestionAnswers)
            {
                QuestionAnswer questionAnswer = (QuestionAnswer)obj;
                if (questionAnswer.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        private void tabControlQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelQuestionList.Visible = false;
            this.panelQuestionList.Controls.Clear();
            if (this.tabControlQuestion.SelectedTab == this.tabPageReadingM)
            {
                Passage passage = (Passage)this.paper.ReadingQuestions[this.indexReading];
                float num = 0f;
                foreach (object obj in passage.PassageQuestions)
                {
                    Question question = (Question)obj;
                    num += question.Mark;
                }
                this.lblMark.Text = num.ToString();
                this.txtReadingM.Focus();
                this.timerTopMost.Enabled = true;
                this.DisableNonFunctionKeys();
            }
            if (this.tabControlQuestion.SelectedTab == this.tabPageGrammar)
            {
                Question question = (Question)this.paper.GrammarQuestions[this.indexGrammar];
                this.lblMark.Text = question.Mark.ToString();
                this.timerTopMost.Enabled = true;
                this.DisableNonFunctionKeys();
                this.panelQuestionList.Visible = true;
                this.AddQuestionButon(this.paper.GrammarQuestions.Count);
                int num2 = 0;
                foreach (object obj2 in this.paper.GrammarQuestions)
                {
                    Question q = (Question)obj2;
                    num2++;
                    if (this.IsQuestionAnswered(q))
                    {
                        this.SetButtonColor(num2, Color.GreenYellow);
                    }
                }
            }
            if (this.tabControlQuestion.SelectedTab == this.tabPageMatching)
            {
                MatchQuestion matchQuestion = (MatchQuestion)this.paper.MatchQuestions[this.indexMatching];
                this.lblMark.Text = matchQuestion.Mark.ToString();
                this.timerTopMost.Enabled = true;
                this.EnableNonFunctionKeys();
            }
            if (this.tabControlQuestion.SelectedTab == this.tabPageIndicateMistake)
            {
                Question question = (Question)this.paper.IndicateMQuestions[this.indexIndicateMistake];
                this.lblMark.Text = question.Mark.ToString();
                this.timerTopMost.Enabled = true;
                this.DisableNonFunctionKeys();
                this.panelQuestionList.Visible = true;
                this.AddQuestionButon(this.paper.IndicateMQuestions.Count);
                int num2 = 0;
                foreach (object obj3 in this.paper.IndicateMQuestions)
                {
                    Question q = (Question)obj3;
                    num2++;
                    if (this.IsQuestionAnswered(q))
                    {
                        this.SetButtonColor(num2, Color.GreenYellow);
                    }
                }
            }
            if (this.tabControlQuestion.SelectedTab == this.tabPageFillBlank)
            {
                float num = 0f;
                foreach (object obj4 in this.paper.FillBlankQuestions)
                {
                    Question question = (Question)obj4;
                    num += question.Mark;
                }
                this.lblMark.Text = num.ToString();
                Question question2 = (Question)this.paper.FillBlankQuestions[this.indexFill];
                if (question2.QType == QuestionType.FILL_BLANK_EMPTY)
                {
                    this.timerTopMost.Enabled = true;
                    this.EnableNonFunctionKeys();
                }
                else
                {
                    this.DisableNonFunctionKeys();
                    this.timerTopMost.Enabled = false;
                }
            }
        }

        private SubmitPaper CreateSubmitPaper(bool finish)
        {
            SubmitPaper result;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, this.paper);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                Paper paper = (Paper)binaryFormatter.Deserialize(memoryStream);
                if (paper.ReadingQuestions != null)
                {
                    foreach (object obj in paper.ReadingQuestions)
                    {
                        Passage passage = (Passage)obj;
                        passage.Preapare2Submit();
                    }
                }
                if (paper.MatchQuestions != null)
                {
                    foreach (object obj2 in paper.MatchQuestions)
                    {
                        MatchQuestion matchQuestion = (MatchQuestion)obj2;
                        matchQuestion.Preapare2Submit();
                    }
                }
                if (paper.GrammarQuestions != null)
                {
                    foreach (object obj3 in paper.GrammarQuestions)
                    {
                        Question question = (Question)obj3;
                        question.Preapare2Submit();
                    }
                }
                if (paper.IndicateMQuestions != null)
                {
                    foreach (object obj4 in paper.IndicateMQuestions)
                    {
                        Question question = (Question)obj4;
                        question.Preapare2Submit();
                    }
                }
                if (paper.FillBlankQuestions != null)
                {
                    foreach (object obj5 in paper.FillBlankQuestions)
                    {
                        Question question = (Question)obj5;
                        question.Preapare2Submit();
                    }
                }
                if (paper.EssayQuestion != null)
                {
                    paper.EssayQuestion.Preapare2Submit();
                    paper.EssayQuestion.Development = this.txtWrittingEssay.Text;
                }
                paper.AudioData = null;
                result = new SubmitPaper
                {
                    LoginId = this.loginId,
                    SPaper = paper,
                    TimeLeft = this.timeLeft,
                    IndexG = this.indexGrammar,
                    IndexReading = this.indexReading,
                    IndexIndiM = this.indexIndicateMistake,
                    IndexMatch = this.indexMatching,
                    TabIndex = this.tabControlQuestion.SelectedIndex,
                    IndexFill = this.indexFill,
                    Finish = finish,
                    ID = EncryptSupport.GetMD5(this.examData.RegData.Login + this.examData.RegData.Password)
                };
            }
            catch
            {
                result = null;
            }
            return result;
        }

        private void btnNextQuestionM_Click(object sender, EventArgs e)
        {
            if (this.lstReadingQuestionM.Items.Count > 0)
            {
                if (this.IsNeedSaveMultiple())
                {
                    this.Submit2Server_SaveAtClient();
                }
                this.indexReadingQuestion = this.lstReadingQuestionM.SelectedIndex;
                this.indexReadingQuestion++;
                this.indexReadingQuestion %= this.lstReadingQuestionM.Items.Count;
                this.lstReadingQuestionM.SelectedIndex = this.indexReadingQuestion;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.EnableAllKeyBoard();
            this.EnableMouse();
            if (this.timeLeft != 0)
            {
                if (!this.chbWantFinish.Checked)
                {
                    return;
                }
                if (!this.chbWantFinishTop.Checked)
                {
                    return;
                }
            }
            this.lblSaveServer.Text = "";
            this.finishClick = true;
            this.chbWantFinish.Enabled = false;
            this.chbWantFinishTop.Enabled = false;
            this.FinishExam();
            this.tabControlQuestion.Visible = false;
            this.panelQuestionList.Visible = false;
            if (this.waveOut != null && this.waveOut.PlaybackState != PlaybackState.Stopped)
            {
                this.waveOut.Stop();
            }
        }

        private void SendAllMissScreenImages()
        {
            int i = 3;
            if (this.listMissScreenImages.Count > 0)
            {
                while (i > 0)
                {
                    try
                    {
                        IRemoteMonitorServer remoteMonitorServer = (IRemoteMonitorServer)Activator.GetObject(typeof(IRemoteMonitorServer), this.monitorURL);
                        while (this.listMissScreenImages.Count > 0)
                        {
                            byte[] img = (byte[])this.listMissScreenImages[0];
                            this.listMissScreenImages.RemoveAt(0);
                            this.captureScreenInterval = remoteMonitorServer.SaveScreenImage(img, this.imageIndex++, this.examData.RegData.ExamCode, this.loginId);
                        }
                        i = 0;
                    }
                    catch
                    {
                        i--;
                    }
                }
            }
            if (this.exitButtonClicked)
            {
                Application.Exit();
            }
        }

        private void SendOneMissScreenImage()
        {
            if (this.listMissScreenImages.Count > 0)
            {
                try
                {
                    IRemoteMonitorServer remoteMonitorServer = (IRemoteMonitorServer)Activator.GetObject(typeof(IRemoteMonitorServer), this.monitorURL);
                    byte[] img = (byte[])this.listMissScreenImages[0];
                    this.captureScreenInterval = remoteMonitorServer.SaveScreenImage(img, this.imageIndex++, this.examData.RegData.ExamCode, this.loginId);
                    lock (this.listMissScreenImages)
                    {
                        this.listMissScreenImages.RemoveAt(0);
                    }
                }
                catch
                {
                }
            }
        }

        private bool SaveAtServerWhenFinish()
        {
            this.lblSaveServer.Text = "";
            bool result;
            try
            {
                IRemoteServer remoteServer = (IRemoteServer)Activator.GetObject(typeof(IRemoteServer), this.remotingURL);
                string text = "";
                if (remoteServer.Submit(this.sPaper, ref text) == SubmitStatus.OK)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void FinishExam()
        {
            this.sPaper = this.CreateSubmitPaper(true);
            this.SaveAtClient();
            if (!this.finishClick)
            {
                this.timerCountDown.Enabled = false;
                this.timerTopMost.Enabled = false;
                this.tabControlQuestion.TabPages.Remove(this.tabPageGrammar);
                this.tabControlQuestion.TabPages.Remove(this.tabPageIndicateMistake);
                this.tabControlQuestion.TabPages.Remove(this.tabPageMatching);
                this.tabControlQuestion.TabPages.Remove(this.tabPageReadingM);
                this.tabControlQuestion.TabPages.Remove(this.tabPageFillBlank);
                this.nudFontSize.Enabled = false;
                this.btnFinish.Enabled = false;
                this.lblOver.Text = "Examination Over!";
            }
            bool flag = this.SaveAtServerWhenFinish();
            if (flag)
            {
                this.btnFinish.Enabled = false;
                this.btnFinishTop.Enabled = false;
                this.btnExit.Enabled = true;
                this.lblSaveServer.Text = "Exam finished successfully.";
            }
            else
            {
                this.btnFinish.Enabled = true;
                this.btnFinishTop.Enabled = true;
                this.btnExit.Enabled = false;
                this.lblSaveServer.Text = "Save at server failed! Please click Finish button again later.";
            }
            base.TopMost = false;
            base.WindowState = FormWindowState.Normal;
        }

        private void chbWantFinish_CheckedChanged(object sender, EventArgs e)
        {
            this.btnFinish.Enabled = this.chbWantFinish.Checked;
            this.btnFinishTop.Enabled = this.chbWantFinish.Checked;
            this.chbWantFinishTop.Checked = this.chbWantFinish.Checked;
        }

        private void tabPageMatching_SizeChanged(object sender, EventArgs e)
        {
            int num = this.tabControlQuestion.Width;
            int num2 = 10;
            num = num - 5 * num2 - this.lstAnswerMatch.Width;
            this.txtColumnA.Left = num2;
            this.txtColumnA.Width = num / 2;
            this.lstAnswerMatch.Left = num2 + this.txtColumnA.Width + num2;
            this.lblAnswerMatch.Left = this.lstAnswerMatch.Left;
            this.txtColumnB.Left = num2 + this.txtColumnA.Width + num2 + this.lstAnswerMatch.Width + num2;
            this.txtColumnB.Width = num / 2;
            this.txtNumber.Left = num2 + this.txtColumnA.Width - this.txtNumber.Width;
            this.lblNumber.Left = this.txtNumber.Left;
            this.txtLetter.Left = this.txtColumnB.Left;
            this.lblLetter.Left = this.txtLetter.Left;
            this.btnAddMSolution.Left = this.tabControlQuestion.Width / 2 - this.btnAddMSolution.Width - num2;
            this.btnRemoveMSolution.Left = this.btnAddMSolution.Left + this.btnAddMSolution.Width + num2;
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            Font font = new Font(this.txtReadingM.Font.Name, (float)this.nudFontSize.Value);
            this.txtColumnA.Font = font;
            this.txtColumnB.Font = font;
            this.lblMC_Text.Font = font;
            this.txtIndiMistake.Font = font;
            this.txtReadingM.Font = font;
            font = new Font(this.txtWrittingEssay.Font.Name, (float)this.nudFontSize.Value);
            this.txtWrittingEssay.Font = font;
        }

        private ArrayList GetRandomFromList(ArrayList list, int n)
        {
            Random random = new Random((int)(DateTime.Now.Ticks % 2147483647L));
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < n; i++)
            {
                int index = random.Next(list.Count) % list.Count;
                arrayList.Add(list[index]);
                list.RemoveAt(index);
                list.TrimToSize();
            }
            return arrayList;
        }

        private void Shuffle()
        {
            if (this.paper.EssayQuestion == null)
            {
                if (this.paper.IsShuffleReading)
                {
                    this.paper.ReadingQuestions = this.ShuffleList(this.paper.ReadingQuestions);
                }
                foreach (object obj in this.paper.ReadingQuestions)
                {
                    Passage passage = (Passage)obj;
                    if (this.paper.IsShuffleReading)
                    {
                        passage.PassageQuestions = this.ShuffleList(passage.PassageQuestions);
                    }
                    foreach (object obj2 in passage.PassageQuestions)
                    {
                        Question question = (Question)obj2;
                        if (!question.Lock)
                        {
                            question.QuestionAnswers = this.ShuffleList(question.QuestionAnswers);
                        }
                    }
                }
                if (this.paper.QD != null)
                {
                    if (this.paper.GrammarQuestions.Count > this.paper.QD.MultipleChoices)
                    {
                        Hashtable hashtable = new Hashtable();
                        foreach (object obj3 in this.paper.GrammarQuestions)
                        {
                            Question question = (Question)obj3;
                            if (hashtable.ContainsKey(question.ChapterId))
                            {
                                ((ArrayList)hashtable[question.ChapterId]).Add(question);
                            }
                            else
                            {
                                ArrayList arrayList = new ArrayList();
                                arrayList.Add(question);
                                hashtable[question.ChapterId] = arrayList;
                            }
                        }
                        float num = (float)this.paper.QD.MultipleChoices / (float)this.paper.GrammarQuestions.Count;
                        int num2 = 0;
                        ArrayList arrayList2 = new ArrayList();
                        ArrayList arrayList3 = new ArrayList();
                        foreach (object obj4 in hashtable.Keys)
                        {
                            int num3 = (int)obj4;
                            int num4 = (int)(num * (float)((ArrayList)hashtable[num3]).Count);
                            num2 += num4;
                            ArrayList randomFromList = this.GetRandomFromList((ArrayList)hashtable[num3], num4);
                            arrayList2.AddRange(randomFromList);
                            arrayList3.AddRange((ArrayList)hashtable[num3]);
                        }
                        int n = this.paper.QD.MultipleChoices - num2;
                        arrayList2.AddRange(this.GetRandomFromList(arrayList3, n));
                        this.paper.Mark = this.paper.Mark - (float)(this.paper.GrammarQuestions.Count - arrayList2.Count);
                        this.paper.GrammarQuestions = arrayList2;
                    }
                }
                if (this.paper.IsShuffleGrammer)
                {
                    this.paper.GrammarQuestions = this.ShuffleList(this.paper.GrammarQuestions);
                }
                foreach (object obj5 in this.paper.GrammarQuestions)
                {
                    Question question = (Question)obj5;
                    if (!question.Lock)
                    {
                        question.QuestionAnswers = this.ShuffleList(question.QuestionAnswers);
                    }
                }
                if (this.paper.IsShuffleIndicateMistake)
                {
                    this.paper.IndicateMQuestions = this.ShuffleList(this.paper.IndicateMQuestions);
                }
                foreach (object obj6 in this.paper.IndicateMQuestions)
                {
                    Question question = (Question)obj6;
                    if (!question.Lock)
                    {
                        question.QuestionAnswers = this.ShuffleList(question.QuestionAnswers);
                    }
                }
                if (this.paper.IsShuffleFillBlank)
                {
                    this.paper.FillBlankQuestions = this.ShuffleList(this.paper.FillBlankQuestions);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (this.examData == null)
            {
                base.Close();
            }
            else
            {
                this.exitButtonClicked = true;
                if (this.listMissScreenImages.Count > 0)
                {
                    base.Hide();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void frmEnglishExamClient_SizeChanged(object sender, EventArgs e)
        {
            if (base.TopMost)
            {
                base.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnNextFillBlank_Click(object sender, EventArgs e)
        {
            if (this.paper.FillBlankQuestions.Count > 0)
            {
                if (this.txtList != null)
                {
                    Question question = (Question)this.paper.FillBlankQuestions[this.indexFill];
                    for (int i = 0; i < this.txtList.Count; i++)
                    {
                        QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[i];
                        questionAnswer.Text = ((TextBox)this.txtList[i]).Text;
                    }
                }
                if (this.cboList != null)
                {
                    Question question = (Question)this.paper.FillBlankQuestions[this.indexFill];
                    for (int i = 0; i < this.cboList.Count; i++)
                    {
                        QuestionAnswer questionAnswer = (QuestionAnswer)question.QuestionAnswers[i];
                        questionAnswer.Text = ((ComboBox)this.cboList[i]).Text;
                    }
                }
                if (this.IsNeedSaveFillBlank())
                {
                    this.Submit2Server_SaveAtClient();
                }
                this.indexFill++;
                this.indexFill %= this.paper.FillBlankQuestions.Count;
                this.DisplayFillBlankQuestion(this.indexFill);
            }
        }

        private string[] GetFilledWord(string qLine)
        {
            string fillBlank_Pattern = QuestionHelper.fillBlank_Pattern;
            Regex regex = new Regex(fillBlank_Pattern);
            MatchCollection matchCollection = regex.Matches(qLine);
            string[] array = new string[matchCollection.Count];
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string text = matchCollection[i].Value.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);
                array[i] = text.Trim().ToLower();
            }
            Array.Sort<string>(array);
            return array;
        }

        private string[] GetFilledWord_Group(string qLine, int groupNo)
        {
            string fillBlank_Pattern = QuestionHelper.fillBlank_Pattern;
            Regex regex = new Regex(fillBlank_Pattern);
            MatchCollection matchCollection = regex.Matches(qLine);
            string text = matchCollection[groupNo].Value.Remove(0, 1);
            text = text.Trim().Remove(0, 1);
            text = text.Remove(text.Length - 1, 1);
            text = text.Trim();
            text = text.ToLower();
            char[] separator = new char[]
            {
                '~'
            };
            string[] array = text.Split(separator);
            string[] result;
            if (array == null)
            {
                result = null;
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                    if (array[i].StartsWith("="))
                    {
                        array[i] = array[i].Remove(0, 1);
                    }
                }
                Array.Sort<string>(array);
                result = array;
            }
            return result;
        }

        private void ans_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                QuestionAnswer questionAnswer = (QuestionAnswer)textBox.Tag;
                questionAnswer.Text = textBox.Text;
            }
            if (sender is ComboBox)
            {
                ComboBox comboBox = (ComboBox)sender;
                QuestionAnswer questionAnswer = (QuestionAnswer)comboBox.Tag;
                questionAnswer.Text = comboBox.Text;
            }
        }

        private void ans_Leave(object sender, EventArgs e)
        {
            if (this.IsNeedSaveFillBlank())
            {
                this.Submit2Server_SaveAtClient();
            }
        }

        private bool IsNeedSaveFillBlank()
        {
            string text = "";
            if (this.txtList != null)
            {
                foreach (object obj in this.txtList)
                {
                    TextBox textBox = (TextBox)obj;
                    text += textBox.Text;
                }
            }
            if (this.cboList != null)
            {
                foreach (object obj2 in this.cboList)
                {
                    ComboBox comboBox = (ComboBox)obj2;
                    text += comboBox.Text;
                }
            }
            return !this.oldFillBlankAnswer.Equals(text);
        }

        private void txtFill_Validated(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.Tag != null)
                {
                    QuestionAnswer questionAnswer = (QuestionAnswer)textBox.Tag;
                    textBox.Text = QuestionHelper.RemoveSpaces(textBox.Text);
                    questionAnswer.Text = textBox.Text;
                }
            }
        }

        private void txtReadingM_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int x = this.btnNextReadingM.Left + this.tabControlQuestion.Left;
            int y = this.btnNextReadingM.Top + this.tabControlQuestion.Top;
            Cursor.Position = new Point(x, y);
        }

        private void btnShowExam_Click(object sender, EventArgs e)
        {
            if (this.txtOpenCode.Text.Trim().Equals(this.paper.ListenCode))
            {
                this.ControlManager();
                this.Shuffle();
                this.RemoveTabPages();
                this.DisplayReading(this.indexReading);
                this.DisplayMatching(this.indexMatching);
                this.DisplayGrammar(this.indexGrammar);
                this.DisplayIndiMistake(this.indexIndicateMistake);
                this.DisplayFillBlankQuestion(this.indexFill);
                this.DisplayEssay();
                this.txtOpenCode.Enabled = false;
                this.btnShowExam.Enabled = false;
                this.TimerTopMost_FirstDisplay();
                this.DisableAllKeyBoard();
                if (this.paper.AudioData != null && this.paper.AudioData.Length > 0)
                {
                    this.PlayFromBuf(this.paper.AudioData, 0);
                }
            }
            else
            {
                MessageBox.Show("Open Code is invalid!", "Show Question", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PlayFromBuf(byte[] audioData, int pos)
        {
            Stream inputStream = new MemoryStream(audioData);
            this.reader = new Mp3FileReader(inputStream);
            if ((double)pos < this.reader.TotalTime.TotalSeconds)
            {
                this.waveOut = new WaveOut();
                this.waveOut.Init(this.reader);
                this.reader.Position = (long)(this.reader.WaveFormat.AverageBytesPerSecond * pos);
                this.waveOut.Volume = (float)this.nudVol.Value * 0.1f;
                this.waveOut.Play();
            }
        }

        private void CloseApps()
        {
            while (this.timerCountDown.Enabled)
            {
                try
                {
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (!process.MainWindowTitle.Equals("EOS Login Form"))
                        {
                            if (!process.MainWindowTitle.Equals(""))
                            {
                                process.Kill();
                            }
                        }
                    }
                }
                catch
                {
                }
                Thread.Sleep(1000);
            }
        }

        private void timerTopMost_Tick(object sender, EventArgs e)
        {
            if (this.paper.TestType == TestTypeEnum.NOT_WRITING || this.paper.TestType == TestTypeEnum.WRITING_EN)
            {
                base.TopMost = true;
            }
            try
            {
                Clipboard.Clear();
            }
            catch
            {
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            string value = e.KeyChar.ToString();
            string text = "0123456789\b";
            if (text.IndexOf(value) >= 0)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLetter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text = e.KeyChar.ToString();
            string text2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\b";
            if (text2.IndexOf(text.ToUpper()) >= 0)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void run()
        {
            while (!this.finishClick)
            {
                if (this.exitButtonClicked)
                {
                    break;
                }
                if (this.closeConnection && !this.keepConnection)
                {
                    try
                    {
                        string[] array = DisconnectWrapper.Connections(DisconnectWrapper.ConnectionState.Established);
                        foreach (string connectionstring in array)
                        {
                            DisconnectWrapper.CloseConnection(connectionstring);
                        }
                        Thread.Sleep(this.closeTCPInterval);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void StartCloseTCPConnectionsThread()
        {
            ThreadStart start = new ThreadStart(this.run);
            Thread thread = new Thread(start);
            thread.Start();
        }

        private void DisableMouse()
        {
            if (this.mouseEnabled)
            {
                HookManager.MouseClickExt += this.HookManager_MouseMoveExt;
                this.mouseEnabled = false;
            }
        }

        private void EnableMouse()
        {
            if (!this.mouseEnabled)
            {
                HookManager.MouseClickExt -= this.HookManager_MouseMoveExt;
                this.mouseEnabled = true;
            }
        }

        private void DisableAllKeyBoard()
        {
            if (this.keyboardEnabled)
            {
                HookManager.KeyDown += this.HookManager_KeyDown;
                HookManager.KeyUp += this.HookManager_KeyUp;
                HookManager.KeyPress += this.HookManager_KeyPress;
                this.keyboardEnabled = false;
            }
        }

        private void EnableAllKeyBoard()
        {
            if (!this.keyboardEnabled)
            {
                HookManager.KeyDown -= this.HookManager_KeyDown;
                HookManager.KeyUp -= this.HookManager_KeyUp;
                HookManager.KeyPress -= this.HookManager_KeyPress;
                this.keyboardEnabled = true;
            }
        }

        private void EnableNonFunctionKeys()
        {
            this.nonFunctionKeysEnabled = true;
        }

        private void DisableNonFunctionKeys()
        {
            this.nonFunctionKeysEnabled = false;
        }

        private bool IsAllowKey(Keys kCode)
        {
            return kCode == Keys.LShiftKey || kCode == Keys.RShiftKey || kCode == Keys.Oemtilde || kCode == Keys.OemMinus || kCode == Keys.Oemplus || kCode == Keys.OemOpenBrackets || kCode == Keys.OemCloseBrackets || kCode == Keys.OemPipe || kCode == Keys.OemSemicolon || kCode == Keys.OemQuotes || kCode == Keys.Oemcomma || kCode == Keys.OemPeriod || kCode == Keys.OemQuestion || kCode == Keys.Back || kCode == Keys.Space || (kCode >= Keys.D0 && kCode <= Keys.D9) || (kCode >= Keys.NumPad0 && kCode <= Keys.NumPad9) || (kCode >= Keys.A && kCode <= Keys.Z) || (kCode == Keys.Up || kCode == Keys.Down || kCode == Keys.Left || kCode == Keys.Right) || kCode == Keys.Return || (this.paper.EssayQuestion != null && kCode == Keys.Delete) || (this.paper.EssayQuestion != null && kCode == Keys.Tab) || (this.paper.EssayQuestion != null && kCode == Keys.End) || (this.paper.EssayQuestion != null && kCode == Keys.Home);
        }

        private bool IsAllowKey(int kChar)
        {
            string text = "QWERTYUIOPASDFGHJKLZXCVBNM`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_+{}|:\"<>?";
            string value = ((char)kChar).ToString();
            return text.Contains(value) || kChar == 8 || kChar == 32 || kChar == 13 || (this.paper.EssayQuestion != null && kChar == 9);
        }

        private void txtNumber_Enter(object sender, EventArgs e)
        {
            this.EnableNonFunctionKeys();
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            this.DisableNonFunctionKeys();
        }

        private void txtLetter_Leave(object sender, EventArgs e)
        {
            this.DisableNonFunctionKeys();
        }

        private void txtLetter_Enter(object sender, EventArgs e)
        {
            this.EnableNonFunctionKeys();
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.nonFunctionKeysEnabled || !this.IsAllowKey(e.KeyCode))
            {
                e.Handled = true;
            }
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (!this.nonFunctionKeysEnabled || !this.IsAllowKey(e.KeyCode))
            {
                e.Handled = true;
            }
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.nonFunctionKeysEnabled || !this.IsAllowKey((int)e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                e.Handled = true;
            }
        }

        private void btnEssayNormal_Click(object sender, EventArgs e)
        {
            this.pictureBoxEssay.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnEssayZoom_Click(object sender, EventArgs e)
        {
            this.pictureBoxEssay.Top = this.panelPicEssay.Top;
            this.pictureBoxEssay.Left = this.panelPicEssay.Left;
            this.pictureBoxEssay.Width = this.panelPicEssay.Width;
            this.pictureBoxEssay.Height = this.panelPicEssay.Height;
            this.pictureBoxEssay.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnWordCount_Click(object sender, EventArgs e)
        {
            string text = this.txtWrittingEssay.Text.Trim();
            char[] separator = new char[]
            {
                ' ',
                ',',
                '.',
                ':',
                ';',
                '=',
                '\n',
                '\r',
                '-'
            };
            string[] array = text.Split(separator);
            int num = 0;
            foreach (string text2 in array)
            {
                if (text2.Trim().Length > 0)
                {
                    num++;
                }
            }
            this.lblWordCount.Text = num + " words";
        }

        private void btnSaveEssay_Click(object sender, EventArgs e)
        {
            this.paper.EssayQuestion.Development = this.txtWrittingEssay.Text;
            this.Submit2Server_SaveAtClient();
        }

        private void txtWrittingEssay_TextChanged(object sender, EventArgs e)
        {
            if (this.undoStack.Count == 0 || !this.undoStack[this.undoStack.Count - 1].Equals(this.txtWrittingEssay.Text))
            {
                this.undoStack.Add(this.txtWrittingEssay.Text);
                if (this.undoStack.Count > this.depth)
                {
                    this.undoStack.RemoveAt(0);
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (this.undoStack.Count > 1)
            {
                this.redoStack.Add(this.undoStack[this.undoStack.Count - 1]);
                this.undoStack.RemoveAt(this.undoStack.Count - 1);
                if (this.undoStack.Count != 0)
                {
                    this.txtWrittingEssay.Text = this.undoStack[this.undoStack.Count - 1];
                }
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (this.redoStack.Count > 0)
            {
                if (this.redoStack.Count != 0)
                {
                    this.txtWrittingEssay.Text = this.redoStack[this.redoStack.Count - 1];
                }
                this.redoStack.RemoveAt(this.redoStack.Count - 1);
            }
        }

        private void questionButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int num = Convert.ToInt32(button.Text);
            if (this.tabControlQuestion.SelectedTab == this.tabPageGrammar)
            {
                this.indexGrammar = num - 1;
                this.DisplayGrammar(this.indexGrammar);
            }
            if (this.tabControlQuestion.SelectedTab == this.tabPageIndicateMistake)
            {
                this.indexIndicateMistake = num - 1;
                this.DisplayIndiMistake(this.indexIndicateMistake);
            }
        }

        private void SetButtonColor(int qNo, Color clr)
        {
            foreach (object obj in this.panelQuestionList.Controls)
            {
                Button button = (Button)obj;
                int num = Convert.ToInt32(button.Text);
                if (qNo == num)
                {
                    button.BackColor = clr;
                }
            }
        }

        private void AddQuestionButon(int n)
        {
            int num = 0;
            int num2 = 10;
            for (int i = 1; i <= n; i++)
            {
                Button button = new Button();
                button.Text = i.ToString();
                button.Width = 34;
                button.Height = 23;
                button.Left = button.Width * num;
                button.Top = num2;
                num++;
                if (button.Left + button.Width >= this.panelQuestionList.Width - button.Width)
                {
                    num = 0;
                    button.Left = button.Width * num;
                    num2 = num2 + button.Height + 5;
                    button.Top = num2;
                    num++;
                }
                this.panelQuestionList.Controls.Add(button);
                button.Visible = true;
                button.Click += this.questionButton_Click;
            }
        }

        private void chbWantFinishTop_CheckedChanged(object sender, EventArgs e)
        {
            this.btnFinish.Enabled = this.chbWantFinishTop.Checked;
            this.btnFinishTop.Enabled = this.chbWantFinishTop.Checked;
            this.chbWantFinish.Checked = this.chbWantFinishTop.Checked;
        }

        private void btnFinishTop_Click(object sender, EventArgs e)
        {
            this.btnFinish.PerformClick();
        }

        private void txtWrittingEssay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.buf = this.txtWrittingEssay.SelectedText;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                this.txtWrittingEssay.Text = this.txtWrittingEssay.Text.Insert(this.txtWrittingEssay.SelectionStart, this.buf);
            }
        }

        private void frmEnglishExamClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.btnExit.Enabled)
            {
                e.Cancel = true;
            }
        }

        private void CloseAppsTest()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (!process.MainWindowTitle.StartsWith("ExamClient_ENG"))
                {
                    if (!process.MainWindowTitle.Equals("EOS Login Form"))
                    {
                        if (!process.ProcessName.StartsWith("explo"))
                        {
                            if (!process.MainWindowTitle.Equals(""))
                            {
                                process.Kill();
                            }
                        }
                    }
                }
            }
        }

        private void nudVol_ValueChanged(object sender, EventArgs e)
        {
            if (this.waveOut != null)
            {
                this.waveOut.Volume = (float)this.nudVol.Value * 0.1f;
            }
        }

        private void btnReadingRealSize_Click(object sender, EventArgs e)
        {
            this.picBoxReadingM.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnReadingZoomIn_Click(object sender, EventArgs e)
        {
            this.picBoxReadingM.SizeMode = PictureBoxSizeMode.StretchImage;
            int width = (int)((double)this.picBoxReadingM.Width * 1.1);
            int height = (int)((double)this.picBoxReadingM.Height * 1.1);
            this.picBoxReadingM.Size = new Size(width, height);
        }

        private void btnReadingZoomOut_Click(object sender, EventArgs e)
        {
            this.picBoxReadingM.SizeMode = PictureBoxSizeMode.StretchImage;
            int width = (int)((double)this.picBoxReadingM.Width * 0.9);
            int height = (int)((double)this.picBoxReadingM.Height * 0.9);
            this.picBoxReadingM.Size = new Size(width, height);
        }

        private void domainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            Font font = new Font(this.domainUpDown.Text, (float)this.nudFontSize.Value);
            this.txtColumnA.Font = font;
            this.txtColumnB.Font = font;
            this.lblMC_Text.Font = font;
            this.txtIndiMistake.Font = font;
            this.txtReadingM.Font = font;
            this.txtWrittingEssay.Font = font;
        }

        private EOSData examData = null;

        private string remotingURL = null;

        private string monitorURL = null;

        public string loginId;

        private int timeLeft;

        private Paper paper = null;

        private bool finishClick = false;

        private CheckBox[] chkReadingM;

        private CheckBox[] chkGrammar;

        private CheckBox[] chkIndiMistake;

        private int indexReading = 0;

        private int indexReadingQuestion = 0;

        private int indexMatching = 0;

        private int indexGrammar = 0;

        private int indexIndicateMistake = 0;

        private int indexFill = 0;

        private Random ran = new Random((int)DateTime.Now.Ticks);

        private int autoSaveInteval = 120;

        private bool submitFirstTime = false;

        private bool keepConnection = false;

        private ArrayList listMissScreenImages = new ArrayList();

        private string[] qaNo = new string[]
{
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"
};

        private bool closeConnection = true;

        private int closeTCPInterval = 500;

        private int captureScreenInterval = 10;

        private int imageIndex = 0;

        private string oldMultipleChoice = "";

        private int matchingAnswerCount = 0;

        private string oldFillBlankAnswer = "";

        private string oldGrammarChoice = "";

        private string oldIndiMistakeChoice = "";

        private SubmitPaper sPaper;

        private int lastSave = 0;

        private bool exitButtonClicked = false;

        private ArrayList lblList = null;

        private ArrayList txtList = null;

        private ArrayList cboList = null;

        private Mp3FileReader reader = null;

        private WaveOut waveOut = null;

        private bool keyboardEnabled = true;

        private bool nonFunctionKeysEnabled = true;

        private bool mouseEnabled = true;

        private List<string> undoStack = new List<string>();

        private List<string> redoStack = new List<string>();

        private int depth = 20;

        private string buf = null;

        private delegate void SetTextCallback(string text);
    }
}
