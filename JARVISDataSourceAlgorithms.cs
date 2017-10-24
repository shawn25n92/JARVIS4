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
    }
}
