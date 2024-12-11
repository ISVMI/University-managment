using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
async static Task UnivercityAsync()
{
    // Создание университета

    University university = new University("Технический университет");

    // Создание групп
    Group group1 = new Group("202", "Программирование");
    Group group2 = new Group("212", "Экономика");
    Group group3 = new Group("222", "Юриспруденция");
    Group group4 = new Group("232", "Инженерное дело");

    // Создание студентов
    Student student1 = new("Иван", "Петров", new(2005, 11, 02), 3.5);
    Student student2 = new("Анна", "Сидорова", new(2005, 10, 07), 4);
    Student student3 = new("Пётр", "Иванов", new(20051105), 4.2);
    Student student4 = new("Денис", "Шацкий", new(2005, 03, 27), 5);
    Student student5 = new("Елена", "Полено", new(2005, 08, 08), 3.7);
    Student student6 = new("Елена", "Головач", new(2005, 01, 01), 3.3);
    Student student7 = new("Иван", "Казаров", new(2005, 11, 05), 5);
    Student student8 = new("Алексей", "Ляхов", new(2005, 02, 20), 4.1);
    Student student9 = new("Антонина", "Буженина", new(2005, 07, 07), 3.2);
    Student student10 = new("Анна", "Каревич", new(2005, 12, 12), 3.1);
    // Добавление групп в университет и их удаление из университета
    university.AddGroup(group1);
    university.AddGroup(group2);
    university.AddGroup(group3);
    university.AddGroup(group4);
    university.RemoveGroup(group4);
    university.AddGroup(group4);
    group1.AddStudent(student1);
    group1.AddStudent(student2);
    group1.AddStudent(student3);
    group2.AddStudent(student4);
    group2.AddStudent(student5);
    group3.AddStudent(student6);
    group3.AddStudent(student7);
    group4.AddStudent(student8);
    group4.AddStudent(student9);
    group4.AddStudent(student10);
    group4.RemoveStudent(student10);
    group2.AddStudent(student10);

    // Поиск и операции
    university.FindGroup("212");
    university.FindStudent("Экономика");
    group1.AvgGroup();
    student8.Print();
}
await UnivercityAsync();
enum Speciality
{
    ComputerScience,
    Economics,
    Law,
    Engineering
}

class Student
{
    public string Name;
    public string Surname;
    public DateTime BirthDate;
    public double AvgMark;
    public Student(string name, string surname, DateTime birthdate, double avgmark)
    {
        Name = name;
        Surname = surname;
        BirthDate = birthdate;
        AvgMark = avgmark;
    }
    public void Print()
    {
        Console.WriteLine($"Информация о студенте:");
        Console.WriteLine($"Имя: {Name}:");
        Console.WriteLine($"Фамилия: {Surname}");
        Console.WriteLine($"Дата рождения: {BirthDate.Day}.{BirthDate.Month}.{BirthDate.Year}");
        Console.WriteLine($"Средний балл: {AvgMark}");
    }
}

// Класс Group
class Group
{
    public string GroupName { get; set; }
    public List<Student> Students;
    public Speciality GroupSpeciality;
    public Group(string groupName, string gspeciality)
    {
        this.Students = new List<Student>();
        GroupName = groupName;
        switch (gspeciality)
        {
            case "Программирование": { GroupSpeciality = Speciality.ComputerScience; break; }
            case "Экономика": { GroupSpeciality = Speciality.Economics; break; }
            case "Юриспруденция": { GroupSpeciality = Speciality.Law; break; }
            case "Инженерное дело": { GroupSpeciality = Speciality.Engineering; break; }
            default: { Console.WriteLine("Неизвестная специальность"); break; }
        }
    }
    public void AddStudent(Student student)
    {
        if (Students.Count >= 25)
        throw new ("Достигнут максимальный размер группы (25 студентов)");
        if (student is null)
        throw new ("Студент не может быть пустым!");
        else
        this.Students.Add(student);
    }
    public void RemoveStudent(Student student)
    {
        if (Students is null)
            Console.WriteLine("В группе нет студентов!");
        else
        Students.Remove(student);
    }
    public void AvgGroup()
    {
        double sum = 0;
        int count = 1;
        foreach (Student s in this.Students)
        {
            sum += s.AvgMark;
            count++;
        }
        Console.WriteLine($"Средний балл группы {this.GroupName} = {sum / count}");
    }
}


class University
{
    public string UniversityName;
    List<Group> Groups;
    public University(string universityname)
    {
        UniversityName = universityname;
        this.Groups = new List<Group>();
    }
    public void AddGroup(Group group)
    {
        if (group is null)
            Console.WriteLine("Группа не может быть пустой!");
        else
            Groups.Add(group);
    }
    public void RemoveGroup(Group group)
    {
        if (Groups is null)
            Console.WriteLine("В университете нет групп!");
        else
            Groups.Remove(group);
    }
    public void FindGroup(string request)
    {
        int spec = -1;
        foreach (Group g in Groups)
        {
            if (g.GroupName == request)
            {
                Console.WriteLine($"Запрашиваемая группа: {request} найдена!");
                spec = (int)g.GroupSpeciality;
                switch (spec)
                {
                    case 0: { Console.WriteLine("Специальность группы: Программирование"); ; break; }
                    case 1: { Console.WriteLine("Специальность группы: Экономика"); ; break; }
                    case 2: { Console.WriteLine("Специальность группы: Юриспруденция"); ; break; }
                    case 3: { Console.WriteLine("Специальность группы: Инженерное дело"); ; break; }
                }  
            }
        }
        if (spec == -1) 
            Console.WriteLine($"Запрашиваемая группа {request} не найдена!");
        }
    public void FindStudent(string request)
    {
        int spec = -1;
        switch (request)
        {
            case "Программирование": { spec = 0; break; }
            case "Экономика": { spec = 1; break; }
            case "Юриспруденция": { spec = 2; break; }
            case "Инженерное дело": { spec = 3; break; }
            default: { spec = -1; break; }
        }
        if (spec >= 0)
        {
            foreach (Group g in Groups)
            {
                if ((int)g.GroupSpeciality == spec)
                {
                    Console.WriteLine($"Список студентов cпециальности {request}:");
                    for (int i = 0; i < g.Students.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. Студент:");
                        Console.WriteLine($"Имя: {g.Students[i].Name}");
                        Console.WriteLine($"Фамилия: {g.Students[i].Surname}");
                        Console.WriteLine($"Дата рождения: {g.Students[i].BirthDate.Day}.{g.Students[i].BirthDate.Month}.{g.Students[i].BirthDate.Year}");
                        Console.WriteLine($"Средний балл: {g.Students[i].AvgMark}");
                    }
                }
            }
        }
        else
            Console.WriteLine($"Группа по специальности '{request}' не найдена!");
        }
}
