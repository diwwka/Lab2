using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.dal.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Навігаційна властивість: список курсів цього студента
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
