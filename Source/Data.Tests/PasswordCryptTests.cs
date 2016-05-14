using Data.Validation;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    class PasswordCryptTests
    {
        private Validator _validator;
        private PasswordCrypt _passwordCrypt;
        private ListUserRepository _listUserRepository;
  
        [SetUp]
        public void SetUp()
        {
            _listUserRepository = new ListUserRepository();
            _validator = new Validator(_listUserRepository);
            _passwordCrypt = new PasswordCrypt(); ;
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
    }
}
