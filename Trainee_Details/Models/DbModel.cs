using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Trainee_Details.Models
{
    public enum Gender { Male = 1, Female }
    public class Trainee
    {
        public int TraineeId { get; set; }
        [Required, StringLength(50)]
        public string TraineeName { get; set; } = default!;
        [Required]
        public int Age { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }
        [Required, StringLength(50)]
        public string? Picture { get; set; } = default!;
        public bool? IsRegular { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
    public class Course
    {
        public int CourseId { get; set; }
        [Required, StringLength(50)]
        public string CourseName { get; set; } = default!;

        [Required, Column(TypeName = "money")]
        public decimal CourseFee { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime? AdmissionDate { get; set; }
        [Required, ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public virtual Trainee? Trainee { get; set; }
    }
    public class TraineeDbContext : DbContext
    {
        public TraineeDbContext(DbContextOptions<TraineeDbContext> options) : base(options) { }
        public DbSet<Trainee> Trainees { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Random random = new();
            for (int i = 1; i <= 6; i++)
            {
                modelBuilder.Entity<Trainee>().HasData(
                    new Trainee { TraineeId = i, TraineeName = "Trainee " + i, Age = random.Next(20, 40), Gender = (Gender)random.Next(1, 5), Picture = i + "jpg" }
                    );
            }
            for (int i = 1; i <= 10; i++)
            {
                modelBuilder.Entity<Course>().HasData(
                    new Course { CourseId = i, CourseName = "Course" + i, CourseFee = random.Next(10000, 20000) * i, AdmissionDate = DateTime.Now.AddDays(-1 * random.Next(400, 500)), TraineeId = (i % 5 == 0 ? 5 : i % 5) }
                    );
            }
        }
    }
}
