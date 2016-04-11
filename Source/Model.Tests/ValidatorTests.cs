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

            Assert.That(validator.CheckPasswordsStrenght(""), Is.EqualTo(PasswordStrength.PwdNotSet));
            Assert.That(validator.CheckPasswordsStrenght("lol"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrenght("lol124"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrenght("loL1%"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrenght("AntoshKa@987Lukas"), Is.EqualTo(PasswordStrength.Strong));
            Assert.That(validator.CheckPasswordsStrenght("&^&68Ff*%&(&*"), Is.EqualTo(PasswordStrength.Normal));
        }
    }
}
