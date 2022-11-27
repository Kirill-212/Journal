using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Journal.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [JsonIgnore]
        public List<WorkLog> WorkLogs { get; set; }
    }
}
