using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    /// <summary>
    /// This class is purely for file processing algorithms. Functions here will be passed in as parameters to the filereader
    /// </summary>
    public static class JARVISFileProcessingAlgorithms
    {
        public static bool test_writeline(string output)
        {
            Console.WriteLine("hello {0}", output);
            return true;
        }
    }
}
