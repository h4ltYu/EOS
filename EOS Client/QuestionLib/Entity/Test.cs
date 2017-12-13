using System;

namespace QuestionLib.Entity
{
    public class Test
    {
        public string TestId
        {
            get
            {
                return this._testId;
            }
            set
            {
                this._testId = value;
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

        public string Questions
        {
            get
            {
                return this._questions;
            }
            set
            {
                this._questions = value;
            }
        }

        public int NumOfQuestion
        {
            get
            {
                return this._numOfQuestion;
            }
            set
            {
                this._numOfQuestion = value;
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

        private string _testId;

        private string _courseId;

        private string _questions;

        private int _numOfQuestion;

        private float _mark;

        private string _studentGuide;
    }
}
