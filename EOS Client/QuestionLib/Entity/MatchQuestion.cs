using System;

namespace QuestionLib.Entity
{
    [Serializable]
    public class MatchQuestion
    {
        public int MID
        {
            get
            {
                return this._mid;
            }
            set
            {
                this._mid = value;
            }
        }

        public string CourseId
        {
            get
            {
                return this._courseId;
            }
            set
            {
                this._courseId = value;
            }
        }

        public int ChapterId
        {
            get
            {
                return this._chapterId;
            }
            set
            {
                this._chapterId = value;
            }
        }

        public string ColumnA
        {
            get
            {
                return this._columnA;
            }
            set
            {
                this._columnA = value;
            }
        }

        public string ColumnB
        {
            get
            {
                return this._columnB;
            }
            set
            {
                this._columnB = value;
            }
        }

        public string Solution
        {
            get
            {
                return this._solution;
            }
            set
            {
                this._solution = value;
            }
        }

        public float Mark
        {
            get
            {
                return this._mark;
            }
            set
            {
                this._mark = value;
            }
        }

        public string SudentAnswer
        {
            get
            {
                return this._studentAnswer;
            }
            set
            {
                this._studentAnswer = value;
            }
        }

        public override string ToString()
        {
            return this._mid.ToString();
        }

        public void Preapare2Submit()
        {
            this.Solution = null;
            this.ColumnA = null;
            this.ColumnB = null;
            this.CourseId = null;
        }

        private int _mid;

        private string _courseId;

        private int _chapterId;

        private string _columnA;

        private string _columnB;

        private string _solution;

        private float _mark;

        private string _studentAnswer;
    }
}
