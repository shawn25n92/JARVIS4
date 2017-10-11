using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    /// <summary>
    /// This class will handle all learning functions for JARVIS
    /// </summary>
    public static class JARVISLearning
    {
        public static bool log_command()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
