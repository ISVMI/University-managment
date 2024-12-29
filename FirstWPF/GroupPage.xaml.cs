using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
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

        private Group GetNewGroup()
        {
            string groupCode;
            if (ComboCourse.Text == null || ComboSpec.Text == null || ComboGroup.Text == null)
            {
                return null; 
            }
            groupCode = ComboCourse.Text + ComboSpec.Text + ComboGroup.Text;
            string groupSpeciality = Speciality.Text;
            Group group = new () { 
                GroupName = groupCode, 
                Speciality = groupSpeciality,  
                UniversityId = 1,
            };
            return group;
        }
        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            Group group;
            if (GetNewGroup() == null) { return; }
            group = GetNewGroup();
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityDb.Groups.AddAsync(group);
            UniversityDb.SaveChanges();
            MessageBox.Show($"Успешно добавлена группа {group.GroupName} !");
        }
    }
}
