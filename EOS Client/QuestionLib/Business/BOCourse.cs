using System;
using System.Collections;
using NHibernate;

namespace QuestionLib.Business
{
    public class BOCourse : BOBase
    {
        public BOCourse(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IList LoadChapterByCourse(string courseId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Chapter ch Where CID=:courseId";
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
    }
}
