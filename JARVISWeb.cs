using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace JARVIS4
{
    public static class JARVISWeb
    {
        public static bool create_HTTP_request()
        {
            return true;
        }
        public static StatusObject RecordHTTPRequest()
        {
            StatusObject SO = new StatusObject();
            try
            {
                HttpListener Listener = new HttpListener();
                Listener.Prefixes.Add("http://localhost/claims/");
                Console.WriteLine("Listening");
                Listener.Start();
                var Context = Listener.GetContext();
                var Response = Context.Response;

            }
            catch (Exception e)
            {

            }
            return SO;
        }
    }
}
