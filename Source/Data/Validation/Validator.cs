using System.Linq;
using System.Text;
using Presentation;

namespace Data.Validation
{
    /// <summary>
    /// Класс валидации данных.
    /// </summary>
    public class Validator : IValidator
    {
        private readonly IUserRepository _userRepository;

        public Validator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Проверка корректности логина.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Ошибки, допущенные при задании логина, либо пустая строка, если все верно.</returns>
        public string IsLoginValid(string login)
        {
            var error = new StringBuilder();
            error.Append(ContainsEnglish(login));
            error.Append(CheckLength(login, 15));
            error.Append(IsLoginUnique(login));
            return error.ToString();
        }
        
        /// <summary>
        /// Проверка логина на уникальность в базе данных.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Пустую строку, если логин уникален. "Данный логин уже используется другим пользователем." - если такой логин уже существует.</returns>
        private string IsLoginUnique(string login)
        {
            return _userRepository.GetAllUsers().All(u => u.Login != login) ? "" : "Данный логин уже используется другим пользователем.";
        }

        /// <summary>
        /// Проверка корректности пароля.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Ошибки, допущенные при задании пароля, либо пустая строка, если все верно.</returns>
        public string IsPasswordValid(string password)
        {
            var error = new StringBuilder();
            error.Append(CheckLength(password, 32));
            error.Append(IsPassword(password));
            var passwordStrength = CheckPasswordStrength(password);
            if (passwordStrength == PasswordStrength.Weak ||
                passwordStrength == PasswordStrength.PasswordNotSet)
                error.Append("Password is too weak, or not set");

            return error.ToString();
        }

        /// <summary>
        /// Проверка корректности имени.
        /// </summary>
        /// <param name="name">Имя</param>
        /// <returns>Ошибки, допущенные при задании имени, либо пустая строка, если все верно.</returns>
        public string IsNameValid(string name)
        {
            return IsDataValid(name);
        }

        /// <summary>
        /// Проверка корректности отчества.
        /// </summary>
        /// <param name="surname">Отчество</param>
        /// <returns>Ошибки, допущенные при задании отчества, либо пустая строка, если все верно.</returns>
        public string IsSurnameValid(string surname)
        {
            return IsDataValid(surname);
        }

        /// <summary>
        /// Проверка корректности фамилии.
        /// </summary>
        /// <param name="lastname">Фамилия</param>
        /// <returns>Ошибки, допущенные при задании фамилии, либо пустая строка, если все верно.</returns>
        public string IsLastnameValid(string lastname)
        {
            return IsDataValid(lastname);
        }

        /// <summary>
        /// Проверка корректности должности
        /// </summary>
        /// <param name="position">Должность</param>
        /// <returns>Ошибки, допущенные при задании должности, либо пустая строка, если все верно.</returns>
        public string IsPositionValid(string position)
        {
            return IsDataValid(position);
        }

        /// <summary>
        /// Проверка корректности инициалов.
        /// </summary>
        /// <param name="initials">Сгенерированые инициалы</param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Отчество</param>
        /// <returns>Ошибки, допущенные при автоматической генерации инициалов, либо пустая строка, если все верно.</returns>
        public string AreInitialsValid(string initials, string name, string surname)
        {
            return initials != GetInitialsFromNameAndSurname(name, surname) ? "Initials are not correct" : "";
        }

        /// <summary>
        /// Автоматическая генерация инициалов на основе полученного имени и отчества.
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Отчество</param>
        /// <returns>Инициалы.</returns>
        private static string GetInitialsFromNameAndSurname(string name, string surname)
        {
            return name.First() + "." + surname.First();
        }

        /// <summary>
        /// Общая проверка введенных данных на:
        /// 1. Начинаются с большой буквы;
        /// 2. Содержат только английский и русские буквы;
        /// 3. Поле имеет длину не более 20 символов.
        /// </summary>
        /// <param name="str">Строка с данными</param>
        /// <returns>Ошибки, допущенные при вводе данных(п.1,п.2,п.3).</returns>
        private static string IsDataValid(string str)
        {
            var error = new StringBuilder();
            error.Append(StartingWithUpper(str));
            error.Append(ContainsOnlyLetters(str));
            error.Append(CheckLength(str, 20));

            return error.ToString();
        }

        /// <summary>
        /// Проверка на то, начинается ли поле с данными с большой буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Field have to be started with Upper and have not to contain non-english symbols".</returns>
        private static string StartingWithUpper(string str)
        {
           if ((str.First() >= 'A' && str.First() <= 'Z') || (str.First() >= 'А' && str.First() <= 'Я')) return "";
                return "Field have to be started with Upper and have not to contain non-english symbols"; 
        }

        /// <summary>
        /// Проверка на то, содержит ли поле только английские или русские буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Field have to contain only letters".</returns>
        private static string ContainsOnlyLetters(string str)
        {
            if (str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z') || (t >= 'А' && t  <= 'Я') || (t >= 'а' && t  <= 'я'))))
            {
                return "Field have to contain only letters";
            }
            return "";
        }

        /// <summary>
        /// Проверка на то, содержит ли поле только английские буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Field have to contain only english symbols".</returns>
        private static string ContainsEnglish(string str)
        {
            if (str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z'))))
            {
                return "Field have to contain only english symbols";
            }
            return "";
        }

        /// <summary>
        /// Проверка на то, содержит ли пароль допустимые символы.
        /// </summary>
        /// <param name="str">Пароль</param>
        /// <returns>Если все верно - пустая строка, иначе: "Not allowed symbols for password".</returns>
        private static string IsPassword(string str)
        {
            if (str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z') || (t >= '!' && t <= '@'))))
            {
                return "Not allowed symbols for password";
            }
            return "";
        }

        /// <summary>
        /// Проверка поля с данными на длину.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <param name="allowedLength">Максимальная длина для данного поля</param>
        /// <returns>Если поле пустое или пробельное:"Field is too short", если длина поля в рамках разрешенной длины - пустая строка, если поле длиннее, чем должно быть:"Field is too long".</returns>
        private static string CheckLength(string str, int allowedLength)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "Field is too short";
            return str.Length <= allowedLength ? "" : "Field is too long";
        }

        /// <summary>
        /// Отнесение введенного пароля к определенному классу сложности.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Класс сложности, которому соответствует введенный пароль.</returns>
        public PasswordStrength CheckPasswordStrength(string password)
        {

            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "1234567890";
            const string specialChars = "!@#$%^&*();><?{}/*-+:";
            var hasLowers = false;
            var hasUppers = false;
            var hasDigits = false;
            var hasSpecials = false;
            var difficulty = 0;
            var pwdLenght = password.Length;
            if (pwdLenght == 0) return PasswordStrength.PasswordNotSet;

            for (var i = 0; i < pwdLenght; ++i)
            {
                if (!hasLowers && lowerChars.IndexOf(password[i]) != -1) hasLowers = true;
                if (!hasUppers && upperChars.IndexOf(password[i]) != -1) hasUppers = true;
                if (!hasDigits && digits.IndexOf(password[i]) != -1) hasDigits = true;
                if (!hasSpecials && specialChars.IndexOf(password[i]) != -1) hasSpecials = true;
            }
            if (hasLowers) difficulty++;
            if (hasUppers) difficulty++;
            if (hasDigits) difficulty++;
            if (hasSpecials) difficulty++;
            if (pwdLenght >= 9 && pwdLenght < 15 && difficulty >= 3) return PasswordStrength.Normal;
            if (pwdLenght >= 15 && difficulty == 4) return PasswordStrength.Strong;
            return PasswordStrength.Weak;
        }
    }
}

