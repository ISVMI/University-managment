
namespace FirstWPF
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public double AvgMark { get; set; }
        public Group Group { get; set; }
        public override string ToString()
        {
            return this.Surname + " " + this.Name;
        }
    }
}
