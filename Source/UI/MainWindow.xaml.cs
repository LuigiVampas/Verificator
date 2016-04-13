using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Model;
using Presentation.UserList;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        private readonly MainWindowItemsSource _itemsSource;

        public MainWindow()
        {
            Application.Current.MainWindow = this;
            InitializeComponent();
            _itemsSource = new MainWindowItemsSource();
        }

        public event EventHandler LoadCompleted;

        public event EventHandler InsertingUser;

        public event EventHandler DeletingUser;

        public IList<User> Users 
        {
            get { return _itemsSource.Users.ToList(); } 
            set { _itemsSource.Users = new ObservableCollection<User>(value); } 
        }

        public User SelectedUser
        {
            get
            {
                var selectedIndex = UsersList.SelectedIndex;
                if (selectedIndex == -1)
                    return null;

                return Users[selectedIndex];
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }

        private void InsertButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnInsertingUser();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnDeletingUser();
        }

        private void OnInsertingUser()
        {
            if (InsertingUser != null)
                InsertingUser(this, EventArgs.Empty);
        }

        private void OnDeletingUser()
        {
            if (DeletingUser != null)
                DeletingUser(this, EventArgs.Empty);
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
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
