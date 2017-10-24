using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JARVIS4
{
    /// <summary>
    /// This class handles all data source management, i.e. adding of data sources, testing connections, removing of data sources
    /// </summary>
    public static class JARVISDataSourceManager
    {
        /// <summary>
        /// Private Declaration of variables that will be shared throughout
        /// </summary>
        private static string file_directory = @"C:\JARVIS4\UserDefinedDataSources";
        public static StatusObject AddDataSource(string Server, string Database, string UserID, string Password)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection connection = new SqlConnection(String.Format("server={0};database={1};user id={2};password={3}", Server, Database, UserID, Password));
                // Test the database connection
                connection.Open();
                connection.Close();
                // If the connection succeeds then save it to a textfile somewhere and return a new JARVISDataSource
                Directory.CreateDirectory(file_directory);
                StreamWriter datasource_file = new StreamWriter(String.Format(@"{0}\SqlAuthDataSources.txt", file_directory), append: true);
                datasource_file.WriteLine(String.Format("{0},{1},{2},{3}", Server, Database, UserID, Password));
                datasource_file.Close();
                JARVISDataSource new_data_source = new JARVISDataSource(Server, Database, UserID, Password);
                SO.UDDynamic = new_data_source;
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager_AddDataSource", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
        public static StatusObject AddDataSource(string Server, string Database)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection connection = new SqlConnection(String.Format("server={0};database={1};trusted_connection=true", Server, Database));
                // Test the database connection
                connection.Open();
                connection.Close();
                // If the connection succeeds then save it to a textfile somewhere and return a new JARVISDataSource
                Directory.CreateDirectory(file_directory);
                StreamWriter datasource_file = new StreamWriter(String.Format(@"{0}\WinAuthDataSources.txt", file_directory), append: true);
                datasource_file.WriteLine(String.Format("{0},{1},{2}", Server, Database, true));
                datasource_file.Close();
                JARVISDataSource new_data_source = new JARVISDataSource(Server, Database);
                SO.UDDynamic = new_data_source;
            }
            catch (Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager_AddDataSource", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
        public static StatusObject ConnectToDataSource()
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Server=sql2008kl;Database=claims_dev;user id=sa;password=password";
                connection.Open();
                connection.Close();
            }
            catch (Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager_01", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
        public static StatusObject ConnectToDataSource(string server, string database, string user_id, string password)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = String.Format("Server={0};Database={1};user id={2};password={3}", server, database, user_id, password);
                connection.Open();
                
                connection.Close();
                
            }
            catch (Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager_01", StatusObject.StatusCode.FAILURE, e.ToString());
                
            }
            return SO;
        }
        /// <summary>
        /// Load all data sources
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public static StatusObject LoadDataSourceConnectionInformation(out List<JARVISDataSource> DataSourceList)
        {
            StatusObject SO = new StatusObject();
            try
            {

                DataSourceList = new List<JARVISDataSource>();
            }
            catch(Exception e)
            {
                DataSourceList = new List<JARVISDataSource>();
            }
            return SO;
        }
        /// <summary>
        /// This function will build a map of any datasource, create a list of indexed sql query strings
        /// </summary>
        /// <returns></returns>
        public static StatusObject BuildDataSourceMapping()
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
    }
}
