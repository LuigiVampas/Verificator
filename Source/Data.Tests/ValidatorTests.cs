using Data.Validation;
using Model;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        private Validator _validator;
        private ListUserRepository _listUserRepository;

        [SetUp]
        public void SetUp()
        {
            _listUserRepository = new ListUserRepository();
            _validator = new Validator(_listUserRepository);
        }

        [Test]
        public void IsLoginValidTest()
        {
            _listUserRepository.AddUser(new User { Login = "asdfgh" });
            Assert.That(_validator.IsLoginValid("qwerty"), Is.EqualTo(""));
            Assert.That(_validator.IsLoginValid("йцукен"), Is.EqualTo("Field have to contain only english symbols\n"));
            Assert.That(_validator.IsLoginValid("asdfgh"), Is.EqualTo("Данный логин уже используется другим пользователем.\n"));
        }

        [Test]
        public void IsNameContainsOnlyLettersTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(""));
            Assert.That(_validator.IsNameValid("Andrey123"), Is.EqualTo("Field have to contain only letters\n"));
        }
        [Test]
        public void IsPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo(""));
            Assert.That(_validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo("Not allowed symbols for password\n"));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid(""), Is.EqualTo("Field is too short\nPassword is too weak, or not set\n"));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            Assert.That(_validator.IsLoginValid(""), Is.EqualTo("Field is too short\n"));
        }

        [Test]
        public void IsNameValidTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(""));
            Assert.That(_validator.IsNameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\n"));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            Assert.That(_validator.IsSurnameValid("Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.IsSurnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\n"));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            Assert.That(_validator.IsLastnameValid("Sokov"), Is.EqualTo(""));
            Assert.That(_validator.IsLastnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\n"));
        }

        [Test]
        public void IsPositionValidTest()
        {
            Assert.That(_validator.IsPositionValid("Engineer"), Is.EqualTo(""));
            Assert.That(_validator.IsPositionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo("Field is too long\n"));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            Assert.That(_validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo("Initials are not correct\n"));
        }

}
}
