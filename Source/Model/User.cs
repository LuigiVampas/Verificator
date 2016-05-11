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
            get;
         //   {
//                var firstNameLetter = Name.First();
   //             var firstLastnameLetter = Lastname.First();
      //          return firstNameLetter + "." + firstLastnameLetter + ".";
       //     }
            set ;// { throw new NotImplementedException(); }
        }

        public string Position { get; set; }

        public object Clone()
        {
            return new User
            {
                Id = Id,
                Login = Login,
                Password = Password,
                Surname = Surname,
                Name = Name,
                Lastname = Lastname,
                Position = Position
            };
        }
    }
}
