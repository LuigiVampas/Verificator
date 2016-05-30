using Data.Validation;
using Model;
using NUnit.Framework;
using Presentation.Validation;

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
            Assert.That(_validator.IsLoginValid("qwerty"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsLoginValid("йцукен"), Is.EqualTo(ValidatorMessages.MustContainOnlyEnglish));
            Assert.That(_validator.IsLoginValid("asdfgh"), Is.EqualTo(ValidatorMessages.UnuniqueLogin));
        }

        [Test]
        public void IsNameContainsOnlyLettersTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsNameValid("Andrey123"), Is.EqualTo(ValidatorMessages.MustContainOnlyLetters));
        }
        [Test]
        public void IsPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid("AntoshKa@987Lukas"), Is.EqualTo(ValidatorMessages.StrongPassword));
            Assert.That(_validator.IsPasswordValid("&^&68Ff*%&(&*"), Is.EqualTo(ValidatorMessages.UnallowedSymbols));
            Assert.That(_validator.IsPasswordValid("Password8"), Is.EqualTo(ValidatorMessages.NormalPassword));
            Assert.That(_validator.IsPasswordValid("password8"), Is.EqualTo(ValidatorMessages.WeakPassword));
            Assert.That(_validator.IsPasswordValid("12312&&^*FJKDFsasklanf!"), Is.EqualTo(ValidatorMessages.UnallowedSymbols));
            Assert.That(_validator.IsPasswordValid("1234Lans329@!"), Is.EqualTo(ValidatorMessages.StrongPassword));
        }

        [Test]
        public void IsEmptyPasswordValidTest()
        {
            Assert.That(_validator.IsPasswordValid(""), Is.EqualTo(ValidatorMessages.FieldIsNotSet));
        }

        [Test]
        public void IsEmptyLoginValidTest()
        {
            Assert.That(_validator.IsLoginValid(""), Is.EqualTo(ValidatorMessages.FieldIsNotSet));
        }

        [Test]
        public void IsNameValidTest()
        {
            Assert.That(_validator.IsNameValid("Andrey"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsNameValid("йцукен"), Is.EqualTo(ValidatorMessages.MustStartWithUpperAndContainsOnlyLetter));
        }

        [Test]
        public void IsSurnameValidTest()
        {
            Assert.That(_validator.IsSurnameValid("Igorevich"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsSurnameValid("йцукен"), Is.EqualTo(ValidatorMessages.MustStartWithUpperAndContainsOnlyLetter));
        }

        [Test]
        public void IsLastnameValidTest()
        {
            Assert.That(_validator.IsLastnameValid("Sokov"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsLastnameValid("йцукен"), Is.EqualTo(ValidatorMessages.MustStartWithUpperAndContainsOnlyLetter));
        }

        [Test]
        public void IsPositionValidTest()
        {
            Assert.That(_validator.IsPositionValid("Engineer"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.IsPositionValid("Engineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineerengineer"), Is.EqualTo(ValidatorMessages.LongField));
        }

        [Test]
        public void AreInitialsValidTest()
        {
            Assert.That(_validator.AreInitialsValid("A.I", "Andrey", "Igorevich"), Is.EqualTo(ValidatorMessages.HasNoErrors));
            Assert.That(_validator.AreInitialsValid("A.I.", "Andrey", "Igorevich"), Is.EqualTo(ValidatorMessages.InvalidInitials));
        }

}
}
