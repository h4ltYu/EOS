using System;

namespace QuestionLib
{
    [Serializable]
    public class QuestionDistribution
    {
        public int MultipleChoices { get; set; }

        public int Reading { get; set; }

        public int FillBlank { get; set; }

        public int Matching { get; set; }

        public int IndicateMistake { get; set; }
    }
}
