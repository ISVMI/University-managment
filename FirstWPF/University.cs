using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstWPF
{

    public class University
    {
        public int Id { get; set; }
        public string UniversityName { get; set; } = "Технический Университет";
        public List<Group> Groups = [];
    }

}
