using System.Linq;
using System.Text;

namespace Model
{
    public class Validator
    {
        public bool IsLoginValid(string login)
        {
            var result = new StringBuilder();
            result.AppendLine(ContainsEnglish(login));
            result.AppendLine(CheckLength(login, 15));
            if (!string.IsNullOrWhiteSpace(result.ToString()))
                throw new DataIsNotValidReason(result.ToString());
            return true;
        }

        public bool IsPasswordValid(string password)
        {
            var result = new StringBuilder();
            result.AppendLine(CheckLength(password, 32));
            result.AppendLine(IsPassword(password));
            if (CheckPasswordStrength(password) == PasswordStrength.Weak ||
                CheckPasswordStrength(password) == PasswordStrength.PwdNotSet)
                result.AppendLine("Password is too weak, or not set");
            if (!string.IsNullOrWhiteSpace(result.ToString()))
                throw new DataIsNotValidReason(result.ToString());
            return true;
        }

        public bool IsNameValid(string name)
        {
            return IsDataValid(name);
        }

        public bool IsSurnameValid(string surname)
        {
            return IsDataValid(surname);
        }

        public bool IsLastnameValid(string lastname)
        {
            return IsDataValid(lastname);
        }

        public bool IsPostionValid(string position)
        {
            return IsDataValid(position);
        }

        public bool AreInitialsValid(string initials, string name, string surname)
        {
            return initials == GetInitialsFromNameAndSurname(name, surname);
        }

        private static string GetInitialsFromNameAndSurname(string name, string surname)
        {
            return name.First() + "." + surname.First();
        }

        private static bool IsDataValid(string str)
        {
            var result = new StringBuilder();
            var lenght = str.Length;
           result.AppendLine(StartingWithUpper(str));
           result.AppendLine(CheckLength(str, 20));
            if (!string.IsNullOrWhiteSpace(result.ToString()))
                throw new DataIsNotValidReason(result.ToString());
            return true;
        }

        private static string StartingWithUpper(string str)
        {
           if ((str.First() >= 'A' && str.First() <= 'Z') || (str.First() >= 'А' && str.First() <= 'Я')) return "";
            return "Field have to be started with Upper and have not to contain non-english symbols"; 
        }

        private static string ContainsEnglish(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!((str[i] >= 'A' && str[i] <= 'Z') || (str[i] >= 'a' && str[i] <= 'z'))) return "Field have to contain only english symbols";
            return "";
        }
        private static string IsPassword(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!((str[i] >= 'A' && str[i] <= 'Z') || (str[i] >= 'a' && str[i] <= 'z') || (str[i] >= '!' && str[i] <= '@'))) return "Not allowed symbols for password";
            return "";
        }

        private static string CheckLength(string str, int allowedLength)
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
            if (pwdLenght == 0) return PasswordStrength.PwdNotSet;

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

