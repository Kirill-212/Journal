using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Create
{
    public class PostPersonDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int DepartmentId { get; set; }

    }
}
