using System;
using Model;
using Moq;
using NUnit.Framework;
using Presentation.Contexts;
using Presentation.Validation;

namespace Presentation.Tests
{
    [TestFixture]
    public class ContextsTests
    {
        private User _user;
        private Mock<IValidator> _validatorMock;
        private Mock<IPasswordCrypt> _passwordCryptMock;
        
        [SetUp]
        public void SetUp()
        {
            _user = new User
            {
                Id = 0,
                Login = "Login",
                Lastname = "Lastname",
                Password = "Password",
                Name = "Name",
                Surname = "Surname",
                Position = "Position",
                Initials = "N.L."
            };

            _validatorMock = new Mock<IValidator>();
            _passwordCryptMock = new Mock<IPasswordCrypt>();
        }

        [Test]
        public void UserDataContextEmptyCreationTest()
        {
            var userDataContext = new UserDataContext(_validatorMock.Object, _passwordCryptMock.Object);

            Assert.That(userDataContext.Id, Is.EqualTo(0));
            Assert.That(userDataContext.Surname, Is.EqualTo(""));
            Assert.That(userDataContext.Name, Is.EqualTo(""));
            Assert.That(userDataContext.Lastname, Is.EqualTo(""));
            Assert.That(userDataContext.Position, Is.EqualTo(""));
            Assert.That(userDataContext.Login, Is.EqualTo(""));
            Assert.That(userDataContext.Password, Is.EqualTo(""));
            Assert.That(userDataContext.Initials, Is.EqualTo(""));
            Assert.That(userDataContext.CreateUser(false), Is.EqualTo(new User()));
        }

        [Test]
        public void UserDataContextCreatingWithUserTest()
        {
            var userDataContext = new UserDataContext(_validatorMock.Object, _passwordCryptMock.Object);
            userDataContext.Initialize(_user);

            Assert.That(userDataContext.Id, Is.EqualTo(_user.Id));
            Assert.That(userDataContext.Surname, Is.EqualTo(_user.Surname));
            Assert.That(userDataContext.Name, Is.EqualTo(_user.Name));
            Assert.That(userDataContext.Lastname, Is.EqualTo(_user.Lastname));
            Assert.That(userDataContext.Position, Is.EqualTo(_user.Position));
            Assert.That(userDataContext.Login, Is.EqualTo(_user.Login));
            Assert.That(userDataContext.Password, Is.EqualTo(_user.Password));
            Assert.That(userDataContext.Initials, Is.EqualTo(_user.Initials));
            Assert.That(userDataContext.CreateUser(false), Is.EqualTo(_user));
        }

        [Test]
        public void UserDataContextChangingPropertiesTest()
        {
            var userDataContext = new UserDataContext(_validatorMock.Object, _passwordCryptMock.Object);

            var propertyChangedName = "";
            userDataContext.PropertyChanged += (sender, e) => propertyChangedName = e.PropertyName;

            userDataContext.Login = _user.Login;
            _validatorMock.Verify(v => v.IsLoginValid(_user.Login), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Login"));

            userDataContext.Password = _user.Password;
            _validatorMock.Verify(v => v.IsPasswordValid(_user.Password), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Password"));

            userDataContext.Surname = _user.Surname;
            _validatorMock.Verify(v => v.IsSurnameValid(_user.Surname), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Surname"));

            userDataContext.Lastname = _user.Lastname;
            _validatorMock.Verify(v => v.IsLastnameValid(_user.Lastname), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Lastname"));

            userDataContext.Name = _user.Name;
            _validatorMock.Verify(v => v.IsNameValid(_user.Name), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Name"));

            userDataContext.Position = _user.Position;
            _validatorMock.Verify(v => v.IsPositionValid(_user.Position), Times.Once);
            Assert.That(propertyChangedName, Is.EqualTo("Position"));

            Assert.That(userDataContext.Surname, Is.EqualTo(_user.Surname));
            Assert.That(userDataContext.Name, Is.EqualTo(_user.Name));
            Assert.That(userDataContext.Lastname, Is.EqualTo(_user.Lastname));
            Assert.That(userDataContext.Position, Is.EqualTo(_user.Position));
            Assert.That(userDataContext.Login, Is.EqualTo(_user.Login));
            Assert.That(userDataContext.Password, Is.EqualTo(_user.Password));
            Assert.That(userDataContext.Initials, Is.EqualTo(_user.Initials));
            Assert.That(userDataContext.CreateUser(false), Is.EqualTo(_user));

            _passwordCryptMock.Setup(pc => pc.GetHashString(It.IsAny<string>())).Returns("HashPassword");

            var userWithSaltPassword = userDataContext.CreateUser(true);

            Assert.That(userWithSaltPassword.Password, Is.EqualTo("HashPassword"));
        }

        [Test]
        public void UserDataContextWrongDataTest()
        {
            _validatorMock.Setup(v => v.IsLoginValid(It.IsAny<string>())).Returns("Login");
            _validatorMock.Setup(v => v.IsPasswordValid(It.IsAny<string>())).Returns("Password");
            _validatorMock.Setup(v => v.IsSurnameValid(It.IsAny<string>())).Returns("Surname");
            _validatorMock.Setup(v => v.IsNameValid(It.IsAny<string>())).Returns("Name");
            _validatorMock.Setup(v => v.IsLastnameValid(It.IsAny<string>())).Returns("Lastname");
            _validatorMock.Setup(v => v.IsPositionValid(It.IsAny<string>())).Returns("Position");

            var userDataContext = new UserDataContext(_validatorMock.Object, _passwordCryptMock.Object);

            try
            {
                userDataContext.Login = _user.Login;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Login"));
            }

            try
            {
                userDataContext.Password = _user.Password;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Password"));
            }

            try
            {
                userDataContext.Surname = _user.Surname;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Surname"));
            }

            try
            {
                userDataContext.Name = _user.Name;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Name"));
            }

            try
            {
                userDataContext.Lastname = _user.Lastname;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Lastname"));
            }

            try
            {
                userDataContext.Position = _user.Position;
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("Position"));
            }
        }

        [Test]
        public void PasswordEditContextAddingNewPasswordTest()
        {
            var passwordEditContext = new PasswordEditContext(_validatorMock.Object, _passwordCryptMock.Object);

            Assert.That(passwordEditContext.NewPassword, Is.EqualTo(""));

            var propertyHasChanged = false;

            passwordEditContext.PropertyChanged += (sender, e) => propertyHasChanged = true;

            passwordEditContext.NewPassword = "Password";

            Assert.That(passwordEditContext.NewPassword, Is.EqualTo("Password"));
            Assert.That(propertyHasChanged, Is.True);
            _validatorMock.Verify(v => v.IsPasswordValid("Password"), Times.Once);

            _passwordCryptMock.Setup(pc => pc.GetHashString(It.IsAny<string>())).Returns("PasswordHashString");

            Assert.That(passwordEditContext.GetNewPasswordHash(), Is.EqualTo("PasswordHashString"));
        }

        [Test]
        public void PasswordEditContextAddingWrongPasswordMustThrowArgumentExceptionTest()
        {
            var passwordEditContext = new PasswordEditContext(_validatorMock.Object, _passwordCryptMock.Object);

            _validatorMock.Setup(v => v.IsPasswordValid(It.IsAny<string>())).Returns("PasswordError");

            try
            {
                passwordEditContext.NewPassword = "Password";
            }
            catch (Exception e)
            {
                Assert.That(e is ArgumentException, Is.True);
                Assert.That(e.Message, Is.EqualTo("PasswordError"));
            }
        }

        [Test]
        public void PasswordEditContextAddingStringTwiceMustDoNothingTest()
        {
            var passwordEditContext = new PasswordEditContext(_validatorMock.Object, _passwordCryptMock.Object);

            var passwordString = "Password";

            passwordEditContext.NewPassword = passwordString;
            passwordEditContext.NewPassword = "Password";

            Assert.That(ReferenceEquals(passwordEditContext.NewPassword, passwordString), Is.True);
        }
    }
}