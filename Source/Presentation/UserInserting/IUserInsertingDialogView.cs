using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    /// <summary>
    /// ��������� ���� ������� ���������� ������������.
    /// </summary>
    public interface IUserInsertingDialogView : IDialogView
    {
        /// <summary>
        /// ����������, ������������ � �������, ������������ �� ���������� ����.
        /// </summary>
        UserDataContext UserDataContext { get; }
    }
}