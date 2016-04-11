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

            Assert.That(validator.CheckPasswordsStrengh(""), Is.EqualTo(PasswordStrength.PwdNotSet));
            Assert.That(validator.CheckPasswordsStrengh("lol"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrengh("lol124"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrengh("loL1%"), Is.EqualTo(PasswordStrength.Weak));
            Assert.That(validator.CheckPasswordsStrengh("AntoshKa@987Lukas"), Is.EqualTo(PasswordStrength.Strong));
            Assert.That(validator.CheckPasswordsStrengh("&^&68Ff*%&(&*"), Is.EqualTo(PasswordStrength.Normal));
        }
    }
}
