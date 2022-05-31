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

namespace WorkingHours.Pages
{
    public partial class QuestionPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public QuestionPage()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            wnd.DialogResult = true;
        }
    }
}
