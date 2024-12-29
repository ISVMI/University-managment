using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWPF
{
    public class UniversityService
    {
        private readonly UniversityContext _context;

        public UniversityService(UniversityContext context)
        {
            _context = context;
        }
        public async Task<List<StudentViewModel>> GetStudents() 
        {

            var students = await _context.Students
                .Select(s => new StudentViewModel
                {
                    Имя = s.Name,
                    Фамилия = s.Surname,
                    Группа = s.Group.GroupName
                })
                .ToListAsync();
            return students; 
        }

        public async Task<List<GroupViewModel>> GetGroups()
        {
            var groups = await _context.Groups
                .Select(g => new GroupViewModel
                {
                    Группа = g.GroupName,
                    Специальность = g.Speciality,
                    Студенты = ""
                })
                .ToListAsync();
            return groups;
        }
    }
}
