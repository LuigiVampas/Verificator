using Model;
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
        User User { get; }
    }
}