using System.Linq;
using System.Windows;
using Presentation.Services;

namespace UI
{
    public class ActiveControlProvider : IActiveControlProvider
    {
        public object ActiveControl
        {
            get { return Application.Current.Windows.Cast<Window>().SingleOrDefault(w => w.IsActive); }
        }
    }
}
