using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Update
{
    public class UpdateDepartmentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }
    }
}
