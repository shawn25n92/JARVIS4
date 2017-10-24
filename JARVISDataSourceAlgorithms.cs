using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace JARVIS4
{
    public static class JARVISDataSourceAlgorithms
    {
        public static StatusObject SearchTableForValue(JARVISDataSource datasource, string value)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection sql_connection = new SqlConnection(datasource.GetConnectionString());
                sql_connection.Open();
                sql_connection.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceAlgorithms", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
        public static StatusObject ExecuteSQL(SqlConnection DatabaseConnection, string SQLCommandString)
        {
            StatusObject SO = new StatusObject();
            SqlCommand new_command = new SqlCommand(SQLCommandString, DatabaseConnection);
            DatabaseConnection.Open();
            SqlDataReader reader = new_command.ExecuteReader();
            try
            {
                
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1}", reader[0], reader[1]);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DatabaseConnection.Close();
            return SO;
        }
    }
}
