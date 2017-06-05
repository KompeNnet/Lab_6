using System;
using ShifrDll;
using System.IO;
using System.Text;

namespace Lab_4.Helpers.Formatters
{
    public class Adapter : ShifrClass, IFormatter
    {
        private static string formatterRules = "";

        public string Format(string input)
        {
            byte[] ar = Encoding.Default.GetBytes(input);
            using (FileStream fs = new FileStream("temp.txt", FileMode.OpenOrCreate))
            {
                fs.Write(ar, 0, ar.Length);
            }

            Base64Encrypt("temp.txt", "temp");

            string result = "";
            using (FileStream fstream = File.OpenRead("temp.Base64"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                result = Encoding.Default.GetString(array);
            }
            return result;
        }

        public string ReFormat(string input)
        {
            byte[] ar = Encoding.Default.GetBytes(input);
            using (FileStream fs = new FileStream("temp.Base64", FileMode.OpenOrCreate))
            {
                fs.Write(ar, 0, ar.Length);
            }

            Base64Encrypt("temp.Base64", "temp");

            string result = "";
            using (FileStream fstream = File.OpenRead("temp.decr"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                result = Encoding.Default.GetString(array);
            }
            return result;
        }

        public string GetRules()
        {
            return formatterRules;
        }

        public void SetRules(string rules)
        {
            formatterRules = rules;
        }

        public bool IsCompatible(string extension)
        {
            return true;
        }
    }
}
