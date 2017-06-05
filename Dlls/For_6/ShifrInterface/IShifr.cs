using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShifrInterface
{
    public interface IShifr
    {
        void Base64Encrypt(string sInputFilename, string sOutputFilename);
        void Base64Decrypt(string sInputFilename, string sOutputFilename);
    }
}
