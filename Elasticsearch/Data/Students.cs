using Elasticsearch.Models;
using System;
using System.Collections.Generic;

namespace Elasticsearch.Data
{
    internal static class Students
    {
        public static IReadOnlyList<Student> GetStudents => new[]
        {
            new Student
            {
                Id = 1,
                Name = "Sergey",
                Surname = "Pethykov",
                IntroductionDate = new DateTime(2010, 5, 10),
                Greetings = "Hello! My name is Sergey and I'm glad to be a part of EPAM!",
                PersonalScore = 4.3f,
                PreviousProjectIds = new [] { 1, 2, 3 },
                Bio = new Bio { DriverID = "ase-123cews-2", Passport = "5814-2356-2347-9870", Female = false }
            },
            new Student
            {
                Id = 2,
                Name = "Andrey",
                Surname = "Tarynin",
                IntroductionDate = new DateTime(2008, 12, 14),
                Greetings = "My name is Andrey and it's nice to be here",
                PersonalScore = 4.3f,
                PreviousProjectIds = new [] { 1, 2, 3, 6 },
                Bio = new Bio { DriverID = "ase-133cews-2", Passport = "5814-2336-2347-9870", Female = false }
            },
            new Student
            {
                Id = 3,
                Name = "Alina",
                Surname = "Pyazok",
                IntroductionDate = new DateTime(2019, 1, 9),
                Greetings = "I'm just a movie maker",
                PersonalScore = 4.3f,
                PreviousProjectIds = new [] { 1, 3 },
                Bio = new Bio { DriverID = "ase-133cews-2", Passport = "5814-2336-2347-9870", Female = true }
            },
            new Student
            {
                Id = 4,
                Name = "Ivan",
                Surname = "Ivanov",
                IntroductionDate = new DateTime(2015, 6, 10),
                Greetings = "I have worked with Python",
                PersonalScore = 2.6f,
                PreviousProjectIds = new [] { 3 },
                Bio = new Bio { DriverID = "qse-133ceerq-2", Passport = "5814-2336-5347-9870", Female = false }
            },
            new Student
            {
                Id = 5,
                Name = "Dmitriy",
                Surname = "Dmitrov",
                IntroductionDate = new DateTime(2015, 6, 10),
                Greetings = "I'm from Java Pool",
                PersonalScore = 5.0f,
                PreviousProjectIds = new [] { 5, 6, 7 },
                Bio = new Bio { DriverID = "pbn-1332eerq-2", Passport = "5814-2336-5347-0070", Female = false }
            },
            new Student
            {
                Id = 6,
                Name = "Artem",
                Surname = "Artemov",
                IntroductionDate = new DateTime(2018, 7, 14),
                PersonalScore = 2.6f,
                PreviousProjectIds = new [] { 6 },
                Bio = new Bio { DriverID = "qse-133cebrq-2", Passport = "5814-2331-5347-9870", Female = false }
            },
            new Student
            {
                Id = 7,
                Name = "Alyona",
                Surname = "Alyonina",
                IntroductionDate = new DateTime(2019, 4, 29),
                Greetings = "I have worked with Python",
                PersonalScore = 2.6f,
                PreviousProjectIds = new [] { 3 },
                Bio = new Bio { Passport = "5814-4589-5347-9870", Female = true }
            },
        };
    }
}
