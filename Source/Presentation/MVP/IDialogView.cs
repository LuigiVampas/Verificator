namespace Presentation.MVP
{
    /// <summary>
    /// ��� �������.
    /// </summary>
    public interface IDialogView : IView
    {
        /// <summary>
        /// ���������� ��������� �������.
        /// </summary>
        bool? Result { get; }

        /// <summary>
        /// �������� ���������� ����.
        /// </summary>
        /// <returns>true - ���� ������������ OK, false - � ��������� ������.</returns>
        bool? ShowDialog();
    }
}