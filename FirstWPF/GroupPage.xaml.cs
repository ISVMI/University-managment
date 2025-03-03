using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public List<Student> studentsList { get; set; } = [];
        public GroupPage()
        {
            InitializeComponent();
        }

        private void GBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindow().mainPage);
        }

        private Group GetNewGroup()
        {
            if (ComboCourse.Text == null
                || ComboSpec.Text == null
                || ComboGroup.Text == null)
            {
                return null;
            }
            else if (Speciality.Text == ""
                && ComboSpec.SelectedIndex == 0
                && ComboGroup.SelectedIndex == 0
                && ComboCourse.SelectedIndex == 0)
            {
                return null;
            }
            string groupCode;
            groupCode = ComboCourse.Text + ComboSpec.Text + ComboGroup.Text;
            string groupSpeciality = Speciality.Text;
            List<Student> studentList = [];
            foreach (var item in StudentList.SelectedItems)
            {
                studentList.Add((Student)item);
            }
            Group group = new()
            {
                GroupName = groupCode,
                Speciality = groupSpeciality,
                Students = studentList
            };
            return group;
        }
        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            Group group;
            if (GetNewGroup() == null)
            {
                MessageBox.Show("Вы ввели не все необходимые данные!");
                return;
            }
            group = GetNewGroup();
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            if (UniversityDb.Groups
                .Any(g => g.GroupName == group.GroupName)
                )
            {
                MessageBoxResult result = MessageBox.
                    Show("Данная группа уже существует, вы хотели добавить в неё студентов?"
                    , "Внимание!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            var groupToChange = UniversityDb.Groups.Entry(group).Entity;
                            groupToChange.Students.AddRange(group.Students);
                            UniversityDb.SaveChanges();
                            foreach (var student in group.Students)
                            {
                                if (student.Group == null) 
                                { 
                                studentsList.Remove(student);
                                }
                            }
                            MessageBox.Show("Студенты успешно добавлены");
                            return;
                        }
                    case MessageBoxResult.No:
                        {
                            return;
                        }
                }
                return;
            }
            UniversityDb.Groups.AddAsync(group);
            UniversityDb.SaveChanges();
            MessageBox.Show($"Успешно добавлена группа {group.GroupName} !");
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var students = UniversityDb.Students
                .Select(s=>s)
                .Where(s=>s.Group == null)
                .ToList();
            if (students != null)
            {
                foreach (var student in students)
                {
                    studentsList.Add(student);
                }
            }
            var specialities = UniversityDb.Groups
                .Select(s => s.Speciality).Distinct().ToList();
            SpecialityBox.ItemsSource = specialities;
            StudentList.ItemsSource = studentsList;
        }

        private void SpecialityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialityBox.SelectedIndex != -1)
            {
                Speciality.Text = SpecialityBox.SelectedValue.ToString();
            }
        }

        private void CancelG_Click(object sender, RoutedEventArgs e)
        {
            ComboCourse.SelectedIndex = 0;
            ComboGroup.SelectedIndex = 0;
            ComboSpec.SelectedIndex = 0;
            Speciality.Text = "";
            SpecialityBox.SelectedIndex = -1;
        }
    }
}
