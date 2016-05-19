namespace Presentation.Validation
{
    /// <summary>
    /// Интерфейс шифрования пароля
    /// </summary>
    public interface IPasswordCrypt
    {
        /// <summary>
        /// Шифрование пароля посредством MD5.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>MD5-хэш, взятый от переданного пароля "PasswordHashingTries" (app.config) раз.</returns>
        string GetHashString(string password);

        /// <summary>
        /// Проверка соответвия введеного пароля и его зашифрованной формы из базы данных
        /// </summary>
        /// <param name="passwordInput">Пароль в нормальном виде</param>
        /// <param name="passwordHashFromDatabase">MD5-хэш, взятый от переданного пароля "PasswordHashingTries" (app.config) раз, который находится в БД.</param>
        /// <returns>True - если пароли совпали, False - если нет.</returns>
        bool IsPasswordValid(string passwordInput, string passwordHashFromDatabase);
    }
}