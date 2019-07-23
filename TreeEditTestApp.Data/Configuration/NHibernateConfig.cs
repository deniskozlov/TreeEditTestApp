using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TreeEditTestApp.Data.Mapping;
using Unity;

namespace TreeEditTestApp.Data.Configuration
{
    public class NHibernateConfig
    {
        private static readonly string ConnectionStringName = "DefaultConnection";
        private static readonly string SystemConnectionStringName = "SystemConnection";

        public static void ConfigureDatabase(IUnityContainer container)
        {
            var needToCreate = CheckDbExistsFirst();

            ISessionFactory sessionFactory = default(ISessionFactory);
                var fluentConfiguration = Fluently
                    .Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromConnectionStringWithKey(ConnectionStringName)).ShowSql())
                    .Mappings(m => m.FluentMappings.Add<TreeItemMap>());
            if(needToCreate)
                fluentConfiguration = fluentConfiguration.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            sessionFactory = fluentConfiguration.BuildSessionFactory();

            container.RegisterInstance(typeof(ISessionFactory), sessionFactory);
        }

        private static bool CheckDbExistsFirst()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var systemConnectionString = ConfigurationManager.ConnectionStrings[SystemConnectionStringName].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            var databaseFileName = builder.AttachDBFilename;
            if (databaseFileName.Contains("|DataDirectory|"))
            {
                var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory");
                databaseFileName = databaseFileName.Replace("|DataDirectory|", dataDirectory.ToString());
            }
            if (!File.Exists(databaseFileName))
            {
                //Create database
                var query = $@"
                CREATE DATABASE {databaseName} ON PRIMARY (
                    NAME = '{databaseName}',
                    FILENAME = '{databaseFileName}');";

                using (var connection = new SqlConnection(systemConnectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }

            return false;
        }
    }
}