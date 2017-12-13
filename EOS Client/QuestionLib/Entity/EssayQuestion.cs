using System;

namespace QuestionLib.Entity
{
    [Serializable]
    public class EssayQuestion
    {
        public int EQID { get; set; }

        public string CourseId { get; set; }

        public int ChapterId { get; set; }

        public byte[] Question { get; set; }

        public int QuestionFileSize { get; set; }

        public string Name { get; set; }

        public byte[] Guide2Mark { get; set; }

        public int Guide2MarkFileSize { get; set; }

        public string Development { get; set; }

        public void Preapare2Submit()
        {
            this.CourseId = null;
            this.Question = null;
            this.Name = null;
            this.Guide2Mark = null;
        }
    }
}
