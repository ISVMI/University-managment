using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client.Extensions.Msal;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics.Eventing.Reader;


namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        UniversityContext _context;
        public MainPage(UniversityContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private void TableBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case 0: { DbGrid.ItemsSource = _context.Groups.ToList(); LoadGroupsGrid(); break; }
                case 1: { LoadStudentsGrid(); break; }
            }
        }
        private void LoadStudentsGrid()
        {
            var dbStudents = _context.Students;
            ObservableCollection<Student> students = new(dbStudents);
            DbGrid.ItemsSource = students;
        }
        private void LoadGroupsGrid()
        {
            var dbGroups = _context.Groups.Local.ToObservableCollection();
            ObservableCollection<Group> groups = new(dbGroups);
            DbGrid.ItemsSource = groups;
            DbGrid.Columns[2].Visibility = Visibility.Hidden;
            DbGrid.Columns[3].Visibility = Visibility.Hidden;
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
                case 0: { this.NavigationService.Navigate(new GroupPage()); break; }
                case 1: { this.NavigationService.Navigate(new StudentPage()); break; }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var groups = _context.Groups;
            var students = _context.Students;
            if (DbGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной записи!");
                return;
            }
            else if (TableBox.Text == "Группы")
            {
                var groupItems = DbGrid.SelectedItems;
                if (groupItems.Count > 0)
                {
                    try
                    {
                        foreach (Group item in groupItems)
                        {
                            if (groups.Any(g => g.Id == item.Id))
                            { groups.Remove(item); }

                        }
                        _context.SaveChanges();
                        LoadGroupsGrid();
                    }
                    catch
                    {
                        MessageBox.Show("В выбранной вами строке нет данных");
                    }
                }
                
            }
            else if (TableBox.Text == "Студенты")
            {
                var studentItems = DbGrid.SelectedItems;
                if (studentItems != null)
                {
                    try
                    {
                        foreach (Student item in studentItems)
                            if (_context.Students.Any(i => i.Id == item.Id))
                            { _context.Students.Remove(item); }
                        _context.SaveChanges();
                        DbGrid.Items.Refresh();
                        LoadStudentsGrid();
                    }
                    catch 
                    {
                        MessageBox.Show("В выбранной вами строке нет данных");
                    }
                }
            }
        }

        /// <summary>
        /// ObservableCollection
        /// </summary>
        private void DbGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var groups = _context.Groups;
            var students = _context.Students;
            if (TableBox.Text == "Студенты")
            {
                try
                {
                    var newStudent = e.Row.Item as Student;
                    var existingStudent = students.FirstOrDefault(s => s.Id == newStudent.Id);
                    var newValue = (e.EditingElement as TextBox).Text;
                    if (existingStudent != null)
                    {
                        switch (e.Column.Header.ToString())
                        {
                            case "Surname": existingStudent.Surname = newValue; break;
                            case "Name": existingStudent.Name = newValue; break;
                            case "AvgMark":
                                newValue = newValue.Replace('.', ',');
                                if (Double.TryParse(newValue, out var mark))
                                {
                                    existingStudent.AvgMark = mark;
                                }
                                else
                                {
                                    MessageBox.Show("Задайте оценку правильно!");
                                }
                                break;
                            case "BirthDate":
                                try
                                {
                                    string day = newValue.Split('/')[1];
                                    string month = newValue.Split('/')[0];
                                    string year = newValue.Split('/')[2];
                                    newValue = $"{day}.{month}.{year}";
                                    existingStudent.BirthDate = Convert.ToDateTime(newValue);
                                }
                                catch
                                {
                                    MessageBox.Show("Введите дату в правильном формате!");
                                }
                                break;
                        }
                        _context.Students.Update(existingStudent);
                        _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка доступа к экземпляру базы данных! {ex.Message}");
                }
            }
            else if (TableBox.Text == "Группы")
            {
                try
                {
                    var newGroup = e.Row.Item as Group;
                    var existingGroup = groups.Find(newGroup.Id);
                    var newValue = (e.EditingElement as TextBox).Text;
                    if (existingGroup != null)
                    {
                        switch (e.Column.Header.ToString())
                        {
                            case "GroupName": existingGroup.GroupName = newValue; break;
                            case "Speciality": existingGroup.Speciality = newValue; break;
                        }
                        _context.Groups.Update(existingGroup);
                        _context.SaveChangesAsync();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка доступа к экземпляру базы данных! {ex.Message}");
                }
            }
        }
    }
}
