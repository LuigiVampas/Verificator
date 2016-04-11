using System.Text;

namespace Model
{
    public class Validator
    {
        public string IsLoginValid(string login)
        {
            var result = new StringBuilder();
            
            return result.ToString();
        }

        public string IsUsersDataVaild(string name, string surname, string lastname, string initials, string positon)
        {
            var result = new StringBuilder();

            return result.ToString();
        }

        public PasswordStrength CheckPasswordsStrenght(string password)
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

