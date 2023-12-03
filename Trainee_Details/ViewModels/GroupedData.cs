
using Trainee_Details.Models;

namespace Trainee_Details.ViewModels
{
    public class GroupedData
    {
        public string Key { get; set; } = default!;
        public IEnumerable<Course> Data { get; set; } = default!;
    }
}
