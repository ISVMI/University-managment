using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Options;
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

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public GroupPage()
        {
            InitializeComponent();
        }

        private void GBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            /*string groupCode = ComboCourse.Text + ComboSpec.Text + ComboGroup.Text;
            string groupSpeciality = Speciality.Text;
            int groupId = MainPage.GroupId;
            Group group = new();
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityDb.Groups.Add*/
        }
    }
}
