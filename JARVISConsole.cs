using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    public static class JARVISConsole
    {
        public static bool list_to_console(List<string> outputs)
        {
            try
            {
                foreach (string output in outputs)
                {
                    Console.WriteLine(output);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
