using System.ComponentModel.DataAnnotations;
namespace LASMSProject.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Sick, Casual, Earned

        public int DefaultDays { get; set; }

        public bool IsPaid { get; set; } = true; // True = Paid, False = Unpaid
    }
}
