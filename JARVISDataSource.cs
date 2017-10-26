using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    public class JARVISDataSource
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string AuthenticationType { get; set; }
        public bool TrustedConnection { get; set; }
        public JARVISDataSource(
            string Server, 
            string Database, 
            string UserID, 
            string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.AuthenticationType = "sqlauth";
        }
        public JARVISDataSource(
            string Server,
            string Database)
        {
            this.Server = Server;
            this.Database = Database;
            this.TrustedConnection = true;
            this.AuthenticationType = "winauth";
        }
        public JARVISDataSource(string ConnectionString)
        {
            List<string> ConnectionStringParameters = ConnectionString.Split(';').Select(x => x.Trim()).ToList();
            if(ConnectionStringParameters.Count == 2)
            {
                this.Server = ConnectionStringParameters[0];
                this.Database = ConnectionStringParameters[1];
                this.TrustedConnection = true;
                this.AuthenticationType = "winauth";
            }
            else if(ConnectionStringParameters.Count == 4)
            {
                this.Server = ConnectionStringParameters[0];
                this.Database = ConnectionStringParameters[1];
                this.UserID = ConnectionStringParameters[2];
                this.Password = ConnectionStringParameters[3];
                this.AuthenticationType = "sqlauth";
            }
        }
        public JARVISDataSource()
        {

        }
        public string GetConnectionString()
        {
            string connection_string = "";
            if (this.AuthenticationType == "sqlauth")
            {
                connection_string = String.Format(
                    "server={0};" +
                    "database={1};" +
                    "user id={2};" +
                    "password={3}",
                    this.Server,
                    this.Database,
                    this.UserID,
                    this.Password);
            }
            else if (this.AuthenticationType == "winauth")
            {
                connection_string = String.Format(
                    "server={0};" +
                    "database={1};" +
                    "trusted_connection=true;",
                    this.Server,
                    this.Database,
                    true);
            }
            return connection_string;
        }
        public SqlConnection CreateConnection()
        {
            SqlConnection new_connection = new SqlConnection();
            try
            {
                new_connection = new SqlConnection();
                new_connection.ConnectionString = this.GetConnectionString();
            }
            catch(Exception e)
            {
                new_connection = null;
            }
            return new_connection;
        }
    }
}
