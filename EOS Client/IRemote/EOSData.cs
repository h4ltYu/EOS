using System;
using QuestionLib;

namespace IRemote
{
    [Serializable]
    public class EOSData
    {
        public RegisterStatus Status;

        public Paper ExamPaper;

        public SubmitPaper StudentSubmitPaper;

        public byte[] GUI;

        public int OriginSize;

        public ServerInfo ServerInfomation;

        public RegisterData RegData;
    }
}
