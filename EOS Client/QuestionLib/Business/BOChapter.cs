using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOChapter : BOBase
    {
        public BOChapter(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IList LoadFillBlankQuestionByChapter(int chapterId)
        {
            QuestionType questionType = QuestionType.FILL_BLANK_ALL;
            QuestionType questionType2 = QuestionType.FILL_BLANK_GROUP;
            QuestionType questionType3 = QuestionType.FILL_BLANK_EMPTY;
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Question q Where (q.QType=:type1 OR q.QType=:type2 OR q.QType=:type3)  AND ChapterId=:chapterId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("type1", questionType);
                query.SetParameter("type2", questionType2);
                query.SetParameter("type3", questionType3);
                query.SetParameter("chapterId", chapterId.ToString());
                result = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return result;
        }

        public IList LoadQuestionByChapter(QuestionType qt, int chapterId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Question q Where q.QType=:type and ChapterId=:chapterId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("type", qt);
                query.SetParameter("chapterId", chapterId.ToString());
                result = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return result;
        }

        public IList LoadPassageByChapter(int chapterId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Passage p Where ChapterId=:chapterId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("chapterId", chapterId);
                result = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return result;
        }

        public IList LoadMatchQuestionByChapter(int chapterId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from MatchQuestion m Where ChapterId=:chapterId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("chapterId", chapterId);
                result = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return result;
        }
    }
}
