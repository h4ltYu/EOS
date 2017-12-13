using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOPassage : BOBase
    {
        public BOPassage(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Passage LoadPassage(int pid)
        {
            this.session = this.sessionFactory.OpenSession();
            IList list;
            try
            {
                string text = "from Passage p Where p.PID=:pid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("pid", pid);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return (list.Count > 0) ? ((Passage)list[0]) : null;
        }

        public IList LoadPassageByCourse(string courseId)
        {
            this.session = this.sessionFactory.OpenSession();
            IList result;
            try
            {
                string text = "from Passage p Where CourseId=:courseId";
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

        public void Delete(int pid, int[] qid_list)
        {
            this.session = this.sessionFactory.OpenSession();
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string text = "from Passage p Where p.PID=" + pid.ToString();
                this.session.Delete(text);
                text = "from Question q Where q.PID=" + pid.ToString();
                this.session.Delete(text);
                foreach (int num in qid_list)
                {
                    text = "from QuestionAnswer qa Where qa.QID=" + num.ToString();
                    this.session.Delete(text);
                }
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

        public bool Delete(int chapterID, string conStr)
        {
            QuestionType questionType = QuestionType.READING;
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            string cmdText = string.Concat(new object[]
            {
                "DELETE FROM QuestionAnswer WHERE qid IN (SELECT qid FROM Question WHERE QType=",
                (int)questionType,
                " AND chapterId=",
                chapterID,
                ")"
            });
            string cmdText2 = string.Concat(new object[]
            {
                "DELETE FROM Question WHERE  QType=",
                (int)questionType,
                " AND chapterID=",
                chapterID
            });
            string cmdText3 = "DELETE FROM Passage WHERE chapterID=" + chapterID;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.Transaction = sqlTransaction;
            SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection);
            sqlCommand2.Transaction = sqlTransaction;
            SqlCommand sqlCommand3 = new SqlCommand(cmdText3, sqlConnection);
            sqlCommand3.Transaction = sqlTransaction;
            bool result;
            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();
                sqlCommand3.ExecuteNonQuery();
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

        public bool SaveList(IList list)
        {
            ISession session = this.sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            bool result;
            try
            {
                foreach (object obj in list)
                {
                    Passage passage = (Passage)obj;
                    session.Save(passage);
                    foreach (object obj2 in passage.PassageQuestions)
                    {
                        Question question = (Question)obj2;
                        question.PID = passage.PID;
                        session.Save(question);
                        foreach (object obj3 in question.QuestionAnswers)
                        {
                            QuestionAnswer questionAnswer = (QuestionAnswer)obj3;
                            questionAnswer.QID = question.QID;
                            session.Save(questionAnswer);
                        }
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
    }
}
