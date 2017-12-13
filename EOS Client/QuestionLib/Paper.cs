using System;
using System.Collections;
using QuestionLib.Entity;

namespace QuestionLib
{
    [Serializable]
    public class Paper
    {
        public bool IsShuffleReading
        {
            get
            {
                return this._isShuffleReading;
            }
            set
            {
                this._isShuffleReading = value;
            }
        }

        public bool IsShuffleGrammer
        {
            get
            {
                return this._isShuffleGrammer;
            }
            set
            {
                this._isShuffleGrammer = value;
            }
        }

        public bool IsShuffleMatch
        {
            get
            {
                return this._isShuffleMatch;
            }
            set
            {
                this._isShuffleMatch = value;
            }
        }

        public QuestionDistribution QD { get; set; }

        public bool IsShuffleIndicateMistake
        {
            get
            {
                return this._isShuffleIndicateMistake;
            }
            set
            {
                this._isShuffleIndicateMistake = value;
            }
        }

        public bool IsShuffleFillBlank
        {
            get
            {
                return this._isShuffleFillBlank;
            }
            set
            {
                this._isShuffleFillBlank = value;
            }
        }

        public Paper()
        {
            this._reading = new ArrayList();
            this._grammar = new ArrayList();
            this._match = new ArrayList();
            this._indicateMistake = new ArrayList();
            this._fillBlank = new ArrayList();
            this.TestType = TestTypeEnum.NOT_WRITING;
            this._audioData = null;
        }

        public int Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
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

        public int NoOfQuestion
        {
            get
            {
                return this._noOfQuestion;
            }
            set
            {
                this._noOfQuestion = value;
            }
        }

        public ArrayList ReadingQuestions
        {
            get
            {
                return this._reading;
            }
            set
            {
                this._reading = value;
            }
        }

        public ArrayList GrammarQuestions
        {
            get
            {
                return this._grammar;
            }
            set
            {
                this._grammar = value;
            }
        }

        public ArrayList MatchQuestions
        {
            get
            {
                return this._match;
            }
            set
            {
                this._match = value;
            }
        }

        public ArrayList IndicateMQuestions
        {
            get
            {
                return this._indicateMistake;
            }
            set
            {
                this._indicateMistake = value;
            }
        }

        public ArrayList FillBlankQuestions
        {
            get
            {
                return this._fillBlank;
            }
            set
            {
                this._fillBlank = value;
            }
        }

        public EssayQuestion EssayQuestion
        {
            get
            {
                return this._essay;
            }
            set
            {
                this._essay = value;
            }
        }

        public string StudentGuide
        {
            get
            {
                return this._studentGuide;
            }
            set
            {
                this._studentGuide = value;
            }
        }

        public string ListenCode
        {
            get
            {
                return this._listenCode;
            }
            set
            {
                this._listenCode = value;
            }
        }

        public string Password
        {
            get
            {
                return this._pwd;
            }
            set
            {
                this._pwd = value;
            }
        }

        public TestTypeEnum TestType
        {
            get
            {
                return this._testType;
            }
            set
            {
                this._testType = value;
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

        private TestTypeEnum _testType;

        private string _examCode;

        private int _duration;

        private float _mark;

        private int _noOfQuestion;

        private ArrayList _reading;

        private ArrayList _grammar;

        private ArrayList _match;

        private ArrayList _indicateMistake;

        private ArrayList _fillBlank;

        private EssayQuestion _essay;

        private bool _isShuffleReading;

        private bool _isShuffleGrammer;

        private bool _isShuffleMatch;

        private bool _isShuffleIndicateMistake;

        private bool _isShuffleFillBlank;

        private string _studentGuide;

        private string _listenCode;

        private string _pwd;

        private byte[] _audioData;

        private int _audioSize;
    }
}
