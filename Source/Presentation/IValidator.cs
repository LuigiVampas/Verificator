namespace Presentation
{
    public interface IValidator
    {
        string IsLoginValid(string login);

        string IsPasswordValid(string password);

        string IsNameValid(string name);

        string IsSurnameValid(string surname);

        string IsLastnameValid(string lastname);

        string IsPositionValid(string position);

        string AreInitialsValid(string initials, string name, string surname);
    }
}