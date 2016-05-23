using System.ComponentModel;

namespace Presentation.Contexts
{
    /// <summary>
    /// ��������� ��������� ������������� ��� ��������� ������.
    /// </summary>
    public interface IPasswordEditContext : INotifyPropertyChanged
    {
        /// <summary>
        /// �������� ��� ���������� ����� ������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string NewPassword { get; set; }

        /// <summary>
        /// ���������� ��� ������ ������.
        /// </summary>
        /// <returns>��� ������ ������.</returns>
        string GetNewPasswordHash();
    }
}