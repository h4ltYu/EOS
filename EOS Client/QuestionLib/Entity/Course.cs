using System;

namespace QuestionLib.Entity
{
    public class Course
    {
        public Course()
        {
        }

        public Course(string _cid, string _name)
        {
            this._cid = _cid;
            this._name = _name;
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

        private string _cid;

        private string _name;
    }
}
