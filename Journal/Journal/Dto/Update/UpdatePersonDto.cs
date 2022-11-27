using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Update
{
    public class UpdatePersonDto
    {
        [Required]
        public int Id { get; set; }

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
