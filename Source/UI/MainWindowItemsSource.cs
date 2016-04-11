using System.Collections.ObjectModel;
using Model;

namespace UI
{
    public class MainWindowItemsSource
    {
        public MainWindowItemsSource()
        {
            Users = new ObservableCollection<User>();
        }
        
        public ObservableCollection<User> Users { get; set; } 
    }
}
