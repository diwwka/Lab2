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

        // Зовнішній ключ (Foreign Key)
        public int? DepartmentId { get; set; }
        // Навігаційна властивість
        public Department? Department { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
