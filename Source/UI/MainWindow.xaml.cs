using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Model;
using Presentation;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        private readonly MainWindowDataContext _itemsSource;

        public MainWindow()
        {
            InitializeComponent();
            _itemsSource = new MainWindowDataContext();
        }

        public event EventHandler LoadCompleted;

        public IList<User> Users 
        {
            get { return _itemsSource.Users.ToList(); } 
            set { _itemsSource.Users = new ObservableCollection<User>(value); } 
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }
    }
}
