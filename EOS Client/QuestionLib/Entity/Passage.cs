using System;
using System.Collections;
using NHibernate;
using QuestionLib.Business;

namespace QuestionLib.Entity
{
    [Serializable]
    public class Passage
    {
        public Passage()
        {
            this._passageQuestions = new ArrayList();
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

        public ArrayList PassageQuestions
        {
            get
            {
                return this._passageQuestions;
            }
            set
            {
                this._passageQuestions = value;
            }
        }

        public override string ToString()
        {
            return this._pid.ToString();
        }

        public void LoadQuestions(ISessionFactory sessionFactory)
        {
            BOQuestion boquestion = new BOQuestion(sessionFactory);
            this._passageQuestions = (ArrayList)boquestion.LoadPassageQuestion(this._pid);
        }

        public void Preapare2Submit()
        {
            this.Text = null;
            this.CourseId = null;
            foreach (object obj in this.PassageQuestions)
            {
                Question question = (Question)obj;
                question.Preapare2Submit();
            }
        }

        private int _pid;

        private string _courseId;

        private int _chapterId;

        private string _text;

        private ArrayList _passageQuestions;
    }
}
