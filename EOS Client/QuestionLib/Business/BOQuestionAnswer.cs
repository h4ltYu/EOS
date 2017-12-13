using System;
using System.Collections;
using NHibernate;

namespace QuestionLib.Business
{
    public class BOQuestionAnswer : BOBase
    {
        public BOQuestionAnswer(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IList LoadAnswer(int qid)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from QuestionAnswer qa Where qa.QID=:qid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("qid", qid);
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
