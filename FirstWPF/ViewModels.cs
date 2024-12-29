using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FirstWPF
{
    public class StudentViewModel
    {
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Группа { get; set; }
        public DateTime Дата_Рождения { get; set; }
        public double Средний_Балл { get; set; }
    }
    public class GroupViewModel
    {
        public string Группа { get; set; }
        public string Специальность { get; set; }
        public string Студенты { get; set; }
    }
}
