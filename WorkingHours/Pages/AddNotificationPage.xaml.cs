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
    public partial class AddNotificationPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public AddNotificationPage()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtbDesc.Text != "" && txtbTitle.Text != "" && db.Users.FirstOrDefault(UserControl => UserControl.Email == txtbRecipient.Text) != null)
            {
                NManager.SendNotification(txtbTitle.Text, txtbDesc.Text, txtbRecipient.Text);
                mainWindow.showAlert("Уведомление отправлено!");
            }
            else
            {
                mainWindow.showAlert("Не все поля заполнены");
            }
        }
    }
}
