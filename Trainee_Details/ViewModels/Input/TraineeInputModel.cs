
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Trainee_Details.Models;

namespace Trainee_Details.ViewModels.Input
{
    public class TraineeInputModel
    {
        public int TraineeId { get; set; }
        [Required, StringLength(50)]
        public string TraineeName { get; set; } = default!;
        [Required]
        public int Age { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; } = default!;
        public bool IsRegular { get; set; }
        public virtual List<Course> Courses { get; set; } = new List<Course>();
    }
}
