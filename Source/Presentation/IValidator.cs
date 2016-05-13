namespace Presentation
{
    /// <summary>
    /// Интерфейс валидатора, проверяющего данные.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Проверка корректности логина.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Ошибки, допущенные при задании логина, либо пустая строка, если все верно.</returns>
        string IsLoginValid(string login);

        /// <summary>
        /// Проверка корректности пароля.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Ошибки, допущенные при задании пароля, либо пустая строка, если все верно.</returns>
        string IsPasswordValid(string password);

        /// <summary>
        /// Проверка корректности имени.
        /// </summary>
        /// <param name="name">Имя</param>
        /// <returns>Ошибки, допущенные при задании имени, либо пустая строка, если все верно.</returns>
        string IsNameValid(string name);

        /// <summary>
        /// Проверка корректности отчества.
        /// </summary>
        /// <param name="surname">Отчество</param>
        /// <returns>Ошибки, допущенные при задании отчества, либо пустая строка, если все верно.</returns>
        string IsSurnameValid(string surname);

        /// <summary>
        /// Проверка корректности фамилии.
        /// </summary>
        /// <param name="lastname">Фамилия</param>
        /// <returns>Ошибки, допущенные при задании фамилии, либо пустая строка, если все верно.</returns>
        string IsLastnameValid(string lastname);

        /// <summary>
        /// Проверка корректности должности
        /// </summary>
        /// <param name="position">Должность</param>
        /// <returns>Ошибки, допущенные при задании должности, либо пустая строка, если все верно</returns>
        string IsPositionValid(string position);

        /// <summary>
        /// Проверка корректности инициалов.
        /// </summary>
        /// <param name="initials">Сгенерированые инициалы</param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Отчество</param>
        /// <returns>Ошибки, допущенные при автоматической генерации инициалов, либо пустая строка, если все верно.</returns>
        string AreInitialsValid(string initials, string name, string surname);
    }
}