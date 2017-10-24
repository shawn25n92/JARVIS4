using System;
using System.Collections.Generic;
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
                    "trusted_connection={2};" +
                    this.Server,
                    this.Database,
                    true);
            }
            return connection_string;
        }
    }
}
