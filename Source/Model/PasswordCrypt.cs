using System;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class PasswordCrypt
    {
        public string GetHashString(string password)
        {
            return GetSaltPwdHash(password);
        }

        public bool IsPasswordValid(string passwordInput, string pwdHashFromDb)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(GetHashString(passwordInput), pwdHashFromDb);
        }

        private string GetPwdHashOnce(string password)
        {
            string pwdHash = string.Empty;
            byte[] bytes = Encoding.Default.GetBytes(password);
            MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();

            byte[] byteHash = csp.ComputeHash(bytes);
            
            foreach (byte b in byteHash)
                pwdHash += string.Format("{0:x2}", b);  

            return pwdHash;
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
