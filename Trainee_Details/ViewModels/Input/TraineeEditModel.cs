
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Trainee_Details.Models;

namespace Trainee_Details.ViewModels.Input
{
    public class TraineeEditModel
    {
        public int TraineeId { get; set; }
        [Required, StringLength(50)]
        public string TraineeName { get; set; } = default!;
        [Required]
        public int Age { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        public IFormFile? Picture { get; set; } = default!;
        public bool IsRegular { get; set; }
    }
}
