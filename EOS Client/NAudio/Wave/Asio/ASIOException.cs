using System;

namespace NAudio.Wave.Asio
{
    internal class ASIOException : Exception
    {
        public ASIOException()
        {
        }

        public ASIOException(string message) : base(message)
        {
        }

        public ASIOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ASIOError Error
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;
                this.Data["ASIOError"] = this.error;
            }
        }

        public static string getErrorName(ASIOError error)
        {
            return Enum.GetName(typeof(ASIOError), error);
        }

        private ASIOError error;
    }
}
