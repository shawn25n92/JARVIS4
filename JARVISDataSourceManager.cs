using System;
using System.Collections.Generic;
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

            }
            catch (Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISDataSourceManager_01", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
    }
}
