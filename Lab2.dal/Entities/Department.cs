using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.dal.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; } // Наприклад, номер корпусу

        // Навігаційна властивість: Один факультет має багато курсів
        public ICollection<Course> Courses { get; set; }
    }

}
