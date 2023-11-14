using System.ComponentModel.DataAnnotations;

namespace TestCoreApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? EmployeeName { get; set; }
        [Required]
        public int? EmployeePhone { get; set;}

        public int? EmployeeEmail { get; set; }
        public int? EmployeeAge { get; set;}
        public int? EmployeeSalary { get; set;}
    }
}
