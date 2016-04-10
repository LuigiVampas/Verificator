using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Presentation;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        private MainWindowDataContext _itemsSource;

        public MainWindow()
        {
            InitializeComponent();
            InitializeItemsSource(new MainWindowDataContext());
            
        }

        public void InitializeItemsSource(MainWindowDataContext itemsSource)
        {
            _itemsSource = itemsSource;
            UsersList.ItemsSource = _itemsSource.Users;
        }

        public IList<User> Users 
        {
            get { return _itemsSource.Users.ToList(); } 
            set { _itemsSource.Users = new ObservableCollection<User>(value); } 
        }

        private void DataGridLoaded(object sender, EventArgs e)
        {
            _itemsSource.Users.Add(new User { Login = "Login", Name = "Name", Soname = "Soname", LastName = "LastName", Password = "Password", Position = "Position" });
        }
    }
}
