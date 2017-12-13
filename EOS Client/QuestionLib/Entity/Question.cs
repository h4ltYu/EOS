using System;
using System.Collections;
using NHibernate;
using QuestionLib.Business;

namespace QuestionLib.Entity
{
    [Serializable]
    public class Question
    {
        public Question()
        {
            this._questionAnswers = new ArrayList();
        }

        public int QID
        {
            get
            {
                return this._qid;
            }
            set
            {
                this._qid = value;
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

        public int PID
        {
            get
            {
                return this._pid;
            }
            set
            {
                this._pid = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
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

        public ArrayList QuestionAnswers
        {
            get
            {
                return this._questionAnswers;
            }
            set
            {
                this._questionAnswers = value;
            }
        }

        public QuestionType QType
        {
            get
            {
                return this._qType;
            }
            set
            {
                this._qType = value;
            }
        }

        public bool Lock
        {
            get
            {
                return this._lock;
            }
            set
            {
                this._lock = value;
            }
        }

        public byte[] ImageData
        {
            get
            {
                return this._imageData;
            }
            set
            {
                this._imageData = value;
            }
        }

        public int ImageSize
        {
            get
            {
                return this._imageSize;
            }
            set
            {
                this._imageSize = value;
            }
        }

        public override string ToString()
        {
            return this._text;
        }

        public void LoadAnswers(ISessionFactory sessionFactory)
        {
            BOQuestionAnswer boquestionAnswer = new BOQuestionAnswer(sessionFactory);
            this._questionAnswers = (ArrayList)boquestionAnswer.LoadAnswer(this._qid);
        }

        public void Preapare2Submit()
        {
            this.Text = null;
            this.CourseId = null;
            this.ImageData = null;
            this.ImageSize = 0;
            if (this.QType != QuestionType.FILL_BLANK_ALL)
            {
                if (this.QType != QuestionType.FILL_BLANK_GROUP)
                {
                    if (this.QType != QuestionType.FILL_BLANK_EMPTY)
                    {
                        foreach (object obj in this.QuestionAnswers)
                        {
                            QuestionAnswer questionAnswer = (QuestionAnswer)obj;
                            questionAnswer.Text = null;
                        }
                    }
                }
            }
        }

        private int _qid;

        private string _courseId;

        private int _chapterId;

        private int _pid;

        private string _text;

        private float _mark;

        private ArrayList _questionAnswers;

        private QuestionType _qType;

        private bool _lock;

        private byte[] _imageData;

        private int _imageSize;
    }
}
