namespace ShifrInterface
{
    public interface IShifr
    {
        void Base64Encrypt(string sInputFilename, string sOutputFilename);
        void Base64Decrypt(string sInputFilename, string sOutputFilename);
    }
}
