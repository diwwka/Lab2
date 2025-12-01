using System;
using System.Collections.Generic;
using System.Text;
using Lab2.dal.Entities;

namespace Lab2.dal
{
    public static class DbInitializer
    {
        public static void Initialize(LabDbContext context)
        {
            // Перевіряємо, чи база існує
            context.Database.EnsureCreated();

            // Якщо в базі вже є студенти — нічого не робимо (база вже засіяна)
            if (context.Students.Any())
            {
                return;
            }

            // 1. Створюємо студентів
            var students = new Student[]
            {
                new Student { FirstName = "Carson", LastName = "Alexander" },
                new Student { FirstName = "Meredith", LastName = "Alonso" },
                new Student { FirstName = "Arturo", LastName = "Anand" },
                new Student { FirstName = "Gytis", LastName = "Barzdukas" }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            // 2. Створюємо курси
            var courses = new Course[]
            {
                new Course { Title = "Chemistry", Credits = 3 },
                new Course { Title = "Microeconomics", Credits = 3 },
                new Course { Title = "Macroeconomics", Credits = 3 },
                new Course { Title = "Calculus", Credits = 4 }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            // 3. Додаємо зв'язки (Студент <-> Курс)
            // Беремо першого студента і записуємо його на курс Хімії
            var studentCourses = new StudentCourse[]
            {
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[2].CourseId }
            };

            context.StudentCourses.AddRange(studentCourses);
            context.SaveChanges();
        }
    }
}
