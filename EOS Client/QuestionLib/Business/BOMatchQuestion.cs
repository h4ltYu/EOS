using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOMatchQuestion : BOBase
    {
        public BOMatchQuestion(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public MatchQuestion LoadMatch(int mid)
        {
            this.session = this.sessionFactory.OpenSession();
            IList list;
            try
            {
                string text = "from MatchQuestion mq Where  mq.MID=:mid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("mid", mid);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return (list.Count > 0) ? ((MatchQuestion)list[0]) : null;
        }

        public IList LoadMatchOfCourse(string courseId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from MatchQuestion mq Where  mq.CourseId=:courseId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("courseId", courseId);
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

        public bool SaveList(IList list)
        {
            ISession session = this.sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            bool result;
            try
            {
                foreach (object obj in list)
                {
                    MatchQuestion matchQuestion = (MatchQuestion)obj;
                    session.Save(matchQuestion);
                }
                transaction.Commit();
                result = true;
            }
            catch
            {
                transaction.Rollback();
                result = false;
            }
            return result;
        }

        public void Delete(int chid)
        {
            this.session = this.sessionFactory.OpenSession();
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string text = "from MatchQuestion mq Where ChapterId=" + chid.ToString();
                this.session.Delete(text);
                transaction.Commit();
                this.session.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                this.session.Close();
                throw ex;
            }
        }
    }
}
