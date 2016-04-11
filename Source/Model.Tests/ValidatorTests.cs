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
        [ExpectedException (typeof(DataIsNotValidReason))]
        public void IsLoginValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsLoginValid("qwerty"));
            Assert.True(validator.IsLoginValid("йцукен"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsPasswordValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsPasswordValid("AntoshKa@987Lukas"));
            Assert.True(validator.IsPasswordValid("&^&68Ff*%&(&*"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsEmptyPasswordValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsPasswordValid(""));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsEmptyLoginValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsLoginValid(""));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsNameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsNameValid("Andrey"));
            Assert.True(validator.IsNameValid("йцукен"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsSurnameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsSurnameValid("Igorevich"));
            Assert.True(validator.IsSurnameValid("йцукен"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsLastnameValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsLastnameValid("Sokov"));
            Assert.True(validator.IsLastnameValid("йцукен"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void IsPositionValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.IsPostionValid("Engineer"));
            Assert.True(validator.IsPostionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"));
        }

        [Test]
        [ExpectedException(typeof(DataIsNotValidReason))]
        public void AreInitialsValidTest()
        {
            var validator = new Validator();

            Assert.True(validator.AreInitialsValid("A.I", "Andrey", "Igorevich"));
            Assert.True(validator.IsPostionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"));
        }

}
}
