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
    public partial class EditProfilePage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public EditProfilePage()
        {
            InitializeComponent();

            txtbFio.Text = $"{UserForRole.userForRole.MiddleName} {UserForRole.userForRole.FirstName} {UserForRole.userForRole.Patronymic}";
            txtbEmail.Text = UserForRole.userForRole.Email;
            dateBox.Text = UserForRole.userForRole.DateBirthday.ToShortDateString();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (db.Users.Where(i => i.Email == txtbEmail.Text && i.IdUser != UserForRole.userForRole.IdUser).ToList().Count == 0)
            {
                if (passwBox2.Password == UserForRole.userForRole.Password)
                {
                    if (AuthModules.IsValidEmail(txtbEmail.Text))
                    {
                        AuthModules.UpdateUser(passwBox.Password, txtbFio.Text, txtbEmail.Text, dateBox.SelectedDate.Value.Date);
                        mainWindow.showAlert("Данные обновлены");
                    }
                    else
                    {
                        mainWindow.showAlert("Некорректный Email!");
                    }
                }
                else
                {
                    mainWindow.showAlert("Вы неверно ввели текущий пароль");
                }
            }
            else
            {
                mainWindow.showAlert($"Пользователь {txtbEmail.Text} уже существует!");
            }
        }
    }
}
