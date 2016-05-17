using System;

namespace Model
{
    /// <summary>
    /// Описание класса пользователя и его полей.
    /// </summary>
    public class User : ICloneable
    {
        public User()
        {
            Login = "";
            Password = "";
            Surname = "";
            Name = "";
            Lastname = "";
            Position = "";
            Initials = "";
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Initials { get; set; }

        public string Position { get; set; }

        /// <summary>
        /// Создание нового пользователя, который является копией текущего.
        /// </summary>
        /// <returns>Новый пользователь, который является копией текущего.</returns>
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

        /// <summary>
        /// Проверка эквивалентности пользователей.
        /// </summary>
        /// <param name="other">Пользователь, с кем сравнивать текущего</param>
        /// <returns>True - пользователи идиентичны, False - отличаются.</returns>
        protected bool Equals(User other)
        {
            return string.Equals(Login, other.Login) && string.Equals(Password, other.Password) 
                && string.Equals(Surname, other.Surname) && string.Equals(Name, other.Name) 
                && string.Equals(Lastname, other.Lastname) && string.Equals(Initials, other.Initials) 
                && string.Equals(Position, other.Position);
        }

        /// <summary>
        /// Сравниваем пользователей, как объекты.
        /// </summary>
        /// <param name="obj">Объект другого пользователя</param>
        /// <returns>True - пользователи идиентичны как объекты, False - отличаются как объекты.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        /// <summary>
        /// Хеш-функция для типа данных User.
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Перегрузка оператора равенства
        /// </summary>
        /// <param name="left">Первый объект для сравнения</param>
        /// <param name="right">Второй объект для сравнения</param>
        /// <returns>True - пользователи идиентичны, False - отличаются.</returns>
        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Перегрузка оператора неравенства
        /// </summary>
        /// <param name="left">Первый объект для сравнения</param>
        /// <param name="right">Второй объект для сравнения</param>
        /// <returns>False - пользователи идиентичны, True - отличаются.</returns>
        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}
