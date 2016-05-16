using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Presentation.Contexts;
using Presentation.UserList;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        /// <summary>
        /// Пользователи, показываемые в окне.
        /// </summary>
        private ObservableCollection<IUserDataContext> _users;

        /// <summary>
        /// Создаёт главный вид приложения.
        /// </summary>
        public MainWindow()
        {
            Application.Current.MainWindow = this;

            InitializeComponent();

            _users = new ObservableCollection<IUserDataContext>();
            UsersList.ItemsSource = _users;
        }

        /// <summary>
        /// Событие, сообщающее об окончании загрузки вида.
        /// </summary>
        public event EventHandler LoadCompleted;


        /// <summary>
        /// Событие добавления нового пользователя.
        /// </summary>
        public event EventHandler InsertingUser;

        /// <summary>
        /// Событие удаления пользователя.
        /// </summary>
        public event EventHandler DeletingUser;

        public event EventHandler EditingUser;

        /// <summary>
        /// Список пользователей, отображаемых на экране.
        /// </summary>
        public IList<IUserDataContext> Users 
        {
            get { return _users; }
            set
            {
                _users = new ObservableCollection<IUserDataContext>(value);
                UsersList.ItemsSource = _users;
            } 
        }

        /// <summary>
        /// Выбранный пользователь.
        /// </summary>
        public IUserDataContext SelectedUser
        {
            get
            {
                var selectedIndex = UsersList.SelectedIndex;
                if (selectedIndex < 0)
                    return null;

                return Users[selectedIndex];
            }
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            Close();
        }

        /// <summary>
        /// Обработчик стандартного события загрузки окна.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку добавления нового пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void InsertButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnInsertingUser();
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку удаления пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnDeletingUser();
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnEditingUser();
        }

        /// <summary>
        /// Трансляция стандарного события.
        /// </summary>
        private void OnInsertingUser()
        {
            if (InsertingUser != null)
                InsertingUser(this, EventArgs.Empty);
        }

        /// <summary>
        /// Трансляция стандарного события.
        /// </summary>
        private void OnDeletingUser()
        {
            if (DeletingUser != null)
                DeletingUser(this, EventArgs.Empty);
        }

        private void OnEditingUser()
        {
            if (EditingUser != null)
                EditingUser(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на клавишу клавиатуры.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)

            {
                case Key.Insert:
                    OnInsertingUser();
                    break;
                case Key.Delete:
                    OnDeletingUser();
                    break;
            }
        }
    }
}
