using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Create
{
    public class PostDepartmentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }
    }
}
