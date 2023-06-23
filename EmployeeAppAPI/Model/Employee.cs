using System.ComponentModel.DataAnnotations;

namespace EmployeeAppAPI.Model
{
    public class Employee
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int phone { get; set; }
        [Required]
        public int salary { get; set; }
        [Required]
        public string designation { get; set; }
        [Required]
        public string email { get; set; }
    }
}
