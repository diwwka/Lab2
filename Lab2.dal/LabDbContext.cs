using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Lab2.dal.Entities;

namespace Lab2.dal
{
    public class LabDbContext : DbContext
    {
        // Конструктор, який передає налаштування в базовий клас
        public LabDbContext(DbContextOptions<LabDbContext> options) : base(options)
        {
        }

        // Таблиці, які будуть у базі даних
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        // Налаштування зв'язків (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Встановлюємо композитний ключ (Primary Key) для проміжної таблиці
            // Ключ складається з пари: ID студента + ID курсу
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            // 2. Налаштовуємо зв'язок: Студент -> Курси
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            // 3. Налаштовуємо зв'язок: Курс -> Студенти
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
