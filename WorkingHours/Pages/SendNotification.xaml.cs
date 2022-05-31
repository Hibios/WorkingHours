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
using static WorkingHours.EF.Connection;
using WorkingHours.Modules;
using WorkingHours.Model;

namespace WorkingHours.Pages
{
    public partial class SendNotification : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public SendNotification()
        {
            InitializeComponent();
        }

        private void btnSendNotification_Click(object sender, RoutedEventArgs e)
        {
            User recipient = db.Users.FirstOrDefault(user => user.isOrganizationOwner == true && user.Email == txbOrgEmail.Text);
            if (recipient != null)
            {
                NManager.SendRequestNotification(recipient);
                this.NavigationService.Navigate(new Uri("Pages/WaitForAccept.xaml", UriKind.Relative));
            }
            else
            {
                mainWindow.showAlert("Предприятия с таким Email не существует");
            }
        }
    }
}
