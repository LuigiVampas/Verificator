using Model;
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
        User User { get; }
    }
}