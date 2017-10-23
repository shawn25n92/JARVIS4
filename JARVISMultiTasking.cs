using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    public static class JARVISMultiTasking
    {
        public static void ExecuteFunction(Func<string, StatusObject> function_prototype, string function_arguments)
        {
            try
            {
                function_prototype.Invoke(function_arguments);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static StatusObject AddThread(string thread_name)
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
    }
}
