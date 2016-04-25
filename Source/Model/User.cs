using System;
using System.Linq;

namespace Model
{
    public class User : ICloneable
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Initials
        {
            get
            {
                var firstNameLetter = Name.First();
                var firstSurnameLetter = Surname.First();
                return firstNameLetter + "." + firstSurnameLetter;
            }
        }

        public string Position { get; set; }

        public object Clone()
        {
            return new User { Id = Id, Name = Name, Surname = Surname, Position = Position, 
                Login = Login, Password = Password, Lastname = Lastname};
        }
    }
}
