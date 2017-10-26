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
                " where routine_type = 'PROCEDURE'" +
                " and Left(Routine_Name, 3) NOT IN('sp_', 'xp_', 'ms_', 'dt_')" +
                " order by SPECIFIC_NAME asc", DatabaseConnection);
            
            try
            {
                string ssp_directory = String.Format(@"{0}\StoredProcedures\{1}", file_directory, DatabaseConnection.Database);
                Directory.CreateDirectory(ssp_directory);
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
                        StreamWriter stored_procedure_file = new StreamWriter(String.Format(@"{0}\{1}.txt", ssp_directory, stored_procedure));
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
        public static StatusObject TrackStoredProcedureChanges(SqlConnection DatabaseConnection)
        {
            StatusObject SO = new StatusObject();
            SqlCommand stored_procedure_list_query = new SqlCommand(
                @"select STOREDPROCNAME=SPECIFIC_NAME from information_schema.routines" +
                " where routine_type = 'PROCEDURE'" +
                " and Left(Routine_Name, 3) NOT IN('sp_', 'xp_', 'ms_', 'dt_')" +
                " order by SPECIFIC_NAME asc", DatabaseConnection);
            try
            {
                // Get a baseline
                // Get all stored procedure text at a certain point and store it in a dictionary/or to file? can probably run a comparison
                string ssp_directory = String.Format(@"{0}\StoredProcedureChanges\{1}", file_directory, DatabaseConnection.Database);
                Directory.CreateDirectory(ssp_directory);
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
                    SqlCommand stored_procedure_content = new SqlCommand(
                        String.Format(
                            "select OBJECT_DEFINITION(OBJECT_ID('{0}'))",
                            stored_procedure),
                        DatabaseConnection);
                    DatabaseConnection.Open();
                    SqlDataReader stored_procedure_content_reader = stored_procedure_content.ExecuteReader();
                    while (stored_procedure_content_reader.Read())
                    {
                        Console.WriteLine("BEGIN");
                        if (File.Exists(String.Format(@"{0}\{1}.txt", ssp_directory, stored_procedure)))
                        {
                            Console.WriteLine("Comparing Stored Procedure {0}", stored_procedure);
                            StreamReader stored_procedure_current_file = new StreamReader(String.Format(@"{0}\{1}.txt", ssp_directory, stored_procedure));
                            string stored_procedure_current_text = stored_procedure_current_file.ReadToEnd();
                            stored_procedure_current_file.Close();
                            if(stored_procedure_current_text != stored_procedure_content_reader[0].ToString())
                            {
                                // Copy the changed file
                                DateTime TimeStamp = DateTime.Now;
                                Console.WriteLine("Stored Procedure Changed");

                                StreamWriter stored_procedure_old_file = new StreamWriter(String.Format(@"{0}\{1}_{2}_OLD.txt", ssp_directory, stored_procedure, TimeStamp.ToString("yyyyMMdd_HHmmss")));
                                stored_procedure_old_file.Write(stored_procedure_current_text);
                                stored_procedure_old_file.Close();

                                StreamWriter stored_procedure_changed_file = new StreamWriter(String.Format(@"{0}\{1}_{2}_NEW.txt", ssp_directory, stored_procedure, TimeStamp.ToString("yyyyMMdd_HHmmss")));
                                stored_procedure_changed_file.Write(stored_procedure_content_reader[0]);
                                stored_procedure_changed_file.Close();
                                // Should also update the base file
                                Console.WriteLine("Updating Stored Procedure {0}", stored_procedure);
                                StreamWriter stored_procedure_file = new StreamWriter(String.Format(@"{0}\{1}.txt", ssp_directory, stored_procedure));
                                stored_procedure_file.Write(stored_procedure_content_reader[0]);
                                stored_procedure_file.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Copying Stored Procedure {0}", stored_procedure);
                            StreamWriter stored_procedure_file = new StreamWriter(String.Format(@"{0}\{1}.txt", ssp_directory, stored_procedure));
                            stored_procedure_file.Write(stored_procedure_content_reader[0]);
                            stored_procedure_file.Close();
                        }
                        Console.WriteLine("END");
                    }
                    DatabaseConnection.Close();
                }
                // 
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceAlgorithms", StatusObject.StatusCode.FAILURE, e.ToString());
                DatabaseConnection.Close();
            }
            return SO;
        }
    }
}
