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

namespace WorkingHours.Pages
{
    public partial class AddBreakPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public AddBreakPage()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtbDescription.Text != "" && txtbDuration.Text != "")
            {
                StaffControl.AddBreak(txtbDescription.Text, txtbDuration.Text);
                mainWindow.showAlert("Перерыв добавлен!");
            }
            else
            {
                mainWindow.showAlert("Не все поля заполнены");
            }
        }
    }
}
