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
using WorkingHours.Windows;
using WorkingHours.Pages;
using static WorkingHours.EF.Connection;
using WorkingHours.Modules;
using System.Windows.Forms;

namespace WorkingHours
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new SignInPage());
        }

        private void HideWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ResizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Если состояние окна нормальное (не свёрнутое и не расвёрнутое) растягиваем его на весь экран
            if (WindowState.ToString() == "Normal" && ResizeMode.ToString() != "NoResize")
            {
                //FormBorderStyle = FormBorderStyle.None;
                WindowState = WindowState.Maximized;
            }
            else
                // Иначе просто оставляем нормальным окно
                this.WindowState = WindowState.Normal;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            string[] notMainPages = new string[] { "MessagePage", "QuestionPage", "EditProfilePage", "AddBreakPage", "AddNotificationPage" };

            // Разлогиниваемся в случае если закрывается главное окно и мы входили в систему
            if (!notMainPages.Contains(MainFrame.NavigationService.Content.GetType().Name.ToString()) && UserForRole.userForRole != null)
            {
                UserForRole.Logout();
            }
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public void showAlert(string message, string title = "Сообщение", string alertType = "info")
        {
            var alertWindow = new MainWindow();
            if (alertType == "info")
            {
                var alertPage = new MessagePage();
                alertWindow.Height = 200;
                alertWindow.Width = 450;
                alertWindow.MinHeight = 200;
                alertWindow.MinWidth = 450;
                alertPage.Title = title;
                alertPage.alert_message.Text = message;
                alertWindow.MainFrame.Navigate(alertPage);
                alertWindow.ShowDialog();
            }
            else
            {
                var alertPage = new QuestionPage();
                alertWindow.Height = 200;
                alertWindow.Width = 450;
                alertWindow.MinHeight = 200;
                alertWindow.MinWidth = 450;
                alertPage.Title = title;
                alertPage.alert_message.Text = message;
                alertWindow.MainFrame.Navigate(alertPage);
                alertWindow.AppGrid.Background = Brushes.Red;
                bool? result = alertWindow.ShowDialog();
                if (result ?? true)
                {
                    UserForRole.Logout();
                    MainFrame.Navigate(new SignInPage());
                }
            }
        }
    }
}
