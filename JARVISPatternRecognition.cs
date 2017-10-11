using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    /// <summary>
    /// This class will handle all forms of pattern recognition
    /// </summary>
    public class JARVISPatternRecognition
    {
        public JARVISPatternRecognition()
        {

        }
        public List<string> ListCommonPatterns(string input_1, string input_2)
        {
            List<string> returned_list = new List<string>();
            try
            {
                if (input_1.Length >= input_2.Length) // Always compare the shorter string against the longer string
                {
                    char[] common_substring = new char[input_1.Length];
                    for (int i = 0; i < input_1.Length; i++)
                    {
                        
                    }
                }
                else if(input_1.Length < input_2.Length)
                {

                }
            }
            catch (Exception ex)
            {
                returned_list.Add(ex.ToString());
            }
            return returned_list;
        }
    }
}
