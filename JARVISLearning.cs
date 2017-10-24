using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace JARVIS4
{
    /// <summary>
    /// This class will handle all learning functions for JARVIS
    /// </summary>
    public static class JARVISLearning
    {
        private static string file_directory = "";
        public static bool log_command(string executable_root, string folder_name, string log_file_name, string log_message)
        {
            try
            {
                Directory.CreateDirectory(String.Format("{0}\\{1}", executable_root, folder_name));
                StreamWriter log_file = new StreamWriter(String.Format("{0}\\{1}\\{2}", executable_root, folder_name, log_file_name), append: true);
                log_file.WriteLine("{0}\t{1}", DateTime.Now, log_message);
                log_file.Close();
                return true;
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory(String.Format("{0}\\{1}", executable_root, folder_name));
                StreamWriter log_file = new StreamWriter(String.Format("{0}\\{1}\\{2}", executable_root, folder_name, log_file_name), append: true);
                log_file.WriteLine("{0}\t{1}", DateTime.Now, ex.Message);
                log_file.Close();
                return false;
            }
        }
    }
}
