using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace JARVIS4
{
    public static class JARVISDataSourceAlgorithms
    {
        private static string file_directory = @"C:\JARVIS4\UserDefinedDataSources";
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
            
            try
            {
                DatabaseConnection.Open();
                SqlDataReader reader = new_command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Available Columns: {0}", reader.FieldCount);
                }
                DatabaseConnection.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject("", "", StatusObject.StatusCode.FAILURE, e.ToString());
                DatabaseConnection.Close();
            }
            
            return SO;
        }
        public static StatusObject ExecuteSQL(SqlConnection DatabaseConnection, string SQLCommandString, Func<StatusObject>[] Processes)
        {
            StatusObject SO = new StatusObject();
            SqlCommand new_command = new SqlCommand(SQLCommandString, DatabaseConnection);
            try
            {
                DatabaseConnection.Open();
                SqlDataReader reader = new_command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1}", reader[0].GetType(), reader[1].GetType());
                }
                DatabaseConnection.Close();
            }
            catch (Exception e)
            {
                SO = new StatusObject("", "", StatusObject.StatusCode.FAILURE, e.ToString());
                DatabaseConnection.Close();
            }
            
            return SO;
        }
        public static StatusObject StoredProceduresToFile(SqlConnection DatabaseConnection)
        {
            StatusObject SO = new StatusObject();
            SqlCommand stored_procedure_list_query = new SqlCommand(
                @"select STOREDPROCNAME=SPECIFIC_NAME from information_schema.routines" +
                " where routine_type = 'PROCEDURE'"+
                " and Left(Routine_Name, 3) NOT IN('sp_', 'xp_', 'ms_', 'dt_')", DatabaseConnection);
            
            try
            {
                Directory.CreateDirectory(file_directory);
                DatabaseConnection.Open();
                SqlDataReader reader = stored_procedure_list_query.ExecuteReader();
                List<string> stored_procedure_list = new List<string>();
                while (reader.Read())
                {
                    stored_procedure_list.Add(reader[0].ToString());
                    string stored_procedure_name = reader[0].ToString();
                    
                }
                DatabaseConnection.Close();
                foreach (string stored_procedure in stored_procedure_list)
                {
                    Console.WriteLine("Copying Stored Procedure {0}", stored_procedure);
                    SqlCommand stored_procedure_content = new SqlCommand(
                        String.Format(
                            "select OBJECT_DEFINITION(OBJECT_ID('{0}'))",
                            stored_procedure),
                        DatabaseConnection);
                    DatabaseConnection.Open();
                    SqlDataReader stored_procedure_content_reader = stored_procedure_content.ExecuteReader();
                    while (stored_procedure_content_reader.Read())
                    {
                        StreamWriter stored_procedure_file = new StreamWriter(String.Format(@"{0}\{1}.txt", file_directory, stored_procedure));
                        stored_procedure_file.Write(stored_procedure_content_reader[0]);
                        stored_procedure_file.Close();
                    }
                    DatabaseConnection.Close();
                }
                // Get List of Stored Procedures
            }
            catch(Exception e)
            {
                SO = new StatusObject("", "", StatusObject.StatusCode.FAILURE, e.ToString());
                DatabaseConnection.Close();
            }
            return SO;
        }
    }
}
