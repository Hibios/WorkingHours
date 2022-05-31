using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkingHours.Modules;

namespace WorkingHours.Pages
{
    public partial class WaitForAccept : Page
    {
        public WaitForAccept()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            UserForRole.Logout();
            this.NavigationService.Navigate(new Uri("Pages/SignInPage.xaml", UriKind.Relative));
        }
    }
}
