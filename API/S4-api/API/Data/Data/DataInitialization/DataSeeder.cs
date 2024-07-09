using s4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s4.Data.DataInitialization
{
    public class DataSeeder
    {
        public static List<Class> SeedClass()
        {
            return new List<Class>
            {
                new Class { Id =  new Guid("D74FEBBF-8D2A-4A18-9E7B-08DB4D6C094C"), Code = "Math-01", Title = "Math", Description = "Fun Mathematics" },
                new Class { Id =  new Guid("8B4EB3A7-9F88-4A6D-9E7C-08DB4D6C094C"), Code = "Physic-01", Title = "Physic", Description = "Fun Physics" }
            };
        }

        public static List<Student> SeedStudent()
        {
            return new List<Student>
            {
                new Student { Id = new Guid("B4C05021-C2A1-4B2F-9E7D-08DB4D6C094C"), LastName = "Perez", FirstName = "Marco" },
                new Student { Id = new Guid("53C3B5B8-3A86-46F5-9E7E-08DB4D6C094C"), LastName = "Velasco", FirstName = "Juan" }
            };
        }

        public static List<StudentClass> SeedStudentClass()
        {
            return new List<StudentClass>
            {
                new StudentClass { StudentId = new Guid("B4C05021-C2A1-4B2F-9E7D-08DB4D6C094C"), ClassId =  new Guid("D74FEBBF-8D2A-4A18-9E7B-08DB4D6C094C") },
                new StudentClass { StudentId = new Guid("B4C05021-C2A1-4B2F-9E7D-08DB4D6C094C"), ClassId =  new Guid("8B4EB3A7-9F88-4A6D-9E7C-08DB4D6C094C") },
                new StudentClass { StudentId = new Guid("53C3B5B8-3A86-46F5-9E7E-08DB4D6C094C"), ClassId =  new Guid("8B4EB3A7-9F88-4A6D-9E7C-08DB4D6C094C") },
            };
        }
    }
}
