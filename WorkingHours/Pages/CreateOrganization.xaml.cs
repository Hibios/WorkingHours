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
using WorkingHours.Model;
using WorkingHours.Modules;

namespace WorkingHours.Pages
{
    public partial class CreateOrganization : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public CreateOrganization()
        {
            InitializeComponent();

            List<string> items = new List<string>();
            foreach (OrganizationType orgType in db.OrganizationTypes.AsEnumerable()) { items.Add(orgType.TypeName); }
            comboOrgType.ItemsSource = items;
        }

        private void btnCreateOrg_Click(object sender, RoutedEventArgs e)
        {
            if (txbOrgName.Text != "" && comboOrgType.Text != "")
            {
                if (db.Organizations.Where(org => org.Name == txbOrgName.Text).ToList().Count() > 0)
                {
                    mainWindow.showAlert("Предприятие с таким названием уже существует!");
                }
                else {
                    AuthModules.CreateOrganization(UserForRole.userForRole, txbOrgName.Text, db.OrganizationTypes.Where(i => i.TypeName == (string)comboOrgType.SelectedItem).FirstOrDefault());
                    mainWindow.showAlert("Вы полностью создали профиль!");
                    this.NavigationService.Navigate(new Uri("Pages/SignInPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                mainWindow.showAlert("Заполните все поля!");
            }
        }
    }
}
