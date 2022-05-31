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

namespace WorkingHours.Pages
{
    public partial class RolePage : Page
    {
        public RolePage()
        {
            InitializeComponent();
        }

        private void btnOrganizator_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/CreateOrganization.xaml", UriKind.Relative));
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/SendNotification.xaml", UriKind.Relative));
        }
    }
}
