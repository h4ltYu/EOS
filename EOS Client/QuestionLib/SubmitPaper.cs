using System;

namespace QuestionLib
{
    [Serializable]
    public class SubmitPaper
    {
        public override bool Equals(object obj)
        {
            SubmitPaper submitPaper = (SubmitPaper)obj;
            return this.ID.Equals(submitPaper.ID) && this.SPaper.ExamCode.Equals(submitPaper.SPaper.ExamCode);
        }

        public string LoginId;

        public int TimeLeft;

        public int IndexFill;

        public int IndexReading;

        public int IndexG;

        public int IndexIndiM;

        public int IndexMatch;

        public bool Finish;

        public Paper SPaper;

        public int TabIndex;

        public DateTime SubmitTime;

        public string ID;
    }
}
