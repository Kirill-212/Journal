using System;
using System.ComponentModel.DataAnnotations;

namespace Journal.Dto.Create
{
    public class PostWorkLogDto
    {
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
