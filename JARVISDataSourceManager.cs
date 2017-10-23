using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    public static class JARVISDataSourceManager
    {
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
        public static StatusObject LoadDataSourceConnectionInformation(string file_path)
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {

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
