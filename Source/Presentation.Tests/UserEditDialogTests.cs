using System;
using Model;
using Moq;
using NUnit.Framework;
using Presentation.Contexts;
using Presentation.PasswordEdit;
using Presentation.UserEdit;

namespace Presentation.Tests
{
    [TestFixture]
    public class UserEditDialogTests
    {
        private Mock<IPasswordEditPresenter> _passwordEditPresenterMock;
        private Mock<IUserEditDialogView> _userEditDialogViewMock;
        private Mock<IUserDataContext> _userDataContextMock; 
        private Func<IUserDataContext> _userDataContextFactory;

        [SetUp]
        public void SetUp()
        {
            _passwordEditPresenterMock = new Mock<IPasswordEditPresenter>();
            _userEditDialogViewMock = new Mock<IUserEditDialogView>();
            _userDataContextMock = new Mock<IUserDataContext>();
            _userDataContextFactory = () => _userDataContextMock.Object;
        }

        [Test]
        public void UserEditDialogRunDialogTest()
        {
            var userEditDialogPresenter = new UserEditDialogPresenter(_passwordEditPresenterMock.Object, _userDataContextFactory);

            Assert.That(userEditDialogPresenter.RunDialog(), Is.False);
        }

        [Test]
        public void UserEditDialogOnEditPasswordTest()
        {
            var userEditDialogPresenter = new UserEditDialogPresenter(_passwordEditPresenterMock.Object, _userDataContextFactory);

            userEditDialogPresenter.View = _userEditDialogViewMock.Object;
            _userEditDialogViewMock.Raise(v => v.LoadCompleted += null, EventArgs.Empty);

            var testUser = new User();
            _userDataContextMock.Setup(c => c.CreateUser(false)).Returns(testUser);

            _passwordEditPresenterMock.Setup(p => p.EditPassword(It.IsAny<string>())).Returns("Password");

            _userEditDialogViewMock.SetupGet(v => v.UserDataContext).Returns(_userDataContextMock.Object);
            _userEditDialogViewMock.Raise(v => v.EditPassword += null, EventArgs.Empty);

            _userDataContextMock.Verify(c => c.Initialize(testUser), Times.Once);
            Assert.That(testUser.Password, Is.EqualTo("Password"));
        }

        [Test]
        public void UserEditDialogEditUserTest()
        {
            var userEditDialogPresenter = new UserEditDialogPresenter(_passwordEditPresenterMock.Object, _userDataContextFactory);

            userEditDialogPresenter.View = _userEditDialogViewMock.Object;
            _userEditDialogViewMock.Raise(v => v.LoadCompleted += null, EventArgs.Empty);

            var editingUser = new User();
            var changedUser = new User { Password = "Another password"};

            _userEditDialogViewMock.Setup(v => v.ShowDialog()).Returns(true);
            _userEditDialogViewMock.SetupGet(v => v.UserDataContext).Returns(_userDataContextMock.Object);
            _userDataContextMock.Setup(c => c.CreateUser(false)).Returns(changedUser);

            Assert.That(userEditDialogPresenter.EditUser(editingUser), Is.EqualTo(changedUser));

            _userDataContextMock.Verify(c => c.Initialize(editingUser), Times.Once);
            _userEditDialogViewMock.VerifySet(v => v.UserDataContext = _userDataContextMock.Object, Times.Once);
        }

        [Test]
        public void UserEditDialogCancelEditingUserTest()
        {
            var userEditDialogPresenter = new UserEditDialogPresenter(_passwordEditPresenterMock.Object, _userDataContextFactory);

            userEditDialogPresenter.View = _userEditDialogViewMock.Object;
            _userEditDialogViewMock.Raise(v => v.LoadCompleted += null, EventArgs.Empty);

            var editingUser = new User();

            _userEditDialogViewMock.Setup(v => v.ShowDialog()).Returns(false);

            Assert.That(userEditDialogPresenter.EditUser(editingUser), Is.EqualTo(editingUser));

            _userDataContextMock.Verify(c => c.Initialize(editingUser), Times.Once);
            _userEditDialogViewMock.VerifySet(v => v.UserDataContext = _userDataContextMock.Object, Times.Once);
        }
    }
}