using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOEssayQuestion : BOBase
    {
        public BOEssayQuestion(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public EssayQuestion Load(int eqid)
        {
            IList list = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from EssayQuestion q Where q.EQID=:eqid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("eqid", eqid);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return (list.Count > 0) ? ((EssayQuestion)list[0]) : null;
        }

        public IList LoadByCourse(string courseId)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from EssayQuestion q Where CourseId=:courseId";
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

        public IList LoadByChapter(int chapterId)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from EssayQuestion q Where ChapterId=:chapterId";
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

        public void Delete(int eqid)
        {
            this.session = this.sessionFactory.OpenSession();
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string text = "from EssayQuestion q Where q.EQID=" + eqid.ToString();
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

        public bool SaveList(IList list)
        {
            ISession session = this.sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            bool result;
            try
            {
                foreach (object obj in list)
                {
                    EssayQuestion essayQuestion = (EssayQuestion)obj;
                    session.Save(essayQuestion);
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

        public void DeleteQuestionInChapter(int chapterId)
        {
            this.session = this.sessionFactory.OpenSession();
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string text = "from EssayQuestion q Where q.ChapterId=" + chapterId.ToString();
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
