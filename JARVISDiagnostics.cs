using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace JARVIS4
{
    public class JARVISDiagnostics
    {
        public JARVISDiagnostics()
        {

        }
        public bool StartProcess(string process_path)
        {
            try
            {
                Process.Start(process_path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
