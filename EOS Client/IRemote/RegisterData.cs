using System;

namespace IRemote
{
    [Serializable]
    public class RegisterData
    {
        public string Login
        {
            get
            {
                return this._login;
            }
            set
            {
                this._login = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._startTime;
            }
            set
            {
                this._startTime = value;
            }
        }

        public string Machine
        {
            get
            {
                return this._machine;
            }
            set
            {
                this._machine = value;
            }
        }

        public string ExamCode
        {
            get
            {
                return this._examCode;
            }
            set
            {
                this._examCode = value;
            }
        }

        private string _login;

        private string _password;

        private DateTime _startTime;

        private string _machine;

        private string _examCode;
    }
}
