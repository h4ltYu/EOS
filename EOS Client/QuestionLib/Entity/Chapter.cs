using System;

namespace QuestionLib.Entity
{
    public class Chapter
    {
        public Chapter()
        {
        }

        public Chapter(int _chid, string _cid, string _name)
        {
            this._chid = _chid;
            this._cid = _cid;
            this._name = _name;
        }

        public int ChID
        {
            get
            {
                return this._chid;
            }
            set
            {
                this._chid = value;
            }
        }

        public string CID
        {
            get
            {
                return this._cid;
            }
            set
            {
                this._cid = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public override string ToString()
        {
            return this._name;
        }

        private int _chid;

        private string _cid;

        private string _name;
    }
}
