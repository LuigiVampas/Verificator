using System.Linq;
using System.Text;
using Presentation;

namespace Data.Validation
{
    public class Validator : IValidator
    {
        private readonly IUserRepository _userRepository;

        public Validator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string IsLoginValid(string login)
        {
            var error = new StringBuilder();
            error.AppendLine(ContainsEnglish(login));
            error.AppendLine(CheckLength(login, 15));
            error.AppendLine(IsLoginUnique(login));
            return error.ToString();
        }

        private string IsLoginUnique(string login)
        {
            return _userRepository.GetAllUsers().All(u => u.Login != login) ? "" : "Данный логин уже используется другим пользователем.";
        }

        public string IsPasswordValid(string password)
        {
            var error = new StringBuilder();
            error.AppendLine(CheckLength(password, 32));
            error.AppendLine(IsPassword(password));
            var passwordStrength = CheckPasswordStrength(password);
            if (passwordStrength == PasswordStrength.Weak ||
                passwordStrength == PasswordStrength.PasswordNotSet)
                error.AppendLine("Password is too weak, or not set");

            return error.ToString();
        }

        public string IsNameValid(string name)
        {
            return IsDataValid(name);
        }

        public string IsSurnameValid(string surname)
        {
            return IsDataValid(surname);
        }

        public string IsLastnameValid(string lastname)
        {
            return IsDataValid(lastname);
        }

        public string IsPositionValid(string position)
        {
            return IsDataValid(position);
        }

        public string AreInitialsValid(string initials, string name, string surname)
        {
            if (initials != GetInitialsFromNameAndSurname(name, surname))
                return "Initials are not correct";
            return "";
        }

        private string GetInitialsFromNameAndSurname(string name, string surname)
        {
            return name.First() + "." + surname.First();
        }

        private string IsDataValid(string str)
        {
            var error = new StringBuilder();
            error.AppendLine(StartingWithUpper(str));
            error.AppendLine(CheckLength(str, 20));

            return error.ToString();
        }

        private string StartingWithUpper(string str)
        {
           if ((str.First() >= 'A' && str.First() <= 'Z') || (str.First() >= 'А' && str.First() <= 'Я')) return "";
                return "Field have to be started with Upper and have not to contain non-english symbols"; 
        }

        private string ContainsEnglish(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!((str[i] >= 'A' && str[i] <= 'Z') || (str[i] >= 'a' && str[i] <= 'z'))) return "Field have to contain only english symbols";
            return "";
        }
        private string IsPassword(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!((str[i] >= 'A' && str[i] <= 'Z') || (str[i] >= 'a' && str[i] <= 'z') || (str[i] >= '!' && str[i] <= '@'))) return "Not allowed symbols for password";
            return "";
        }

        private string CheckLength(string str, int allowedLength)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "Field is too short";
            if (str.Length <= allowedLength)
                return "";
            return "Field is too long";
        }

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

