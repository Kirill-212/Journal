using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Journal.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        [JsonIgnore]
        public List<Person> Persons { get; set; }
    }
}
