using System;
using System.Collections.Generic;

namespace Elasticsearch.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Greetings { get; set; }
        public DateTime IntroductionDate { get; set; }
        public Bio Bio { get; set; }
        public IReadOnlyList<int> PreviousProjectIds { get; set; }
        public float PersonalScore { get; set; }
    }

    public class Bio
    {
        public string Passport { get; set; }
        public string DriverID { get; set; }
        public bool Female { get; set; }
    }
}
