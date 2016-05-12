namespace Presentation
{
    public interface IPasswordCrypt
    {
        string GetHashString(string password);

        bool IsPasswordValid(string passwordInput, string pwdHashFromDb);
    }
}