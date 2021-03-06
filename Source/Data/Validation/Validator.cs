﻿using System.Linq;
using System.Text;
using Presentation;
using Presentation.Validation;

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
            if (!IsFieldSetted(login)) return ValidatorMessages.FieldIsNotSet;
            var error = new StringBuilder();
            error.Append(ContainsEnglish(login));
            error.Append(CheckLength(login, 20));
            error.Append(IsLoginUnique(login));

            return error.Length != 0 ? error.ToString() : ValidatorMessages.HasNoErrors;
        }

        /// <summary>
        /// Проверка логина на уникальность в базе данных.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Пустую строку, если логин уникален. "Данный логин уже используется другим пользователем." - если такой логин уже существует.</returns>
        private string IsLoginUnique(string login)
        {
            return _userRepository.GetAllUsers().All(u => u.Login != login) ? "" : ValidatorMessages.UnuniqueLogin;
        }

        /// <summary>
        /// Проверка корректности пароля.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Ошибки, допущенные при задании пароля, либо пустая строка, если все верно.</returns>
        public string IsPasswordValid(string password)
        {
            if (!IsFieldSetted(password)) return ValidatorMessages.FieldIsNotSet;
            var error = new StringBuilder();
            error.Append(CheckLength(password, 32));
            error.Append(IsPassword(password));
            var result = error.ToString();

            return string.IsNullOrWhiteSpace(result) ? GetPasswordStrength(password) : result;
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
            return IsFieldSetted(lastname) ? IsDataValid(lastname) : ValidatorMessages.HasNoErrors;
        }

        /// <summary>
        /// Проверка корректности должности
        /// </summary>
        /// <param name="position">Должность</param>
        /// <returns>Ошибки, допущенные при задании должности, либо пустая строка, если все верно.</returns>
        public string IsPositionValid(string position)
        {
            return IsFieldSetted(position) ? IsDataValid(position) : ValidatorMessages.HasNoErrors;
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
            return initials != GetInitialsFromNameAndSurname(name, surname) ? ValidatorMessages.InvalidInitials : ValidatorMessages.HasNoErrors;
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
            if (!IsFieldSetted(str)) return ValidatorMessages.FieldIsNotSet;
            var error = new StringBuilder();
            error.Append(StartingWithUpper(str));
            error.Append(ContainsOnlyLetters(str));
            error.Append(CheckLength(str, 20));

            return error.Length != 0 ? error.ToString() : ValidatorMessages.HasNoErrors;
        }

        /// <summary>
        /// Проверка на то, начинается ли поле с данными с большой буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Поле должно начинаться с заглавной буквы и содержать только буквы".</returns>
        private static string StartingWithUpper(string str)
        {
            return (str.First() >= 'A' && str.First() <= 'Z') || (str.First() >= 'А' && str.First() <= 'Я')
                ? ""
                : ValidatorMessages.MustStartWithUpperAndContainsOnlyLetter;
        }

        /// <summary>
        /// Проверка на то, содержит ли поле только английские или русские буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Поле должно содержать только буквы".</returns>
        private static string ContainsOnlyLetters(string str)
        {
            return str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z') || (t >= 'А' && t <= 'Я') || (t >= 'а' && t <= 'я'))) ? ValidatorMessages.MustContainOnlyLetters : "";
        }

        /// <summary>
        /// Проверка на то, содержит ли поле только английские буквы.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <returns>Если все верно - пустая строка, иначе: "Поле должно содержать только английские буквы".</returns>
        private static string ContainsEnglish(string str)
        {
            return str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z'))) ? ValidatorMessages.MustContainOnlyEnglish : "";
        }

        /// <summary>
        /// Проверка на то, содержит ли пароль допустимые символы.
        /// </summary>
        /// <param name="str">Пароль</param>
        /// <returns>Если все верно - пустая строка, иначе: "Недопустимые символы".</returns>
        private static string IsPassword(string str)
        {
            return str.Any(t => !((t >= 'A' && t <= 'Z') || (t >= 'a' && t <= 'z') || (t >= '!' && t <= '@'))) ? ValidatorMessages.UnallowedSymbols : "";
        }

        /// <summary>
        /// Проверка поля с данными на длину.
        /// </summary>
        /// <param name="str">Поле с данными</param>
        /// <param name="allowedLength">Максимальная длина для данного поля</param>
        /// <returns>Если поле пустое или пробельное -  пустая строка, если длина поля в рамках разрешенной длины - пустая строка, если поле длиннее, чем должно быть:"Поле слишком длинное".</returns>
        private static string CheckLength(string str, int allowedLength)
        {
            return str.Length <= allowedLength ? "" : ValidatorMessages.LongField;
        }

        /// <summary>
        /// Проверяет установленность поля.
        /// </summary>
        /// <param name="str">Поле</param>
        /// <returns>true - поле задано, иначе false.</returns>
        private static bool IsFieldSetted(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Отнесение введенного пароля к определенному классу сложности.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Класс сложности, которому соответствует введенный пароль.</returns>
        public PasswordStrength CheckPasswordStrength(string password)
        {
            
            var lowerChars = ValidatorMessages.passwordLowerChars;
            var upperChars = ValidatorMessages.passwordUpperChars;
            var digits = ValidatorMessages.passwordDigits;
            var specialChars = ValidatorMessages.passwordSpecialChars;

            var hasLowers = false;
            var hasUppers = false;
            var hasDigits = false;
            var hasSpecials = false;
            var passwordDifficulty = 0;
            var passwordLength = password.Length;
            if (passwordLength == 0) return PasswordStrength.PasswordNotSet;

            for (var i = 0; i < passwordLength; ++i)
            {
                if (!hasLowers && lowerChars.IndexOf(password[i]) != -1) hasLowers = true;
                if (!hasUppers && upperChars.IndexOf(password[i]) != -1) hasUppers = true;
                if (!hasDigits && digits.IndexOf(password[i]) != -1) hasDigits = true;
                if (!hasSpecials && specialChars.IndexOf(password[i]) != -1) hasSpecials = true;
            }
            var currentPasswordType = PasswordStrength.Weak;
            if (hasLowers) passwordDifficulty++;
            if (hasUppers) passwordDifficulty++;
            if (hasDigits) passwordDifficulty++;
            if (hasSpecials) passwordDifficulty++;
            if (passwordLength >= 6 && passwordLength <= 12 && passwordDifficulty >= 2 && passwordDifficulty <= 4) currentPasswordType = PasswordStrength.Normal;
            if (passwordLength > 12 && passwordDifficulty >= 3) currentPasswordType = PasswordStrength.Strong;

            return currentPasswordType;
        }

        /// <summary>
        /// Сопоставляет сложность пароля с цветом.
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Тип пароля.</returns>
        private string GetPasswordStrength(string password)
        {
            var currentStrength = CheckPasswordStrength(password);
            switch (currentStrength)
            {
                case  PasswordStrength.Weak :
                    return ValidatorMessages.WeakPassword;
                case PasswordStrength.Normal:
                    return ValidatorMessages.NormalPassword;
                case PasswordStrength.Strong:
                    return ValidatorMessages.StrongPassword;
            }

            return null;
        }
    }
}

