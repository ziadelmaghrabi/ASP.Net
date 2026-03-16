using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace UniversityMVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(x => new { x.StudentId, x.CourseId });

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(s => s.Student)
                .WithMany(st => st.StuCrsRes)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(c => c.Course)
                .WithMany(cr => cr.StuCrsRes)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);  
             
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithMany()
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

             
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasColumnType("decimal(18,2)");
        }

    }
}
