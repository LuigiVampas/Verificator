using System;
using Moq;
using NUnit.Framework;
using Presentation.MVP;

namespace Presentation.Tests
{
    [TestFixture]
    public class MvpTests
    {
        private Mock<IView> _viewMock;
        private Mock<IDialogView> _dialogViewMock;
        
        [SetUp]
        public void SetUp()
        {
            _viewMock = new Mock<IView>();
            _dialogViewMock = new Mock<IDialogView>();
        }
            
        [Test]
        public void PresenterBaseInitializationTest()
        {
            var presenterBase = new PresenterBase<IView>();

            var loadViewCompletedEventWasCalled = false;
            presenterBase.LoadViewCompleted += (sender, e) => loadViewCompletedEventWasCalled = true;

            var presenter = (IPresenter) presenterBase;
            presenter.View = _viewMock.Object;

            Assert.That(ReferenceEquals(presenter.View, _viewMock.Object), Is.True);

            presenterBase.Initialize();

            presenterBase.Run();

            _viewMock.Verify(v => v.Show(), Times.Once);

            _viewMock.Raise(v => v.LoadCompleted += null, EventArgs.Empty);
            Assert.That(loadViewCompletedEventWasCalled, Is.True);
        }

        [Test]
        public void DialogPresenterBaseInitializationTest()
        {
            var dialogPresenterBase = new DialogPresenterBase<IDialogView>();

            dialogPresenterBase.View = _dialogViewMock.Object;

            dialogPresenterBase.RunDialog();

            _dialogViewMock.Verify(dv => dv.ShowDialog(), Times.Once);
        }
    }
}
