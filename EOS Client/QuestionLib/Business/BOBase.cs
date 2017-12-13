using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOBase
    {
        public BOBase()
        {
        }

        public BOBase(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public object SaveOrUpdate(object obj)
        {
            this.session = this.sessionFactory.OpenSession();
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    this.session.SaveOrUpdate(obj);
                    this.session.Flush();
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
            return obj;
        }

        public object Save(object obj)
        {
            this.session = this.sessionFactory.OpenSession();
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    this.session.Save(obj);
                    this.session.Flush();
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
            return obj;
        }

        public object Save(object obj, ISession mySession)
        {
            try
            {
                mySession.Save(obj);
                mySession.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public object Update(object obj)
        {
            this.session = this.sessionFactory.OpenSession();
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    this.session.Update(obj);
                    this.session.Flush();
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
            return obj;
        }

        public object Update(object obj, ISession mySession)
        {
            try
            {
                mySession.Update(obj);
                mySession.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public void Load(object obj, object id)
        {
            this.session = this.sessionFactory.OpenSession();
            try
            {
                this.session.Load(obj, id);
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
        }

        public void Delete(object obj)
        {
            this.session = this.sessionFactory.OpenSession();
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    this.session.Delete(obj);
                    this.session.Flush();
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

        public IList List(string typeName)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                IQuery query = this.session.CreateQuery("from " + typeName);
                result = query.List();
                transaction.Commit();
                this.session.Close();
            }
            return result;
        }

        public IList ListID(string typeName, QuestionType qt, int chapterID)
        {
            IList result = null;
            string text;
            string text2;
            if (qt == QuestionType.READING)
            {
                text = "pid";
                text2 = "=0";
            }
            else if (qt == QuestionType.MULTIPLE_CHOICE)
            {
                text = "qid";
                text2 = "=1";
            }
            else if (qt == QuestionType.INDICATE_MISTAKE)
            {
                text = "qid";
                text2 = "=2";
            }
            else if (qt == QuestionType.MATCH)
            {
                text = "mid";
                text2 = "=3";
            }
            else
            {
                text = "qid";
                text2 = ">3";
            }
            string text3 = string.Concat(new object[]
            {
                "SELECT ",
                text,
                " FROM ",
                typeName,
                " WHERE chapterId=",
                chapterID,
                " AND qType=",
                text2
            });
            SqlConnection sqlConnection = (SqlConnection)this.sessionFactory.ConnectionProvider.GetConnection();
            return result;
        }

        public IList ListID(string typeName, QuestionType qt, string courseID)
        {
            return null;
        }

        protected ISessionFactory sessionFactory;

        protected ISession session;
    }
}
