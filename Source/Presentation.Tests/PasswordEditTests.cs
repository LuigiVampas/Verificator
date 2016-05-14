using System;
using Moq;
using NUnit.Framework;
using Presentation.Contexts;
using Presentation.PasswordEdit;

namespace Presentation.Tests
{
    [TestFixture]
    public class PasswordEditTests
    {
        private Mock<IPasswordEditContext> _passwordEditContextMock;
        private Mock<IPasswordEditView> _passwordEditViewMock;
        private Func<IPasswordEditContext> _passwordEditContextFactory; 

        [SetUp]
        public void SetUp()
        {
            _passwordEditContextMock = new Mock<IPasswordEditContext>();
            _passwordEditViewMock = new Mock<IPasswordEditView>();
            _passwordEditContextFactory = () => _passwordEditContextMock.Object;
        }
            
        [Test]
        public void PasswordEditPresenterEditingPasswordTest()
        {
            var passwordEditPresenter = new PasswordEditPresenter(_passwordEditContextFactory);
            passwordEditPresenter.View = _passwordEditViewMock.Object;

            _passwordEditViewMock.Setup(v => v.ShowDialog()).Returns(true);
            _passwordEditViewMock.SetupGet(v => v.PasswordDataContext).Returns(_passwordEditContextMock.Object);

            _passwordEditContextMock.Setup(c => c.GetNewPasswordHash()).Returns("NewPasswordHash");

            Assert.That(passwordEditPresenter.EditPassword("OldPassword"), Is.EqualTo("NewPasswordHash"));

            _passwordEditViewMock.Setup(v => v.ShowDialog()).Returns(false);

            _passwordEditViewMock.Verify(v => v.PasswordDataContext, Times.Once);
            Assert.That(passwordEditPresenter.EditPassword("OldPassword"), Is.EqualTo("OldPassword"));
        }
    }
}