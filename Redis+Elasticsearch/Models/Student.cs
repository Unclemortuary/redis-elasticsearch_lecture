namespace Redis_Elasticsearch.Models
{
    public class Student
    {
        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public Student(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
    }
}
