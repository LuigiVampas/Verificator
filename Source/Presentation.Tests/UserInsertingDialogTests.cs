using System;
using Model;
using Moq;
using NUnit.Framework;
using Presentation.Contexts;
using Presentation.UserInserting;

namespace Presentation.Tests
{
    [TestFixture]
    public class UserInsertingDialogTests
    {
        private Mock<IUserDataContext> _userDataContextMock;
        private Mock<IUserInsertingDialogView> _userInsertingDialogViewMock; 
        private Func<IUserDataContext> _userDataContextFactory;

        [SetUp]
        public void SetUp()
        {
            _userDataContextMock = new Mock<IUserDataContext>();
            _userInsertingDialogViewMock = new Mock<IUserInsertingDialogView>();
            _userDataContextFactory = () => _userDataContextMock.Object;
        }
            
        [Test]
        public void ShowDialogTest()
        {
            var userInsertingDialogPresenter = new UserInsertingDialogPresenter(_userDataContextFactory);

            userInsertingDialogPresenter.View = _userInsertingDialogViewMock.Object;

            _userInsertingDialogViewMock.Setup(v => v.ShowDialog()).Returns(true);
            _userInsertingDialogViewMock.SetupGet(v => v.UserDataContext).Returns(_userDataContextMock.Object);

            var testUser = new User();

            _userDataContextMock.Setup(c => c.CreateUser(true)).Returns(testUser);

            Assert.That(userInsertingDialogPresenter.ShowDialog(), Is.True);
            Assert.That(ReferenceEquals(userInsertingDialogPresenter.User, testUser), Is.True);

            _userInsertingDialogViewMock.VerifySet(v => v.UserDataContext = _userDataContextMock.Object, Times.Once);
        }

        [Test]
        public void ShowDialogMustBeFalseTest()
        {
            var userInsertingDialogPresenter = new UserInsertingDialogPresenter(_userDataContextFactory);

            userInsertingDialogPresenter.View = _userInsertingDialogViewMock.Object;

            _userInsertingDialogViewMock.Setup(v => v.ShowDialog()).Returns(false);
            _userInsertingDialogViewMock.SetupGet(v => v.UserDataContext).Returns(_userDataContextMock.Object);
            
            Assert.That(userInsertingDialogPresenter.ShowDialog(), Is.False);

            _userInsertingDialogViewMock.VerifySet(v => v.UserDataContext = _userDataContextMock.Object, Times.Once);
        }
    }
}