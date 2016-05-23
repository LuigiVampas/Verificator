using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    /// <summary>
    /// ��������� ����, ����������� �� ��������� ������ ������������.
    /// </summary>
    public interface IPasswordEditView : IDialogView
    {
        /// <summary>
        /// �������� ��� ����� �������� ������������� ��� ��������� ������.
        /// </summary>
        IPasswordEditContext PasswordDataContext { get; set; }
    }
}