using System;

namespace NAudio.Wave
{
    public class StoppedEventArgs : EventArgs
    {
        public StoppedEventArgs(Exception exception = null)
        {
            this.exception = exception;
        }

        public Exception Exception
        {
            get
            {
                return this.exception;
            }
        }

        private readonly Exception exception;
    }
}
