using NUnit.Framework;

namespace Model.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void CheckPasswordStrengthTest()
        {
            Assert.That(Validator.CheckPasswordStrength(""), Is.EqualTo(PasswordStrength.PasswordNotSet));
            Assert.That(Validator.CheckPasswordStrength("lol"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(Validator.CheckPasswordStrength("lol124"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(Validator.CheckPasswordStrength("loL1%"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(Validator.CheckPasswordStrength("AntoshKa@987Lukas"), Is.EqualTo(PasswordStrength.Strong));
            Assert.That(Validator.CheckPasswordStrength("&^&68Ff*%&(&*"), Is.EqualTo(PasswordStrength.Normal));
        }

        [Test]
        public void PasswordCryptTest()
        {
            Assert.True(PasswordCrypt.IsPasswordValid("AntoshKa@987Lukas", PasswordCrypt.GetHashString("AntoshKa@987Lukas")));
        }

        [Test]
        public void IsLoginValidTest()
        {
            Assert.That(Validator.IsLoginValid("qwerty"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsLoginValid("йцукен"), Is.EqualTo("Field have to contain only english symbols\r\n\r\n"));
        }

        [Test]
        public void IsPasswordValidTest()
        {
            Assert.That(Validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo("\r\nNot allowed symbols for password\r\n"));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            Assert.That(Validator.IsPasswordValid(""), Is.EqualTo("Field is too short\r\n\r\nPassword is too weak, or not set\r\n"));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            Assert.That(Validator.IsLoginValid(""), Is.EqualTo("\r\nField is too short\r\n"));
        }

        [Test]
        public void IsNameValidTest()
        {
            Assert.That(Validator.IsNameValid("Andrey"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsNameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            Assert.That(Validator.IsSurnameValid("Igorevich"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsSurnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            Assert.That(Validator.IsLastnameValid("Sokov"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsLastnameValid("йцукен"), Is.EqualTo("Field have to be started with Upper and have not to contain non-english symbols\r\n\r\n"));
        }

        [Test]
        public void IsPositionValidTest()
        {
            Assert.That(Validator.IsPositionValid("Engineer"), Is.EqualTo("\r\n\r\n"));
            Assert.That(Validator.IsPositionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo("\r\nField is too long\r\n"));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            Assert.That(Validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(""));
            Assert.That(Validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo("Initials are not correct"));
        }

}
}
