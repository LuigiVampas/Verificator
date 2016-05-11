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

        protected bool Equals(User other)
        {
            return string.Equals(Login, other.Login) && string.Equals(Password, other.Password) 
                && string.Equals(Surname, other.Surname) && string.Equals(Name, other.Name) 
                && string.Equals(Lastname, other.Lastname) && string.Equals(Initials, other.Initials) 
                && string.Equals(Position, other.Position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Login != null ? Login.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Surname != null ? Surname.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Lastname != null ? Lastname.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Initials != null ? Initials.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Position != null ? Position.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}
