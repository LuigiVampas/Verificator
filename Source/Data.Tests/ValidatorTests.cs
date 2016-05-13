using Data.Validation;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        private Validator _validator;
        private PasswordCrypt _passwordCrypt;
        private ListUserRepository _listUserRepository;

        [SetUp]
        public void SetUp()
        {
            _listUserRepository = new ListUserRepository();
            _validator = new Validator(_listUserRepository);
            _passwordCrypt = new PasswordCrypt();;
        }

        [Test]
        public void CheckPasswordStrengthTest()
        {
            Assert.That(_validator.CheckPasswordStrength(""), Is.EqualTo(PasswordStrength.PasswordNotSet));
            Assert.That(_validator.CheckPasswordStrength("lol"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(_validator.CheckPasswordStrength("lol124"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(_validator.CheckPasswordStrength("loL1%"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(_validator.CheckPasswordStrength("AntoshKa@987Lukas"), Is.EqualTo(PasswordStrength.Strong));
            Assert.That(_validator.CheckPasswordStrength("&^&68Ff*%&(&*"), Is.EqualTo(PasswordStrength.Normal));
        }

        [Test]
        public void PasswordCryptTest()
        {
            Assert.True(_passwordCrypt.IsPasswordValid("AntoshKa@987Lukas", _passwordCrypt.GetHashString("AntoshKa@987Lukas")));
        }

        [Test]
        public void IsLoginValidTest()
        {
            Assert.That(_validator.IsLoginValid("qwerty"), Is.EqualTo(""));
            Assert.That(_validator.IsLoginValid("йцукен"), Is.EqualTo("Field have to contain only english symbols"));
        }

        [Test]
        public void IsPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo(""));
            Assert.That(_validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo("Not allowed symbols for password"));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid(""), Is.EqualTo("Field is too shortPassword is too weak, or not set"));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            Assert.That(_validator.IsLoginValid(""), Is.EqualTo("Field is too short"));
        }

        [Test]
        public void IsNameValidTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(""));
            Assert.That(_validator.IsNameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols"));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            Assert.That(_validator.IsSurnameValid("Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.IsSurnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols"));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            Assert.That(_validator.IsLastnameValid("Sokov"), Is.EqualTo(""));
            Assert.That(_validator.IsLastnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols"));
        }

        [Test]
        public void IsPositionValidTest()
        {
            Assert.That(_validator.IsPositionValid("Engineer"), Is.EqualTo(""));
            Assert.That(_validator.IsPositionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo("Field is too long"));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            Assert.That(_validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(""));
            Assert.That(_validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo("Initials are not correct"));
        }

}
}
