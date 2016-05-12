using System;
using System.Security.Cryptography;
using System.Text;
using Presentation;

namespace Data.Validation
{
    public class PasswordCrypt : IPasswordCrypt
    {
        public string GetHashString(string password)
        {
            return GetSaltPwdHash(password);
        }

        public bool IsPasswordValid(string passwordInput, string pwdHashFromDb)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(GetHashString(passwordInput), pwdHashFromDb);
        }

        private string GetPwdHashOnce(string password)
        {
            var passwordHash = string.Empty;
            var bytes = Encoding.Default.GetBytes(password);
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            var byteHash = md5CryptoServiceProvider.ComputeHash(bytes);
            
            foreach (byte b in byteHash)
                passwordHash += string.Format("{0:x2}", b);  

            return passwordHash;
        }

        private string GetSaltPwdHash(string password)
        {
            var saltPwdHash = string.Empty;
            for (int i = 0; i < 9999; ++i)
                saltPwdHash = GetPwdHashOnce(password);

            return saltPwdHash;
        }
    }
}
