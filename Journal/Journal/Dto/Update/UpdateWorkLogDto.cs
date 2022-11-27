using System;
using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Update
{
    public class UpdateWorkLogDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int PersonId { get; set; }
    }
}
