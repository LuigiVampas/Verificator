using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    /// <summary>
    /// »нтерфейс вида диалога добавлени€ пользовател€.
    /// </summary>
    public interface IUserInsertingDialogView : IDialogView
    {
        /// <summary>
        /// ¬озвращает, пользовател€ с данными, заполненными на диалоговом окне.
        /// </summary>
        UserDataContext UserDataContext { get; }
    }
}