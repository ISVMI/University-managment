using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static int StudentId { get; private set; }
        public static int GroupId { get; private set; }
        public MainPage()
        {
            InitializeComponent();
        }

        public int GetStudentId() => StudentId;
        public int GetGroupId() => GroupId;
        private void TableBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case 0: { LoadGroupsGrid(); break; }
                case 1: { LoadStudentsGrid(); break; }
            }
        }
        private void LoadStudentsGrid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityService universityService = new(UniversityDb);
            var students = universityService.GetStudents();
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = students;
            StudentId = 2;
            StudentId += DbGrid.Columns.Count;
        }
        private void LoadGroupsGrid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityService universityService = new(UniversityDb);
            var groups = universityService.GetGroups();
            var students = universityService.GetStudents();
            foreach (var student in students)
            {
                switch (student.Группа)
                {
                    case "121": groups[0].Студенты += student.Фамилия + " " + student.Имя + " "; break;
                    case "131": groups[1].Студенты += student.Фамилия + " " + student.Имя + " "; break;
                    case "141": groups[2].Студенты += student.Фамилия + " " + student.Имя + " "; break;
                }
            }
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = groups;
            GroupId = 1;
            GroupId += DbGrid.Columns.Count;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (TableBox.SelectedIndex == 0)
                MainFrame.Navigate(new StudentPage());
            MainFrame.Navigate(new GroupPage());
        }
    }
}
