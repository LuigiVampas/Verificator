using System.Linq;

namespace Model
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Initials
        {
            get
            {
                var firstNameLetter = Name.First();
                var firstLastNameLetter = LastName.First();
                return firstNameLetter + "." + firstLastNameLetter;
            }
        }

        public string Position { get; set; }
    }
}
