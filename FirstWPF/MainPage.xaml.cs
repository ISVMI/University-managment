using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        public MainPage()
        {
            InitializeComponent();
        }

        private void TableBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case 0: { LoadGroupsGridAsync(); break; }
                case 1: { LoadStudentsGridAsync(); break; }
            }
        }
        private void LoadStudentsGridAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var dbStudents = UniversityDb.Students.ToList();
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = dbStudents;
        }
        private void LoadGroupsGridAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var dbGroups = UniversityDb.Groups.ToList();
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = dbGroups;
            //List <string> students = [];
            //foreach (var group in dbGroups) 
            //{
            //    foreach (var student in group.Students) 
            //    { students.Add(student.ToString()); }
            //}
            //DataColumn dc = new ("Студенты", typeof(string));
            //DbGrid.Columns.Add(dc);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case -1: { MessageBox.Show("Выберите таблицу!"); return; }   
                case 0: { MainFrame.Navigate(new GroupPage()); break; }
                case 1: { MainFrame.Navigate(new StudentPage()); break; }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var groups = UniversityDb.Groups;
            var students = UniversityDb.Students;
            if (DbGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной записи!");
                return;
            }
            else if(TableBox.Text == "Группы")
            {
                var groupItem = DbGrid.SelectedItems;
                foreach (Group item in groupItem) 
                {
                    if (groups.Any(g => g.Id == item.Id))
                    { groups.Remove(item);  }

                }
                UniversityDb.SaveChanges();
                DbGrid.ItemsSource = groups.ToList();
            }
            else if (TableBox.Text == "Студенты")
            {
                var studentItem = DbGrid.SelectedItems;
                foreach (Student item in studentItem)
                if (UniversityDb.Students.Any(i => i.Id == item.Id))
                { UniversityDb.Students.Remove(item); }
                UniversityDb.SaveChanges();
                DbGrid.ItemsSource = students.ToList();
            }
            

        }
    }
}
