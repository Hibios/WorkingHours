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
using static WorkingHours.EF.Connection;

namespace WorkingHours.Pages
{
    public partial class OrganizatorProfile : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public OrganizatorProfile()
        {
            InitializeComponent();

            mainWindow.ResizeMode = ResizeMode.CanResize;
            txbFioRole.Text = $"{UserForRole.userForRole.MiddleName} {UserForRole.userForRole.FirstName} {UserForRole.userForRole.Patronymic} | {UserForRole.userForRole.Role.RoleName}";
            txbOrg.Text = $"Организация: {UserForRole.userForRole.Organization.Name}";
            txbBirthday.Text = $"Дата рождения: {UserForRole.userForRole.DateBirthday.ToShortDateString()}";

            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";
            txtbBaseFolder.Text = UserForRole.BaseFolder;

            if (UserForRole.userForRole.isOrganizationOwner == false)
            {
                MainMenu.Children.Remove(btnStaff);
                profileGrid.Children.Remove(folderChanger);
            }
            NManager.CheckNotifications(btnNotification, this);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showAlert("Вы действительно хотите выйти?", "Сообщение", "question");
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new MainWindow();
            var editPage = new EditProfilePage();
            editWindow.Height = 450;
            editWindow.Width = 400;
            editPage.Title = "Редактировать профиль";
            editWindow.MainFrame.Navigate(editPage);
            editWindow.ShowDialog();
            this.NavigationService.Refresh();
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }

        private void btnVideoFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                txtbBaseFolder.Text = openFileDlg.SelectedPath;
                UserForRole.BaseFolder = openFileDlg.SelectedPath;
                UserForRole.ChangeVideoManager();
            }
        }
    }
}