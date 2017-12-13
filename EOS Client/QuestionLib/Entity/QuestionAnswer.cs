using System;

namespace QuestionLib.Entity
{
    [Serializable]
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
        }

        public QuestionAnswer(string text, bool chosen)
        {
            this._text = text;
            this._chosen = chosen;
        }

        public int QAID
        {
            get
            {
                return this._qaid;
            }
            set
            {
                this._qaid = value;
            }
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

        public bool Chosen
        {
            get
            {
                return this._chosen;
            }
            set
            {
                this._chosen = value;
            }
        }

        public bool Selected
        {
            get
            {
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }

        public bool Done
        {
            get
            {
                return this._done;
            }
            set
            {
                this._done = value;
            }
        }

        private int _qaid;

        private int _qid;

        private string _text;

        private bool _chosen;

        private bool _selected;

        private bool _done;
    }
}
