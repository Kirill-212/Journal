using System;
using System.Collections.Generic;

namespace Journal.Models
{
    public class WorkLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}
