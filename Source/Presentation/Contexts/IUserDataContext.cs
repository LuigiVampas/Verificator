using System.ComponentModel;
using Model;

namespace Presentation.Contexts
{
    /// <summary>
    /// ��������� ��������� ������������� ��� ����� ������ � ������������.
    /// </summary>
    public interface IUserDataContext : INotifyPropertyChanged
    {
        /// <summary>
        /// ���������� id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// �������� ��� ���������� �����. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// �������� ��� ���������� ������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// �������� ��� ���������� �������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Surname { get; set; }

        /// <summary>
        /// �������� ��� ���������� ���. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// �������� ��� ���������� ��������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Lastname { get; set; }

        /// <summary>
        /// ���������� ��������.
        /// </summary>
        string Initials { get;  }

        /// <summary>
        /// �������� ��� ���������� ���������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        string Position { get; set; }

        /// <summary>
        /// �������������� �������� ����� �������������.
        /// </summary>
        /// <param name="user">����� ������������, ��������� � ������ ����������.</param>
        void Initialize(User user);

        /// <summary>
        /// ���������� ������������, ���������� � ������ ����������.
        /// </summary>
        /// <param name="needHash">����� �� ���������� ��� ������ ������.</param>
        /// <returns></returns>
        User CreateUser(bool needHash);
    }
}