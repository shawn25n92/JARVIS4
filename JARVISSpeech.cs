using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Speech.Synthesis;


namespace JARVIS4
{
    public static class JARVISSpeech
    {
        public static bool OutputSpeech()
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
