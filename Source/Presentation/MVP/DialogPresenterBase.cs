namespace Presentation.MVP
{
    /// <summary>
    /// ������� ����� ����������, ����������� �� �������.
    /// </summary>
    /// <typeparam name="TView">���, ��������� � �����������.</typeparam>
    public class DialogPresenterBase<TView> :  PresenterBase<TView>, IDialogPresenter<TView> where TView : IDialogView
    {
        /// <summary>
        /// ��������� ������.
        /// </summary>
        /// <returns>true - ���� ������������ OK, false - � ��������� ������.</returns>
        public bool? RunDialog()
        {
            return View.ShowDialog();
        }
    }
}