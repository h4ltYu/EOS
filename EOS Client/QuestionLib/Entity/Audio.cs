using System;

namespace QuestionLib.Entity
{
    [Serializable]
    public class Audio
    {
        public int AuID
        {
            get
            {
                return this._auID;
            }
            set
            {
                this._auID = value;
            }
        }

        public int ChID
        {
            get
            {
                return this._chID;
            }
            set
            {
                this._chID = value;
            }
        }

        public string AudioFile
        {
            get
            {
                return this._audioFile;
            }
            set
            {
                this._audioFile = value;
            }
        }

        public int AudioSize
        {
            get
            {
                return this._audioSize;
            }
            set
            {
                this._audioSize = value;
            }
        }

        public byte[] AudioData
        {
            get
            {
                return this._audioData;
            }
            set
            {
                this._audioData = value;
            }
        }

        public int AudioLength
        {
            get
            {
                return this._audioLength;
            }
            set
            {
                this._audioLength = value;
            }
        }

        private int _auID;

        private int _chID;

        private string _audioFile;

        private int _audioSize;

        private byte[] _audioData;

        private int _audioLength;
    }
}
