namespace Presentation
{
    /// <summary>
    /// ��������� ����������, ������������ ������.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// �������� ������������ ������.
        /// </summary>
        /// <param name="login">�����</param>
        /// <returns>������, ���������� ��� ������� ������, ���� ������ ������, ���� ��� �����.</returns>
        string IsLoginValid(string login);

        /// <summary>
        /// �������� ������������ ������.
        /// </summary>
        /// <param name="password">������</param>
        /// <returns>������, ���������� ��� ������� ������, ���� ������ ������, ���� ��� �����.</returns>
        string IsPasswordValid(string password);

        /// <summary>
        /// �������� ������������ �����.
        /// </summary>
        /// <param name="name">���</param>
        /// <returns>������, ���������� ��� ������� �����, ���� ������ ������, ���� ��� �����.</returns>
        string IsNameValid(string name);

        /// <summary>
        /// �������� ������������ ��������.
        /// </summary>
        /// <param name="surname">��������</param>
        /// <returns>������, ���������� ��� ������� ��������, ���� ������ ������, ���� ��� �����.</returns>
        string IsSurnameValid(string surname);

        /// <summary>
        /// �������� ������������ �������.
        /// </summary>
        /// <param name="lastname">�������</param>
        /// <returns>������, ���������� ��� ������� �������, ���� ������ ������, ���� ��� �����.</returns>
        string IsLastnameValid(string lastname);

        /// <summary>
        /// �������� ������������ ���������
        /// </summary>
        /// <param name="position">���������</param>
        /// <returns>������, ���������� ��� ������� ���������, ���� ������ ������, ���� ��� �����</returns>
        string IsPositionValid(string position);

        /// <summary>
        /// �������� ������������ ���������.
        /// </summary>
        /// <param name="initials">�������������� ��������</param>
        /// <param name="name">���</param>
        /// <param name="surname">��������</param>
        /// <returns>������, ���������� ��� �������������� ��������� ���������, ���� ������ ������, ���� ��� �����.</returns>
        string AreInitialsValid(string initials, string name, string surname);
    }
}