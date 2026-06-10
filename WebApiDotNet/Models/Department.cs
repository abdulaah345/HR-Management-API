using System.Text.Json.Serialization;

namespace WebApiDotNet.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ?ManagarName { get; set; }
        [JsonIgnore]
        public List<Employee>? Emps { get; set; }
    }
}
