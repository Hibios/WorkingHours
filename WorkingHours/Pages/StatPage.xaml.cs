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
using WorkingHours.Model;
using WorkingHours.Modules;


namespace WorkingHours.Pages
{
    public partial class StatPage : Page
    {
        public StatPage(string userId)
        {
            InitializeComponent();

            User targetUser = db.Users.FirstOrDefault(user => user.IdUser.ToString() == userId);

            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";
            NManager.CheckNotifications(btnNotification, this);


            statBox.ItemsSource = db.TimeMonitorings.Where(tm => tm.IdUser == targetUser.IdUser).ToList();
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }
    }
}
