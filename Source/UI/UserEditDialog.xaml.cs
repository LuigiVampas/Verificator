using Presentation;
using Presentation.UserEdit;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserEditDialog.xaml
    /// </summary>
    public partial class UserEditDialog : IUserEditDialogView
    {
        public UserEditDialog()
        {
            InitializeComponent();
        }

        public UserDataContext UserDataContext { get; set; }
    }
}
