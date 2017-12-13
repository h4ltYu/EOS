using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOTest : BOBase
    {
        public BOTest(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IList LoadTest(string courseId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Test t Where CourseId=:courseId";
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

        public Test LoadTestByTestId(string testId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList list;
            try
            {
                string text = "from Test t Where TestId=:testId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("testId", testId);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            Test result;
            if (list.Count > 0)
            {
                result = (Test)list[0];
            }
            else
            {
                result = null;
            }
            return result;
        }

        public IList LoadTestByCourse(string courseId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Test t Where CourseId=:courseId";
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

        public bool IsTestExists(string testId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList list;
            try
            {
                string text = "from Test t Where TestId=:testId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("testId", testId);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return list.Count != 0;
        }
    }
}
