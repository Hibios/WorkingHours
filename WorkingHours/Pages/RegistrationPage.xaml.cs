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
    public partial class RegistrationPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (db.Users.Where(i => i.Email == txtbEmail.Text).ToList().Count == 0)
            {
                if (passwBox.Password == passwBox2.Password)
                {
                    if (AuthModules.IsValidEmail(txtbEmail.Text))
                    {
                        AuthModules.AddNewUser(passwBox2.Password, txtbFio.Text, txtbEmail.Text, dateBox.SelectedDate.Value.Date);
                        mainWindow.showAlert($"Пользователь {txtbEmail.Text} зарегистрирован!");
                        this.NavigationService.Navigate(new Uri("Pages/SignInPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        mainWindow.showAlert("Некорректный Email!");
                    }
                }
                else
                {
                    mainWindow.showAlert("Пароли не совпадают");
                }
            }
            else
            {
                mainWindow.showAlert($"Пользователь {txtbEmail.Text} уже существует!");
            }
        }
    }
}
