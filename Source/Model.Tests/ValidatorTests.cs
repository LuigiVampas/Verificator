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
        [ExpectedException (typeof(DataIsNotValidReason))]
        public void IsLoginValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsLoginValid("qwerty"));
            Assert.True(validator.IsLoginValid("qwertyqwertyqwertyqwerty"));
        }
        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsNameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsNameValid("Andrey"));
            Assert.True(validator.IsNameValid("andrey"));
            
        }
        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsSurnameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsSurnameValid("Igorevich"));
            Assert.True(validator.IsSurnameValid("igorevich"));
            
        }
        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsLastnameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsLastnameValid("Sokov"));
            Assert.True(validator.IsLastnameValid("sokov"));
            
        }
}
}
