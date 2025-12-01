using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.dal.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Навігаційна властивість: список студентів на цьому курсі
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
