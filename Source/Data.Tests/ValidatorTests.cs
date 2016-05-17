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
            Assert.That(_validator.IsLoginValid("йцукен"), Is.EqualTo("Поле должно содержать только английские буквы"));
            Assert.That(_validator.IsLoginValid("asdfgh"), Is.EqualTo("Данный логин уже используется другим пользователем"));
        }

        [Test]
        public void IsNameContainsOnlyLettersTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(""));
            Assert.That(_validator.IsNameValid("Andrey123"), Is.EqualTo("Поле должно содержать только буквы"));
        }
        [Test]
        public void IsPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo("Хороший пароль"));
            Assert.That(_validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo("Недопустимые символы"));
            Assert.That(_validator.IsPasswordValid("Password8"), Is.EqualTo("Средний пароль"));
            Assert.That(_validator.IsPasswordValid("password8"), Is.EqualTo("Слабый пароль"));
            Assert.That(_validator.IsPasswordValid("12312&&^*FJKDFsasklanf!"), Is.EqualTo("Недопустимые символы"));
            Assert.That(_validator.IsPasswordValid("1234Lans329@!"), Is.EqualTo("Хороший пароль"));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid(""), Is.EqualTo("Поле не установлено"));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            Assert.That(_validator.IsLoginValid(""), Is.EqualTo("Поле не установлено"));
        }

        [Test]
        public void IsNameValidTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(""));
            Assert.That(_validator.IsNameValid("йцукен"), Is.EqualTo("Поле должно начинаться с заглавной буквы и содержать только буквы"));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            Assert.That(_validator.IsSurnameValid("Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.IsSurnameValid("йцукен"), Is.EqualTo("Поле должно начинаться с заглавной буквы и содержать только буквы"));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            Assert.That(_validator.IsLastnameValid("Sokov"), Is.EqualTo(""));
            Assert.That(_validator.IsLastnameValid("йцукен"), Is.EqualTo("Поле должно начинаться с заглавной буквы и содержать только буквы"));
        }

        [Test]
        public void IsPositionValidTest()
        {
            Assert.That(_validator.IsPositionValid("Engineer"), Is.EqualTo(""));
            Assert.That(_validator.IsPositionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo("Поле слишком длинное"));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            Assert.That(_validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo("Инициалы не верны"));
        }

}
}
