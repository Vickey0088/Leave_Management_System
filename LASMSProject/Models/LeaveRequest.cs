using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LASMSProject.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays => (EndDate - StartDate).Days + 1; 

        [Required]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        public DateTime AppliedOn { get; set; } = DateTime.Now;

        // Who Approved it?
        public string? ApprovedById { get; set; }
        [ForeignKey("ApprovedById")]
        public ApplicationUser? ApprovedBy { get; set; }

        public string? ManagerRemarks { get; set; }
    }
}
