using s4.Data.Models;
using Microsoft.EntityFrameworkCore;
using s4.Configuration;

namespace s4.Data
{
    public class S4DBContext : DbContext
    {
        private readonly IApplicationConfiguration _applicationConfiguration;
        public S4DBContext(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_applicationConfiguration.GetDatabaseConnectionString().DATABASE);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id)
                    .HasName("PK_Student_Id");
            });
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(c => c.Id)
                    .HasName("PK_Class_Id");
            });
            modelBuilder.Entity<StudentClass>(entity =>
            {
                entity.HasKey(sc => sc.Id)
                    .HasName("PK_StudentClass_Id");
                entity.HasOne(sc => sc.Student)
                    .WithMany()
                    .HasForeignKey(sc => sc.StudentId)
                    .HasConstraintName("FK_StudentClass_StudentId")
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sc => sc.Class)
                    .WithMany()
                    .HasForeignKey(sc => sc.ClassId)
                    .HasConstraintName("FK_StudentClass_ClassId")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
