using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using QuestionLib.Entity;

namespace QuestionLib
{
    public class QuestionHelper
    {
        public static void SaveSubmitPaper(string folder, SubmitPaper submitPaper)
        {
            submitPaper.SubmitTime = DateTime.Now;
            string path = folder + submitPaper.LoginId + ".dat";
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, submitPaper);
            fileStream.Flush();
            fileStream.Close();
        }

        public static SubmitPaper LoadSubmitPaper(string savedFile)
        {
            FileStream fileStream = new FileStream(savedFile, FileMode.Open, FileAccess.Read);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SubmitPaper result = (SubmitPaper)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return result;
        }

        private static Passage GetPassage(Paper oPaper, int pid)
        {
            foreach (object obj in oPaper.ReadingQuestions)
            {
                Passage passage = (Passage)obj;
                if (passage.PID == pid)
                {
                    return passage;
                }
            }
            return null;
        }

        private static bool ReConstructQuestion(Question sq, Question oq)
        {
            bool result;
            if (sq.QID == oq.QID)
            {
                sq.CourseId = oq.CourseId;
                sq.Text = oq.Text;
                sq.ImageData = oq.ImageData;
                sq.ImageSize = oq.ImageSize;
                bool flag = false;
                if (sq.QType == QuestionType.FILL_BLANK_ALL)
                {
                    flag = true;
                }
                if (sq.QType == QuestionType.FILL_BLANK_GROUP)
                {
                    flag = true;
                }
                if (sq.QType == QuestionType.FILL_BLANK_EMPTY)
                {
                    flag = true;
                }
                foreach (object obj in sq.QuestionAnswers)
                {
                    QuestionAnswer questionAnswer = (QuestionAnswer)obj;
                    foreach (object obj2 in oq.QuestionAnswers)
                    {
                        QuestionAnswer questionAnswer2 = (QuestionAnswer)obj2;
                        if (questionAnswer.QAID == questionAnswer2.QAID)
                        {
                            if (flag)
                            {
                                string text = QuestionHelper.RemoveSpaces(questionAnswer.Text).Trim().ToLower();
                                string value = QuestionHelper.RemoveSpaces(questionAnswer2.Text).Trim().ToLower();
                                if (text.Equals(value))
                                {
                                    questionAnswer.Chosen = true;
                                    questionAnswer.Selected = true;
                                }
                            }
                            else
                            {
                                questionAnswer.Text = questionAnswer2.Text;
                                questionAnswer.Chosen = questionAnswer2.Chosen;
                            }
                            break;
                        }
                    }
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private static void ReConstructEssay(EssayQuestion sEssay, EssayQuestion oEssay)
        {
            if (sEssay.EQID == oEssay.EQID)
            {
                sEssay.CourseId = oEssay.CourseId;
                sEssay.Question = oEssay.Question;
            }
        }

        public static Paper Re_ConstructPaper(Paper oPaper, SubmitPaper submitPaper)
        {
            Paper spaper = submitPaper.SPaper;
            foreach (object obj in spaper.ReadingQuestions)
            {
                Passage passage = (Passage)obj;
                Passage passage2 = QuestionHelper.GetPassage(oPaper, passage.PID);
                passage.Text = passage2.Text;
                passage.CourseId = passage2.CourseId;
                foreach (object obj2 in passage.PassageQuestions)
                {
                    Question sq = (Question)obj2;
                    foreach (object obj3 in passage2.PassageQuestions)
                    {
                        Question oq = (Question)obj3;
                        if (QuestionHelper.ReConstructQuestion(sq, oq))
                        {
                            break;
                        }
                    }
                }
            }
            foreach (object obj4 in spaper.MatchQuestions)
            {
                MatchQuestion matchQuestion = (MatchQuestion)obj4;
                foreach (object obj5 in oPaper.MatchQuestions)
                {
                    MatchQuestion matchQuestion2 = (MatchQuestion)obj5;
                    if (matchQuestion.MID == matchQuestion2.MID)
                    {
                        matchQuestion.CourseId = matchQuestion2.CourseId;
                        matchQuestion.ColumnA = matchQuestion2.ColumnA;
                        matchQuestion.ColumnB = matchQuestion2.ColumnB;
                        matchQuestion.Solution = matchQuestion2.Solution;
                        break;
                    }
                }
            }
            foreach (object obj6 in spaper.GrammarQuestions)
            {
                Question sq = (Question)obj6;
                foreach (object obj7 in oPaper.GrammarQuestions)
                {
                    Question oq = (Question)obj7;
                    if (QuestionHelper.ReConstructQuestion(sq, oq))
                    {
                        break;
                    }
                }
            }
            foreach (object obj8 in spaper.IndicateMQuestions)
            {
                Question sq = (Question)obj8;
                foreach (object obj9 in oPaper.IndicateMQuestions)
                {
                    Question oq = (Question)obj9;
                    if (QuestionHelper.ReConstructQuestion(sq, oq))
                    {
                        break;
                    }
                }
            }
            foreach (object obj10 in spaper.FillBlankQuestions)
            {
                Question sq = (Question)obj10;
                foreach (object obj11 in oPaper.FillBlankQuestions)
                {
                    Question oq = (Question)obj11;
                    if (QuestionHelper.ReConstructQuestion(sq, oq))
                    {
                        break;
                    }
                }
            }
            if (oPaper.EssayQuestion != null)
            {
                QuestionHelper.ReConstructEssay(spaper.EssayQuestion, oPaper.EssayQuestion);
            }
            spaper.AudioData = oPaper.AudioData;
            return spaper;
        }

        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            string text;
            do
            {
                text = s;
                s = s.Replace("  ", " ");
            }
            while (s.Length != text.Length);
            return s;
        }

        public static string RemoveAllSpaces(string s)
        {
            s = s.Trim();
            string text;
            do
            {
                text = s;
                s = s.Replace(" ", "");
            }
            while (s.Length != text.Length);
            return s;
        }

        public static bool IsFillBlank(QuestionType qt)
        {
            return qt == QuestionType.FILL_BLANK_ALL || qt == QuestionType.FILL_BLANK_GROUP || qt == QuestionType.FILL_BLANK_EMPTY;
        }

        private static string RemoveNewLine(string s)
        {
            s = s.Replace(Environment.NewLine, "");
            s = QuestionHelper.RemoveSpaces(s);
            return s;
        }

        public static string WordWrap(string str, int width)
        {
            string pattern = QuestionHelper.fillBlank_Pattern;
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection matchCollection = regex.Matches(str);
            str = regex.Replace(str, "(###)");
            string[] array = QuestionHelper.SplitLines(str);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                string str2 = array[i];
                if (i < array.Length - 1)
                {
                    str2 = array[i] + Environment.NewLine;
                }
                ArrayList arrayList = QuestionHelper.Explode(str2, QuestionHelper.splitChars);
                int num = 0;
                for (int j = 0; j < arrayList.Count; j++)
                {
                    string text = (string)arrayList[j];
                    if (num + text.Length > width)
                    {
                        if (num > 0)
                        {
                            if (!stringBuilder.ToString().EndsWith(Environment.NewLine))
                            {
                                stringBuilder.Append(Environment.NewLine);
                            }
                            num = 0;
                        }
                        while (text.Length > width)
                        {
                            stringBuilder.Append(text.Substring(0, width - 1) + "-");
                            text = text.Substring(width - 1);
                            if (!stringBuilder.ToString().EndsWith(Environment.NewLine))
                            {
                                stringBuilder.Append(Environment.NewLine);
                            }
                            stringBuilder.Append(Environment.NewLine);
                        }
                        text = text.TrimStart(new char[0]);
                    }
                    stringBuilder.Append(text);
                    num += text.Length;
                }
            }
            str = stringBuilder.ToString();
            pattern = "\\(###\\)";
            regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            for (int j = 0; j < matchCollection.Count; j++)
            {
                string replacement = QuestionHelper.RemoveNewLine(matchCollection[j].Value);
                str = regex.Replace(str, replacement, 1);
            }
            return str;
        }

        private static ArrayList Explode(string str, char[] splitChars)
        {
            ArrayList arrayList = new ArrayList();
            int num = 0;
            for (; ; )
            {
                int num2 = str.IndexOfAny(splitChars, num);
                if (num2 == -1)
                {
                    break;
                }
                string text = str.Substring(num, num2 - num);
                char c = str.Substring(num2, 1)[0];
                if (char.IsWhiteSpace(c))
                {
                    arrayList.Add(text);
                    arrayList.Add(c.ToString());
                }
                else
                {
                    arrayList.Add(text + c);
                }
                num = num2 + 1;
            }
            arrayList.Add(str.Substring(num));
            return arrayList;
        }

        private static string[] SplitLines(string str)
        {
            string newLine = Environment.NewLine;
            Regex regex = new Regex(newLine, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return regex.Split(str);
        }

        public static string[] GetFillBlankWord(string text)
        {
            string pattern = QuestionHelper.fillBlank_Pattern;
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection matchCollection = regex.Matches(text);
            string[] array = new string[matchCollection.Count];
            for (int i = 0; i < matchCollection.Count; i++)
            {
                string text2 = matchCollection[i].Value.Remove(0, 1);
                text2 = text2.Remove(text2.Length - 1, 1);
                array[i] = text2;
            }
            return array;
        }

        public static string Sec2TimeString(int sec)
        {
            int num = sec / 3600;
            int num2 = sec % 3600 / 60;
            int num3 = sec % 60;
            string text = "0" + num;
            text = text.Substring(text.Length - 2, 2);
            string text2 = "0" + num2;
            text2 = text2.Substring(text2.Length - 2, 2);
            string text3 = "0" + num3;
            text3 = text3.Substring(text3.Length - 2, 2);
            return string.Concat(new string[]
            {
                text,
                ":",
                text2,
                ":",
                text3
            });
        }

        public static string[] MultipleChoiceQuestionType = new string[]
{
            "Grammar",
            "Indicate Mistake"
};

        public static string fillBlank_Pattern = "\\([0-9a-zA-Z;:=?<>/`,'’ .+_~!@#$%^&*\\r\\n-]+\\)";

        private static char[] splitChars = new char[]
{
            ' ',
            '-',
            '\t'
};

        public static int LineWidth = 100;
    }
}
