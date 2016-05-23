namespace Presentation.Validation
{
    /// <summary>
    /// ��������� ���������� ������
    /// </summary>
    public interface IPasswordCrypt
    {
        /// <summary>
        /// ���������� ������ ����������� MD5.
        /// </summary>
        /// <param name="password">������</param>
        /// <returns>MD5-���, ������ �� ����������� ������ "PasswordHashingTries" (app.config) ���.</returns>
        string GetHashString(string password);

        /// <summary>
        /// �������� ���������� ��������� ������ � ��� ������������� ����� �� ���� ������
        /// </summary>
        /// <param name="passwordInput">������ � ���������� ����</param>
        /// <param name="passwordHashFromDatabase">MD5-���, ������ �� ����������� ������ "PasswordHashingTries" (app.config) ���, ������� ��������� � ��.</param>
        /// <returns>True - ���� ������ �������, False - ���� ���.</returns>
        bool IsPasswordValid(string passwordInput, string passwordHashFromDatabase);
    }
}