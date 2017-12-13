using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace QuestionLib
{
    public class NHHelper
    {
        public void Configure()
        {
            Configuration configuration = new Configuration().Configure();
            configuration.AddAssembly("QuestionLib");
            configuration.Properties["hibernate.connection.connection_string"] = NHHelper.ConnectionString;
            configuration.Properties["connection.connection_string"] = NHHelper.ConnectionString;
            this.SessionFactory = configuration.BuildSessionFactory();
        }

        public void ExportTables()
        {
            Configuration configuration = new Configuration().Configure();
            configuration.AddAssembly("QuestionLib");
            new SchemaExport(configuration).Create(true, true);
        }

        public static ISessionFactory GetSessionFactory()
        {
            NHHelper nhhelper = new NHHelper();
            nhhelper.Configure();
            return nhhelper.SessionFactory;
        }

        public static string ConnectionString = "";

        public ISessionFactory SessionFactory;
    }
}
