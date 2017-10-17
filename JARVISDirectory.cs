using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace JARVIS4
{
    public static class JARVISDirectory
    {
        public static bool directory_create(string file_path)
        {
            try
            {
                Directory.CreateDirectory(file_path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
