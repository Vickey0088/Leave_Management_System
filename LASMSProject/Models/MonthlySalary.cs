using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LASMSProject.Models
{
    public class MonthlySalary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; } // HRA + DA

        public int TotalUnpaidDays { get; set; }
        public decimal LOPDeduction { get; set; } // Loss of Pay Amount

        public decimal NetSalary { get; set; } // Final Amount to Pay

        public DateTime ProcessedDate { get; set; } = DateTime.Now;
    }
}
