using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
            if (SurnameBox.Text.IsNullOrEmpty() ||
                NameBox.Text.IsNullOrEmpty() ||
                AvgMarkBox.Text.IsNullOrEmpty() ||
            BirthDate.Text.IsNullOrEmpty() ||
            GroupBox.SelectedIndex == -1)
            {
                MessageBox.Show($"Вы ввели не все данные!");
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
            Student student = new() { Surname = surname, Name = name, AvgMark = avgMark, BirthDate = birthDate };
            return student;
        }
        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student;
            if (GetNewStudent() == null) { return; }
            student = GetNewStudent();
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            try
            {
                var newStudent = UniversityDb.Groups
                    .Select(g => new Group()
                    {
                        Id = g.Id,
                        GroupName = g.GroupName,
                        Speciality = g.Speciality,
                        StudentId = g.StudentId,
                        Students = g.Students,
                    })
                    .Where(g => g.GroupName == GroupBox.SelectedValue.ToString())
                    .ToList();
                foreach (var groups in newStudent)
                {
                    groups.Students.Add(student);
                }
            }
            catch 
            {
                MessageBox.Show("Сначала добавьте хотя бы одну группу!"); 
                return;
            }
            UniversityDb.Students.AddAsync(student);
            UniversityDb.SaveChanges();
            MessageBox.Show($"Новый студент {student.Surname} {student.Name} успешно добавлен!");
        } 

        private async void Grid_Initialized(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            var groupNames = await UniversityDb.Groups.Select(g => g.GroupName).ToListAsync();
            GroupBox.ItemsSource = groupNames;
        }

        private void CancelS_Click(object sender, RoutedEventArgs e)
        {
            SurnameBox.Text = "";
            NameBox.Text = "";
            AvgMarkBox.Text = "";
            BirthDate.Text = "";
            GroupBox.SelectedIndex = -1;
        }

        private void AvgMarkBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры и одну запятую
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            // Регулярное выражение для проверки
            Regex regex = new (@"^[2-5]([,]\d{0,1})?$");
            e.Handled = !regex.IsMatch(fullText);
        }

        private void AvgMarkBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                AvgMarkBox.Text = AvgMarkBox.Text.Trim();
                AvgMarkBox.CaretIndex = AvgMarkBox.Text.Length;
            }
        }
    }
}
