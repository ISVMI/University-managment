using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    /// Логика взаимодействия для StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public StudentPage()
        {
            InitializeComponent();
        }

        private void SBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private Student GetNewStudent() 
        {
            string surname = SurnameBox.Text;
            string name = NameBox.Text;
            float avgMark;
            DateTime birthDate;
            string gName;
            if (SurnameBox.Text.IsNullOrEmpty() &&
                NameBox.Text.IsNullOrEmpty() &&
                AvgMarkBox.Text.IsNullOrEmpty() &&
            BirthDate.Text.IsNullOrEmpty() &&
            GroupBox.SelectedIndex == -1)
            {
                MessageBox.Show($"Вы не ввели данные!");
                return null;
            }
            try
            {
                avgMark = Convert.ToSingle(AvgMarkBox.Text);
            }
            catch
            {
                MessageBox.Show($"Некорректный формат оценки!");
                AvgMarkBox.Text = "";
                return null;
            }
            try
            {
                birthDate = Convert.ToDateTime(BirthDate.Text);
            }
            catch
            {
                MessageBox.Show($"Ошибка ввода даты!");
                BirthDate.Text = "";
                return null;
            }
            try
            {
                gName = GroupBox.SelectedValue.ToString();
            }
            catch
            {
                MessageBox.Show($"Выберите группу!");
                GroupBox.SelectedIndex = -1;
                return null;
            }
            Group sGroup = new() { GroupName = gName, UniversityId = 1 };
            Student student = new() { Surname = surname, Name = name, AvgMark = avgMark, BirthDate = birthDate, Group = sGroup };
            return student;
        }
        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student;
            if (GetNewStudent() == null) { return; }
            student = GetNewStudent();
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityDb.Students.AddAsync(student);
            UniversityDb.SaveChanges();
        }

        private async void Grid_Initialized(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var groupNames = await UniversityDb.Groups.Select(g => g.GroupName).ToListAsync();
            GroupBox.ItemsSource = groupNames;
        }
    }
}
