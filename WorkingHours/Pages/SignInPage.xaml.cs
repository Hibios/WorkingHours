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

namespace WorkingHours.Pages
{
    public partial class SignInPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public SignInPage()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var qwery = db.Users.Where(i => i.Email == txtbEmail.Text && i.Password == passwBox.Password).ToList();
            if (qwery.Count > 0)
            {
                UserForRole.Login(qwery.FirstOrDefault());
                // Организатор
                if (UserForRole.userForRole.isOrganizationOwner)
                {
                    this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
                }
                // Ожидающий сотрудник
                else if (UserForRole.userForRole.isVaitingForOrg && UserForRole.userForRole.IdOrganization == null)
                {
                    this.NavigationService.Navigate(new Uri("Pages/WaitForAccept.xaml", UriKind.Relative));
                }
                // Сотрудник предприятия
                else if (!UserForRole.userForRole.isVaitingForOrg && UserForRole.userForRole.IdOrganization != null)
                {
                    this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
                }
                // Новый пользователь
                else
                {
                    this.NavigationService.Navigate(new Uri("Pages/RolePage.xaml", UriKind.Relative));
                }
            }
            else
            {
                mainWindow.showAlert($"Пользователь {txtbEmail.Text} не найден!");
            }
        }
    }
}
