using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
    public class BOAudio : BOBase
    {
        public BOAudio(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Audio Load(int auid)
        {
            IList list = null;
            this.session = this.sessionFactory.OpenSession();
            try
            {
                string text = "from Audio a Where a.AuID=:auid";
                IQuery query = this.session.CreateQuery(text);
                query.SetParameter("auid", auid);
                list = query.List();
                this.session.Close();
            }
            catch (Exception ex)
            {
                this.session.Close();
                throw ex;
            }
            return (list.Count > 0) ? ((Audio)list[0]) : null;
        }

        public Audio LoadChapterAudio(int chid, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            string cmdText = "SELECT ChID,AudioFile,AudioSize,AudioData,AudioLength FROM Audio WHERE ChID= " + chid;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            Audio audio = null;
            if (sqlDataReader.Read())
            {
                audio = new Audio();
                audio.ChID = chid;
                audio.AudioFile = sqlDataReader["AudioFile"].ToString();
                audio.AudioSize = (int)sqlDataReader["AudioSize"];
                audio.AudioData = new byte[audio.AudioSize];
                sqlDataReader.GetBytes(3, 0L, audio.AudioData, 0, audio.AudioSize);
                audio.AudioLength = (int)sqlDataReader["AudioLength"];
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return audio;
        }

        public static bool Delete(int chapterID, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            string cmdText = "DELETE FROM Audio WHERE ChID= " + chapterID;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            int num = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return num > 0;
        }

        public static bool AudioExist(int chapterID, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            string cmdText = "SELECT * FROM Audio WHERE ChID= " + chapterID;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool result;
            if (sqlDataReader.HasRows)
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                result = true;
            }
            else
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                result = false;
            }
            return result;
        }

        public static string GetAudioFile(int chapterID, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            string cmdText = "SELECT AudioFile FROM Audio WHERE ChID= " + chapterID;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            string result;
            if (sqlDataReader.Read())
            {
                string text = sqlDataReader["AudioFile"].ToString();
                sqlDataReader.Close();
                sqlConnection.Close();
                result = text;
            }
            else
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                result = null;
            }
            return result;
        }

        public static int GetAudioLength(int chapterID, string conStr)
        {
            SqlConnection sqlConnection = new SqlConnection(conStr);
            sqlConnection.Open();
            string cmdText = "SELECT AudioLength FROM Audio WHERE ChID= " + chapterID;
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            int result;
            if (sqlDataReader.Read())
            {
                int @int = sqlDataReader.GetInt32(0);
                sqlDataReader.Close();
                sqlConnection.Close();
                result = @int;
            }
            else
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                result = 0;
            }
            return result;
        }
    }
}
