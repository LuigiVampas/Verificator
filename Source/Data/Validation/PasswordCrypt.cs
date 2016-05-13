using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Presentation;

namespace Data.Validation
{
    /// <summary>
    /// Класс шифрования пароля.
    /// </summary>
    public class PasswordCrypt : IPasswordCrypt
    {
        /// <summary>
        /// Шифрование пароля посредством MD5.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>MD5-хэш, взятый от переданного пароля "PasswordHashingTries" (app.config) раз.</returns>
        public string GetHashString(string password)
        {
            return GetSaltPasswordHash(password);
        }

        /// <summary>
        /// Проверка соответвия введеного пароля и его зашифрованной формы из базы данных
        /// </summary>
        /// <param name="passwordInput">Пароль в нормальном виде</param>
        /// <param name="passwordHashFromDatabase">MD5-хэш, взятый от переданного пароля "PasswordHashingTries" (app.config) раз, который находится в БД.</param>
        /// <returns>True - если пароли совпали, False - если нет.</returns>
        public bool IsPasswordValid(string passwordInput, string passwordHashFromDatabase)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(GetHashString(passwordInput), passwordHashFromDatabase);
        }

        /// <summary>
        /// Взятие MD5-хеша один раз.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>MD5-хеш.</returns>
        private static string GetPasswordHashOnce(string password)
        {
            var passwordHash = string.Empty;
            var bytes = Encoding.Default.GetBytes(password);
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            var byteHash = md5CryptoServiceProvider.ComputeHash(bytes);

            string result = passwordHash;
            foreach (var b in byteHash)
                result = result + string.Format("{0:x2}", b);
            return result;
        }

        /// <summary>
        /// Организация подсоленности пароля (Взятие MD5-хеша "PasswordHashingTries" (app.config) раз)
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Подсоленный MD5-хеш пароля.</returns>
        private static string GetSaltPasswordHash(string password)
        {
            var iterationNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordHashingTries"]);

            var saltPasswordHash = password;
            for (int i = 0; i < iterationNumber; ++i)
                saltPasswordHash = GetPasswordHashOnce(saltPasswordHash);

            return saltPasswordHash;
        }
    }
}
