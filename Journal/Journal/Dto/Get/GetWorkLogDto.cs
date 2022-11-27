using System;

namespace Journal.Dto.Get
{
    public class GetWorkLogDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public GetPersonDto GetPersonDto { get; set; }
    }
}
