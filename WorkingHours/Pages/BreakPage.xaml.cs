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
    public partial class BreakPage : Page
    {
        public ListCollectionView _cview;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public BreakPage()
        {
            InitializeComponent();

            UserForRole.breakList = db.Breaks.Where(b => b.IdTimeMonitoring == UserForRole.monitoring.IdTimeMonitoring).ToList();

            this._cview = new ListCollectionView(UserForRole.breakList);
            this.DataContext = _cview;
            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";

            if (UserForRole.userForRole.isOrganizationOwner == false)
            {
                MainMenu.Children.Remove(btnStaff);
            }

            NManager.CheckNotifications(btnNotification, this);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            Break breakT = (Break)breakBox.SelectedItem;
            StaffControl.UseBreak(breakT);
            _cview.Refresh();
            this.DataContext = _cview;
            this.NavigationService.Refresh();
        }

        private void addBreakButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new MainWindow();
            var addPage = new AddBreakPage();
            addWindow.Height = 300;
            addWindow.Width = 400;
            addPage.Title = "Добавление перерыва";
            addWindow.MainFrame.Navigate(addPage);
            addWindow.ShowDialog();
            this.NavigationService.Refresh();
        }
    }
}
