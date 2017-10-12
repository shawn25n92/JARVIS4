using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace JARVIS4
{
    public static class JARVISDiagnostics
    {
        public static bool StartProcess(string process_path)
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
        public static List<string> list_JARVIS_types()
        {
            List<string> JARVIS_types = new List<string>();
            try
            {
                Assembly JARVIS_assembly = Assembly.GetExecutingAssembly();
                Type[] JARVIS_type_array = JARVIS_assembly.GetTypes();
                foreach(Type JARVIS_type in JARVIS_type_array)
                {
                    JARVIS_types.Add(JARVIS_type.FullName);
                }
            }
            catch(Exception ex)
            {
                JARVIS_types.Add(ex.ToString());
            }
            return JARVIS_types;
        }
        public static List<string> list_JARVIS_type_properties(string JARVIS_type_full_name)
        {
            List<string> JARVIS_type_properties = new List<string>();
            try
            {
                Type JARVIS_type = Type.GetType(JARVIS_type_full_name);
                if(JARVIS_type != null)
                {
                    FieldInfo[] JARVIS_type_field_array = JARVIS_type.GetFields();
                    PropertyInfo[] JARVIS_type_property_array = JARVIS_type.GetProperties();
                    MethodInfo[] JARVIS_type_method_array = JARVIS_type.GetMethods();
                    JARVIS_type_properties.Add(String.Format("Field Information for {0}", JARVIS_type_full_name));
                    foreach(FieldInfo JARVIS_type_field in JARVIS_type_field_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} {1}", JARVIS_type_field.Name, JARVIS_type_field.FieldType));
                    }
                    JARVIS_type_properties.Add(String.Format("Property Information for {0}", JARVIS_type_full_name));
                    foreach(PropertyInfo JARVIS_type_property in JARVIS_type_property_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} {1}", JARVIS_type_property.Name, JARVIS_type_property.PropertyType));
                    }
                    JARVIS_type_properties.Add(String.Format("Method Information for {0}", JARVIS_type_full_name));
                    foreach(MethodInfo JARVIS_type_method in JARVIS_type_method_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} returns {1}", JARVIS_type_method.Name, JARVIS_type_method.ReturnType));
                    }
                }
                else
                {
                    JARVIS_type_properties.Add(String.Format("Unable to find JARVIS type {0}", JARVIS_type_full_name));
                }
            }
            catch(Exception ex)
            {

            }
            return JARVIS_type_properties;
        }
        public static bool run_JARVIS_function(string type_name, string function_name, string function_parameters)
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
        public static string get_JARVIS_executable_path()
        {
            string JARVIS_executable_path = "";
            try
            {
                Assembly JARVIS_assembly = Assembly.GetExecutingAssembly();
                JARVIS_executable_path = System.IO.Path.GetDirectoryName(JARVIS_assembly.Location);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JARVIS_executable_path;
        }
    }
}
