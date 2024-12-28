using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace FirstWPF
{
    public class Group
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        // Внешний ключ для университета
        public int UniversityId { get; set; }
        // Навигационное свойство
        public University? University { get; set; }

        // Навигационное свойство для студентов
        public List<Student> Students = [];
        public string? Speciality { get; set; }
    }
}
