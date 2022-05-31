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
    public partial class StaffPage : Page
    {
        public ListCollectionView _cview;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public StaffPage()
        {
            InitializeComponent();

            this._cview = new ListCollectionView(db.Users.Where(user => user.IdOrganization == UserForRole.userForRole.IdOrganization && user.IdUser != UserForRole.userForRole.IdUser).ToList());
            this.DataContext = _cview;
            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";

            NManager.CheckNotifications(btnNotification, this);
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }

        private void trashButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)staffBox.SelectedItem;
            int Id = user.IdUser;
            StaffControl.employeeDismiss(user);
            _cview = new ListCollectionView(db.Users.Where(u => u.IdOrganization == UserForRole.userForRole.IdOrganization && u.IdUser != UserForRole.userForRole.IdUser && u.IdUser != Id).ToList());
            this.DataContext = _cview;
        }

        private void videoButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)staffBox.SelectedItem;
            this.NavigationService.Navigate(new VideoListPage(user.IdUser.ToString()));
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }

        private void statButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)staffBox.SelectedItem;
            this.NavigationService.Navigate(new VideoListPage(user.IdUser.ToString()));
        }
    }
}
