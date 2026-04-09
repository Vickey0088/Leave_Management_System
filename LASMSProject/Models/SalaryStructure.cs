using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LASMSProject.Models
{
    public class SalaryStructure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal HRA { get; set; } // House Rent Allowance
        public decimal DA { get; set; }  // Dearness Allowance

        public decimal GrossSalary => BasicSalary + HRA + DA;
    }
}
