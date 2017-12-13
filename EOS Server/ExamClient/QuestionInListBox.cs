using System;
using QuestionLib.Entity;

namespace ExamClient
{
    public class QuestionInListBox
    {
        public QuestionInListBox(Question question, int number)
        {
            this._question = question;
            this._number = number;
        }

        public override string ToString()
        {
            return this._number.ToString();
        }

        public Question GetQuestion()
        {
            return this._question;
        }

        private int _number;

        private Question _question;
    }
}
