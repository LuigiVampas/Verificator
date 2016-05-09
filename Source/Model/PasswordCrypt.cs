using System;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public static class PasswordCrypt
    {
        public static string GetHashString(string password)
        {
            return GetSaltPwdHash(password);
        }

        public static bool IsPasswordValid(string passwordInput, string pwdHashFromDb)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(GetHashString(passwordInput), pwdHashFromDb);
        }

        private static string GetPwdHashOnce(string password)
        {
            var passwordHash = string.Empty;
            var bytes = Encoding.Default.GetBytes(password);
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            var byteHash = md5CryptoServiceProvider.ComputeHash(bytes);
            
            foreach (byte b in byteHash)
                passwordHash += string.Format("{0:x2}", b);  

            return passwordHash;
        }

        private static string GetSaltPwdHash(string password)
        {
            var saltPwdHash = string.Empty;
            for (int i = 0; i < 9999; ++i)
                saltPwdHash = GetPwdHashOnce(password);

            return saltPwdHash;
        }
    }
}
