using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SchoolAPI.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        // Shadow Foreign key = TeacherId
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public List<Student> Students { get; set; }
    }

    // Student heeft 1 docent
    // Docent kan meerdere studenten
}
