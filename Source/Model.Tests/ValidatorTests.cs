using NUnit.Framework;

namespace Model.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void CheckPasswordStrengthTest()
        {
            var validator = new Validator();

            Assert.That(validator.CheckPasswordStrength(""), Is.EqualTo(PasswordStrength.PwdNotSet));
            Assert.That(validator.CheckPasswordStrength("lol"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordStrength("lol124"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordStrength("loL1%"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordStrength("AntoshKa@987Lukas"), Is.EqualTo(PasswordStrength.Strong));
            Assert.That(validator.CheckPasswordStrength("&^&68Ff*%&(&*"), Is.EqualTo(PasswordStrength.Normal));
        }

        [Test]
        public void PasswordCrypt()
        {
            var pwdCrypt = new PasswordCrypt();

            Assert.True(pwdCrypt.IsPasswordValid("AntoshKa@987Lukas", pwdCrypt.GetHashString("AntoshKa@987Lukas")));
        }

        [Test]
        public void IsLoginValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsLoginValid("qwerty"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsLoginValid("йцукен"), Is.EqualTo("Field have to contain only english symbols\r\n\r\n"));
        }

        [Test]
        public void IsPasswordValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo("\r\nNot allowed symbols for password\r\n"));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsPasswordValid(""), Is.EqualTo("Field is too short\r\n\r\nPassword is too weak, or not set\r\n"));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsLoginValid(""), Is.EqualTo("\r\nField is too short\r\n"));
        }

        [Test]
        public void IsNameValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsNameValid("Andrey"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsNameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsSurnameValid("Igorevich"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsSurnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsLastnameValid("Sokov"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsLastnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsPositionValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.IsPostionValid("Engineer"), Is.EqualTo("\r\n\r\n"));
            Assert.That(validator.IsPostionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo("\r\nField is too long\r\n"));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            var validator = new Validator();

            Assert.That(validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(""));
            Assert.That(validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo("Initials are not correct"));
        }

}
}
