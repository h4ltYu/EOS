using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOQuestion : BOBase
    {
        public BOQuestion(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IList LoadPassageQuestion(int pid)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Question q Where q.PID=:pid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("pid", pid);
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

        public Question Load(int qid)
        {
            IList list = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Question q Where q.QID=:qid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("qid", qid);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return (list.Count > 0) ? ((Question)list[0]) : null;
        }

        public IList LoadByType(QuestionType type)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Question q Where q.QType=:type";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("type", type);
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

        public IList LoadByTypeOfCourse(QuestionType type, string courseId)
        {
            IList result = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Question q Where q.QType=:type and CourseId=:courseId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("type", type);
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

        public IList LoadFillBlankByTypeOfCourse(string courseId)
        {
            IList result = null;
            QuestionType questionType = QuestionType.FILL_BLANK_ALL;
            QuestionType questionType2 = QuestionType.FILL_BLANK_EMPTY;
            QuestionType questionType3 = QuestionType.FILL_BLANK_GROUP;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Question q Where (q.QType=:type1 OR q.QType=:type2 OR q.QType=:type3) and CourseId=:courseId";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("type1", questionType);
                query.SetParameter("type2", questionType2);
                query.SetParameter("type3", questionType3);
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

        public void Delete(int qid)
        {
            this.session = this.sessionFactory.OpenSession();
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string text = "from Question q Where q.QID=" + qid.ToString();
                this.session.Delete(text);
                text = "from QuestionAnswer qa Where qa.QID=" + qid.ToString();
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
                    Question question = (Question)obj;
                    session.Save(question);
                    foreach (object obj2 in question.QuestionAnswers)
                    {
                        QuestionAnswer questionAnswer = (QuestionAnswer)obj2;
                        questionAnswer.QID = question.QID;
                        session.Save(questionAnswer);
                    }
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

        public bool Delete(int chapterID, QuestionType qt, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            string cmdText = string.Concat(new object[]
            {
                "DELETE FROM QuestionAnswer WHERE qid in (SELECT qid FROM Question WHERE QType=",
                (int)qt,
                " AND chapterId=",
                chapterID,
                ")"
            });
            string cmdText2 = string.Concat(new object[]
            {
                "DELETE FROM Question WHERE QType=",
                (int)qt,
                " AND chapterID=",
                chapterID
            });
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.Transaction = sqlTransaction;
            SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection);
            sqlCommand2.Transaction = sqlTransaction;
            bool result;
            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();
                sqlTransaction.Commit();
                sqlConnection.Close();
                result = true;
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                sqlConnection.Close();
                throw ex;
            }
            return result;
        }
    }
}
