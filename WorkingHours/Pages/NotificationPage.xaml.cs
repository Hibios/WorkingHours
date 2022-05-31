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
    public partial class NotificationPage : Page
    {
        public ListCollectionView _cview;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public NotificationPage()
        {
            InitializeComponent();

            this._cview = new ListCollectionView(db.Notifications.Where(n => n.IdRecipient == UserForRole.userForRole.IdUser).ToList());
            this.DataContext = _cview;
            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";

            if (notificationBox.Items.Count != 0)
            {
                foreach (ListViewItem nt in notificationBox.Items)
                {
                    Button btn = nt.Template.FindName("okButton", nt) as Button;
                    btn.Visibility = Visibility.Hidden;
                    Console.WriteLine(btn);
                }
            }

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

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            Notification notification = (Notification)notificationBox.SelectedItem;
            if (notification.IdTypeNotification == 1)
            {
                User user = db.Users.FirstOrDefault(usr => usr.IdUser == notification.IdSender);
                NManager.AcceptNotificationRequest(user, notification);
                _cview.Refresh();
                this.DataContext = _cview;
                this.NavigationService.Refresh();
            }
            else
            {
                NManager.AcceptNotification(notification);
                this.DataContext = _cview;
                this.NavigationService.Refresh();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Notification notification = (Notification)notificationBox.SelectedItem;
            User user = db.Users.FirstOrDefault(usr => usr.IdUser == notification.IdSender);
            int Id = notification.IdNotification;
            NManager.RejectNotificationRequest(user, notification);
            _cview = new ListCollectionView(db.Notifications.Where(n => n.IdNotification != Id).ToList());
            this.DataContext = _cview;
            this.NavigationService.Refresh();
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }

        private void addNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new MainWindow();
            var addPage = new AddNotificationPage();
            addWindow.Height = 350;
            addWindow.Width = 400;
            addPage.Title = "Отправка уведомления";
            addWindow.MainFrame.Navigate(addPage);
            addWindow.ShowDialog();
            this.NavigationService.Refresh();
        }
    }
}
