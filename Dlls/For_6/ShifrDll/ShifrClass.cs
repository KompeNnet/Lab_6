using System;
using System.Text;
using ShifrInterface;
using System.IO;

namespace ShifrDll
{
    public class ShifrClass : IShifr
    {
        public void Base64Encrypt(string sInputFilename, string sOutputFilename)
        {
            sOutputFilename += ".Base64";
            string textFromFile;
            using (FileStream fstream = File.OpenRead(sInputFilename))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                textFromFile = Encoding.Default.GetString(array);
            }

            var s = Encoding.UTF8.GetBytes(textFromFile);
            string code = Convert.ToBase64String(s);
            byte[] ar = Encoding.Default.GetBytes(code);

            using (FileStream fstream = new FileStream(sOutputFilename, FileMode.OpenOrCreate))
            {
                fstream.Write(ar, 0, ar.Length);
            }
            File.Delete(sInputFilename);
        }

        public void Base64Decrypt(string sInputFilename, string sOutputFilename)
        {
            sOutputFilename += ".decr";
            string textFromFile;
            using (FileStream fstream = File.OpenRead(sInputFilename))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                textFromFile = Encoding.Default.GetString(array);
            }
            try
            {
                var enTextBytes = Convert.FromBase64String(textFromFile);
                string deText = Encoding.UTF8.GetString(enTextBytes);
                byte[] ar = Encoding.Default.GetBytes(deText);

                using (FileStream fstream = new FileStream(sOutputFilename, FileMode.OpenOrCreate))
                {
                    fstream.Write(ar, 0, ar.Length);
                }
            }
            catch { }
        }
    }
}
