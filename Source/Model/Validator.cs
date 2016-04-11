using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Model
{
    public class Validator
    {
        public bool IsLoginValid(string login)
        {
            return IsFieldValid(login, DataType.Login);
        }

        public bool IsNameValid(string name)
        {
            return IsFieldValid(name, DataType.Name);
        }

        public bool IsSurnameValid(string surname)
        {
            return IsFieldValid(surname, DataType.Surname);
        }

        public bool IsLastnameValid(string lastname)
        {
            return IsFieldValid(lastname, DataType.Lastname);
        }

        public bool IsPostionValid(string position)
        {
            return IsFieldValid(position, DataType.Position);
        }

        public bool AreInitialsValid(string initials, string name, string surname, string lastname)
        {
            if (initials != GetInitialsFromNameAndLastname(name, surname, lastname))
                return false;
            return true;
        }

        private string GetInitialsFromNameAndLastname(string name, string surname, string lastname)
        {
            return name.First() + "." + surname.First() + "." +lastname.First();
        }

        private bool IsFieldValid(string field, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Name :
                    IsDataValid(field);
                    break;
                case DataType.Surname:
                    IsDataValid(field);
                    break;
                case DataType.Lastname:
                    IsDataValid(field);
                    break;
                case DataType.Initials:
                    if (field.Length > 3 && !field.Contains("."))
                        return false;
                    break;
                case DataType.Position:
                    if (field.Length < 50 && IsDataValid(field))
                        return true;
                    break;
                case DataType.Password:
                    if (field.Length > 500)
                        return false;
                    break;
            }
            return true;
        }

        private bool IsDataValid(string str)
        {
            var result = new StringBuilder();
            var lenght = str.Length;
            if (lenght == 0)
                result.Append("Поле должно содержать больше символов");
            if (lenght > 20)
                result.Append("Поле содержит слишком много символов");
            if (!(str.First() >= 'A' && str.First() <= 'Z') ||
                (str.First() >= 'А' && str.First() <= 'Я'))
                result.Append("Поле должно начинаться с заглавной буквы или не содержать посторонних символов");
            if (result.ToString() != "")
                throw new DataNotValidReason(result.ToString());
            return true;
        }

        public enum DataType
        {
            Login, Password, Name, Surname, Lastname, Initials, Position
        }


        public PasswordStrength CheckPasswordsStrengh(string password)
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

