using System;
using System.Collections.Generic;
using Model;
using Moq;
using NUnit.Framework;
using Presentation.Contexts;
using Presentation.UserDeleting;
using Presentation.UserEdit;
using Presentation.UserInserting;
using Presentation.UserList;

namespace Presentation.Tests
{
    [TestFixture]
    public class UserListTests
    {
        private UserListPresenter _userListPresenter;
        private Mock<IMainView> _mainViewMock; 
        private Mock<IUserInsertingDialogPresenter> _userInsertingDialogPresenterMock;
        private Mock<IUserDeletingDialogPresenter> _userDeletingDialogPresenterMock;
        private Mock<IUserEditDialogPresenter> _userEditDialogPresenterMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserDataContext> _userDataContextMock;
        private Func<IUserDataContext> _userDataContextFactory;
        private List<User> _usersInRepository;
        private List<IUserDataContext> _userContextsFromView;
        
        [SetUp]
        public void SetUp()
        {
            _mainViewMock = new Mock<IMainView>();
            _userInsertingDialogPresenterMock = new Mock<IUserInsertingDialogPresenter>();
            _userDeletingDialogPresenterMock = new Mock<IUserDeletingDialogPresenter>();
            _userEditDialogPresenterMock = new Mock<IUserEditDialogPresenter>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userDataContextMock = new Mock<IUserDataContext>();
            _userDataContextFactory = () => _userDataContextMock.Object;

            _usersInRepository = new List<User>
            {
                new User { Id = 0, Login = "0" },
                new User { Id = 1, Login = "1" }
            };
            _userContextsFromView = new List<IUserDataContext>();

            _userListPresenter = new UserListPresenter(_userInsertingDialogPresenterMock.Object, _userDeletingDialogPresenterMock.Object,
                _userEditDialogPresenterMock.Object, _userRepositoryMock.Object, _userDataContextFactory);

            InitializePresenter();
        }

        private void InitializePresenter()
        {
            _userRepositoryMock.Setup(r => r.GetAllUsers()).Returns(_usersInRepository);

            _mainViewMock.SetupGet(v => v.Users).Returns(_userContextsFromView);

            _userListPresenter.View = _mainViewMock.Object;

            _mainViewMock.Raise(v => v.LoadCompleted += null, EventArgs.Empty);

            _userDataContextMock.Verify(c => c.Initialize(It.IsAny<User>()), Times.Exactly(_usersInRepository.Count));
            Assert.That(_userContextsFromView.Count, Is.EqualTo(_usersInRepository.Count));

            _userContextsFromView.Clear();
        }

        [Test]
        public void InsertingUserTest()
        {
            var insertingUser = new User();

            _userInsertingDialogPresenterMock.Setup(p => p.ShowDialog()).Returns(true);
            _userInsertingDialogPresenterMock.SetupGet(p => p.User).Returns(insertingUser);

            _mainViewMock.Raise(v => v.InsertingUser += null, EventArgs.Empty);

            _userRepositoryMock.Verify(r => r.AddUser(insertingUser), Times.Once);
            _userDataContextMock.Verify(c => c.Initialize(insertingUser), Times.Once);
            Assert.That(_userContextsFromView.Count, Is.EqualTo(1));
        }

        [Test]
        public void EditingUserTest()
        {
            _mainViewMock.SetupGet(v => v.SelectedUser).Returns(_userDataContextMock.Object);

            _mainViewMock.Raise(v => v.EditingUser += null, EventArgs.Empty);
        }
    }
}