using System.Collections.ObjectModel;
using Model;

namespace UI
{
    public class MainWindowDataContext
    {
        public MainWindowDataContext()
        {
            Users = new ObservableCollection<User>();
        }
        
        public ObservableCollection<User> Users { get; set; } 
    }
}
