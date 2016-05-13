using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Presentation;

namespace Data.Validation
{
    public class PasswordCrypt : IPasswordCrypt
    {
        public string GetHashString(string password)
        {
            return GetSaltPasswordHash(password);
        }

        public bool IsPasswordValid(string passwordInput, string pwdHashFromDb)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(GetHashString(passwordInput), pwdHashFromDb);
        }

        private string GetPasswordHashOnce(string password)
        {
            var passwordHash = string.Empty;
            var bytes = Encoding.Default.GetBytes(password);
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            var byteHash = md5CryptoServiceProvider.ComputeHash(bytes);
            
            foreach (byte b in byteHash)
                passwordHash += string.Format("{0:x2}", b);  

            return passwordHash;
        }

        private string GetSaltPasswordHash(string password)
        {
            var iterationNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordHashingTries"]);

            var saltPwdHash = password;
            for (int i = 0; i < iterationNumber; ++i)
                saltPwdHash = GetPasswordHashOnce(saltPwdHash);

            return saltPwdHash;
        }
    }
}
